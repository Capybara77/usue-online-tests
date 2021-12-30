using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    public class HomeController : Controller
    {
        public DataContext DataContext { get; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            DataContext = dataContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUserByName(string name)
        {
            User user = DataContext.Users.FirstOrDefault(user1 => user1.Name == name);
            return user == null ? new NotFoundResult() : View(user);
        }

        public async Task<IActionResult> Privacy()
        {
            DataContext.Users.Add(new User()
            {
                Name = "Andrey",
                Role = Roles.Admin
            });
            await DataContext.SaveChangesAsync();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
