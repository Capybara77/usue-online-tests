using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using usue_online_tests.Data;

namespace usue_online_tests.Controllers
{
    [Authorize]
    public class Tests : Controller
    {
        public List<Type> AllTests { get; set; } = new List<Type>();

        public DataContext DataContext { get; }

        public Tests(DataContext dataContext)
        {
            DataContext = dataContext;
            LoadTests();
        }

        public IActionResult Index()
        {
            return View(AllTests.ToArray());
        }

        private void LoadTests()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName == "usue online tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    Type[] interfaces = type.GetInterfaces();
                    foreach (Type i in interfaces)
                    {
                        if (i.Name == "ITestCreater")
                        {
                            AllTests.Add(type);
                        }
                    }
                }
            }
        }
    }
}
