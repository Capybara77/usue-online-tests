using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace07 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора v с опред. коорд. в базисе B";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            public string[] arr { get; } = { 
                "\\(B = \\left\\{ t^2, x^2, y^2, z^2 \\right\\}\\) вектор \\(v\\) имеет координаты \\(\\left(-1; 3; 0; 1\\right)\\).",
                "\\(Q = \\left\\{ x^2, xy, yx, y^2 \\right\\}\\) (переменные \\(x, y\\) не коммутируют) вектор \\(v\\) имеет координаты \\(\\left(0; 1; 5; -3\\right)\\)",
                "\\(P = \\left\\{ x^0, x, x^2, x^3 \\right\\}\\) вектор \\(v\\) имеет координаты \\(\\left(2; 0; 4; -1\\right)\\).",
                "\\(T = \\left\\{ x^3, x^2y, xy^2, y^3 \\right\\}\\) вектор \\(v\\) имеет координаты \\(\\left(3; 0; -2; 5\\right)\\)."
            };
            public string[] ans { get; } = { "-t^2+3x^2+z^2",
                                            "xy+5yx-3y^2",
                                            "2+4x^2-1x^3",
                                            "3x^3-2xy^2+5y^3"
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace07();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"В базисе {data.arr[num]} \r\n  Тогда \\(v = <ans0:20>\\) \r\n" +
                $"Решение не имеет пробелов и знаков умножения, степень пишется знаком \" ^ \". \r\n" +
                $"Пример решения: (4+a^2-ab+ba+5c^3-e^6f-f)";

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
        public int TimeLimitSeconds { get; set; } = 90;
        public bool IsHidden { get; set; } = true;
    }
}
