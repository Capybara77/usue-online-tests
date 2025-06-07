using System;
using System.Collections.Generic;
using System.IO;
using Test_Wrapper;

namespace usue_online_tests.Tests
{
    public class SimpleTest : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name => "Simple Test";
        public string Description => "Easy test.";

        public ITest CreateTest(int randomSeed)
        {
            var result = new SimpleTest();
            var random = new Random(randomSeed);
            var a = random.Next(0, 20);
            result.Text = $"Решите уравнение: \\(<a>\\) + 5 = {a + 5}\r\n";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);
            int a = random.Next(0, 20);
            int total = 0;

            if (answers.TryGetValue("a", out var userAnswer) &&
                int.TryParse(userAnswer, out var userNumber) &&
                userNumber == a)
            {
                total++;
            }

            return total;
        }


        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
