using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Значение x равенства в поле";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "\\(3\\) равенство \\(1 + x = 0\\) выполняется при \\(x =\\)",
                "\\(5\\) равенство \\(1 + x = 0\\) выполняется при \\(x =\\)"
            };
            public string[] ans { get; } = {
                "2", "4"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst03();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"В поле характеристики {data.arr[num]} \\(<ans0>\\)";

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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
