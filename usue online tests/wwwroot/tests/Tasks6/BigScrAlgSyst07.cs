using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst07 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Выбор верного утверждения для конгруэнции";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "P", "Q", "S", "T",
            };
            public string[][] text { get; set; } = {
                new string[] { "\\text{x ∈ P,} \\\\ \\text{y ∈ P}", "x + y ∈ P;",
                               "\\text{(x, y) ∈ P,} \\\\ \\text{(a, b) ∈ P}", "(x + a, y + b) ∈ P;",
                               "\\text{(x, y) ∈ P,} \\\\ \\text{(a, b) ∈ P}", "(x + y, a + b) ∈ P.",
                             },
                new string[] { "\\text{(x, y) ∈ Q,} \\\\ \\text{(a, b) ∈ Q}", "(x ⋂ y, a ⋂ b) ∈ Q;",
                               "\\text{(x, y) ∈ Q,} \\\\ \\text{(a, b) ∈ Q}", "(x ⋂ a, y ⋂ b) ∈ Q;",
                               "\\text{x ∈ Q,} \\\\ \\text{y ∈ Q}", "x ⋂ y ∈ Q."
                             },
                new string[] { "\\text{x ∈ S,} \\\\ \\text{y ∈ S}", "x * y ∈ S;",
                               "\\text{(x, y) ∈ S,} \\\\ \\text{(a, b) ∈ S}", "(x * y, a * b) ∈ S;",
                               "\\text{(x, y) ∈ S,} \\\\ \\text{(a, b) ∈ S}", "(x * a, y * b) ∈ S.",
                             },
                new string[] { "\\text{(x, y) ∈ T,} \\\\ \\text{(a, b) ∈ T}", "(max\\{x, a\\}, max\\{y, b\\}) ∈ T;",
                               "\\text{(x, y) ∈ T,} \\\\ \\text{(a, b) ∈ T}", "(max\\{x, y\\}, max\\{a, b\\}) ∈ T;",
                               "\\text{x ∈ T,} \\\\ \\text{y ∈ T}", "max\\{x, y\\} ∈ T."
                             }
            };
            public string[] ans { get; } = {
                "2", "2", "3", "1"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst07();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Пусть \\({data.arr[num]}\\) - конгруэнция.\r\n" +
                          $"Номер верного утверждения:\r\n" +
                          $"\\(1) \\begin{{cases}} " + data.text[num][0] + $"\\end{{cases}} ⇒ " + data.text[num][1] + $"\\)\r\n" +
                          $"\\(2) \\begin{{cases}} " + data.text[num][2] + $"\\end{{cases}} ⇒ " + data.text[num][3] + $"\\)\r\n" +
                          $"\\(3) \\begin{{cases}} " + data.text[num][4] + $"\\end{{cases}} ⇒ " + data.text[num][5] + $"\\)\r\n" +
                          $"Ответ: \\(<ans0>\\)";

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
