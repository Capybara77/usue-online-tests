using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll05 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Выражение координаты вектора в базисе через матрицу лин. оператора и столбца коорд. вектора";
        public string Description { get; } = "Линейные операторы All";

        public class Data
        {

            public string[][] ans { get; set; } = {
                new string[] { "S", "P" },
                new string[] { "P", "S" },
                new string[] { "L", "F" },
                new string[] { "Q", "Z" }
            };
            public string[][] text { get; set; } = {
                new string[] { "S", "p", "P" },
                new string[] { "P", "s", "S" },
                new string[] { "L", "f", "F" },
                new string[] { "Q", "z", "Z" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll05();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            string oper = data.text[num][0];
            string vec = data.text[num][1];
            string col = data.text[num][2];

            result.Text =
                $"Координаты вектора \\(\\hat{{{oper}}}(\\vec{{{vec}}})\\) " +
                $"в базисе \\(Б\\) выразить через матрицу \\({oper}\\) линейного оператора \\(\\hat{{{oper}}}\\) " +
                $"и столбца \\({col}\\) координат вектора \\(\\vec{{{vec}}}\\):\r\n" +
                $"\\( [\\hat{{{oper}}}(\\vec{{{vec}}})]_Б = <ans0>*<ans1> \\)";

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
                for (int i = 0; i < 2; i++)
                {
                    if (answers["ans" + i] == ans[i].ToString()) total++;
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
        public bool IsHidden { get; set; } = true;
    }
}
