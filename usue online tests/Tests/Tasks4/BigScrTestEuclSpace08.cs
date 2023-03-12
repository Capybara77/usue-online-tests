using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace08 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Определение базиса Q при известных базисе P и матрице перехода";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            static int num;
            public Data(int n)
            {
                num = n;
                basis = new string[][]{
                new string[] { "\\begin{pmatrix} 1 & 2 \\\\ 2 & 1 \\end{pmatrix}", "\\begin{pmatrix} 2 & -1 \\\\ -1 & 2 \\end{pmatrix}" },
                new string[] { "\\begin{pmatrix} 1 & 2 \\\\ 3 & 6 \\end{pmatrix}", "\\begin{pmatrix} -2 & -4 \\\\ -1 & -2 \\end{pmatrix}" },
                new string[] { "2^x + 2^y", "2^x - 2^y" },
                new string[] { "x^2 - y^2", "x^2 + y^2" }
            };
                arr = new string[][] {
                new string[] { "\\(P = \\left\\{ " + basis[num][0] + " , " + basis[num][1] + " \\right\\}\\)", "\\(T_{P⇒Q}\\)  = \\(\\begin{pmatrix} -1 & 0 \\\\ 3 & 2 \\end{pmatrix}\\)" },
                new string[] { "\\(P = \\left\\{ " + basis[num][0] + " , " + basis[num][1] + " \\right\\}\\)", "\\(T_{P⇒Q}\\)  = \\(\\begin{pmatrix} 2 & -1 \\\\ 1 & 0 \\end{pmatrix}\\)" },
                new string[] { "\\(P = \\left\\{2^x + 2^y, 2^x - 2^y\\right\\}\\)", "\\(T_{P⇒Q}\\)  = \\(\\begin{pmatrix} 0 & 2 \\\\ -2 & -3 \\end{pmatrix}\\)"},
                new string[] { "\\(P = \\left\\{x^2 - y^2, x^2 + y^2\\right\\}\\)", "\\(T_{P⇒Q}\\)  = \\(\\begin{pmatrix} 3 & 1 \\\\ 0 & -2 \\end{pmatrix}\\)"}

                };
                anstext = new string[] {
            "\\( \\left\\{ <ans0> * " + basis[num][0] + " + <ans1> * " + basis[num][1] + " , \\right.\\)\r\n" +
                     "\\(,\\left.<ans2> * " + basis[num][0] + " + <ans3> * " + basis[num][1] + " \\right\\} =\\)\r\n" +
            " = \\(\\left\\{ \\begin{pmatrix} <ans4> & <ans5> \\\\ <ans6> & <ans7> \\end{pmatrix}," +
                      " \\begin{pmatrix} <ans8> & <ans9> \\\\ <ans10> & <ans11> \\end{pmatrix} \\right\\}. \\)",

            "\\( \\left\\{<ans0:3>*\\left(" + basis[num][0] + "\\right) + <ans1:3>*\\left(" + basis[num][1] + "\\right)\\right.,\\)\r\n" +
                       "\\(,\\left.<ans2>*\\left(" + basis[num][0] + "\\right) + <ans3:3>*\\left(" + basis[num][1] + "\\right) \\right\\} =\\)\r\n" +
                    "= \\(\\left\\{ <ans4:3>*<ans5:3>^{<ans6:3>} + <ans7:3>*<ans8:3>^{<ans9:3>} , <ans10:3>*<ans11:3>^{<ans12:3>} + <ans13:3>*<ans14:3>^{<ans15:3>} \\right\\}.\\)" +
                    "\r\nПоля ответа всегда должны иметь значения, в том числе \"\\(0, 1, -1\\)\"."
                };
            }
            public static string[][] basis;
            public string[][] arr { get; set; }

            public string[][] ans { get; set; } = {
                new string[] { "-1", "3", "0", "2",
                                "5", "-5", "-5", "5",
                                "4", "-2", "-2", "4"},
                new string[] {  "2", "1", "-1", "0",
                                "0", "0", "5", "10",
                               "-1", "-2", "-3", "-6"},
                new string[] {  "0", "-2", "2", "-3",
                               "-2", "2", "x", "2", "2", "y", "-1", "2", "x", "5", "2", "y"},
                new string[] {  "3", "0", "1", "-2",
                                "3", "x", "2", "-3", "y", "2", "-1", "x", "2", "-3", "y", "2"}

            };
            public string[] anstext { get; set; }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace08();

            Random random = new Random(randomSeed);
            int number = random.Next(4);
            Data data = new Data(number);

            int atnum = 0;
            if (number > 1) atnum = 1;

            result.Text = $" Известны базис {data.arr[number][0]} и матрица перехода {data.arr[number][1]} \r\n" +
            $"Тогда базис \\(Q =\\) {data.anstext[atnum]}";
            return result;
        }
        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            int num = random.Next(4);
            Data data = new Data(num);

            string[] ans = data.ans[num];
            try
            {
                for (int i = 0; i < 16; i++)
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
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = true;
    }
}
