using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst05 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Значение x равенства в поле Галуа";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "\\((1 + 1 + 1)x = 1\\) в поле \\(GF(5) = \\{0,1,2,3,4\\}\\)",
                "\\((1 + 1)x = 1\\) в поле \\(GF(5) = \\{0,1,2,3,4\\}\\)"
            };
            public string[] ans { get; } = {
                "2", "3"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst05();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"Решите уравнение {data.arr[num]}.\r\n" +
                          $"\\(x = <ans0>\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(2);
            string ans = data.ans[num];
            try
            {
                if (answers["ans0"] == ans) total++;
            }
            catch
            {
                // ignored
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true; 
    }
}
