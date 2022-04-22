using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    public class Login : Controller
    {
        public DataContext DataContext { get; }

        public Login(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IActionResult Index(string message)
        {
            return RedirectAuthorizedUsers() ?? View("Index", message);
        }

        [HttpPost]
        public IActionResult LoginIn(string login, string password)
        {
            User user = DataContext.Users.FirstOrDefault(user1 => user1.Login == login && user1.Password == password);

            if (user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, login),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };

                ClaimsIdentity ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal cp = new ClaimsPrincipal(ci);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp, new AuthenticationProperties()
                {
                    ExpiresUtc = DateTimeOffset.Now + new TimeSpan(365, 0, 0)
                });
                return Redirect("/profile");
            }

            return RedirectToAction("Index", "Login", new { message = "Неправильный логин или пароль" });
            //return Redirect("/login?message=Неправильный логин или пароль");
        }

        public IActionResult LoginOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }



        public IActionResult NoAccess() => View();

        private IActionResult RedirectAuthorizedUsers()
        {
            if (HttpContext.User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Profile");
            }

            return null;
        }
    }
}
