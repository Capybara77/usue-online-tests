using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace01 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Определение матрицы перехода из базиса в базис";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            public string[] arr { get; set; } = {
                "\\(Р_{U→V}\\) из базиса \\(U = \\left\\{u_1, u_2, u_3\\right\\}\\) в базис \\(V = \\left\\{v_1, v_2, v_3\\right\\}\\).",
                "\\(М_{N→Q}\\) из базиса \\(N = \\left\\{n_1, n_2, n_3, n_4\\right\\}\\) в базис \\(Q = \\left\\{q_1, q_2, q_3, q_4\\right\\}\\).",
                "\\(Q_{K→M}\\) из базиса \\(K = \\left\\{k_1, k_2, k_3, k_4\\right\\}\\) в базис \\(M = \\left\\{m_1, m_2, m_3, m_4\\right\\}\\).",
                "\\(K_{L→D}\\) из базиса \\(L = \\left\\{l_1, l_2, l_3\\right\\}\\) в базис \\(D = \\left\\{d_1, d_2, d_3\\right\\}\\)."
            };
            public string[][] ans { get; set; } = {
                new string[] { "vi", "j=1", "3", "pji", "uj" },
                new string[] { "qi", "j=1", "4", "mji", "nj" },
                new string[] { "mi", "j=1", "4", "qji", "kj" },
                new string[] { "di", "j=1", "3", "kji", "lj" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace01();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Запишите равенство из определения матрицы перехода {data.arr[num]} \r\n" +
                $"\\(<ans0>  = \\sum_{{<ans1>}}^{{<ans2>}}<ans3>*<ans4>\\)\r\n" +
                $"Индексы пишутся сразу после переменной. \r\n" +
                $"Пример решения: чтобы получить \\((a_i = \\sum_{{n=1}}^{{3}}b_{{cd}}h_e)\\) необходимо ввести \\((ai = \\sum_{{n=1}}^{{3}}bcd\\ \\ he)\\)";

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
                for (int i = 0; i < 5; i++)
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
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = true;
    }
}
