using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class ComplexDivisionTest : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Complex Numbers";
        public string Name { get; } = "Деление комплексных чисел как многочленов от i";
        public string Description { get; } = "Деление комплексных чисел как многочленов от i";

        public class Data
        {
            public int Ax, Ay, Bx, By;
            public int Cx, Cy;

            public Data(Random random)
            {
                GenerateNumbers(random);
                CalculateResults();
            }

            private void GenerateNumbers(Random random)
            {
                var numbers = new List<int>();
                for (int i = -10; i <= 10; i++)
                {
                    if (i != 0) numbers.Add(i);
                }

                // Гарантируем уникальность всех значений
                Ax = numbers[random.Next(numbers.Count)];
                numbers.Remove(Ax);

                Ay = numbers[random.Next(numbers.Count)];
                numbers.Remove(Ay);

                Bx = numbers[random.Next(numbers.Count)];
                numbers.Remove(Bx);

                By = numbers[random.Next(numbers.Count)];
            }

            private void CalculateResults()
            {
                // Вычисление C = A * B (по условию задачи)
                Cx = Ax * Bx - Ay * By;
                Cy = Ax * By + Ay * Bx;
            }
        }

        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            string signAy = data.Ay >= 0 ? "+" : "";
            string signBy = data.By >= 0 ? "+" : "";

            ComplexDivisionTest result = new ComplexDivisionTest
            {
                Text = $"Решите уравнение: \\(\\frac{{{data.Cx} + {data.Cy}i}}{{{data.Ax} {signAy} {data.Ay}i}} = \\)" +
                       $"\\(\\boxed{{<ans[0]:3>}}\\)" +
                       $" + " +
                       $"\\(\\boxed{{<ans[1]:3>}}\\)" +
                       "$i$"
            };

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            if (answers.TryGetValue("ans[0]", out string ans0) && ans0 == data.Bx.ToString())
            {
                total++;
            }
            if (answers.TryGetValue("ans[1]", out string ans1) && ans1 == data.By.ToString())
            {
                total++;
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; } = new List<Image>();
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}