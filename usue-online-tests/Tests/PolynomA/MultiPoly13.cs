using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Test_Wrapper;

namespace usue_online_tests.Tests.PolynomA
{
    public class MultiPoly13 : ITestCreator, ITest, ITestGroup
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
            int Ax = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ax);
            int Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            int By = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(By);                                        

            int Ay = 2 * Bx;                                               
            int Az = Bx * Bx + By;                                        
            char letterA = Letters[random.Next(0, Letters.Length)]; 


            ITest result = new MultiPoly13();
            result.Text =
                $"\\({Ax} \\cdot {letterA}^{{2}}+{Ay} \\cdot {letterA}+{Az}=" +
                $"<Ax:5>\\left({letterA}+<Bx>\\right)^{{2}}+<By:5>\\)";
            result.Text = result.Text.Replace("+ -", "-");
            result.Text = result.Text.Replace("- -", "+");
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);
            int[] values = {-9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            List<int> listIndex = [.. values];
            int Ax = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ax);
            int Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            int By = listIndex[random.Next(0, listIndex.Count)];
            //listIndex.Remove(By);    

            var letterA = Letters[random.Next(0, Letters.Length)];

            var total = 0;

            if (answers.TryGetValue("Ax", out var userAnswer0) &&
                int.TryParse(userAnswer0, out var userNumber0) &&
                userNumber0 == Ax)
            {
                total++;
            }
            if (answers.TryGetValue("Bx", out var userAnswer1) &&
                int.TryParse(userAnswer1, out var userNumber1) &&
                userNumber1 == Bx)
            {
                total++;
            }
            if (answers.TryGetValue("By", out var userAnswer2) &&
                int.TryParse(userAnswer2, out var userNumber2) &&
                userNumber2 == By)
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
