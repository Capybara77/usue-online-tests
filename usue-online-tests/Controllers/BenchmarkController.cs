using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BenchmarkController : Controller
    {
        public TestsLoader TestsLoader { get; }

        public BenchmarkController(TestsLoader testsLoader)
        {
            TestsLoader = testsLoader;
        }

        public IActionResult GetBenchmarkResult(int testId, int count = 100)
        {
            var testCreator = TestsLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);
            if (testCreator == null)
                return StatusCode(400);

            var timeStart = DateTime.Now.ToNowEkb();

            for (int i = 0; i < count; i++)
            {
                testCreator.CreateTest(i);
            }

            return View((DateTime.Now.ToNowEkb() - timeStart).TotalMilliseconds);
        }
    }
}