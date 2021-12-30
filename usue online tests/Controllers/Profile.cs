using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using usue_online_tests.Data;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class Profile : Controller
    {
        public DataContext DataContext { get; }

        public Profile(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(DataContext.Users.FirstOrDefault(user => user.Login == HttpContext.User.Identity.Name));
        }
    }
}
