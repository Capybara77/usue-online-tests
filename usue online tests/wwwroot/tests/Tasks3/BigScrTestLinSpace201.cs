using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace201 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Матрица перехода  из базиса P в Q";
        public string Description { get; } = "Линейные пространства 2";

        public class Data
        {
            public string[][] arr { get; set; } = {
               new string[] { "\\(P = \\left\\{x^2y, xy^2\\right\\}\\)", "\\(Q = \\left\\{x^2y - xy^2, 2x^2y + 3xy^2\\right\\}\\)"},    
               new string[] { "\\(P\\) = \\(\\left\\{ \\begin{pmatrix} 2 & 1 \\\\ 0 & 0 \\end{pmatrix} , \\begin{pmatrix} 0 & 0 \\\\ 2 & 1 \\end{pmatrix} \\right\\}\\)" ,
                              "\\(Q\\) = \\(\\left\\{ \\begin{pmatrix} 2 & 1 \\\\ 6 & 3 \\end{pmatrix} , \\begin{pmatrix} -4 & -2 \\\\ 8 & 4 \\end{pmatrix} \\right\\}\\)"} 
            };
            public string[][] ans { get; set; } = {
                new string[] { "1", "2", "-1", "3"},
                new string[] { "1", "-2", "3", "4"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace201();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $" Найдите матрицу перехода \\(T_{{P⇒Q}}\\) для базисов {data.arr[num][0]} и {data.arr[num][1]} \r\n" +
                $"\\(T_{{P⇒Q}}\\)  = \\(\\begin{{pmatrix}} <ans0> & <ans1> \\\\ <ans2> & <ans3> \\end{{pmatrix}}\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(2);
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
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
