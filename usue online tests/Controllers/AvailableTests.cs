using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class AvailableTests : Controller
    {
        public DataContext Context { get; }
        public GetUserByCookie GetUserByCookie { get; }

        public AvailableTests(DataContext context, GetUserByCookie getUserByCookie)
        {
            Context = context;
            GetUserByCookie = getUserByCookie;
        }

        public IActionResult Index()
        {
            User user = GetUserByCookie.GetUser();

            var dateTimeNow = DateTime.Now;

            Exam[] exams = Context.Exams.Where(exam =>
                    exam.Group == user.Group &&
                    exam.DateTimeEnd > dateTimeNow &&
                    exam.DateTimeStart < dateTimeNow &&
                    !Context.UserExamResults.Any(result =>
                        exam.Id == result.Exam.Id && result.IsCompleted && result.User.Id == user.Id))
                .Include(exam => exam.Preset)
                .Include(exam => exam.Preset.Owner)
                .ToArray();

            return View(exams);
        }

        [HttpPost]
        public int Count()
        {
            User user = GetUserByCookie.GetUser();

            return Context.Exams.Count(exam => exam.Group == user.Group &&
                                               exam.DateTimeEnd > DateTime.Now &&
                                               !Context.UserExamResults.Any(result =>
                                                   exam.Id == result.Exam.Id &&
                                                   result.IsCompleted &&
                                                   result.User.Id == user.Id));
        }
    }
}
