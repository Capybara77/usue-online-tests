using Microsoft.AspNetCore.Mvc;

namespace usue_online_tests.Controllers
{
    public class ProtectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ddos()
        {
            return View();
        }
    }
}
