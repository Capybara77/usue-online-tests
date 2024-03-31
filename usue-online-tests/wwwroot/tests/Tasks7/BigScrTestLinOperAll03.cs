using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Утверждение равенством собственности для линейного оператора вектора линейного пространства, отвечающего собственному значению";
        public string Description { get; } = "Линейные операторы All";

        public class Data
        {
            
            public string[][] ans { get; set; } = {
                new string[] { "p", "8", "p" },
                new string[] { "a", "5", "a" },
                new string[] { "f", "-4","f" },
                new string[] { "t", "-3","t" }
            };
            public string[][] text { get; set; } = {
                new string[] { "p", "S" , "8" },
                new string[] { "a", "𝜙" , "5" },
                new string[] { "f", "P" , "-4" },
                new string[] { "t", "П" , "-3" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll03();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = 
                $"Запишите в виде равенства утверждение о том, что вектор \\({data.text[num][0]}\\) " +
                $"линейного пространства \\(U\\) является собственным для линейного оператора \\(\\hat{{{data.text[num][1]}}}\\), " +
                $"отвечающим собственному значению \\({data.text[num][2]}\\).\r\n" +
                $"\\(\\hat{{{data.text[num][1]}}}(<ans0>) =  <ans1>*<ans2>\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(4);
            string[] ans = data.ans[num];
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (answers["ans" + i]  == ans[i].ToString()) total++;
                }
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
