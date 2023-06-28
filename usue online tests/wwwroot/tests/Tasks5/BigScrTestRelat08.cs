using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat08 : ITestCreator, ITest, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Таблица значений предиката-функции при транзитивном отношении";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {

            public string[][] tvals { get; set; } =
            {
                new string[]
                {
                    "<f00>", "0", "1",
                    "1", "1", "<f12>",
                    "<f20>", "<f21>", "<f22>"
                },
                new string[]
                {
                    "0", "<f01>", "<f02>",
                    "1", "<f11>", "0",
                    "<f20>", "1", "<f22>"
                },
                new string[]
                {
                    "1", "1", "<f02>",
                    "1", "1", "0",
                    "1", "<f21>", "0"
                },
                new string[]
                {
                    "<f00>", "<f01>", "1",
                    "<f10>", "<f11>", "0",
                    "<f20>", "1", "0"
                }
            };
            public string[][] text { get; set; } = {
                new string[] { "Q", "Ω = \\{2;4;8\\}", "q" },
                new string[] { "V", "Ω = \\{0;2;4\\}", "v" },
                new string[] { "U", "Ω = \\{1;2;4\\}", "u" },
                new string[] { "R", "Ω = \\{1;2;4\\}", "r" }
                };
            public string[][] colrow { get; set; } = {
                new string[] { "1", "3", "5" },
                new string[] { "2", "4", "6" },
                new string[] { "2", "4", "8" },
                new string[] { "1", "2", "4" }
            };
            public string[][] vals { get; set; } = {
                new string[] { "*01",
                               "111",
                               "00*" },
                new string[] { "000",
                               "1*0",
                               "11*" },
                new string[] { "110",
                               "110",
                               "110" },
                new string[] { "*11",
                               "0*0",
                               "010" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat08();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $" Если отношение \\({data.text[num][0]}\\) на множестве \\({data.text[num][1]}\\) транзитивно, то таблица значений \\({data.text[num][2]}(s,t)\\) имеет вид: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"s &#10741; t & \\text{{{data.colrow[num][0]}}} & \\text{{{data.colrow[num][1]}}} & \\text{{{data.colrow[num][2]}}} \\\\" +
                $"\\hline" +
                $" {data.colrow[num][0]} & {data.tvals[num][0]} & {data.tvals[num][1]} & {data.tvals[num][2]} \\\\ " +
                $" {data.colrow[num][1]} & {data.tvals[num][3]} & {data.tvals[num][4]} & {data.tvals[num][5]} \\\\ " +
                $" {data.colrow[num][2]} & {data.tvals[num][6]} & {data.tvals[num][7]} & {data.tvals[num][8]} " +
                $"\\end{{array}} \\) \r\n" +
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
                        try
                        {
                            if (answers["f" + i + j] == vals[i][j].ToString())
                            {
                                total++;
                            }
                        }
                        catch
                        {

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
        public int TimeLimitSeconds { get; set; } = 90;
    }
}
