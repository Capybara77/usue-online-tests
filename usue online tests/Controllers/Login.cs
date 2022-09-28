using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
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

        private string GetStrHash(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            MD5 md5Hasher = MD5.Create();
            byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Encoding.UTF8.GetString(hashed);
        }

        [AntiDos(Delay = 3)]
        [HttpPost]
        public IActionResult LoginIn(string login, string password)
        {
            password = GetStrHash(password);
            User user = DataContext.Users.FirstOrDefault(user1 => user1.Login == login && user1.Password == password);


            if (user == null)
                return RedirectToAction("Index", "Login", new { message = "Неправильный логин или пароль" });

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

            if (HttpContext.Request.Headers.ContainsKey("referer"))
            {
                var refererUrl = new Uri(HttpContext.Request.Headers["referer"]);
                string urlPara = HttpUtility.ParseQueryString(refererUrl.Query).Get("ReturnUrl");
                if (!string.IsNullOrWhiteSpace(urlPara))
                    return LocalRedirect(urlPara);
            }

            return Redirect("/profile");
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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AntiDos(Delay = 500)]
        public IActionResult Register(string name, string group, string login, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(group) || string.IsNullOrEmpty(login) ||
                string.IsNullOrEmpty(password)) return StatusCode(400);

            if (name.Length > 40 || group.Length > 20 || login.Length > 30
                || password.Length > 30) return StatusCode(400);

            if (HttpContext.User.Identity is { IsAuthenticated: true }) return StatusCode(400);

            if (DataContext.Users.Any(user => user.Login == login || user.Name == name)) return StatusCode(400);

            DataContext.Users.Add(new User
            {
                Group = group,
                IsDark = false,
                Login = login,
                Name = name,
                Password = password,
                Role = Roles.User
            });
            DataContext.SaveChanges();

            return LocalRedirect("/profile");
        }
    }
}
