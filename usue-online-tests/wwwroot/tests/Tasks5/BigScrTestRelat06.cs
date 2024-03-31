using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat06 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Таблица значений предиката-функции при симметричном отношении";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {

            public string[][] tvals { get; set; } =
            {
                new string[]
                {
                    "<f00>", "1", "<f02>",
                    "<f10>", "<f11>", "0",
                    "0", "<f21>", "<f22>"
                },
                new string[]
                {
                    "<f00>", "<f01>", "<f02>",
                    "1", "<f11>", "0",
                    "1", "<f21>", "<f22>"
                },
                new string[]
                {
                    "<f00>", "0", "<f02>",
                    "<f10>", "<f11>", "1",
                    "0", "<f21>", "<f22>"
                },
                new string[]
                {
                    "<f00>", "1", "<f02>",
                    "<f10>", "<f11>", "<f12>",
                    "0", "1", "<f22>"
                }
            };
            public string[][] vals { get; set; } = {
                new string[] { "*10",
                               "1*0",
                               "00*" },
                new string[] { "*11",
                               "1*0",
                               "10*" },
                new string[] { "*00",
                               "0*1",
                               "01*" },
                new string[] { "*10",
                               "1*1",
                               "01*" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat06();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $" Если отношение \\(V\\) на множестве \\(Ω = \\{{0;1;2\\}}\\) симметрично, то таблица значений \\(v(s,t)\\) имеет вид: \r\n" +
                $"\\( \\begin{{array}}{{c|ccc}}" +
                $"s &#10741; t & \\text{0} & \\text{1} & \\text{2} \\\\" +
                $"\\hline" +
                $" 0 & {data.tvals[num][0]} & {data.tvals[num][1]} & {data.tvals[num][2]} \\\\ " +
                $" 1 & {data.tvals[num][3]} & {data.tvals[num][4]} & {data.tvals[num][5]} \\\\ " +
                $" 2 & {data.tvals[num][6]} & {data.tvals[num][7]} & {data.tvals[num][8]} " +
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
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
