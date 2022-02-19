using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using usue_online_tests.Data;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ApiController : Controller
    {
        public DataContext Data { get; }
        public GetUserByCookie UserByCookie { get; }
        public TestsLoader TestLoader { get; }

        public ApiController(DataContext data, GetUserByCookie userByCookie, TestsLoader testLoader)
        {
            Data = data;
            UserByCookie = userByCookie;
            TestLoader = testLoader;
        }

        public async Task<IActionResult> GetGroupList()
        {
            return Json((await Data.Users.Select(user => user.Group).ToListAsync()).Distinct());
        }

        public IActionResult GetTasksList()
        {
            return Json(TestLoader.TestCreaters.Select(creator => new {label = creator.Name, value = creator.TestID}).ToArray());
        }
    }
}