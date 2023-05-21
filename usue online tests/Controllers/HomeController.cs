using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    public class HomeController : Controller
    {
        public DataContext DataContext { get; }
        public IHostApplicationLifetime ApplicationLifetime { get; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext, IHostApplicationLifetime applicationLifetime)
        {
            DataContext = dataContext;
            ApplicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("/restart")]
        public void Restart()
        {
            ApplicationLifetime.StopApplication();
        }

        [Authorize(Roles = "Admin")]
        [Route("/time")]
        public string Time()
        {
            return DateTime.Now.ToNowEkb().ToString(CultureInfo.InvariantCulture);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
