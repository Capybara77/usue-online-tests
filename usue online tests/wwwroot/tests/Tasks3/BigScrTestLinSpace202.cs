using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace202 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора v с опред. коорд. в базисе B";
        public string Description { get; } = "Линейные пространства 2";

        public class Data
        {
            public string[] arr { get;} = { "\\(B = \\left\\{ t^2, x^2, y^2, z^2 \\right\\}\\) вектор \\(v\\) имеет координаты \\(\\left(-1; 2; 0; 1\\right)\\).",
                "\\(B = \\left\\{ x^2, xy, yx, y^2 \\right\\}\\) (переменные \\(x, y\\) не коммутируют) вектор \\(v\\) имеет координаты \\(\\left(0; 1; 5; -3\\right)\\)" };
            public string[] ans { get;} = { "-t^2+2x^2+z^2",
                                            "xy+5yx-3y^2"};
        }   
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace202();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"В базисе {data.arr[num]} \r\n  Тогда \\(v = <ans0>\\) \r\n" +
                $"Решение не должно иметь пробелов. Для степени использовать \"^\". Пример решения: 4+a^2-ba+c^3-e^6f-f";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(2);
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
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = true;
    }
}
