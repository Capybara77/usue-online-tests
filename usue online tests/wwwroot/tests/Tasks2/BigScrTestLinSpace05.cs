using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace05 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Линейные пространства";
        public string Description { get; } = "Теорема о внутренней характеризации линейной оболочки";

        public class Data
        {
            public string[] arr { get; set; } = { "\\(\\left\\langle u, v, w \\right\\rangle\\)",
                "\\(\\left\\langle p_1, p_2, p_3 \\right\\rangle\\)" };
            public string[][] ans { get; set; } = {
                new string[] { "au", "bv", "cw", "K"},
                new string[] { "ap1", "bp2", "cp3", "K"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace05();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"В силу теоремы о внутренней характеризации линейной оболочки системы векторов линейного пространства над полем \\(K\\) имеем {data.arr[num]} = {{...}}.\r\n" +
                $" \\(\\left\\{{<ans0> + <ans1> + <ans2> \\middle|\\ \\left\\{{ a, b, c \\right\\}} ⊆ <ans3> \\right\\}}\\) \r\n" +
                $"Формула имеет вид «коэффициент*вектор». Индексы пишутся сразу после переменной.";

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
