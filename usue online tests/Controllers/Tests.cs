using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using Test_Wrapper;
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class Tests : Controller
    {
        public List<ITestCreator> TestCreaters { get; set; }

        public DataContext DataContext { get; }
        public TestsLoader TestsLoader { get; }

        public Tests(DataContext dataContext, TestsLoader testsLoader)
        {
            DataContext = dataContext;
            TestsLoader = testsLoader;
            TestCreaters = testsLoader.TestCreaters;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult View()
        {
            //TestCreaterInfo[] testsInfo = new TestCreaterInfo[AllTests.Count];
            //for (int i = 0; i < testsInfo.Length; i++)
            //{
            //    testsInfo[i] = new TestCreaterInfo()
            //    {
            //        Name = (string)AllTests[i].GetProperty("Name")?.GetValue(null),
            //        Description = (string)AllTests[i].GetProperty("Description")?.GetValue(null),
            //        Id =i
            //    };
            //}
            return View(TestsLoader.TestCreaters);
        }

        public IActionResult Start(int id)
        {
            ITestCreator creator = TestCreaters.FirstOrDefault(testCreater => testCreater.TestID == id);
            int hash = new Random().Next();
            ITest test = null;
            try
            {
                if (creator != null) test = creator.CreateTest(hash);
            }
            catch
            {

            }

            ITestWrapper testWrapper = new ITestWrapper
            {
                Hash = hash,
                Test = test,
                TestId = id
            };
            return View(testWrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckAnswers(int testId, int hash, int TestsCount)
        {
            string[] skipData = { "__RequestVerificationToken", "testId", "hash", "TestsCount" };
            ITestCreator creator = TestsLoader.TestCreaters.FirstOrDefault(creater => creater.TestID == testId);
            TestResult testResult = new();

            if (creator != null)
            {
                // create dictionary with answers
                KeyValuePair<string, StringValues>[] paramsArray = HttpContext.Request.Form.Where(pair => !skipData.Contains(pair.Key)).ToArray();
                Dictionary<string, string> userAnswer = new Dictionary<string, string>();
                foreach (KeyValuePair<string, StringValues> keyValuePair in paramsArray)
                {
                    userAnswer.Add(keyValuePair.Key, keyValuePair.Value);
                }

                testResult.Total = userAnswer.Count;
                try
                {
                    testResult.TotalRight = creator.CheckAnswer(hash, userAnswer);
                }
                catch
                {
                    testResult.TotalRight = -1;
                }
            }

            testResult.Total = TestsCount;

            return View(testResult);
        }
    }
}
