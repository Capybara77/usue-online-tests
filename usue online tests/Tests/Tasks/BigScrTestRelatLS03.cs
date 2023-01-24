using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Однозначность разложения";
        public string Description { get; } = "Однозначность разложения";

        public class Data
        {
            public string answer { get; set; } = "1212";
            public string[] textstr { get; set; } = {
                "𝑥 по {𝑢, 𝑣,𝑤}",
                "𝑠 по {𝑝, 𝑞, 𝑟}",
                "𝑠 по {𝑝, 𝑞, 𝑟}",
                "𝑥 по {𝑘, 𝑚, 𝑛}" };
            public string[][] koef { get; set; } = {
            new[] { "\\text{{𝑥=𝛼𝑢+𝛽𝑣+𝛾𝑤,}} \\\\ \\\\ \\text{{𝑥=𝜆𝑢+𝜇𝑣+𝜈𝑤}}",
                "\\text{𝛼 = 𝜆}, \\\\ \\\\ \\text{𝛽 = 𝜇}, \\\\ \\\\ \\text{𝛾 = 𝜈}." ,
                "\\text{𝑥=𝛼𝑢+𝛽𝑣+𝛾𝑤}, \\\\ \\\\ \\text{𝑥=𝜆𝑢+𝜇𝑣+𝜈𝑤}" ,
                "\\left[ \\begin{gathered} \\text{𝛼 = 𝜆}, \\\\ \\\\ \\text{𝛽 = 𝜇}, \\\\ \\\\ \\text{𝛾 = 𝜈}. \\end{gathered} \\right."},
            new[] { "\\text{{𝑠=𝛼𝑝+𝛽𝑞+𝛾𝑟,}} \\\\ \\\\ \\text{{𝑠=𝜆𝑝+𝜇𝑞+𝜈𝑟}}",
                "\\text{𝛼 = 𝛽 = 𝛾}, \\\\ \\\\ \\text{𝜆 = 𝜇 = 𝜈}." ,
                "\\text{𝑠=𝛼𝑝+𝛽𝑞+𝛾𝑟}, \\\\ \\\\ \\text{𝑠=𝜆𝑝+𝜇𝑞+𝜈𝑟}" ,
                "\\begin{cases} \\text{𝛼 = 𝜆}, \\\\ \\\\ \\text{𝛽 = 𝜇}, \\\\ \\\\ \\text{𝛾 = 𝜈}. \\end{cases}"},
            new[] { "\\text{{𝑠=𝛼𝑝+𝛽𝑞+𝛾𝑟,}} \\\\ \\\\ \\text{{𝑠=𝜆𝑝+𝜇𝑞+𝜈𝑟}}",
                "\\text{𝛼 = 𝜆}, \\\\ \\\\ \\text{𝛽 = 𝜇}, \\\\ \\\\ \\text{𝛾 = 𝜈}." ,
                "\\text{𝑠=𝛼𝑝+𝛽𝑞+𝛾𝑟}, \\\\ \\\\ \\text{𝑠=𝜆𝑝+𝜇𝑞+𝜈𝑟}" ,
                "\\text{p = q = r.}"},
            new[] { "\\text{{𝑥=𝛼𝑘+𝛽𝑚+𝛾𝑛,}} \\\\ \\\\ \\text{{𝑥=𝜆𝑘+𝜇𝑚+𝜈𝑛}}",
                "\\text{𝑘 = 𝑚}, \\\\ \\\\ \\text{𝑚 = 𝑛}, \\\\ \\\\ \\text{𝑘 = 𝑛}." ,
                "\\text{𝑥=𝛼𝑘+𝛽𝑚+𝛾𝑛}, \\\\ \\\\ \\text{𝑥=𝜆𝑘+𝜇𝑚+𝜈𝑛}" ,
                "\\begin{cases} \\text{𝛼 = 𝜆}, \\\\ \\\\ \\text{𝛽 = 𝜇}, \\\\ \\\\ \\text{𝛾 = 𝜈}. \\end{cases}"},
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS03();

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(4);

            result.Text = $"Однозначность разложения {data.textstr[num]} — утверждение под номером \\(<ans>\\): \r\n";

            result.Text += $"\\(1) \\begin{{cases}}" +
                    data.koef[num][0] +
                    $"\\end{{cases}}" +
            $" ⇒ \\begin{{cases}}" +
            data.koef[num][1] +
                    $"\\end{{cases}} \\) \r\n" +
            $"\\(2) \\begin{{cases}}" +
                    data.koef[num][2] +
                    $"\\end{{cases}} \\)" +
            $"\\( ⇒ {data.koef[num][3]} \\)";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            Data data = new Data();

            try
            {
                if (answers.ContainsKey("ans")) return (data.answer[random.Next(4)] == Convert.ToChar(answers["ans"])) ? 1 : 0;
            }
            catch
            {
                // ignored
            }
            return 0;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
    }
}
