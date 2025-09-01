using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat05 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Таблица значений предиката-функции при рефлексивном отношении";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {

            public string[][] text { get; set; } = {
                new string[] { "M", "Ω = \\{2;4;8\\}", "m" },
                new string[] { "K", "Ω = \\{0;2;4\\}", "k" },
                new string[] { "U", "Ω = \\{1;2;4\\}", "u" },
                new string[] { "V", "Ω = \\{1;2;4\\}", "v" }
                };
            public string[][] colrow { get; set; } = {
                new string[] { "2", "4", "8" },
                new string[] { "0", "2", "4" },
                new string[] { "1", "2", "4" },
                new string[] { "1", "2", "4" }
            };
            public string[][] vals { get; set; } = {
                new string[] { "1**",
                               "*1*",
                               "**1" },
                new string[] { "1**",
                               "*1*",
                               "**1" },
                new string[] { "1**",
                               "*1*",
                               "**1" },
                new string[] { "1**",
                               "*1*",
                               "**1" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat05();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $" Если отношение \\({data.text[num][0]}\\) на множестве \\({data.text[num][1]}\\) рефлексивно, то таблица значений \\({data.text[num][2]}(s,t)\\) имеет вид: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"s &#10741; t & \\text{{{data.colrow[num][0]}}} & \\text{{{data.colrow[num][1]}}} & \\text{{{data.colrow[num][2]}}} \\\\" +
                $"\\hline" +
                $" {data.colrow[num][0]} & <f00> & <f01> & <f02> \\\\ " +
                $" {data.colrow[num][1]} & <f10> & <f11> & <f12> \\\\ " +
                $" {data.colrow[num][2]} & <f20> & <f21> & <f22> " +
                $"\\end{{array}} \\)\r\n" +
                $"Вводите *, если значение не определено.";
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
