using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using Test_Wrapper;
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Tests;
using Microsoft.Extensions.Logging;
using usue_online_tests.Services;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        public DataContext Context { get; }
        public GetUserByCookie GetUserByCookie { get; }
        public TestsLoader TestsLoader { get; }
        public Random Random { get; set; } = new();

        public ExamController(DataContext context, GetUserByCookie getUserByCookie, TestsLoader testsLoader)
        {
            Context = context;
            GetUserByCookie = getUserByCookie;
            TestsLoader = testsLoader;
        }

        private int[] ShuffleSequence(int arrayLength, int randomSeed)
        {
            int[] sequence = Enumerable.Range(1, arrayLength).ToArray();
            Random rnd = new Random(randomSeed);
            for (int i = 0; i < sequence.Length; i++)
            {
                int randomIndex = rnd.Next(i, sequence.Length);
                (sequence[i], sequence[randomIndex]) = (sequence[randomIndex], sequence[i]);
            }

            return sequence;
        }

        public async Task<IActionResult> StartTest(int examId, int testNumber)
        {
            int maxAttempt = 3;
            int attempt = 1;

            while (attempt != maxAttempt)
            {
                try
                {
                    User user = GetUserByCookie.GetUser();
                    Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);

                    // если экзамена нет
                    if (exam == null) return View("ErrorPage", "Тест не существует");
                    TestPreset preset = exam.Preset;

                    if (preset.IsHomework) return StartHomework(examId, testNumber);

                    // если пользователь из другой группы
                    if (user.Group != exam.Group) View("ErrorPage", "Тест для другой группы");

                    UserExamResult userExamResult =
                        Context.UserExamResults
                            .Include(result => result.ExamTestAnswers)
                            .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

                    // если это первый запуск
                    if (userExamResult == null)
                    {
                        userExamResult = new UserExamResult
                        {
                            User = user,
                            Exam = exam,
                            DateTimeStart = DateTime.Now.ToNowEkb(),
                            IsCompleted = false
                        };
                        Context.UserExamResults.Add(userExamResult);
                        Context.SaveChanges();
                    }

                    // время истекло
                    if (exam.DateTimeEnd < DateTime.Now.ToNowEkb())
                        return View("ErrorPage", "Время истекло");

                    // некорректный номер теста
                    if (testNumber < 1) return View("ErrorPage", "Некорректный номер теста");

                    // сохранение результата теста
                    if (preset.Tests.Length < testNumber)
                    {
                        await SaveTestResult(userExamResult);
                        return LocalRedirect("/profile");
                    }

                    // создание новой последовательности тестов
                    int[] userSequenceOrder = ShuffleSequence(preset.Tests.Length, CreateHash(user.Name));
                    int currentRealTestNumber = userSequenceOrder[testNumber - 1];

                    int testId = preset.Tests[currentRealTestNumber - 1];

                    //проверка на существование такого ответа
                    if (userExamResult.ExamTestAnswers != null && userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
                        return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");

                    // выдача задания и фиксация времени
                    userExamResult.ExamTestAnswers ??= new List<ExamTestAnswer>();

                    if (userExamResult.ExamTestAnswers.All(answer => answer.TestId != testId))
                    {
                        userExamResult.ExamTestAnswers.Add(new ExamTestAnswer
                        {
                            DateTimeStart = DateTime.Now.ToNowEkb(),
                            TestId = testId
                        });
                        Context.SaveChanges();
                    }

                    int spentTime = (int)(DateTime.Now.ToNowEkb() - userExamResult.ExamTestAnswers.First(answer => answer.TestId == testId).DateTimeStart).TotalSeconds;

                    int hash = CreateHash(user.Name + user.Group + exam.Id);

                    Logger.LogInfo($"{user.Name} запустил тест - #{testNumber} ({testId}) экзамен - #{examId} Хэш: {hash}");

                    ITestCreator testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);

                    if (testCreator == null)
                        return View("ErrorPage", "Нет генератора в системе. Обратитесь по номеру +79533804297");

                    TestWrapper test = new TestWrapper
                    {
                        Hash = hash,
                        Test = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId)?.CreateTest(hash),
                        TestId = testId,
                        BtnText = testNumber == preset.Tests.Length ? "Завершить" : "Следующий вопрос",
                        Link = $"/exam/CheckAnswersExam?examId={examId}&testNumber={testNumber + 1}",
                        TimeLimited = exam.Preset.TimeLimited,
                        ExamId = examId
                    };

                    if (test.TimeLimited)
                        test.SecLimit = testCreator is ITimeLimit timeLimitCreator ? timeLimitCreator.TimeLimitSeconds - spentTime : 60 - spentTime;

                    return View(test);
                } catch(Exception ex)
                {
                    attempt++;
                    Logger.LogError("Ошибка при запуске тестов", ex);
                }
            }
            return View("ErrorPage", "Возникла ошибка при генерации теста");
        }

        [HttpPost]
        public ActionResult CheckAnswersExam(int examId, int testNumber, int testsCount, int hash, int testId)
        {
            string[] skipData = { "__RequestVerificationToken", "testId", "hash", "testsCount" };
            ITestCreator creator = TestsLoader.TestCreators.FirstOrDefault(testCreator => testCreator.TestID == testId);
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            if (exam == null) return View("ErrorPage", "Нет теста");
            TestPreset preset = exam.Preset;

            // если в экзамене нет такого задания
            if (!preset.Tests.Contains(testId)) return View("ErrorPage", "Нет такого задания в этом тесте");

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            // если пользователь не запустил первое задание
            if (userExamResult == null)
                return View("ErrorPage", "Вы не запустили тестирование");

            // если уже ответил на этот вопрос
            if (userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
                return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");

            // если исчез генератор
            if (creator == null) return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");

            if (CreateHash(user.Name + user.Group + exam.Id) != hash && !preset.IsHomework) return View("ErrorPage", "");

            ITest newTest = creator.CreateTest(hash);
            if (new Regex("<(.*?)>").Matches(newTest.Text).Count + newTest.CheckBoxes?.Length < testsCount)
                return View("ErrorPage", "Некорректное количество ответов");

            // create dictionary with answers
            KeyValuePair<string, StringValues>[] paramsArray = HttpContext.Request.Form.Where(pair => !skipData.Contains(pair.Key)).ToArray();
            Dictionary<string, string> userAnswer = new Dictionary<string, string>();

            foreach (KeyValuePair<string, StringValues> keyValuePair in paramsArray)
            {
                userAnswer.Add(keyValuePair.Key, keyValuePair.Value);
            }

            var examTestAnswer = userExamResult.ExamTestAnswers.FirstOrDefault(answer => answer.TestId == testId);

            // если пользователь не запрашивал тест
            if (examTestAnswer == null)
                return View("ErrorPage", "Вы не запустили тестирование");

            // проверка на истекшее время теста
            if (exam.Preset.TimeLimited)
            {
                int secLimit = creator is ITimeLimit testCreatorLimit ? testCreatorLimit.TimeLimitSeconds : 60;

                if (examTestAnswer.DateTimeStart + TimeSpan.FromSeconds(10 + secLimit) < DateTime.Now.ToNowEkb())
                {
                    examTestAnswer.TotalAnswers = testsCount;
                    examTestAnswer.DateTimeEnd = DateTime.Now.ToNowEkb();
                    examTestAnswer.CorrectAnswers = 0;

                    AddTestResultToExamResult(userExamResult, examTestAnswer);
                    Context.SaveChanges();
                    return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");
                }
            }

            examTestAnswer.TotalAnswers = testsCount;
            examTestAnswer.DateTimeEnd = DateTime.Now.ToNowEkb();

            try
            {
                examTestAnswer.CorrectAnswers = creator.CheckAnswer(hash, userAnswer);
            }
            catch
            {
                examTestAnswer.CorrectAnswers = -1;
            }

            AddTestResultToExamResult(userExamResult, examTestAnswer);

            Context.SaveChanges();

            return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");
        }

        private IActionResult StartHomework(int examId, int testNumber)
        {
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            // если экзамена нет
            if (exam == null) return View("ErrorPage", "Тест не существует");
            TestPreset preset = exam.Preset;

            // если пользователь из другой группы
            if (user.Group != exam.Group) View("ErrorPage", "Тест для другой группы");

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            // если это первый запуск
            if (userExamResult == null)
            {
                userExamResult = new UserExamResult
                {
                    User = user,
                    Exam = exam,
                    DateTimeStart = DateTime.Now.ToNowEkb(),
                    IsCompleted = false
                };
                Context.UserExamResults.Add(userExamResult);
                Context.SaveChanges();
            }

            // время истекло
            if (exam.DateTimeEnd < DateTime.Now.ToNowEkb())
                return View("ErrorPage", "Время истекло");

            // некорректный номер теста
            if (testNumber < 1) return View("ErrorPage", "Некорректный номер теста");

            // сохранение результата теста
            // TODO добавить страницу с вопросом о сохрании результата
            if (preset.Tests.Length < testNumber)
            {
                return View("StartHomework", new ExamWrapper()
                {
                    ExamId = examId,
                    TestPreset = preset,
                    SaveResult = true
                });
            }

            int testId = preset.Tests[testNumber - 1];

            //проверка на существование такого ответа
            if (userExamResult.ExamTestAnswers != null &&
                userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
            {
                return View("StartHomework", new ExamWrapper
                {
                    ExamId = examId,
                    ChangeAnswer = true,
                    TestPreset = preset,
                    OldTestResult = userExamResult.ExamTestAnswers.First(answer => answer.TestId == testId && answer.DateTimeEnd != default),
                });

                return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");
            }

            // выдача задания и фиксация времени
            userExamResult.ExamTestAnswers ??= new List<ExamTestAnswer>();

            if (userExamResult.ExamTestAnswers.All(answer => answer.TestId != testId))
            {
                userExamResult.ExamTestAnswers.Add(new ExamTestAnswer
                {
                    DateTimeStart = DateTime.Now.ToNowEkb(),
                    TestId = testId
                });
                Context.SaveChanges();
            }

            int spentTime = (int)(DateTime.Now.ToNowEkb() - userExamResult.ExamTestAnswers.First(answer => answer.TestId == testId).DateTimeStart).TotalSeconds;

            int hash = CreateHash(user.Name + user.Group + exam.Id + Random.Next());

            ITestCreator testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);

            if (testCreator == null)
                return View("ErrorPage", "Нет генератора в системе");

            TestWrapper test = new TestWrapper
            {
                Hash = hash,
                Test = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId)?.CreateTest(hash),
                TestId = testId,
                BtnText = testNumber == preset.Tests.Length ? "Завершить" : "Следующий вопрос",
                Link = $"/exam/CheckAnswersExam?examId={examId}&testNumber={testNumber + 1}",
                TimeLimited = exam.Preset.TimeLimited,
                ExamId = examId
            };

            if (test.TimeLimited)
                test.SecLimit = testCreator is ITimeLimit timeLimitCreator ? timeLimitCreator.TimeLimitSeconds - spentTime : 60 - spentTime;

            ExamWrapper examWrapper = new ExamWrapper
            {
                TestPreset = preset,
                TestWrapper = test,
                ExamId = examId
            };

            return View("StartHomework", examWrapper);
        }

        [HttpPost]
        public async Task<IActionResult> SaveHomeworkResult(int examId)
        {
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            // если экзамена нет
            if (exam == null) return View("ErrorPage", "Тест не существует");
            TestPreset preset = exam.Preset;

            // если пользователь из другой группы
            if (user.Group != exam.Group) View("ErrorPage", "Тест для другой группы");

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            // если это первый запуск
            if (userExamResult == null)
            {
                userExamResult = new UserExamResult
                {
                    User = user,
                    Exam = exam,
                    DateTimeStart = DateTime.Now.ToNowEkb(),
                    IsCompleted = false
                };
                Context.UserExamResults.Add(userExamResult);
                Context.SaveChanges();
            }

            // время истекло
            if (exam.DateTimeEnd < DateTime.Now.ToNowEkb())
                return View("ErrorPage", "Время истекло");


            // сохранение результата теста
            await SaveTestResult(userExamResult);
            return LocalRedirect("/profile");
        }

        public async Task<IActionResult> ChangeHomeworkAnswer(int examId, int testNumber)
        {
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            // если экзамена нет
            if (exam == null) return View("ErrorPage", "Тест не существует");
            TestPreset preset = exam.Preset;

            // если пользователь из другой группы
            if (user.Group != exam.Group) View("ErrorPage", "Тест для другой группы");

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            // время истекло
            if (exam.DateTimeEnd < DateTime.Now.ToNowEkb())
                return View("ErrorPage", "Время истекло");

            // некорректный номер теста
            if (testNumber < 1) return View("ErrorPage", "Некорректный номер теста");

            int testId = preset.Tests[testNumber - 1];

            var answers = Context.ExamTestAnswers.Where(answer => answer.TestId == testId).ToArray();
            Context.ExamTestAnswers.RemoveRange(answers);
            await Context.SaveChangesAsync();

            return LocalRedirect($"/exam/starttest?examId={examId}&testNumber={testNumber}");
        }

        private async Task SaveTestResult(UserExamResult userExamResult)
        {
            userExamResult.IsCompleted = true;
            await Context.SaveChangesAsync();
        }

        private static void AddTestResultToExamResult(UserExamResult userExamResult, ExamTestAnswer examTestAnswer)
        {
            if (userExamResult.ExamTestAnswers != null)
                userExamResult.ExamTestAnswers.Add(examTestAnswer);
            else
                userExamResult.ExamTestAnswers = new List<ExamTestAnswer> { examTestAnswer };
        }

        private static int CreateHash(string input)
        {
            byte[] hashed = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToInt32(hashed, 0);
        }
    }
}
