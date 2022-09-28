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
            if (CheckTimeExpiration(preset, userExamResult, bonusTime: false)) return StatusCode(402);

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

            ITestWrapper test = new ITestWrapper
            {
                Hash = hash,
                Test = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId)?.CreateTest(hash),
                TestId = testId,
                BtnText = testNumber == preset.Tests.Length ? "Завершить" : "Следующий вопрос",
                Link = $"/exam/CheckAnswersExam?examId={examId}&testNumber={testNumber + 1}"
            };
            return View(test);
        }

        private bool CheckTimeExpiration(TestPreset preset, UserExamResult userExamResult, bool bonusTime = true)
        {
            if (preset == null || userExamResult == null) return true;

            if (bonusTime)
            {
                if (preset.TimeLimited &&
                    userExamResult.DateTimeStart + new TimeSpan(0, 0, preset.MinutesToPass + 1, 0) <
                    DateTime.Now)
                {
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (preset.TimeLimited && userExamResult.DateTimeStart < DateTime.Now)
                {
                    return true;
                }
            }

            return false;
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

            if (!preset.Tests.Contains(testId)) return StatusCode(406);

            UserExamResult userExamResult =
                Context.UserExamResults
                    .Include(result => result.ExamTestAnswers)
                    .FirstOrDefault(result => result.User.Id == user.Id && result.Exam.Id == examId);

            if (userExamResult == null)
            {
                return StatusCode(400);
            }

            if (userExamResult.ExamTestAnswers.Any(answer => answer.TestId == testId && answer.DateTimeEnd != default))
                return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");

            if (CheckTimeExpiration(preset, userExamResult)) return StatusCode(407);

            if (creator == null) return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber + 1}");

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

            if (examTestAnswer == null)
                return StatusCode(410);

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

            if (userExamResult.ExamTestAnswers != null)
                userExamResult.ExamTestAnswers.Add(examTestAnswer);
            else
                userExamResult.ExamTestAnswers = new List<ExamTestAnswer> { examTestAnswer };

            Context.SaveChanges();

            return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");
        }

        private static int CreateHash(string input)
        {
            byte[] hashed = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToInt32(hashed, 0);
        }
    }
}
