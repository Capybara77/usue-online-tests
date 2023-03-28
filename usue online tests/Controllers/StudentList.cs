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

        [HttpGet]
        public IActionResult CreatePage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreatePage(string groupName, string studentList)
        {
            string[] students = studentList.Split("\r\n").Distinct().ToArray();
            
            Random random = new Random();

            User[] users = new User[students.Length];

            for (var i = 0; i < students.Length; i++)
            {
                users[i] = new User
                {
                    Group = groupName,
                    IsDark = false,
                    Login = random.Next(100000, 999999).ToString(),
                    Name = students[i],
                    Role = Roles.User,
                    Password = random.Next(100000, 999999).ToString()
                };
            }

            DataContext.Users.AddRange(users);
            DataContext.SaveChanges();

            return View("DisplayUsers", users);
        }
    }
}
