using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang004 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Перевод с языка матричных операций в целом на язык элементов матриц: произведение 01";
        public string Description { get; } = "Матричные операции";

        public class Data
        {            
            public string[][] ans { get; set; } = {
                new string[] { "k1u*m1v+k2u*m2v+k3u*m3v" },
                new string[] { "pa1*qb1+pa2*qb2+pa3*qb3" },
                new string[] { "mp1*k1q+mp1*k1q+mp1*k1q" },
                new string[] { "p1c*qd1+p2c*qd2+p3c*qd3" },
                new string[] { "v1f*u1g+v2f*u2g+v3f*u3g" },
                new string[] { "m1p*k1q+m2p*k2q+m3p*k3q" },
                new string[] { "y1a*xb1+y2a*xb2+y3a*xb3" },
                new string[] { "bu1*a1v+bu2*a2v+bu3*a3v" }
             };
            public string[][] text { get; set; } = {
                new string[] { "N=K^T*M"  , "K=(k_{ij})", "M=(m_{ij})", "n_{uv}", "nuv" },
                new string[] { "N=P*Q^T"  , "P=(p_{ij})", "Q=(q_{ij})", "n_{ab}", "nab" },
                new string[] { "N=M*K"    , "M=(m_{ij})", "K=(k_{ij})", "n_{pq}", "npq" },
                new string[] { "N=P^T*Q^T", "P=(p_{ij})", "Q=(q_{ij})", "n_{cd}", "ncd" },
                new string[] { "N=V^T*U"  , "U=(u_{ij})", "V=(v_{ij})", "n_{gh}", "ngh" },
                new string[] { "N=M^T*K"  , "K=(k_{ij})", "M=(m_{ij})", "n_{pq}", "npq" },
                new string[] { "N=Y^T*X^T", "X=(x_{ij})", "Y=(y_{ij})", "n_{ab}", "nab" },
                new string[] { "N=B*A"    , "A=(a_{ij})", "B=(b_{ij})", "n_{uv}", "nuv" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang004();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(8);

            result.Text = 
                $"Представьте формулу для элемента \\({data.text[num][3]}\\)  матрицы \\({data.text[num][0]}\\) в виде результата" +
                $" применения матричных операций к матрицами " +
                $"\\({data.text[num][1]},{data.text[num][2]}\\) (элемент \\({data.text[num][3]}\\) запишите как \\({data.text[num][4]}\\) и т.п., "+
                $"не пропускайте знак * операции умножения, скобки не используйте, не переставляйте слагаемые, " +
                $" первый множитель относится к первому индексу, второй - ко второму), " +
                $" \\({data.text[num][3]}=\\)\\(<ans:20>\\)\r\n";
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
