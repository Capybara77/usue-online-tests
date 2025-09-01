using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class ContinuedFraction001 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Number Theory";
        public string Name { get; } = "Непрерывные дроби 001";
        public string Description { get; } = "Непрерывные дроби";

        public class Data
        {

            //public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answ = new int[15];
            public int[] Aa = new int[3];
            public int[] Ab = new int[3];
            public int[] Ac = new int[3];
            public int[] Ad = new int[3];
            public int[] FractA = new int[2];
            public int n = 9;
            public int[] Numbers = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            public int i, k;
            // public int[] MatrB = new int[6];
            // public int[] MatrC = new int[6];
            // public int[] MatrD = new int[6];
            // public int iA, jA, iB, jB, iCx, jCx, iCy, jCy, Vvv, Vvy;
            // public string ElemMatrA = "";
            // public string ElemMatrB = "";
            // public string ElemMatrC = "";
            // public string MCa = "";
            // public string MCb = "";
            // public string MCc = "";
            // public string IndAa = "";
            // public string IndAb = "";
            // public string IndAc = "";
            // public string IndBa = "";
            // public string IndBb = "";
            // public string IndBc = "";
            // public string IndexC = "";
            // public string MatrEqA = "";
            // public int alpha;
            // public int beta;

            public Data(Random random)
            {
                CreateMatrixes(random);
            }
            public void CreateMatrixes(Random random)
            {
                k = random.Next(n - 1);
                Ab[1] = Numbers[k];
                for (int i = k; i < n - 1; i++)
                {
                    Numbers[i] = Numbers[i + 1];
                }
                k = random.Next(n - 2);
                Aa[0] = Numbers[k];
                for (int i = k; i < n - 2; i++)
                {
                    Numbers[i] = Numbers[i + 1];
                }
                k = random.Next(n - 3);
                Aa[1] = Numbers[k];
                for (int i = k; i < n - 3; i++)
                {
                    Numbers[i] = Numbers[i + 1];
                }
                k = random.Next(n - 4);
                Aa[2] = Numbers[k];
                for (int i = k; i < n - 4; i++)
                {
                    Numbers[i] = Numbers[i + 1];
                }
                Ab[0] = 1;
                Ac[0] = Ab[1] * Aa[0];
                Ad[0] = Ac[0] + Ab[0];
                Ab[2] = Ad[0];
                Ac[1] = Ab[2] * Aa[1];
                Ad[1] = Ac[1] + Ab[1];
                FractA[1] = Ad[1];
                Ac[2] = FractA[1] * Aa[2];
                FractA[0] = Ac[2] + Ab[2];
                answ[0] = Aa[2];
                answ[1] = Aa[1];
                answ[2] = Aa[0];
                answ[3] = Ab[1];
                answ[4] = Ac[2];
                answ[5] = Aa[2];
                answ[6] = Ad[1];
                answ[7] = Ab[2];
                answ[8] = Ac[1];
                answ[9] = Aa[1];
                answ[10] = Ad[0];
                answ[11] = Ab[1];
                answ[12] = Ac[0];
                answ[13] = Aa[0];
                answ[14] = Ab[0];
                //MCa = LetterR[Vvv].ToString();
                //MCb = MCa.ToLower();
                //Vvy = 20;
                //int[] Numbers = new int[2*Vvy];
                //Vvy = Vvy - 1;
                //Vvv = random.Next(Vvy-1);
                //MatrA[0] = Numbers[Vvv];
                //Numbers[Vvv] = Numbers[Vvy];
                //Vvy = Vvy - 1;
                //Vvv = random.Next(Vvy-1);
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new ContinuedFraction001();

            Random random = new Random(randomSeed);
            Data data = new Data(random);

            //int num = random.Next(18);
            // for (int i = 0; i < data.answ.Length; i++) // Проверяем все ответы, а не только первые 6
            // {
            //     string key = "ans[" + i + "]"; // Формируем ключ для доступа к ответу
            //
            //     // Логируем значение data.answ[i]
            //     Console.WriteLine($"Правильный ответ для {key}: {data.answ[i]}");
            // }

            result.Text =
            $"\\(\\displaystyle\\frac{{ {data.FractA[0]} }}{{ {data.FractA[1]} }}=\\) " +
            //$"\\({data.Ab[1]},{data.Aa[0]},{data.Aa[1]},{data.Aa[2]}=\\)" +
            $"\\(<ans[0]:3> + \\displaystyle\\frac{{1}}{{<ans[1]:3> + \\displaystyle\\frac{{1}}{{<ans[2]:3> + \\displaystyle\\frac{{1}}{{<ans[3]:3>}} }} }}\\) \r\n" +
            //$"\\( {data.Aa[2]} + \\displaystyle\\frac{{1}}{{ {data.Aa[1]} + \\displaystyle\\frac{{1}}{{ {data.Aa[0]} + \\displaystyle\\frac{{1}}{{ {data.Ab[1]} }} }} }}\\)" +
            //$"\\( {data.answ[0]} + \\displaystyle\\frac{{1}}{{ {data.answ[1]} + \\displaystyle\\frac{{1}}{{ {data.answ[2]} + \\displaystyle\\frac{{1}}{{ {data.answ[3]} }} }} }}\\) \r\n" +
            $"\\(\\begin{{array}}{{r}} \\strut\\\\ \\strut\\\\ \\strut\\\\ \\strut\\\\ <ans[10]:3> \\strut\\\\ <ans[12]:3> \\strut\\\\ \\hline <ans[14]:3> \\strut\\\\ \\end{{array}}\\)" +
            $"\\(\\begin{{array}}{{r}} \\strut\\\\ \\strut\\\\ <ans[6]:3> \\strut\\\\ \\underline{{<ans[8]:3>}}\\strut\\\\ \\begin{{array}}{{|r}} <ans[11]:3> \\strut\\\\ \\hline <ans[13]:3> \\strut\\\\ \\end{{array}}\\\\ \\rule{{0pt}}{{10ex}}\\\\ \\end{{array}}\\)" +
            $"\\(\\begin{{array}}{{r}} {data.FractA[0]} \\strut\\\\ \\underline{{<ans[4]:3>}}\\strut\\\\ \\begin{{array}}{{|r}} <ans[7]:3> \\strut\\\\  \\hline <ans[9]:3> \\strut\\\\ \\end{{array}}\\\\ \\rule{{0pt}}{{27ex}}\\\\ \\end{{array}}\\)" +
            $"\\(\\begin{{array}}{{r}} \\begin{{array}}{{|r}} {data.FractA[1]}\\strut\\\\ \\hline <ans[5]:3> \\strut\\\\  \\end{{array}}\\\\  \\rule{{0pt}}{{41ex}}\\\\ \\end{{array}}\\)";
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            // Инициализация данных с использованием randomSeed для согласованности
            Random random = new Random(randomSeed);
            Data data = new Data(random);



            try
            {
                // Проверяем ответы пользователя
                for (int i = 0; i < data.answ.Length; i++) // Проверяем все ответы, а не только первые 6
                {
                    string key = "ans[" + i + "]"; // Формируем ключ для доступа к ответу

                    // // Логируем значение data.answ[i]
                    // Console.WriteLine($"Правильный ответ для {key}: {data.answ[i]}");

                    // Проверяем, существует ли ключ в словаре answers
                    if (answers.ContainsKey(key))
                    {
                        // // Логируем ответ пользователя
                        // Console.WriteLine($"Ответ пользователя для {key}: {answers[key]}");

                        // Сравниваем ответ пользователя с правильным ответом
                        if (answers[key] == data.answ[i].ToString())
                        {
                            total++; // Увеличиваем счетчик правильных ответов
                        }
                    }
                    else
                    {
                        // // Логируем отсутствие ключа (для отладки)
                        // Console.WriteLine($"Ключ {key} отсутствует в answers.");
                    }
                }
            }
            catch (Exception ex)
            {
                // // Логируем исключение (для отладки)
                // Console.WriteLine($"Ошибка при проверке ответов: {ex.Message}");
            }

            // // Логируем общее количество правильных ответов
            // Console.WriteLine($"Общее количество правильных ответов: {total}");

            return total; // Возвращаем количество правильных ответов
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 120;
        public bool IsHidden { get; set; } = false;
    }
}
