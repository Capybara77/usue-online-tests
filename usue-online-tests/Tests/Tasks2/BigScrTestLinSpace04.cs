using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Линейные пространства";
        public string Description { get; } = "Разложение вектора по координатам в базисе";

        public class Data
        {
            public string[] arr { get; set; } = { "Если \\((a, b, c, d)\\) - координаты вектора \\(p\\) в базисе \\(Б = \\left\\{u_1, u_2, u_3, u_4\\right\\}\\), то \\(p = \\)\r\n\\( p",
                "Если \\((x_1, x_2, x_3, x_4)\\) - координаты вектора \\(u\\) в базисе \\(Б = \\left\\{p, q, r, t\\right\\}\\), то \\(u = \\)\r\n\\( u" };
            public string[][] ans { get; set; } = {
                new string[] { "au1", "bu2", "cu3", "du4"},
                new string[] { "x1p", "x2q", "x3r", "x4t"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace04();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"{data.arr[num]} = <ans0> + <ans1> + <ans2> + <ans3>\\) \r\n" +
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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
