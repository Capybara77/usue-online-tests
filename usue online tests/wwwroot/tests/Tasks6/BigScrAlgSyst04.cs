using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Значение выражения в поле Галуа";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "\\(GF(5) = \\{0,1,2,3,4\\}\\) имеем \\((1+1)(1+1+1)\\)",
                "\\(GF(5) = \\{0,1,2,3,4\\}\\) имеем \\((1+1+1)(1+1+1)\\)",
                "\\(GF(3) = \\{0,1,2\\}\\) имеем \\(2^3\\)",
                "\\(GF(5) = \\{0,1,2,3,4\\}\\) имеем \\(2^2 + 2\\)",
                "\\(GF(5) = \\{0,1,2,3,4\\}\\) имеем \\(2^3\\)",
                "\\(GF(3) = \\{0,1,2\\}\\) имеем \\(2^2 + 2\\)",
            };
            public string[] ans { get; } = {
                "1", "4", "2", "1", "3", "0"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst04();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(6);

            result.Text = $"В поле {data.arr[num]} = \\(<ans0>\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(6);
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
