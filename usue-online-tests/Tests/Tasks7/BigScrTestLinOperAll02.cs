using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll02 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Нахождение собственных значений оператора по многочлену, имеющему в базисе Б матрицу";
        public string Description { get; } = "Линейные операторы All";

        public class Data
        {
            
            public string[][] ans { get; set; } = {
                new string[] { "2", "1", "2", "-3" },
                new string[] { "4", "3", "5", "-6" },
                new string[] { "3", "2", "-4", "-5" },
                new string[] { "2", "3", "1", "0" }
            };
            public string[][] text { get; set; } = {
                new string[] { "3x^2+2y^2", "x^2+y^2", "x^2", "3 & -2 \\\\ 3 & -4" },
                new string[] { "4x-x^2", "x-x^2", "x^2", "2 & 4 \\\\ 6 & -3" },
                new string[] { "5x^2-3", "x^2-1", "x^2", "-2 & -3 \\\\ 2 & -7" },
                new string[] { "5x^3+2y^2", "x^3+y^3", "x^3", "4 & -2 \\\\ 6 & -3" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll02();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = 
                $"Является ли многочлен \\(({data.text[num][0]})\\) собственным вектором оператора \\(\\hat{{P}}\\), " +
                $"имеющего в базисе Б = \\(\\{{{data.text[num][1]}, {data.text[num][2]}\\}}\\) матрицу \\(\\begin{{pmatrix}} {data.text[num][3]} \\end{{pmatrix}}\\).\r\n" +
                $"Найдите собственные значения этого оператора.\r\n" +
                $"\\(({data.text[num][0]}) = <ans0>*({data.text[num][1]}) + <ans1>*{data.text[num][2]}\\), " +
                $"отвечает собственным значениям \\(<ans2>\\) и \\(<ans3>\\)";

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
                for (int i = 0; i < 4; i++)
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = true;
    }
}
