using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class MyTask : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Пример теста в отдельной dll";
        public string Description { get; }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new MyTask();

            int a = new Random(randomSeed).Next(10, 20);
            result.Text = $"В ответ запишите число {a}. \\(<number>\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            try
            {
                return Convert.ToInt32(answers["number"]) == new Random(randomSeed).Next(10, 20) ? 1 : 0;
            }
            catch
            {
                return 0;
            }
        }

        public string Text { get; set; }
        public List<Image> Pictures { get; set; }
    }
}
