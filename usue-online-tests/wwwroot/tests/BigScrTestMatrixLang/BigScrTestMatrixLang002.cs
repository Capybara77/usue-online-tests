using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang002 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Перевод с языка матричных операций в целом на язык элементов: сумма 01";
        public string Description { get; } = "Матричные операции";

        public class Data
        {            
            public string[][] ans { get; set; } = {
                new string[] { "4*kuv-5*mvu" },
                new string[] { "4*pab+2*qba" },
                new string[] { "4*mqp-7*kpq" },
                new string[] { "4*pdc-3*qdc" },
                new string[] { "4*ugh+3*vhg" },
                new string[] { "4*kpq+ *mqp" },
                new string[] { "4*xba-2*yab" },
                new string[] { "4*aqp+3*bqp" }
             };
            public string[][] text { get; set; } = {
                new string[] { "N=4*K-5*M^T", "K=(k_{ij})", "M=(m_{ij})", "n_{uv}", "nuv" },
                new string[] { "N=4*P+2*Q^T"  , "P=(p_{ij})", "Q=(q_{ij})", "n_{ab}", "nab" },
                new string[] { "N=4*M^T-7*K"  , "M=(m_{ij})", "K=(k_{ij})", "n_{pq}", "npq" },
                new string[] { "N=4*P^T-3*Q^T", "P=(p_{ij})", "Q=(q_{ij})", "n_{cd}", "ncd" },
                new string[] { "N=4*U+3*V^T"  , "U=(u_{ij})", "V=(v_{ij})", "n_{gh}", "ngh" },
                new string[] { "N=4*K+M^T"    , "K=(k_{ij})", "M=(m_{ij})", "n_{pq}", "npq" },
                new string[] { "N=4*X^T-2*Y"  , "X=(x_{ij})", "Y=(y_{ij})", "n_{ab}", "nab" },
                new string[] { "N=4*A^T+3*B^T", "A=(a_{ij})", "B=(b_{ij})", "n_{pq}", "npq" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang002();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(8);

            result.Text = 
                $"Представьте формулу для элемента \\({data.text[num][3]}\\)  матрицы \\({data.text[num][0]}\\) в виде результата применения матричных операций к матрицами " +
                $"\\({data.text[num][1]},{data.text[num][2]}\\) (элемент \\({data.text[num][3]}\\) запишите как \\({data.text[num][4]}\\) и т.п., "+
                $" не пропускайте знак * операции умножения, скобки не используйте, не переставляйте множители и слагаемые), " +
                $" \\({data.text[num][3]}=\\)\\(<ans:20>\\)\r\n"; //+
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
