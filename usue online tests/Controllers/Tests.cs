using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
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
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

        public List<ITestCreator> TestCreators { get; set; }

        public DataContext DataContext { get; }
        public TestsLoader TestsLoader { get; }

        public Tests(DataContext dataContext, TestsLoader testsLoader)
        {
            DataContext = dataContext;
            TestsLoader = testsLoader;
            TestCreators = testsLoader.TestCreators;
        }

        public IActionResult Index()
        {
            return View(TestsLoader.TestCreators);
        }

        public IActionResult Start(int id)
        {
            ITestCreator creator = TestCreators.FirstOrDefault(testCreator => testCreator.TestID == id);
            int hash = new Random().Next();
            ITest test = null;
            try
            {
                if (creator != null)
                {
                    test = creator.CreateTest(hash);
                }
            }
            catch
            {
                // ignored
            }

            TestWrapper testWrapper = new TestWrapper
            {
                Hash = hash,
                Test = test,
                TestId = id
            };
            return View(testWrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckAnswers(int testId, int hash, int testsCount)
        {
            string[] skipData = { "__RequestVerificationToken", "testId", "hash", "testsCount" };
            ITestCreator creator = TestsLoader.TestCreators.FirstOrDefault(testCreator => testCreator.TestID == testId);
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

            testResult.Total = testsCount;

            return View(testResult);
        }
    }
}
