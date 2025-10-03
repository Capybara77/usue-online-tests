using Newtonsoft.Json.Bson;
using OfficeOpenXml.Style.Dxf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using Test_Wrapper;
using static OfficeOpenXml.ExcelErrorValue;

namespace usue_online_tests.Tests.PolynomA
{
    public class MultiPoly15 : ITestCreator, ITest, ITestGroup, ITimeLimit
    {
        public int TestID { get; set; }
        public string Name { get; } = "Интерполяционный многочлен Лагранжа";
        public string Description { get; } = "Интерполяционный многочлен Лагранжа01";
        public string GroupName { get; set; } = "PolynomA";
        public int TimeLimitSeconds { get; set; } = 60;
        private readonly char[] LettersB = {'a', 'b', 'c', 'd', 'f', 'g', 'h', 'p', 'q', 'r', 'u', 'v', 'w'};
        private readonly char[] LettersA = { 'x', 'y', 'z', 's', 't' };
        public ITest CreateTest(int randomSeed)
        {
            var random = new Random(randomSeed);

            int[] answ = RandNum(random);
            
            int Cx = answ[4]; //1
            int Cy = answ[5]; //0
            while (answ[4] == 0 || answ[5] == 0)
            {
                answ = RandNum(random);
            }
            int Ax = answ[0];
            int Ay = answ[1];
            int Bx = answ[2];
            int By = answ[3];
            // 0 Ax, 1 Ay, 2 Bx, 3 By, 4 Cx, 5 Cy

            char letterA = LettersA[random.Next(0, LettersA.Length)];
            char letterB = LettersB[random.Next(0, LettersB.Length)];

            ITest result = new MultiPoly15();
            result.Text =
                    $"Если многочлен \\({letterB}\\left({letterA}\\right)\\) таков, " +
                    $"что \\({letterB} \\left({Ax} \\right)={Bx}\\), \\({letterB}\\left({Ay}\\right)={By}\\), " +
                    $"то (дроби не сокращайте!)\n" +
                    $" \\({letterB} \\left( {letterA} \\right)" + " \\)=" +
                    $"\\(\\frac{{<Bx:5>}}{{<Cx:5>}}\\left({letterA} - {Ay}\\right)\\)+" +
                    $"\\(\\frac{{<By:5>}}{{<Cy:5>}}\\left({letterA} - {Ax}\\right)\\)";
            //+ $"\\(\\frac{{{Bx}}}{{{Cx}}} \\frac{{{By}}}{{{Cy}}}\\)";             //Добавить для отладки
            result.Text = result.Text.Replace("+ -", "-");
            result.Text = result.Text.Replace("- -", "+");
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);

            int[] answ = RandNum(random);

            int Cx = answ[4];
            int Cy = answ[5];
            while (answ[4] == 0 || answ[5] == 0)
            {
                answ = RandNum(random);
            }
            int Bx = answ[2];
            int By = answ[3];
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
            if (answers.TryGetValue("Cx", out var userAnswer2) &&
                int.TryParse(userAnswer2, out var userNumber2) &&
                userNumber2 == Cx)
            {
                total++;
            }
            if (answers.TryGetValue("Cy", out var userAnswer3) &&
                int.TryParse(userAnswer3, out var userNumber3) &&
                userNumber3 == Cy)
            {
                total++;
            }

            return total;
        }

        private static int[] RandNum(Random random)
        {
            int[] d1Value = { -5, -4, -3, -2, -1, 1, 2, 3, 4, 5 };
            List<int> dValue = [.. d1Value];
            int Dx = dValue[random.Next(0, dValue.Count)];
            dValue.Remove(Dx);
            int Dy = dValue[random.Next(0, dValue.Count)];
            dValue.Remove(Dy);

            int[] _aValue = { -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6 };
            List<int> aValue = [.. _aValue];
            int Ax = aValue[random.Next(0, aValue.Count - 1)];
            int Ay = aValue[random.Next(aValue.IndexOf(Ax) + 1, aValue.Count)];

            int Bx = Dx + Dy * Ax;
            int By = Dx + Dy * Ay;
            int Cx = Ax - Ay;
            int Cy = Ay - Ax;

            int[] final = { Ax, Ay, Bx, By, Cx, Cy };
            return final;
        }
        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
