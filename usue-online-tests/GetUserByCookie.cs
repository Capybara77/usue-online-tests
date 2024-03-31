using System.Linq;
using Microsoft.AspNetCore.Http;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests;

public class GetUserByCookie
{
    public IHttpContextAccessor HttpContextAccessor { get; }
    public DataContext DataContext { get; }
    private User User { get; set; }

    public GetUserByCookie(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
    {
        HttpContextAccessor = httpContextAccessor;
        DataContext = dataContext;
    }

    public User GetUser()
    {
        return User ??= DataContext.Users.FirstOrDefault(user =>
            user.Login == HttpContextAccessor.HttpContext.User.Identity.Name);
    }
}