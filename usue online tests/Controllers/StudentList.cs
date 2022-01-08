using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class StudentList : Controller
    {
        public DataContext DataContext { get; }

        public StudentList(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IActionResult Index()
        {
            var students = DataContext.Users.Where(user => user.Role == Roles.User).ToList();

            List<IGrouping<string, User>> groups = students.GroupBy(user => user.Group).ToList();

            return View(groups);
        }
    }
}
