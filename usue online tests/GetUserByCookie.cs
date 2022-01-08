using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests
{
    public class GetUserByCookie
    {
        public IHttpContextAccessor HttpContextAccessor { get; }
        public DataContext DataContext { get; }

        public GetUserByCookie(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            HttpContextAccessor = httpContextAccessor;
            DataContext = dataContext;
        }

        public User GetUser()
        {
            return DataContext.Users.FirstOrDefault(user =>
                user.Login == HttpContextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
