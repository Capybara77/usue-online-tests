using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst01 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Значение выражения в поле";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "\\(X\\) - поле характеристики \\(3, a∈X\\),\r\n то \\(7a =\\)",
                "\\(T\\) - поле характеристики \\(5, x∈T\\),\r\n то \\(7x =\\)",
                "\\(U\\) - поле характеристики \\(2, x∈U\\),\r\n то \\(x + x + x + x + x =\\)",
                "\\(T\\) - поле характеристики \\(3, x∈T\\),\r\n то \\(x + x + x + x + x =\\)",
            };
            public string[] ans { get; } = {
                "a", "2x", "x", "2x"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst01();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Если {data.arr[num]} \\(<ans0>\\)\r\n" +
                          $"Ответ пишется без знака умножения. Примеры ответов: \\(5a, 3x, a, x\\).";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(4);
            string ans = data.ans[num];
            try
            {
                if (answers["ans0"] == ans) total++;
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
