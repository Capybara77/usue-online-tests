using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace203 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора в базисе 'X' линейного пространства U";
        public string Description { get; } = "Линейные пространства 2";

        public class Data
        {
            public string[][] arr { get; set; } = { 
            new string[] { "\\(Q = \\left\\{ A = \\begin{pmatrix} 1 & 0 \\\\ 0 & 1 \\end{pmatrix} , B = \\begin{pmatrix} 0 & 1 \\\\ 0 & 0 \\end{pmatrix} , C = \\begin{pmatrix} 0 & 1 \\\\ 1 & 0 \\end{pmatrix} \\right\\}\\)", " \\left[\\ \\begin{pmatrix} -1 & 5 \\\\ 5 & -1 \\end{pmatrix} \\right]_Q" },
            new string[] { "\\(P = \\left\\{ A = \\begin{pmatrix} 1 & 0 \\\\ 0 & 0 \\end{pmatrix} , B = \\begin{pmatrix} 1 & 0 \\\\ 0 & -1 \\end{pmatrix} , C  = \\begin{pmatrix} 0 & 1 \\\\ 1 & 0 \\end{pmatrix} \\right\\}\\)", " \\left[\\ \\begin{pmatrix} -3 & 2 \\\\ 2 & 3 \\end{pmatrix} \\right]_Q" } };
            public string[][] ans { get; set; } = {
                new string[] { "-1", "1", "0", "0", "1", "0", "0", "1", "0", "0", "5", "0", "1", "1", "0", "-1", "0", "5"},
                new string[] { "0", "1", "0", "0", "0", "-3", "1", "0", "0", "-1", "2", "0", "1", "1", "0", "0", "-3", "2"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace203();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"В базисе {data.arr[num][0]} Линейного пространства \\(U\\)\r\n" +
                $"\\({data.arr[num][1]}\\)  = \r\n" +
                $"=\\(\\left[<ans0>*\\begin{{pmatrix}} <ans1> & <ans2> \\\\ <ans3> & <ans4> \\end{{pmatrix}} +<ans5>*\\begin{{pmatrix}} <ans6> & <ans7> \\\\ <ans8> & <ans9> \\end{{pmatrix}} +<ans10>\\begin{{pmatrix}} <ans11> & <ans12> \\\\ <ans13> & <ans14> \\end{{pmatrix}} \\right] \\)=\r\n" +
                $" = \\(\\begin{{pmatrix}} <ans15>\\\\<ans16>\\\\<ans17>\\end{{pmatrix}}\\)";

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
                for (int i = 0; i < 18; i++)
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
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = true;
    }
}
