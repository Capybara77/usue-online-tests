using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using usue_online_tests.Compiler;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class Compiler : Controller
    {
        private readonly TestsLoader _testsLoader;

        public Compiler(TestsLoader testsLoader)
        {
            _testsLoader = testsLoader;
        }

        public IActionResult LoadTestPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadTestCode(string code)
        {
            CodeCompiler compiler = new CodeCompiler();
            bool success = false;
            var obj = compiler.CompileCode(code, ref success);
            return new JsonResult(new { result = obj });
        }
    }
}
