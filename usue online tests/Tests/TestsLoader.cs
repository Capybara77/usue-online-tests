using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public class TestsLoader
    {
        public List<Type> AllTests { get; set; } = new();
        public List<ITestCreater> TestCreaters { get; set; } = new();


        public TestsLoader()
        {
            LoadTests();
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
                        if (i == typeof(ITestCreater))
                        {
                            AllTests.Add(type);
                            ITestCreater creater =
                                (ITestCreater)type.GetConstructor(Type.EmptyTypes)?.Invoke(new object?[0]);

                            string mystring = creater.Name + creater.Description;
                            MD5 md5Hasher = MD5.Create();
                            byte[] hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(mystring));
                            int ivalue = BitConverter.ToInt32(hashed, 0);
                            creater.TestID = ivalue;

                            TestCreaters.Add(creater);
                        }
                    }
                }
            }
        }
    }
}
