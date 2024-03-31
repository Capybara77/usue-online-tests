using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class BigScrTestMatrixLang003 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Перевод с языка элементов на язык матричных операций в целом: произведение 01";
        public string Description { get; } = "Матричные операции";

        public class Data
        {
            
            public string[][] ans { get; set; } = {
                new string[] { "K^T*M"  },
                new string[] { "P*Q^T"  },
                new string[] { "M*K"    },
                new string[] { "P^T*Q^T"},
                new string[] { "V^T*U"  },
                new string[] { "M*K^T"  },
                new string[] { "Y^T*X^T"},
                new string[] { "B^T*A"  }
             };
            public string[][] text { get; set; } = {
                new string[] { "n_{uv}=\\sum\\limits_{w=1}^{3} k_{wu}m_{wv}", "K=(k_{ij})", "M=(m_{ij})", "N"},
                new string[] { "n_{ab}=\\sum\\limits_{c=1}^{3} p_{ac}q_{bc}", "P=(p_{ij})", "Q=(q_{ij})", "N"},
                new string[] { "n_{pq}=\\sum\\limits_{r=1}^{3} m_{pr}k_{rq}", "M=(m_{ij})", "K=(k_{ij})", "N"},
                new string[] { "n_{cd}=\\sum\\limits_{e=1}^{3} p_{ec}q_{de}", "P=(p_{ij})", "Q=(q_{ij})", "N"},
                new string[] { "n_{fg}=\\sum\\limits_{h=1}^{3} u_{hg}v_{hf}", "U=(u_{ij})", "V=(v_{ij})", "N"},
                new string[] { "n_{pq}=\\sum\\limits_{r=1}^{3} k_{qr}m_{pr}", "K=(k_{ij})", "M=(m_{ij})", "N"},
                new string[] { "n_{ab}=\\sum\\limits_{c=1}^{3} x_{bc}y_{ca}", "X=(x_{ij})", "Y=(y_{ij})", "N"},
                new string[] { "n_{uv}=\\sum\\limits_{w=1}^{3} a_{wv}b_{wu}", "A=(a_{ij})", "B=(b_{ij})", "N"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new BigScrTestMatrixLang003();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(8);

            result.Text = 
                $"Представьте матрицу \\({data.text[num][3]}\\) с элементами \\({data.text[num][0]}\\) в виде результата применения матричных операций к матрицами " +
                $"\\({data.text[num][1]},{data.text[num][2]}\\) (результат транспонирования матрицы \\(Z\\) введите как \\(Z^T\\), скобки не используйте, " +
                $"произведение матрицы \\(Z\\), например, на \\(S\\) записывается как \\(Z*S\\)):\r\n " +
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
