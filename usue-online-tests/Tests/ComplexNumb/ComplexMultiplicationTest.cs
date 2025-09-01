using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class ComplexMultiplicationTest : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Complex Numbers";
        public string Name { get; } = "Умножение комплексных чисел как многочленов от i";
        public string Description { get; } = "Умножение комплексных чисел как многочленов от i";

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
                for (int i = -15; i <= 15; i++)
                {
                    if (i != 0) numbers.Add(i);
                }

                Ax = numbers[random.Next(numbers.Count)];
                Ay = numbers[random.Next(numbers.Count)];
                Bx = numbers[random.Next(numbers.Count)];
                By = numbers[random.Next(numbers.Count)];
            }

            private void CalculateResults()
            {
                // Умножение комплексных чисел (Ax + Ay*i) * (Bx + By*i)
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

            ComplexMultiplicationTest result = new ComplexMultiplicationTest
            {
                // Включаем LaTeX в результат
                Text = $"Вычислите произведение комплексных чисел: $({data.Ax} {signAy} {data.Ay}i) \\cdot ({data.Bx} {signBy} {data.By}i)=$" +
                       $"\\(\\begin{{array}}{{r}}  <ans[0]:3> \\end{{array}}\\)" +
                       $"$+$"+
                       $"\\(\\begin{{array}}{{r}}  <ans[1]:3> \\end{{array}}\\)"+
                       $"$i$"
            };

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            Random random = new Random(randomSeed);
            Data data = new Data(random);

            // Проверка ответов на соответствие действительной и мнимой части
            if (answers.TryGetValue("ans[0]", out string ans0) && ans0 == data.Cx.ToString())
            {
                total++;
            }
            if (answers.TryGetValue("ans[1]", out string ans1) && ans1 == data.Cy.ToString())
            {
                total++;
            }

            return total; // Возвращаем количество правильных ответов
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }

        public List<MemoryStream> Pictures { get; set; }

        //public List<Image> Pictures { get; set; } = new List<Image>();
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = false;
    }
}
