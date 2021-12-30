using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public class SimpleTest : ITestCreater, ITest
    {
        public static string Name { get; } = "Simple Test";

        private SimpleTest()
        {

        }

        public Random Random { get; set; }

        public ITest CreateTest(int randomSeed)
        {
            Random = new Random(randomSeed);

            int a = Random.Next(0, 20);
            Texts.Add($"Найдите a для уравнения a + 5 = {a + 5}");
            Numbers.Add(a);
            return this;
        }

        public List<string> Texts { get; set; } = new List<string>();
        public List<double> Numbers { get; set; } = new List<double>();
        public List<Bitmap> Pictures { get; set; } = new List<Bitmap>();
    }

    public class MyTest : ITestCreater
    {
        public static string Name { get; } = "My test 1";

        public ITest CreateTest(int randomSeed)
        {
            return null;
        }
    }
}
