using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Таблица значений предиката-функции при определённом отношении на множестве";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {

            public string[][] text { get; set; } = {
                new string[] { "\\(Ω = \\{0;1;2\\}\\) определено отношение \\(Q = \\{(0;2), (1;2)\\}\\)", "q" },
                new string[] { "\\(Ω = \\{1;2;3\\}\\) определено отношение \\(R = \\{(2;1), (3;2)\\}\\)", "r" },
                new string[] { "\\(Ω = \\{0;2;4\\}\\) определено отношение \\(P = \\{(2;4), (4;0)\\}\\)", "p" },
                new string[] { "\\(Ω = \\{2;3;5\\}\\) определено отношение \\(P = \\{(2;3), (5;5)\\}\\)", "p" }
            };
            public string[][] colrow { get; set; } = {
                new string[] { "0", "1", "2" },
                new string[] { "1", "2", "3" },
                new string[] { "0", "2", "4" },
                new string[] { "2", "3", "5" }
            };
            public string[][] vals { get; set; } = {
                new string[] { "001", 
                               "001",
                               "000" },
                new string[] { "000", 
                               "100", 
                               "010" },
                new string[] { "000", 
                               "001", 
                               "100" },
                new string[] { "010", 
                               "000", 
                               "001" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat04();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $" На множестве {data.text[num][0]}. Предикат-функцию \\({data.text[num][1]}(x,y)\\) можно задать таблицей значений: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"x &#10741; y & \\text{{{data.colrow[num][0]}}} & \\text{{{data.colrow[num][1]}}} & \\text{{{data.colrow[num][2]}}} \\\\" +
                $"\\hline" +
                $" {data.colrow[num][0]} & <f00> & <f01> & <f02> \\\\ " +
                $" {data.colrow[num][1]} & <f10> & <f11> & <f12> \\\\ " +
                $" {data.colrow[num][2]} & <f20> & <f21> & <f22> " +
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
