using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS07 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Задание множества функцией-предикатом";
        public string Description { get; } = "Задание множества функцией-предикатом";

        public class Data
        {
            public string[] arr { get; set; } = { "{0; 1; 2}", "{1; 2; 3}", "{−1; 0; 1}", "{0; 2; 4}" };
            public string[] b { get; set; } = { 
                "𝐵 = {{(1; 0);  (1; 2);  (2; 0); (2; 1)}}",
                "𝐵 = {{(1; 1);  (1; 3);  (2; 1); (3; 2)}}", 
                "𝐵 = {{(−1;−1); (−1; 0); (0; 0)}}", 
                "𝐵 = {{(0; 4);  (2; 2);  (4; 0)}}" };
            public string[][] vals { get; set; } = {
                new[] { "000", "101", "110"},
                new[] { "101", "100", "010"},
                new[] { "110", "010", "000"},
                new[] { "001", "010", "100"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS07();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"{data.b[num]} на множестве {data.arr[num]} можно задать функцией: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"x &#10741; y & \\text{{-1}} & \\text{{0}} & \\text{{1}} \\\\" +
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
                        if (answers["f" + i + j] == vals[i][j].ToString())
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
