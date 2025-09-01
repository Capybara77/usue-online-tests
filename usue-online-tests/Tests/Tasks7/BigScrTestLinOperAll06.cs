using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll06 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Выражение матрицы оператора в двух базисах через матрицы перехода";
        public string Description { get; } = "Линейные операторы All";

        public class Data
        {

            public string[][] ans { get; set; } = {
                new string[] { "P2", "L1", "P1" },
                new string[] { "R2", "N1", "R1" },
                new string[] { "Q2", "M1", "Q1" },
                new string[] { "F2", "H1", "F1" }
            };
            public string[][] text { get; set; } = {
                new string[] { "P", "L" },
                new string[] { "R", "N" },
                new string[] { "Q", "M" },
                new string[] { "F", "H" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll06();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            string mp = data.text[num][0];
            string mo = data.text[num][1];

            result.Text =
                $"Если \\({mp}1\\) матрица перехода из базиса \\(X\\) в базис \\(Y\\), " +
                $"\\({mp}2\\) - матрица обратного перехода, \r\n " +
                $"\\({mo}1\\) и \\({mo}2\\) - матрицы оператора \\(\\hat{{{mo}}}\\) " +
                $"в базисах \\(X\\) и \\(Y\\), " +
                $"то \\({mo}2\\) выражается через остальные матрицы формулой: \r\n" +
                $"\\({mo}2 = <ans0:3>*<ans1:3>*<ans2:3>\\).";

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
