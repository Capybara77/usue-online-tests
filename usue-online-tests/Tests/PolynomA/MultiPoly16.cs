using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    public class MultiPoly16 : ITestCreator, ITest, ITestGroup, ITimeLimit
    {
        public int TestID { get; set; }
        public string Name { get; } = "Интерполяционный многочлен Лагранжа";
        public string Description { get; } = "Интерполяционный многочлен Лагранжа02";
        public string GroupName { get; set; } = "PolynomA";
        public int TimeLimitSeconds { get; set; } = 60;
        private readonly char[] LettersB = {'a', 'b', 'c', 'd', 'f', 'g', 'h', 'p', 'q', 'r', 'u', 'v', 'w'};
        private readonly char[] LettersA = { 'x', 'y', 'z', 's', 't' };
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);

            char letterA = LettersA[random.Next(0, LettersA.Length)];
            char letterB = LettersB[random.Next(0, LettersB.Length)];

            int[] answ = RandNum(random);

            while (answ[3] == 0 || answ[4] == 0 || answ[5] == 0)
            {
                answ = RandNum(random);
            }

            int Bx = answ[0];
            int By = answ[1];
            int Bz = answ[2];
            int Cx = answ[3];
            int Cy = answ[4];
            int Cz = answ[5];
            int Ax = answ[6];
            int Ay = answ[7];
            int Az = answ[8];
            // Bx=[0]  By=[1] Bz=[2] Cx=[3] Cy=[4] Cz=[5] Ax=[6] Ay=[7] Az=[8]

            string resStr =
                    $"Если многочлен \\({letterB}\\left({letterA}\\right)\\) таков, " +
                    $"что \\({letterB} \\left({Ax} \\right)={Bx}\\), \\({letterB}\\left({Ay}\\right)={By}\\), " +
                    $"\\({letterB}\\left({Az}\\right)={Bz}\\), " +
                    $"то (дроби не сокращайте!)\n\r \\({letterB} \\left( {letterA} \\right)" + " \\)=" +
                    $"\\(\\frac{{<Bx:5>}}{{<Cx:5>}}\\left({letterA} - {Ay}\\right)\\)" +
                    $"\\(\\left({letterA}-{Az}\\right)\\)" +
                    $"\\(+\\frac{{<By:5>}}{{<Cy:5>}}\\left({letterA} - {Ax}\\right)\\)" +
                    $"\\(\\left({letterA}-{Az}\\right)\\)" +
                    $"\\(+\\frac{{<Bz:5>}}{{<Cz:5>}}\\left({letterA} - {Ax}\\right)\\)" +
                    $"\\(\\left({letterA}-{Ay}\\right)\\)"

               + $"\n      \\(\\frac{{{Bx}}}{{{Cx}}} \\frac{{{By}}}{{{Cy}}} \\frac{{{Bz}}}{{{Cz}}} \\)";
            resStr = resStr.Replace("--", "+");
            resStr = resStr.Replace("- -", "+");
            resStr = resStr.Replace("+ -", "-");
            //resStr = resStr.Replace("+-", "-");
            //resStr = resStr.Replace("-+", "-");
            //resStr = resStr.Replace("++", "+");

            ITest result = new MultiPoly16();
            result.Text = resStr;

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);
            char _ = LettersA[random.Next(0, LettersA.Length)];
            char _1 = LettersB[random.Next(0, LettersB.Length)];

            int[] answ = RandNum(random);
            while (answ[3] == 0 || answ[4] == 0 || answ[5] == 0)
            {
                answ = RandNum(random);
            }
            int Bx = answ[0];
            int By = answ[1];
            int Bz = answ[2];
            int Cx = answ[3];
            int Cy = answ[4];
            int Cz = answ[5];
            int Ax = answ[6];
            int Ay = answ[7];
            int Az = answ[8];
            // Bx, By, Bz, Cx, Cy, Cz, Ax, Ay, Az
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
            if (answers.TryGetValue("Bz", out var userAnswer4) &&
                int.TryParse(userAnswer4, out var userNumber4) &&
                userNumber4 == Bz)
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
            if (answers.TryGetValue("Cz", out var userAnswer5) &&
                int.TryParse(userAnswer5, out var userNumber5) &&
                userNumber5 == Cz)
            {
                total++;
            }

            return total;
        }

        private static int[] RandNum(Random random)
        {
            int[] d1Value = { -5, -4, -3, -2, -1, 1, 2, 3, 4, 5 };
            List<int> dValue = [.. d1Value];
            List<int> dzValue = new List<int>([-3, -2, -1, 1, 2, 3]);
            int Dx = dValue[random.Next(0, dValue.Count)];
            dValue.Remove(Dx);
            dzValue.Remove(Dx);
            int Dy = dValue[random.Next(0, dValue.Count)];
            dValue.Remove(Dy);
            dzValue.Remove(Dy);
            int Dz = dzValue[random.Next(0, dzValue.Count)];

            int[] _aValue = { -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6 };
            List<int> aValue = [.. _aValue];
            int Ax = aValue[random.Next(0, aValue.Count - 2)];
            int AxIndex = aValue.IndexOf(Ax);
            int Ay = aValue[random.Next(AxIndex + 1, aValue.Count)];
            int Az = aValue[random.Next(AxIndex + 2, aValue.Count)];

            int Cx = (Ax - Ay) * (Ax - Az);
            int Cy = (Ay - Ax) * (Ay - Az);
            int Cz = (Az - Ax) * (Az - Ay);

            int Bx = Dx + Dy * Ax + Dz * Ax * Ax;
            int By = Dx + Dy * Ay + Dz * Ay * Ay;
            int Bz = Dx + Dy * Az + Dz * Az * Az;


            int[] final = { Bx, By, Bz, Cx, Cy, Cz, Ax, Ay, Az };

            return final;
        }
        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
    }
}
