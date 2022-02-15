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
        public GetUserByCookie GetUserByCookie { get; }

        public Profile(DataContext dataContext, GetUserByCookie getUserByCookie)
        {
            DataContext = dataContext;
            GetUserByCookie = getUserByCookie;
        }

        public IActionResult Index()
        {
            return View(DataContext.Users.FirstOrDefault(user => user.Login == HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public IActionResult ChangeUserTheme()
        {
            var user = GetUserByCookie.GetUser();
            user.IsDark = !user.IsDark;
            DataContext.SaveChanges();
            return StatusCode(200);
        }
    }
}
