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
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class Tests : Controller
    {
        public List<ITestCreater> TestCreaters { get; set; }

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
            ITestCreater creater = (ITestCreater)TestCreaters.FirstOrDefault(testCreater => testCreater.TestID == id);
            int hash = new Random().Next();
            ITest test = creater.CreateTest(hash);

            ITestWrapper testWrapper = new ITestWrapper()
            {
                Hash = hash,
                Test = test,
                TestId = id
            };
            return View(testWrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckAnswers(int testId, int hash)
        {
            string[] skipData = { "__RequestVerificationToken", "testId", "hash" };
            ITestCreater creater = TestsLoader.TestCreaters.FirstOrDefault(creater => creater.TestID == testId);
            TestResult testResult = new();

            if (creater != null)
            {
                foreach (KeyValuePair<string, StringValues> pairs in HttpContext.Request.Form)
                {
                    if (skipData.Contains(pairs.Key)) continue;
                    try
                    {
                        if (creater.CheckAnswer(hash, pairs.Key, pairs.Value))
                        {
                            testResult.TotalRight++;
                        }
                    }
                    catch
                    {
                        // ignored
                    }


                    testResult.Total++;
                }
            }

            return View(testResult);
        }

    }
}
