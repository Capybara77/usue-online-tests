using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using usue_online_tests.Data;
using usue_online_tests.Models;

namespace usue_online_tests.Components
{
    public class GetUserComponent : ViewComponent
    {
        public DataContext Data { get; }

        public GetUserComponent(DataContext data)
        {
            Data = data;
        }

        public User Invoke()
        {
            return Data.Users.FirstOrDefault(user => user.Login == HttpContext.User.Identity.Name);
        }
    }
}
