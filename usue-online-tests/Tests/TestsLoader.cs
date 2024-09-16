using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
                assembly.FullName == "usue-online-tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.ExportedTypes)
                {
                    CheckAndLoadCreator(type);
                }
            }
        }

        private void LoadDllTests()
        {
            var pathToDll = _webHostEnvironment.ContentRootPath + "/dll";

            if (!Directory.Exists(pathToDll))
                Directory.CreateDirectory(pathToDll);

            var files = Directory.GetFiles(pathToDll)
                .Where(s => s.EndsWith(".dll")).ToArray();

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                var modules = assembly.GetLoadedModules();

                foreach (var module in modules)
                {
                    try
                    {
                        foreach (var assemblyExportedType in module.Assembly.ExportedTypes)
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

        private void CheckAndLoadCreator(Type type)
        {
            var interfaces = type.GetInterfaces();

            foreach (var i in interfaces)
            {
                if (i != typeof(ITestCreator))
                {
                    continue;
                }

                AllTests.Add(type);
                var creator =
                    (ITestCreator)type.GetConstructor(Type.EmptyTypes)?.Invoke(Array.Empty<object>());

                if (creator == null)
                {
                    continue;
                }

                var myString = creator.Name + creator.Description;
                var md5Hasher = MD5.Create();
                var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(myString));
                var intHash = BitConverter.ToInt32(hashed, 0);

                creator.TestID = intHash;
                TestCreators.Add(creator);
            }
        }
    }
}
