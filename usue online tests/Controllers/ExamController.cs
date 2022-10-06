using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        public DataContext Context { get; }
        public GetUserByCookie GetUserByCookie { get; }
        public TestsLoader TestsLoader { get; }

        public ExamController(DataContext context, GetUserByCookie getUserByCookie, TestsLoader testsLoader)
        {
            Context = context;
            GetUserByCookie = getUserByCookie;
            TestsLoader = testsLoader;
        }

        public IActionResult StartTest(int examId, int testNumber)
        {
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            // если экзамена нет
            if (exam == null) return StatusCode(401);
            TestPreset preset = exam.Preset;

            // если пользователь из другой группы
            if (user.Group != exam.Group) return StatusCode(411);

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
                    DateTimeStart = DateTime.Now,
                    IsCompleted = false
                };
                Context.UserExamResults.Add(userExamResult);
                Context.SaveChanges();
            }

            // время истекло
            if (exam.DateTimeEnd < DateTime.Now)
                return StatusCode(413);

            // некорректный номер теста
            if (testNumber < 1) return StatusCode(403);

            // сохранение результата теста
            if (preset.Tests.Length < testNumber)
            {
                userExamResult.IsCompleted = true;
                Context.SaveChanges();
                return LocalRedirect("/profile");
            }

            int testId = preset.Tests[testNumber - 1];

            //проверка на существование такого ответа
            if (userExamResult.ExamTestAnswers != null && userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
                return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");

            // выдача задания и фиксация времени
            userExamResult.ExamTestAnswers ??= new List<ExamTestAnswer>();


            if (userExamResult.ExamTestAnswers.All(answer => answer.TestId != testId))
            {
                userExamResult.ExamTestAnswers.Add(new ExamTestAnswer
                {
                    DateTimeStart = DateTime.Now,
                    TestId = testId
                });
                Context.SaveChanges();
            }

            int hash = CreateHash(user.Name + user.Group + exam.Id);

            ITestCreator testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);

            if (testCreator == null)
                return StatusCode(412);

            TestWrapper test = new TestWrapper
            {
                Hash = hash,
                Test = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId)?.CreateTest(hash),
                TestId = testId,
                BtnText = testNumber == preset.Tests.Length ? "Завершить" : "Следующий вопрос",
                Link = $"/exam/CheckAnswersExam?examId={examId}&testNumber={testNumber + 1}",
                TimeLimited = exam.Preset.TimeLimited
            };

            if (test.TimeLimited)
                test.SecLimit = testCreator is ITimeLimit timeLimitCreator ? timeLimitCreator.TimeLimitSeconds : 60;
            return View(test);
        }

        [HttpPost]
        public ActionResult CheckAnswersExam(int examId, int testNumber, int testsCount, int hash, int testId)
        {
            string[] skipData = { "__RequestVerificationToken", "testId", "hash", "testsCount" };
            ITestCreator creator = TestsLoader.TestCreators.FirstOrDefault(testCreator => testCreator.TestID == testId);
            User user = GetUserByCookie.GetUser();
            Exam exam = Context.Exams.Include(exam1 => exam1.Preset).FirstOrDefault(exam1 => exam1.Id == examId);
            if (exam == null) return StatusCode(405);
            TestPreset preset = exam.Preset;

            // если в экзамене нет такого задания
            if (!preset.Tests.Contains(testId)) return StatusCode(406);

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            // если пользователь не запуска первое задание
            if (userExamResult == null)
            {
                return StatusCode(400);
            }

            // если уже ответил на этот вопрос
            if (userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
                return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");

            // если исчез генератор
            if (creator == null) return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");

            // если подмена хэша
            if (CreateHash(user.Name + user.Group + exam.Id) != hash) return StatusCode(408);

            ITest newTest = creator.CreateTest(hash);
            if (new Regex("<(.*?)>").Matches(newTest.Text).Count + newTest.CheckBoxes?.Length < testsCount) return StatusCode(409);

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
                return StatusCode(410);

            // проверка на истекшее время теста
            if (exam.Preset.TimeLimited)
            {
                int secLimit = creator is ITimeLimit testCreatorLimit ? testCreatorLimit.TimeLimitSeconds : 60;

                if (examTestAnswer.DateTimeStart + TimeSpan.FromSeconds(10 + secLimit) < DateTime.Now)
                {
                    examTestAnswer.TotalAnswers = testsCount;
                    examTestAnswer.DateTimeEnd = DateTime.Now;
                    examTestAnswer.CorrectAnswers = 0;

                    AddTestResultToExamResult(userExamResult, examTestAnswer);
                    Context.SaveChanges();
                    return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");
                }
            }

            examTestAnswer.TotalAnswers = testsCount;
            examTestAnswer.DateTimeEnd = DateTime.Now;

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
