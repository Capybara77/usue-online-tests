using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll07 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Преобразование многочлена через матрицу линейного оператора в базисе Б";
        public string Description { get; } = "Линейные операторы All";

        public class Data
        {

            public string[][] ans { get; set; } = {
                new string[] { "-1", "3" },
                new string[] { "-1", "-8" },
                new string[] { "3", "-5" },
                new string[] { "-9", "-7" }
            };
            public string[][] text { get; set; } = {
                new string[] { "p", "q", "F", "2p+3q", "<ans0>*p+<ans1>*q" },
                new string[] { "x", "1", "P", "2-3x", "<ans0>*x+<ans1>" },
                new string[] { "1", "x", "L", "3-x", "<ans0>+<ans1>*x" },
                new string[] { "x", "y", "G", "5x-3y", "<ans0>*x+<ans1>*y" }
            };
            public string[][] arr { get; set; } = {
                new string[] { 
                    "-2", "1",
                    "3", "-1" 
                },
                new string[] { 
                    "-3", "4",
                    "2", "-1" 
                },
                new string[] {
                    "-1", "4", 
                    "-2", "3" 
                },
                new string[] { 
                    "-3", "2",
                    "1", "4" 
                }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll07();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            string vec1 = data.text[num][0];
            string vec2 = data.text[num][1];
            string linop = data.text[num][2];
            string expr = data.text[num][3];
            string ans = data.text[num][4];
            string[] arr = data.arr[num];

            result.Text =
                $"В базисе \\(Б = \\{{{vec1},{vec2}\\}} \\) матрица линейного оператора \\(\\hat{{{linop}}}\\) " +
                $"равна \\(\\begin{{pmatrix}} {arr[0]} & {arr[1]} \\\\ {arr[2]} & {arr[3]} \\end{{pmatrix}}\\).\r\n" +
                $"Тогда \\(\\hat{{{linop}}}({expr}) = {ans}\\).";

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
