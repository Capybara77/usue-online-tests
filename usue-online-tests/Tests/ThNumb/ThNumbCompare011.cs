using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class ThNumbCompare011 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Number Theory";
        public string Name { get; } = "Приведенная система вычетов 011";
        public string Description { get; } = "Теория сравнений";

        public class Data
        {

            //public string LetterR = "ABCDFGHKMPRSUVWXYZ";
            public int[] answ = new int[16];
            public int[] Aa = new int[8];
            public int[] Ab = new int[8];
            public int[] Ac = new int[5];
            //public int[] Ad = new int[3];
            //public int[] FractA = new int[2];
            public int n = 10;
            public int[] Numbers = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //public int[] Numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
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
                i = random.Next(3);
                //k = random.Next(n - 1);
                //Aa[2] = Numbers[k];
                //for (int i = k; i < n - 1; i++)
                //{
                //    Numbers[i] = Numbers[i+1];
                //}
                //k = random.Next(n - 2);
                //Ab[2] = Numbers[k];
                //for (int i = k; i < n - 2; i++)
                //{
                //    Numbers[i] = Numbers[i + 1];
                //}
                switch (i)
                {
                    case 0:
                        {
                            Ac[0] = 18;
                            Aa[0] = 1;
                            Aa[1] = 5;
                            Aa[2] = 7;
                            Aa[3] = 11;
                            Aa[4] = 13;
                            Aa[5] = 17;
                            Aa[6] = 111;
                            Aa[7] = 111;
                            k = random.Next(3) + 1;
                            Ac[1] = Aa[k];
                            Ab[0] = Aa[0] * Aa[k];
                            Ab[1] = Aa[1] * Aa[k];
                            Ab[2] = Aa[2] * Aa[k];
                            Ab[3] = Aa[3] * Aa[k];
                            Ab[4] = Aa[4] * Aa[k];
                            Ab[5] = Aa[5] * Aa[k];
                            Ab[6] = 111;// Aa[6] * Aa[k];
                            Ab[7] = 111;// Aa[7] * Aa[k];
                            while (Ab[0] > Ac[0] - 1) { Ab[0] = Ab[0] - Ac[0]; }
                            while (Ab[1] > Ac[0] - 1) { Ab[1] = Ab[1] - Ac[0]; }
                            while (Ab[2] > Ac[0] - 1) { Ab[2] = Ab[2] - Ac[0]; }
                            while (Ab[3] > Ac[0] - 1) { Ab[3] = Ab[3] - Ac[0]; }
                            while (Ab[4] > Ac[0] - 1) { Ab[4] = Ab[4] - Ac[0]; }
                            while (Ab[5] > Ac[0] - 1) { Ab[5] = Ab[5] - Ac[0]; }
                            //while (Ab[6] > Ac[0] - 1) { Ab[6] = Ab[6] - Ac[0]; }
                            //while (Ab[7] > Ac[0] - 1) { Ab[7] = Ab[7] - Ac[0]; }
                            break;
                        }
                    case 1:
                        {
                            Ac[0] = 10;
                            Aa[0] = 1;
                            Aa[1] = 3;
                            Aa[2] = 7;
                            Aa[3] = 9;
                            Aa[4] = 111;
                            Aa[5] = 111;
                            Aa[6] = 111;
                            Aa[7] = 111;
                            k = random.Next(2) + 1;
                            Ac[1] = Aa[k];
                            Ab[0] = Aa[0] * Aa[k];
                            Ab[1] = Aa[1] * Aa[k];
                            Ab[2] = Aa[2] * Aa[k];
                            Ab[3] = Aa[3] * Aa[k];
                            Ab[4] = 111;// Aa[4] * Aa[k];
                            Ab[5] = 111;// Aa[5] * Aa[k];
                            Ab[6] = 111;// Aa[6] * Aa[k];
                            Ab[7] = 111;// Aa[7] * Aa[k];
                            while (Ab[0] > Ac[0] - 1) { Ab[0] = Ab[0] - Ac[0]; }
                            while (Ab[1] > Ac[0] - 1) { Ab[1] = Ab[1] - Ac[0]; }
                            while (Ab[2] > Ac[0] - 1) { Ab[2] = Ab[2] - Ac[0]; }
                            while (Ab[3] > Ac[0] - 1) { Ab[3] = Ab[3] - Ac[0]; }
                            //while (Ab[4] > Ac[0] - 1) { Ab[4] = Ab[4] - Ac[0]; }
                            //while (Ab[5] > Ac[0] - 1) { Ab[5] = Ab[5] - Ac[0]; }
                            //while (Ab[6] > Ac[0] - 1) { Ab[6] = Ab[6] - Ac[0]; }
                            //while (Ab[7] > Ac[0] - 1) { Ab[7] = Ab[7] - Ac[0]; }
                            break;
                        }
                    case 2:
                        {
                            Ac[0] = 24;
                            Aa[0] = 1;
                            Aa[1] = 5;
                            Aa[2] = 7;
                            Aa[3] = 11;
                            Aa[4] = 13;
                            Aa[5] = 17;
                            Aa[6] = 19;
                            Aa[7] = 23;
                            k = random.Next(2) + 1;
                            Ac[1] = Aa[k];
                            Ab[0] = Aa[0] * Aa[k];
                            Ab[1] = Aa[1] * Aa[k];
                            Ab[2] = Aa[2] * Aa[k];
                            Ab[3] = Aa[3] * Aa[k];
                            Ab[4] = Aa[4] * Aa[k];
                            Ab[5] = Aa[5] * Aa[k];
                            Ab[6] = Aa[6] * Aa[k];
                            Ab[7] = Aa[7] * Aa[k];
                            while (Ab[0] > Ac[0] - 1) { Ab[0] = Ab[0] - Ac[0]; }
                            while (Ab[1] > Ac[0] - 1) { Ab[1] = Ab[1] - Ac[0]; }
                            while (Ab[2] > Ac[0] - 1) { Ab[2] = Ab[2] - Ac[0]; }
                            while (Ab[3] > Ac[0] - 1) { Ab[3] = Ab[3] - Ac[0]; }
                            while (Ab[4] > Ac[0] - 1) { Ab[4] = Ab[4] - Ac[0]; }
                            while (Ab[5] > Ac[0] - 1) { Ab[5] = Ab[5] - Ac[0]; }
                            while (Ab[6] > Ac[0] - 1) { Ab[6] = Ab[6] - Ac[0]; }
                            while (Ab[7] > Ac[0] - 1) { Ab[7] = Ab[7] - Ac[0]; }
                            break;
                        }
                    case 3:
                        {
                            Ac[0] = 15;
                            Aa[0] = 1;
                            Aa[1] = 2;
                            Aa[2] = 4;
                            Aa[3] = 7;
                            Aa[4] = 8;
                            Aa[5] = 11;
                            Aa[6] = 13;
                            Aa[7] = 14;
                            k = random.Next(3) + 1;
                            Ac[1] = Aa[k];
                            Ab[0] = Aa[0] * Aa[k];
                            Ab[1] = Aa[1] * Aa[k];
                            Ab[2] = Aa[2] * Aa[k];
                            Ab[3] = Aa[3] * Aa[k];
                            Ab[4] = Aa[4] * Aa[k];
                            Ab[5] = Aa[5] * Aa[k];
                            Ab[6] = Aa[6] * Aa[k];
                            Ab[7] = Aa[7] * Aa[k];
                            while (Ab[0] > Ac[0] - 1) { Ab[0] = Ab[0] - Ac[0]; }
                            while (Ab[1] > Ac[0] - 1) { Ab[1] = Ab[1] - Ac[0]; }
                            while (Ab[2] > Ac[0] - 1) { Ab[2] = Ab[2] - Ac[0]; }
                            while (Ab[3] > Ac[0] - 1) { Ab[3] = Ab[3] - Ac[0]; }
                            while (Ab[4] > Ac[0] - 1) { Ab[4] = Ab[4] - Ac[0]; }
                            while (Ab[5] > Ac[0] - 1) { Ab[5] = Ab[5] - Ac[0]; }
                            while (Ab[6] > Ac[0] - 1) { Ab[6] = Ab[6] - Ac[0]; }
                            while (Ab[7] > Ac[0] - 1) { Ab[7] = Ab[7] - Ac[0]; }
                            break;
                        }
                }
                answ[0] = Aa[0];
                answ[1] = Aa[1];
                answ[2] = Aa[2];
                answ[3] = Aa[3];
                answ[4] = Aa[4];
                answ[5] = Aa[5];
                answ[6] = Aa[6];
                answ[7] = Aa[7];
                answ[8] = Ab[0];
                answ[9] = Ab[1];
                answ[10] = Ab[2];
                answ[11] = Ab[3];
                answ[12] = Ab[4];
                answ[13] = Ab[5];
                answ[14] = Ab[6];
                answ[15] = Ab[7];
                //answ[0] = random.Next(Aa[2] - 2) + 1;
                //answ[1] = random.Next(Ab[2] - 2) + 1;
                //answ[2] = random.Next(Ac[2] - 2) + 1;
                //answ[3] = random.Next(Ad[2] - 2) + 1;
                //Numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
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
            ITest result = new ThNumbCompare011();

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
            //$"\\({data.Aa[0]},{data.Aa[1]},{data.Aa[2]},{data.Aa[3]},{data.Aa[4]},{data.Aa[5]},{data.Aa[6]},{data.Aa[7]}\\) \r\n" +
            //$"\\({data.answ[0]},{data.answ[1]},{data.answ[2]},{data.answ[3]},{data.answ[4]},{data.answ[5]},{data.answ[6]},{data.answ[7]}\\) \r\n" +
            //$"\\({data.Ab[0]},{data.Ab[1]},{data.Ab[2]},{data.Ab[3]},{data.Ab[4]},{data.Ab[5]},{data.Ab[6]},{data.Ab[7]}\\) \r\n" +
            $"В поля для ввода введите минимальные неотрицательные целые вычеты из приведенной системы вычетов " +
            $"по модулю \\({data.Ac[0]}\\) в порядке возрастания: \r\n" +
            $"\\(<ans[0]:3>,<ans[1]:3>, <ans[2]:3>, <ans[3]:3>, <ans[4]:3>, <ans[5]:3>, <ans[6]:3>, <ans[7]:3>\\).\r\n" +
            $"Если разных вычетов меньше 8, в осташиеся поля для ввода вставьте 111.\r\n" +
            $"Минимальные неотрицательные вычеты, сравнимые с произведением указанных вычетов на \\({data.Ac[1]}\\), " +
            $"с сохранением исходного порядка перечисленных выше вычетов:\r\n" +
            $"\\(<ans[8]:3>,<ans[9]:3>, <ans[10]:3>, <ans[11]:3>, <ans[12]:3>, <ans[13]:3>, <ans[14]:3>, <ans[15]:3>\\)";
            //$""
            //$"Для каждого из данных сравнений укажите минимальное неотрицательное целое число, " +
            //$"при котором это сравнение выполняется: \r\n" +
            //$"\\(\\ {data.Aa[1]} \\equiv <ans[0]:3> \\, (\\mbox{{mod }}\\, {data.Aa[2]}) \\); \r\n" +
            //$"\\(\\ {data.Ab[1]} \\equiv <ans[1]:3> \\, (\\mbox{{mod }}\\, {data.Ab[2]}) \\); \r\n" +
            //$"\\(\\ {data.Ac[1]} \\equiv <ans[2]:3> \\, (\\mbox{{mod }}\\, {data.Ac[2]}) \\); \r\n" +
            //$"\\(\\ {data.Ad[1]} \\equiv <ans[3]:3> \\, (\\mbox{{mod }}\\, {data.Ad[2]}) \\). \r\n"; //+
            //$"\\({data.Ab[1]},{data.Aa[0]},{data.Aa[1]},{data.Aa[2]}=\\)" +
            //$"\\(<ans[0]:3> + \\displaystyle\\frac{{1}}{{<ans[1]:3> + \\displaystyle\\frac{{1}}{{<ans[2]:3> + \\displaystyle\\frac{{1}}{{<ans[3]:3>}} }} }}\\) \r\n" +
            //$"\\( {data.Aa[2]} + \\displaystyle\\frac{{1}}{{ {data.Aa[1]} + \\displaystyle\\frac{{1}}{{ {data.Aa[0]} + \\displaystyle\\frac{{1}}{{ {data.Ab[1]} }} }} }}\\)" +
            //$"\\( {data.answ[0]} + \\displaystyle\\frac{{1}}{{ {data.answ[1]} + \\displaystyle\\frac{{1}}{{ {data.answ[2]} + \\displaystyle\\frac{{1}}{{ {data.answ[3]} }} }} }}\\) \r\n" +
            //$"\\(\\begin{{array}}{{r}} \\strut\\\\ \\strut\\\\ \\strut\\\\ \\strut\\\\ <ans[10]:3> \\strut\\\\ <ans[12]:3> \\strut\\\\ \\hline <ans[14]:3> \\strut\\\\ \\end{{array}}\\)" +
            //$"\\(\\begin{{array}}{{r}} \\strut\\\\ \\strut\\\\ <ans[6]:3> \\strut\\\\ \\underline{{<ans[8]:3>}}\\strut\\\\ \\begin{{array}}{{|r}} <ans[11]:3> \\strut\\\\ \\hline <ans[13]:3> \\strut\\\\ \\end{{array}}\\\\ \\rule{{0pt}}{{10ex}}\\\\ \\end{{array}}\\)" +
            //$"\\(\\begin{{array}}{{r}} {data.FractA[0]} \\strut\\\\ \\underline{{<ans[4]:3>}}\\strut\\\\ \\begin{{array}}{{|r}} <ans[7]:3> \\strut\\\\  \\hline <ans[9]:3> \\strut\\\\ \\end{{array}}\\\\ \\rule{{0pt}}{{27ex}}\\\\ \\end{{array}}\\)" +
            //$"\\(\\begin{{array}}{{r}} \\begin{{array}}{{|r}} {data.FractA[1]}\\strut\\\\ \\hline <ans[5]:3> \\strut\\\\  \\end{{array}}\\\\  \\rule{{0pt}}{{41ex}}\\\\ \\end{{array}}\\)";
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
