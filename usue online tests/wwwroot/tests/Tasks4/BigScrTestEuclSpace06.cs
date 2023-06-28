using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace06 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора в базисе 'X' линейного пространства U";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            public string[][] arr { get; set; } = {
                new string[] { "В базисе \\(Q = \\left\\{ \\begin{pmatrix} 1 & 0 \\\\ 0 & 1 \\end{pmatrix},  \\begin{pmatrix} 0 & 1 \\\\ 0 & 0 \\end{pmatrix}, \\begin{pmatrix} 0 & 1 \\\\ 1 & 0 \\end{pmatrix} \\right\\}\\) линейного пространства \\(U\\)", " \\left[\\ \\begin{pmatrix} -1 & 5 \\\\ 5 & -1 \\end{pmatrix} \\right]_Q" },
                new string[] { "В базисе \\(P = \\left\\{ \\begin{pmatrix} 1 & 0 \\\\ 0 & 0 \\end{pmatrix}, \\begin{pmatrix} 1 & 0 \\\\ 0 & -1 \\end{pmatrix}, \\begin{pmatrix} 0 & 1 \\\\ 1 & 0 \\end{pmatrix} \\right\\}\\) линейного пространства \\(U\\)", " \\left[\\ \\begin{pmatrix} -3 & 2 \\\\ 2 & 3 \\end{pmatrix} \\right]_Q" },
                new string[] { "Если \\(B = \\left\\{x - 1, x, x^2\\right\\}\\) — базис линейного пространства \\(U\\), то ", "\\left[4x^2 + x - 1\\right]_B" },
                new string[] { "Если \\(D = \\left\\{x^2 - y^2, xy, x^2\\right\\}\\) — базис линейного пространства \\(U\\), то ", "\\left[3x^2 + xy - 3y^2\\right]_D" }
            };
            public string[][] ans { get; set; } = {
                new string[] { "-1", "0", "5"},
                new string[] { "0", "-3", "2"},
                new string[] { "1", "0", "4"},
                new string[] { "3", "1", "0"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace06();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"{data.arr[num][0]}\r\n" +
                $"\\({data.arr[num][1]}\\) = \\(\\begin{{pmatrix}} <ans0>\\\\<ans1>\\\\<ans2>\\end{{pmatrix}}\\)";

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
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
