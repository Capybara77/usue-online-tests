using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinOperAll04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Равенство из определения матрицы линейного оператора в базисе Б";
        public string Description { get; } = "Линейные операторы All" ;

        public class Data
        {

            public string[][] ans { get; set; } = {
                new string[] { "z12", "z22", "z32", "z42" },
                new string[] { "q14", "q24", "q34", "q44" },
                new string[] { "p13", "p23","p33" },
                new string[] { "s12", "s22","s32" }
            };
            public string[][] text { get; set; } = {
                new string[] { "z", "Z" , "a,b,c,d", "b", "<ans0:3>*a +<ans1:3>*b + <ans2:3>*c +<ans3:3>*d" },
                new string[] { "q", "Q" , "a,b,c,d", "d", "<ans0:3>*a +<ans1:3>*b + <ans2:3>*c +<ans3:3>*d" },
                new string[] { "p", "P" , "u,v,w", "w", "<ans0:3>*u +<ans1:3>*v + <ans2:3>*w" },
                new string[] { "s", "S" , "u,v,w", "v", "<ans0:3>*u +<ans1:3>*v + <ans2:3>*w" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinOperAll04();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            string a = data.text[num][0];

            result.Text =
                $"Запишите  равенство из определения матрицы " +
                $"\\( \\begin{{pmatrix}} {a}11 & {a}12 & ... " +
                $"\\\\ {a}21 & {a}22 & ... " +
                $"\\\\  & ... &  \\end{{pmatrix}} \\) " +
                $"линейного оператора \\(\\hat{{{data.text[num][1]}}}\\) в базисе \\(Б = \\{{{data.text[num][2]}\\}}\\): \r\n" +
                $"\\( \\hat{{{data.text[num][1]}}}({data.text[num][3]}) = {data.text[num][4]} \\)";

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
