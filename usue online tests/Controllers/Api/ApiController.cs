﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Data;
using usue_online_tests.Dto;
using usue_online_tests.Models;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers.Api;

[Route("api/")]
public class ApiController : Controller
{
    private readonly IMapper _mapper;
    private readonly TestsLoader _testsLoader;
    public DataContext Data { get; }
    public GetUserByCookie UserByCookie { get; }
    public TestsLoader TestLoader { get; }

    public ApiController(DataContext data, GetUserByCookie userByCookie, TestsLoader testLoader, IMapper mapper,
        TestsLoader testsLoader)
    {
        _mapper = mapper;
        _testsLoader = testsLoader;
        Data = data;
        UserByCookie = userByCookie;
        TestLoader = testLoader;
    }

    /*LOGIN*/
    [Route("login")]
    [HttpPost]
    public async Task<JsonResult> Login(string login, string password)
    {
        var user = Data.Users
            .FirstOrDefault(user1 => user1.Password == password && user1.Login == login);

        if (user == null)
        {
            return Json(false);
        }

        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, login),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };

        var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var cp = new ClaimsPrincipal(ci);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp, new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.Now + new TimeSpan(48, 0, 0)
        });

        return Json(true);
    }

    /*COMMON*/
    [Route("current-user")]
    [HttpGet]
    public JsonResult GetCurrentUser()
    {
        var currentUser = UserByCookie.GetUser();
        var userDto = _mapper.Map<UserInfoDto>(currentUser);
        return Json(userDto);
    }

    /*TESTS*/
    [Route("tests-list")]
    [HttpGet]
    public JsonResult GetAllTests()
    {
        var tests = _testsLoader.TestCreators;

        return Json(tests.Select(test => new
        {
            test.Name,
            test.Description,
            test.TestID
        }));
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