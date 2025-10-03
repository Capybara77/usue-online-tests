using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using Test_Wrapper;

namespace usue_online_tests.Tests.PolynomA
{
    public class MultiPoly14 : ITestCreator, ITest, ITestGroup, ITimeLimit
    {
        public int TestID { get; set; }
        public string Name { get; } = "Умножение многочленов";
        public string Description { get; } = "Выделение полного квадрата02";
        public string GroupName { get; set; } = "PolynomA";
        public int TimeLimitSeconds { get; set; } = 120;
        private readonly char[] Letters = {'p', 'q', 'r', 'x', 'y', 'z', 't', 'u', 'v', 'w'};
        public ITest CreateTest(int randomSeed)
        {
            var random = new Random(randomSeed);

            int[] answ = RandNum(random);
            //0 Ax, 1 Ay, 2 Az, 3 Bx, 4 By, 5 Cx, 6 Cy
            while (answ[0] == 0 || answ[5] == 0 || answ[6] == 0)
            {
                answ = RandNum(random);
            }
            int Ax = answ[0];
            int Ay = answ[1];
            int Az = answ[2];
            int Bx = answ[3];
            int By = answ[4];
            int Cx = answ[5];
            int Cy = answ[6];

            char letterA = Letters[random.Next(0, Letters.Length)];

            

            ITest result = new MultiPoly14();
            result.Text =
                    $"\\({Ax} \\cdot {letterA}^{{2}} + {Ay} \\cdot {letterA} + {Az} = " +
                    $"<Ax:5> \\left({letterA} + \\frac{{<Bx:5>}}{{<Cx:5>}}\\right)^{{2}} + \\frac{{<By:5>}}{{<Cy:5>}}\\)";
                       //+ $"\\( {Ax} \\frac{{{Bx}}}{{{Cx}}} \\frac{{{By}}}{{{Cy}}}\\)";             //Добавить для отладки
            result.Text = result.Text.Replace("+ -", "-");
            result.Text = result.Text.Replace("- -", "+");
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);

            int[] answ = RandNum(random);

            while (answ[0] == 0 || answ[5] == 0 || answ[6] == 0)
            {
                answ = RandNum(random);
            }
            int Ax = answ[0];
            int Bx = answ[3];
            int By = answ[4];
            int Cx = answ[5];
            int Cy = answ[6];

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
            if (answers.TryGetValue("Cx", out var userAnswer3) &&
                int.TryParse(userAnswer3, out var userNumber3) &&
                userNumber3 == Cx)
            {
                total++;
            }
            if (answers.TryGetValue("Cy", out var userAnswer4) &&
                int.TryParse(userAnswer4, out var userNumber4) &&
                userNumber4 == Cy)
            {
                total++;
            }

            return total;
        }

        private static int Nod(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        private static int[] RandNum(Random random)
        {
            int[] values = { -9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] CxValues = { 2, 3, 5 };

            List<int> listIndex = [.. values];
            int Ax = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ax);
            int Az = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Az);
            int Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            int Cx = CxValues[random.Next(0, 3)];

            Bx = Bx / Nod(Bx, Cx);
            Cx = Cx / Nod(Bx, Cx);
            Ax = Ax * Cx * Cx;

            int Ay = 2 * Ax * Bx / Cx;
            int Cy = Cx * Cx;
            int By = Az * Cy - Ax * Bx;

            int[] final = { Ax, Ay, Az, Bx, By, Cx, Cy};
            return final;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
