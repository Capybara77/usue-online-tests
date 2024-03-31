using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace01 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Линейные пространства";
        public string Description { get; } = "Определение матрицы перехода из базиса в базис";

        public class Data
        {
            public string[] arr { get; set; } = { "\\(Р_{Б→В}\\) из базиса \\(Б = {u_1, u_2, u_3}\\) в базис \\(В = {v_1, v_2, v_3}\\).",
                "\\(М_{Б→В}\\) из базиса \\(Б = {p_1, p_2, p_3, p_4}\\) в базис \\(В = {q_1, q_2, q_3, q_4}\\)." };
            public string[][] ans { get; set; } = {
                new string[] { "pi", "j=1", "3", "pji uj"},
                new string[] { "qi", "j=1", "4", "mji pj"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace01();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"Запишите равенство из определения матрицы перехода {data.arr[num]} \r\n" +
                $"\\(<ans0>  = \\sum_{{<ans1>}}^{{<ans2>}}<ans3>\\)\r\n" +
                $"Пробелы в ячейках ответов ставить только для резделения переменных. Индексы пишутся сразу после переменной. \r\n" +
                $"Пример решения: для \\((a_i = \\sum_{{n=1}}^{{3}}b_{{cd}}h_e)\\) нужно ввести \\((ai = \\sum_{{n=1}}^{{3}}bcd\\ \\ he)\\)";

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
                for (int i = 0; i < 4; i++)
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
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = true;
    }
}
