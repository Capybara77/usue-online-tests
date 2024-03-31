using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang001 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Перевод с языка элементов на язык матричных операций в целом: сумма 01";
        public string Description { get; } = "Матричные операции";

        public class Data
        {
            
            public string[][] ans { get; set; } = {
                new string[] { "4*K-M^T"},
                new string[] { "4*P+2*Q^T"},
                new string[] { "4*M^T-K" },
                new string[] { "4*P^T-3*Q^T" },
                new string[] { "4*U+3*V^T"},
                new string[] { "4*K+M^T"},
                new string[] { "4*X^T-2*Y"},
                new string[] { "4*A^T+3*B^T"}
             };
            public string[][] text { get; set; } = {
                new string[] { "n_{uv}=4k_{uv}- m_{vu}", "K=(k_{ij})", "M=(m_{ij})", "N"},
                new string[] { "n_{ab}=4p_{ab}+2q_{ba}", "P=(p_{ij})", "Q=(q_{ij})", "N"},
                new string[] { "n_{pq}=4m_{qp}- k_{pq}", "M=(m_{ij})", "K=(k_{ij})", "N"},
                new string[] { "n_{cd}=4p_{dc}-3q_{dc}", "P=(p_{ij})", "Q=(q_{ij})", "N"},
                new string[] { "n_{gh}=4u_{gh}+3v_{hg}", "U=(u_{ij})", "V=(v_{ij})", "N"},
                new string[] { "n_{pq}=4k_{pq}+ m_{qp}", "K=(k_{ij})", "M=(m_{ij})", "N"},
                new string[] { "n_{ab}=4x_{ba}-2y_{ab}", "X=(x_{ij})", "Y=(y_{ij})", "N"},
                new string[] { "n_{pq}=4a_{qp}+3b_{qp}", "A=(a_{ij})", "B=(b_{ij})", "N"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang001();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(8);

            result.Text = 
                $"Представьте матрицу \\({data.text[num][3]}\\) с элементами \\({data.text[num][0]}\\) в виде результата применения матричных операций к матрицами " +
                $"\\({data.text[num][1]},{data.text[num][2]}\\) (результат транспонирования матрицы \\(Z\\) введите как \\(Z^T\\), скобки не используйте, " +
                $"произведение матрицы \\(Z\\), например, на 7 записывается как \\(7*Z\\), не переставляйте множители и слагаемые):\r\n " +
                $" \\(N=\\)\\(<ans:20>\\)\r\n"; //+
                //$" {data.ans[num][0]}";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(8);
            string[] ans = data.ans[num];
            try
            {
                //for (int i = 0; i < 8; i++)
                {
                    // if (answers["ans" + i]  == ans[i].ToString()) total++;
                    if(answers["ans"] == data.ans[num][0]) total++;
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
        public bool IsHidden { get; set; } = false;
    }
}
