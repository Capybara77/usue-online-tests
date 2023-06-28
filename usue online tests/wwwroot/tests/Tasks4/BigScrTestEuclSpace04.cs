using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Определение коэффициента Грама в базисе B";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            public string[][] arr { get; set; } = {
                new string[] { " a_{ij}", " в базисе \\(B = \\left\\{\\ b_1, b_2, b_3, b_4 \\right\\}\\)." },
                new string[] { " b_{ij}", " в базисе \\(B = \\left\\{\\ p_1, p_2, p_3, p_4 \\right\\}\\)." },
                new string[] { " s_{ij}", " в базисе \\(B = \\left\\{\\ a_1, a_2, a_3 \\right\\}\\)." },
                new string[] { " p_{ij}", " в базисе \\(B = \\left\\{\\ q_1, q_2, q_3 \\right\\}\\)." }
            };
            public string[][] ans { get; set; } = {
                new string[] { "bi", "bj"},
                new string[] { "pi", "pj"},
                new string[] { "ai", "aj"},
                new string[] { "qi", "qj"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace04();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Коэффициенты матрицы Грама  \\(\\left({data.arr[num][0]}\\right)\\) {data.arr[num][1]} \r\n" +
                $"\\(  {data.arr[num][0]}  = \\left( <ans0>, <ans1> \\right)\\)\r\n" +
                $"Индексы пишутся сразу после переменной, без дополнительных знаков. Например, чтобы отправить \\(h_i\\) необходимо ввести \" hi \".";

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
