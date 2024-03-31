using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS08 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Соответствие отношения формуле";
        public string Description { get; } = "Соответствие отношения формуле";

        public class Data
        {
            public string[] arr { get; set; } = { "𝑎^2 − |𝑎 · 𝑏| = 1", "𝑎^2𝑏^2 = 0", "𝑎^2 + 𝑏^2 − |𝑎 · 𝑏| = 0", "𝑏^2 − |𝑎 · 𝑏| = 1" };
            public string[] ans { get; set; } = { "{(−1;0);(1;0)}", "{(−1;0);(0;−1);(0;0);(0;1);(1;0)}", "{(0;0)}", "{(0;−1);(0;1)}" };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS08();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            int a = new Random(randomSeed).Next(10, 20);
            result.Text = $"Пусть {{𝑎; 𝑏}} ⊆ {{−1; 0; 1}}. Формуле \\({data.arr[num]}\\) соответствует отношение: \\(<rel>\\) \r\n Ответы в ячейках необходимо писать без пробелов. Пример решения: {{(0;0),(0;1)}}";

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
                if (answers["rel"] == ans) total++;
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
        public int TimeLimitSeconds { get; set; } = 100;
    }
}
