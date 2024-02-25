using System;
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
using usue_online_tests.Requests;
using usue_online_tests.Tests;

namespace usue_online_tests.Controllers.Api;

[Route("api/")]
public class ApiController : Controller
{
    private readonly IMapper _mapper;
    private readonly TestsLoader _testsLoader;
    private readonly Random _random;
    private readonly DataContext _dbContext;
    private readonly GetUserByCookie _getUserService;
    private readonly TestsLoader _testLoader;

    public ApiController(DataContext dbContext, GetUserByCookie getUserService, TestsLoader testLoader, IMapper mapper,
        TestsLoader testsLoader)
    {
        _mapper = mapper;
        _testsLoader = testsLoader;
        _dbContext = dbContext;
        _getUserService = getUserService;
        _testLoader = testLoader;
        _random = new Random();
    }

    /*LOGIN*/
    [Route("login")]
    [HttpPost]
    public async Task<JsonResult> Login(string login, string password)
    {
        var user = _dbContext.Users
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

    [Route("logout")]
    [HttpPost]
    public async Task<JsonResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Json(true);
    }

    /*COMMON*/
    [Route("current-user")]
    [HttpGet]
    public JsonResult GetCurrentUser()
    {
        var currentUser = _getUserService.GetUser();
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

    /*START TEST*/
    [Route("create-test")]
    [HttpGet]
    public JsonResult CreateTestById(int testId)
    {
        var testCreator = _testLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testId);
        if (testCreator == null)
        {
            return Json(new { error = "invalid test id" });
        }

        var hash = _random.Next();
        var test = testCreator.CreateTest(hash);

        var testDto = _mapper.Map<TestDto>(test);
        testDto.Hash = hash;
        testDto.TestID = testId;

        return Json(testDto);
    }

    [Route("check-test-result")]
    [HttpPost]
    public JsonResult CheckTestResult([FromBody] GetTestResultRequest testResultRequest)
    {
        var test = _testLoader.TestCreators.FirstOrDefault(creator => creator.TestID == testResultRequest.TestId);
        if (test == null)
        {
            return Json("invalid test id");
        }

        var correctAnswers = test.CheckAnswer(testResultRequest.Hash, testResultRequest.FormData);

        return Json(correctAnswers);
    }

    public async Task<IActionResult> GetGroupList()
    {
        return Json((await _dbContext.Users.Where(user => user.Role == Roles.User).Select(user => user.Group).ToListAsync()).Distinct());
    }

    public IActionResult GetTasksList()
    {
        return Json(_testLoader.TestCreators.Select(creator => new { label = creator.Name, value = creator.TestID }).ToArray());
    }

    public IActionResult DeleteGroup(string groupName)
    {
        var users = _dbContext.Users.Where(user => user.Group == groupName);
        _dbContext.Users.RemoveRange(users);
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            return Json(new { ok = false, message = e.Message });
        }
        return LocalRedirect("/studentlist");
    }
}