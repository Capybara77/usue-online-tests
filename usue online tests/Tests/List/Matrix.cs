using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class Matrix : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Умножение матриц на макроуровне";
        public string Description { get; } = "По строчкам и столбцам";
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            Matrix result = new Matrix();

            result.Text = $"Заполните поля для ввода, подбирая значения с помощью «умножение на макроуровне» (по строчкам и столбцам):" +
                          $"\\({random.Next(-5, 5)} * \\begin{{pmatrix}}{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\end{{pmatrix}} + " +
                          $"{random.Next(-5, 5)} * \\begin{{pmatrix}}{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\end{{pmatrix}} + " +
                          $"{random.Next(-5, 5)} * \\begin{{pmatrix}}{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\\\{random.Next(-5, 5)}\\end{{pmatrix}} = " +
                          $"\\begin{{pmatrix}} <i1> <i2> <i3> \\\\ <i4> <i5> <i6> \\\\ <i7> <i8> <i9> \\end{{pmatrix}} *" +
                          $" \\begin{{pmatrix}} <j1> \\\\ <j2> \\\\ <j3> \\end{{pmatrix}} \\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            int[] ans = new int[12];
            int[] ids = { 2, 6, 10, 3, 7, 11, 4, 8, 12, 1, 5, 9 };
            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = random.Next(-5, 5);
            }

            int total = 0;

            for (int i = 1; i < 10; i++)
            {
                try
                {
                    if (ans[ids[i - 1] - 1] == Convert.ToInt32(answers["i" + i])) total++;
                }
                catch
                {
                    // ignored
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (ans[ids[i + 9] - 1] == Convert.ToInt32(answers["j" + (i + 1)])) total++;
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
    }
}
