using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Models.View;

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
            User user = GetUserByCookie.GetUser();

            ProfileWrapper profileWrapper = new ProfileWrapper
            {
                User = user,
                ExamResults = DataContext.UserExamResults
                    .Where(result => result.IsCompleted && result.User.ID == user.ID)
                    .Include(result => result.Exam.Preset)
                    .Include(result => result.ExamTestAnswers)
            };

            return View(profileWrapper);
        }

        [HttpGet]
        public IActionResult ChangeUserTheme()
        {
            var user = GetUserByCookie.GetUser();
            user.IsDark = !user.IsDark;
            DataContext.SaveChanges();
            return StatusCode(200);
        }
    }
}
