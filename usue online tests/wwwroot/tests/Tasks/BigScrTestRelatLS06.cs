using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS06 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Соответствие формулы предикат функции";
        public string Description { get; } = "Соответствие формулы предикат функции";

        public class Data
        {
            public string[] equation { get; set; } = { "𝑎^2 + 𝑏^2 − |𝑎 · 𝑏| = 0", "𝑏^2 − |𝑎 · 𝑏| = 0", "𝑎^2 − |𝑎 · 𝑏| = 0", "𝑎^2𝑏^2 = 0" };
            public string[][] vals { get; set; } = {
                new[] { "000", "010", "000" },
                new[] { "111", "010", "111" },
                new[] { "101", "111", "101" },
                new[] { "010", "111", "010" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS06();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Пусть \\({{𝑎; 𝑏}} ⊆ {{−1; 0; 1}}\\). Формуле \\({data.equation[num]}\\) соответствует предикат-функция: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"a &#10741; b & \\text{{-1}} & \\text{{0}} & \\text{{1}} \\\\" +
                $"\\hline" +
                $"-1 & <f00> & <f01> & <f02> \\\\ " +
                $" 0 & <f10> & <f11> & <f12> \\\\ " +
                $" 1 & <f20> & <f21> & <f22> " +
                $"\\end{{array}} \\)";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);

            Data data = new Data();
            int num = random.Next(4);
            string[] vals = data.vals[num];
            
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (answers["f"+i+j] == vals[i][j].ToString())
                        {
                            total++;
                        }
                    }
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
        public int TimeLimitSeconds { get; set; } = 100;
    }
}
