using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class Relat03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора при предикате, который задан графиком";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {
            public string[] arr { get; } = {
            "\\(p(x,y)\\) задан графиком: \r\n" +
                    " \r\n" +
                "Тогда \\(P = \\)",
            "\\(u(x,y)\\) задан графиком: \r\n" +
                    " \r\n" +
                "Тогда \\(U = \\)",
            "\\(q(x,y)\\) задан графиком: \r\n" +
                    " \r\n" +
                "Тогда \\(Q = \\)",
            "\\(v(x,y)\\) задан графиком: \r\n" +
                    " \r\n" +
                "Тогда \\(V = \\)"
            };
            public string[][] ans { get; } = {
                new string[] {"0", "1", "2", "0"},
                new string[] {"0", "2", "2", "1"},
                new string[] {"1", "2", "2", "1"},
                new string[] {"0", "1", "2", "2"},
            };
        }
        public ITest CreateTest(int randomSeed)
        {   
            ITest result = new Relat03();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Предикат {data.arr[num]} \\(\\{{ (<ans0>; <ans1>) ,  (<ans2>; <ans3>) \\}}\\).\r\n" +
                $"Порядок написания пар скобок бинарных отношений важен.";

            Image img = Image.FromFile(Environment.CurrentDirectory + $"\\wwwroot\\generators\\Relat\\{num}.jpg");
            //result.Pictures.Add(img);

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(4);
            string[] ans = data.ans[num];
            try
            {
                for (int i = 0; i < data.ans.Length; i++)
                {
                    if (answers["ans" + i] == ans[i]) total++;
                }
            }
            catch
            {
                // ignored
            }

            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<MemoryStream> Pictures { get; set; } = new();
        public int TimeLimitSeconds { get; set; } = 90;
    }
}
