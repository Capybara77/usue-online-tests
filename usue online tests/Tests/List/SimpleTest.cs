using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests
{
    public class SimpleTest : ITestCreater, ITest
    {
        private string[] randomWords = new[] { "саня", "набеков", "стас" };

        public int TestID { get; set; }
        public string Name { get; } = "Simple Test";
        public string Description { get; } = "Easy test.";

        public Random Random { get; set; }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new SimpleTest();

            Random = new Random(randomSeed);

            int a = Random.Next(0, 20);

            ITestBox box1 = new SimpleTestBox()
            {
                Texts = new List<string> { $"Найдите a для уравнения a + 5 = {a + 5}" },
                Numbers = new List<double> {a}
            };

            string word = randomWords[Random.Next(0, randomWords.Length)];

            ITestBox box2 = new SimpleTestBox
            {
                Texts = new List<string>{$"Напишите слово полностью **{word.Remove(0, 2)}"},
                TextAnswers = new List<string>{ word }
            };

            result.Boxes.Add(box1);
            result.Boxes.Add(box2);

            return result;
        }

        public List<ITestBox> Boxes { get; set; } = new List<ITestBox>();
    }
}
