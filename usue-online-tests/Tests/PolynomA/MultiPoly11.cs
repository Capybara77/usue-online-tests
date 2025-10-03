using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Test_Wrapper;

namespace usue_online_tests.Tests.PolynomA
{
    public class MultiPoly11 : ITestCreator, ITest, ITestGroup
    {
        public int TestID { get; set; }
        public string Name { get; } = "Умножение многочленов";
        public string Description { get; } = "Умножение многочленов от одной переменой";
        public string GroupName { get; set; } = "PolynomA";

        private static readonly char[] Letters = ['p', 'q', 'r', 'x', 'y', 'z', 't', 'u', 'v', 'w'];
        public ITest CreateTest(int randomSeed)
        {
            var random = new Random(randomSeed);

            int[] values = {-9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var letterA = Letters[random.Next(0, Letters.Length)];

            List<int> listIndex = [.. values];
            var Ax = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ax);
            var Ay = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ay);
            var Az = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Az);
            var Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            var By = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(By);
            var Bz = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bz);

            //var Ca = Ax * Bx;                                      %Для отладки%
            //var Cb = Ax * By + Ay * Bx;                            
            //var Cc = Ax * Bz + Ay * By + Az * Bx;
            //var Cd = Ay * Bz + Az * By;
            //var Ce = Az * Bz;

            ITest result = new MultiPoly11();
            // формат инпутов для UI - <*:count>, где * это название для инпута (тут наверно лучше использовать Ca, Cb ...)
            // count это длина инпута
            result.Text =
                $"\\(\\left({Ax} + {Ay}\\cdot {letterA} + {Az}\\cdot {letterA}^{{2}}\\right)" +
                $"\\left({Bx} + {By}\\cdot {letterA} + {Bz}\\cdot {letterA}^{{2}}\\right) = \r\n" +
                $"<Ca:5> + <Cb:5>\\cdot {letterA} + <Cc:5>\\cdot {letterA}^{{2}} + " +
                $"<Cd:5>\\cdot {letterA}^{{3}} + <Ce:5>\\cdot {letterA}^{{4}}\\)";

            result.Text = result.Text.Replace("+ -", "-");
            result.Text = result.Text.Replace("- -", "+");
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            var random = new Random(randomSeed);
            int[] values = {-9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var _ = random.Next(0, Letters.Length);

            var listIndex = new List<int>(values);
            var Ax = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ax);
            var Ay = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Ay);
            var Az = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Az);
            var Bx = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bx);
            var By = listIndex[random.Next(0, listIndex.Count)]; //(−2+5⋅y+6⋅y2)(8+−7⋅y+−3⋅y2)= x1 + x2*y + x3*y**2 + x4*y**3 + x4 * y**4
            listIndex.Remove(By);
            var Bz = listIndex[random.Next(0, listIndex.Count)];
            listIndex.Remove(Bz);

            var Ca = Ax * Bx;

            var Cb = Ax * By + Ay * Bx; // ! при генерации тут Ax * Bz + Ay * Bx;

            var Cc = Ax * Bz + Ay * By + Az * Bx;
            var Cd = Ay * Bz + Az * By;
            var Ce = Az * Bz;

            var total = 0;

            if (answers.TryGetValue("Ca", out var userAnswer0) &&
                int.TryParse(userAnswer0, out var userNumber0) &&
                userNumber0 == Ca)
            {
                total++;
            }

            if (answers.TryGetValue("Cb", out var userAnswer1) &&
                int.TryParse(userAnswer1, out var userNumber1) &&
                userNumber1 == Cb)
            {
                total++;
            }

            if (answers.TryGetValue("Cc", out var userAnswer2) &&
                int.TryParse(userAnswer2, out var userNumber2) &&
                userNumber2 == Cc)
            {
                total++;
            }

            if (answers.TryGetValue("Cd", out var userAnswer3) &&
                int.TryParse(userAnswer3, out var userNumber3) &&
                userNumber3 == Cd)
            {
                total++;
            }

            if (answers.TryGetValue("Ce", out var userAnswer4) &&
                int.TryParse(userAnswer4, out var userNumber4) &&
                userNumber4 == Ce)
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
