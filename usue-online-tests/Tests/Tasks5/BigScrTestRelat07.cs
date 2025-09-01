using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat07 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Таблица значений предиката-функции при антисимметричном отношении";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {

            public string[][] tvals { get; set; } =
            {
                new string[]
                {
                    "<f00>", "0", "1",
                    "<f10>", "<f11>", "<f12>",
                    "<f20>", "0", "0"
                },
                new string[]
                {
                    "0", "<f01>", "0",
                    "1", "<f11>", "<f12>",
                    "<f20>", "0", "<f22>"
                },
                new string[]
                {
                    "<f00>", "1", "1",
                    "<f10>", "1", "<f12>",
                    "<f20>", "0", "<f22>"
                },
                new string[]
                {
                    "<f00>", "1", "<f02>",
                    "<f10>", "<f11>", "<f12>",
                    "0", "0", "1"
                }
            };
            public string[][] vals { get; set; } = {
                new string[] { "*01",
                               "1*1",
                               "000" },
                new string[] { "000",
                               "1*1",
                               "10*" },
                new string[] { "*11",
                               "011",
                               "00*" },
                new string[] { "*11",
                               "0*1",
                               "001" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat07();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $" Если отношение \\(T\\) на множестве \\(Ω = \\{{3;5;7\\}}\\) антисимметрично, то таблица значений \\(T(u,v)\\) имеет вид: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"u &#10741; v & \\text{3} & \\text{5} & \\text{7} \\\\" +
                $"\\hline" +
                $" 3 & {data.tvals[num][0]} & {data.tvals[num][1]} & {data.tvals[num][2]} \\\\ " +
                $" 5 & {data.tvals[num][3]} & {data.tvals[num][4]} & {data.tvals[num][5]} \\\\ " +
                $" 7 & {data.tvals[num][6]} & {data.tvals[num][7]} & {data.tvals[num][8]} " +
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
