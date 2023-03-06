using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Test_Wrapper;

namespace usue_online_tests.Tests
{
    public class TestsLoader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public List<Type> AllTests { get; set; } = new();
        public List<ITestCreator> TestCreators { get; set; } = new();


        public TestsLoader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            LoadTests();
            LoadDllTests();
        }

        private void LoadTests()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName == "usue online tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var all = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.ExportedTypes)
                {
                    CheckAndLoadCreator(type);
                }
            }
        }

        private void CheckAndLoadCreator(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            foreach (Type i in interfaces)
            {
                if (i == typeof(ITestCreator))
                {
                    AllTests.Add(type);
                    ITestCreator creator =
                        (ITestCreator)type.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<object>());

                    if (creator != null)
                    {
                        string mystring = creator.Name + creator.Description;
                        MD5 md5Hasher = MD5.Create();
                        byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(mystring));
                        int ivalue = BitConverter.ToInt32(hashed, 0);
                        creator.TestID = ivalue;
                        TestCreators.Add(creator);
                    }
                }
            }
        }

        private void LoadDllTests()
        {
            string pathToDll = _webHostEnvironment.ContentRootPath + "/dll";

            if (!Directory.Exists(pathToDll))
                Directory.CreateDirectory(pathToDll);

            string[] files = Directory.GetFiles(pathToDll)
                .Where(s => s.EndsWith(".dll")).ToArray();

            foreach (var file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);
                Module[] modules = assembly.GetLoadedModules();

                foreach (Module module in modules)
                {
                    try
                    {
                        foreach (Type assemblyExportedType in module.Assembly.ExportedTypes)
                        {
                            CheckAndLoadCreator(assemblyExportedType);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
