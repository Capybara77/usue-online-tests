using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using usue_online_tests.Data;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers;

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
        return Json((await Data.Users.Where(user => user.Role == Roles.User).Select(user => user.Group).ToListAsync()).Distinct());
    }

    public IActionResult GetTasksList()
    {
        return Json(TestLoader.TestCreators.Select(creator => new { label = creator.Name, value = creator.TestID }).ToArray());
    }

    public IActionResult DeleteGroup(string groupName)
    {
        var users = Data.Users.Where(user => user.Group == groupName);
        Data.Users.RemoveRange(users);
        try
        {
            Data.SaveChanges();
        }
        catch (Exception e)
        {
            return Json(new { ok = false, message = e.Message });
        }
        return LocalRedirect("/studentlist");
    }
}