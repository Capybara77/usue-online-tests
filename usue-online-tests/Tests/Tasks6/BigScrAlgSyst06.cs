using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class AlgSyst06 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Значение выражения в гомоморфизме алг. сист. A на B";
        public string Description { get; } = "Алгебраические системы и поля Галуа";

        public class Data
        {
            public string[] arr { get; } = {
                "\\(𝒜 = \\langle\\{x, -x, |x|,-|x|\\}; \\{min,max\\}\\rangle\\) на \\(ℬ = \\langle\\{∅, \\{1\\}, \\{2\\}, \\{1; 2\\}\\}; \\{⋃, ⋂\\}\\rangle\\),\r\n" +
                "причем \\(min^𝜙 = ⋃, max^𝜙 = ⋂, x^𝜙 = \\{1\\}, (-x)^𝜙 = \\{2\\}\\).\r\n" +
                "Тогда \\(|x|^𝜙 = \\begin{matrix} min? \\\\ max? \\end{matrix} <ans0:3>\\{x; (-x)\\} = \\)",

                "\\(𝒜 = \\langle\\{0, 1, x,\\bar{x}\\}; \\{∨,∧\\}\\rangle\\), где \\(x ∈ \\{0; 1\\}\\) на \\(ℬ = \\langle\\{∅, \\{1\\}, \\{2\\}, \\{1; 2\\}\\}; \\{⋂, ⋃\\}\\rangle\\),\r\n" +
                "причем \\(∨^𝜙 = ⋂, ∧^𝜙 = ⋃, x^𝜙 = \\{1\\}, (\\bar{x})^𝜙 = \\{2\\}\\).\r\n" +
                "Тогда \\(1^𝜙 = x \\begin{matrix} ∨? \\\\ ∧? \\end{matrix} <ans0>\\bar{x} = \\)",

                "\\(𝒜 = \\langle\\{0, 1, x,\\bar{x}\\}; \\{∨,∧\\}\\rangle\\), где \\(x ∈ \\{0; 1\\}\\) на \\(ℬ = \\langle\\{∅, \\{1\\}, \\{2\\}, \\{1; 2\\}\\}; \\{⋂, ⋃\\}\\rangle\\),\r\n" +
                "причем \\(∨^𝜙 = ⋃, ∧^𝜙 = ⋂, x^𝜙 = \\{1\\}, (\\bar{x})^𝜙 = \\{2\\}\\).\r\n" +
                "Тогда \\(1^𝜙 = x \\begin{matrix} ∨? \\\\ ∧? \\end{matrix} <ans0>\\bar{x} = \\)",

                "\\(𝒜 = \\langle\\{x, -x, |x|,-|x|\\}; \\{min,max\\}\\rangle\\) на \\(ℬ = \\langle\\{∅, \\{1\\}, \\{2\\}, \\{1; 2\\}\\}; \\{⋃, ⋂\\}\\rangle\\),\r\n" +
                "причем \\(min^𝜙 = ⋂, max^𝜙 = ⋃, x^𝜙 = \\{1\\}, (-x)^𝜙 = \\{2\\}\\).\r\n" +
                "Тогда \\(|x|^𝜙 = \\begin{matrix} min? \\\\ max? \\end{matrix} <ans0:3>\\{x; (-x)\\} = \\)",
            };
            public string[][] ans { get; set; } = {
                new string[] { "max", "1", "⋂", "2", "∅"},
                new string[] { "∨", "1", "⋂", "2", "∅" },
                new string[] { "∨", "1", "⋃", "2", "1;2" },
                new string[] { "max", "1", "⋃", "2", "1;2" }
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new AlgSyst06();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"\\(𝜙\\) - гомоморфизм алгебраической системы {data.arr[num]} \\(\\{{<ans1>\\}} \\begin{{matrix}} ⋃? \\\\ ⋂? \\end{{matrix}} <ans2> \\{{<ans3>\\}} = {{<ans4:3>}}\\)\r\n" +
                          $"Элементы множества перечислять через точку с запятой, без пробелов.\r\n" +
                          $"Для ввода знаков \\(∨ , ∧ , ⋃ , ⋂ , ∅\\) копируйте их из этого списка.";

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
