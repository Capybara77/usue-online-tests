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
            if (exam == null) return StatusCode(400);
            TestPreset preset = exam.Preset;

            UserExamResult userExamResult =
                Context.UserExamResults.FirstOrDefault(result => result.User.ID == user.ID && result.Exam.Id == examId);

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

            if (CheckTimeExpiration(preset, userExamResult, bonusTime: false)) return StatusCode(400);


            if (testNumber < 1) return StatusCode(400);

            // сохранение результата теста
            if (preset.Tests.Length < testNumber)
            {
                userExamResult.IsCompleted = true;
                Context.SaveChanges();
                return LocalRedirect("/profile");
            }

            int testId = preset.Tests[testNumber - 1];

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
            if (exam == null) return StatusCode(400);
            TestPreset preset = exam.Preset;

            if (!preset.Tests.Contains(testId)) return StatusCode(400);
            UserExamResult userExamResult =
                Context.UserExamResults.FirstOrDefault(result => result.User.ID == user.ID && result.Exam.Id == examId);

            if (userExamResult == null)
            {
                return StatusCode(400);
            }

            if (CheckTimeExpiration(preset, userExamResult)) return StatusCode(400);

            if (creator != null)
            {
                if (CreateHash(user.Name + user.Group + exam.Id) != hash) return StatusCode(400);

                ITest newTest = creator.CreateTest(hash);
                if (new Regex("<(.*?)>").Matches(newTest.Text).Count < testsCount) return StatusCode(400);


                // create dictionary with answers
                KeyValuePair<string, StringValues>[] paramsArray = HttpContext.Request.Form.Where(pair => !skipData.Contains(pair.Key)).ToArray();
                Dictionary<string, string> userAnswer = new Dictionary<string, string>();

                foreach (KeyValuePair<string, StringValues> keyValuePair in paramsArray)
                {
                    userAnswer.Add(keyValuePair.Key, keyValuePair.Value);
                }

                ExamTestAnswer examTestAnswer = new ExamTestAnswer
                {
                    TotalAnswers = testsCount,
                    TestId = creator.TestID,
                };

                try
                {
                    examTestAnswer.CorrectAnswers = creator.CheckAnswer(hash, userAnswer);
                }
                catch
                {
                    examTestAnswer.CorrectAnswers = -1;
                }

                if (userExamResult.ExamTestAnswers != null)
                    userExamResult.ExamTestAnswers?.Add(examTestAnswer);
                else
                    userExamResult.ExamTestAnswers = new List<ExamTestAnswer> { examTestAnswer };

                Context.SaveChanges();
            }

            return LocalRedirect($"/exam/StartTest?examId={examId}&testNumber={testNumber}");
        }

        private static int CreateHash(string input)
        {
            byte[] hashed = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToInt32(hashed, 0);
        }
    }
}
