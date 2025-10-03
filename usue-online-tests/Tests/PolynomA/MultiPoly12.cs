using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Test_Wrapper;

namespace usue_online_tests.Tests.PolynomA
{
    public class MultiPoly12 : ITestCreator, ITest, ITestGroup
    {
        public int TestID { get; set; }
        public string Name { get; } = "Умножение многочленов";
        public string Description { get; } = "Выделение полного квадрата01";
        public string GroupName { get; set; } = "PolynomA";
        private readonly char[] Letters = {'p', 'q', 'r', 'x', 'y', 'z', 't', 'u', 'v', 'w'};
        public ITest CreateTest(int randomSeed)
        {
            var random = new Random(randomSeed);

            int[] values = {-9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            List<int> listIndex = [.. values];
            int Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            int By = listIndex[random.Next(0, listIndex.Count)];
            //listIndex.Remove(By);                                         Ненужное действие
            //int Ay = 2 * Bx                                               заменено формулой
            //int Az = Bx * Bx + By;                                        заменено формулой
            var letterA = Letters[random.Next(0, Letters.Length)]; 


            ITest result = new MultiPoly12();
            result.Text =
                $"\\({letterA}^{{2}}+{2*Bx} \\cdot {letterA}+{Bx * Bx + By}=" +
                $"\\left({letterA}+<Bx:5>\\right)^{{2}}+<By:5>\\)";
            result.Text = result.Text.Replace("+ -", "-");
            result.Text = result.Text.Replace("- -", "+");
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);
            int[] values = { -9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<int> listIndex = [.. values];
            int Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            int By = listIndex[random.Next(0, listIndex.Count)];
            //listIndex.Remove(By);                                     Ненужное действие
            //int Ay = 2 * Bx;
            //int Az = Bx * Bx + By;

            var total = 0;

            if (answers.TryGetValue("Bx", out var userAnswer0) &&
                int.TryParse(userAnswer0, out var userNumber0) &&
                userNumber0 == Bx)
            {
                total++;
            }

            if (answers.TryGetValue("By", out var userAnswer1) &&
                int.TryParse(userAnswer1, out var userNumber1) &&
                userNumber1 == By)
            {
                total++;
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
