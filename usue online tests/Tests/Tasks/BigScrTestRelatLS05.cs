using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS05 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Соответствие предикат-функции предикат-высказыванию";
        public string Description { get; } = "Соответствие предикат-функции предикат-высказыванию";

        public class Data
        {
            public string[] predf { get; set; } = { "𝑡(𝑥; 𝑦) : 𝑀 ×𝑀 → {0; 1}", "ℎ(𝑚) : 𝑃 × 𝑃 → {0; 1}", "𝑠(𝑥; 𝑦; 𝑧) : 𝐿 × 𝐿 → {0; 1}", "𝑢(𝑎; 𝑏) : 𝐴 × 𝐴 → {0; 1}" };
            public string[] predv { get; set; } = { "𝑇", "𝐻", "𝑆", "𝑈" };
            public string[] ansf { get; set; } = { "t(x;y)", "h(m)", "s(x;y;z)", "u(a;b)" };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS05();
            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Предикату-функции {data.predf[num]} соответствует предикат-высказывание: {data.predv[num]} ~ \\(<f1> = <f2>\\). \r\nОтветы в ячейках необходимо писать без пробелов.";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);

            Data data = new Data();
            int num = random.Next(4);
            string ansf = data.ansf[num];
            try
            {
                if (answers["f1"] == ansf) total++;
                if (answers["f2"] == "1") total++;
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
