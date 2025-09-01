using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class Relat02 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Вид вектора при предикате, который задан таблицей значений";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {
            public string[] arr { get; } = {
            "\\(p\\) задан таблицей значений: \r\n \\( \\begin{array}{c|cc}" +
                "x &#10741; y & \\text{1} & \\text{2} \\\\" +
                "\\hline" +
                "1 & 0 & 1 \\\\" +
                "2 & 0 & 1" +
                "\\end{array} \\) \r\n" +
                "Тогда \\(P = \\{(<ans0>; <ans1>), (<ans2>;<ans3>)\\} \\)",
            "\\(u\\) задан таблицей значений: \r\n \\( \\begin{array}{c|cc}" +
                "p &#10741; q & \\text{1} & \\text{2} \\\\" +
                "\\hline" +
                "1 & 1 & 1 \\\\" +
                "2 & 0 & 1" +
                "\\end{array} \\) \r\n" +
                "Тогда \\(U = \\{(<ans0>;<ans1>), (<ans2>;<ans3>), (<ans4>;<ans5>)\\}\\)",
            "\\(q\\) задан таблицей значений: \r\n \\( \\begin{array}{c|cc}" +
                "x &#10741; y & \\text{0} & \\text{2} \\\\" +
                "\\hline" +
                "0 & 1 & 0 \\\\" +
                "2 & 1 & 0" +
                "\\end{array} \\) \r\n" +
                "Тогда \\(Q = \\{(<ans0>;<ans1>), (<ans2>;<ans3>)\\}\\)",
            "\\(v\\) задан таблицей значений: \r\n \\( \\begin{array}{c|cc}" +
                "x &#10741; y & \\text{1} & \\text{2} \\\\" +
                "\\hline" +
                "1 & 0 & 1 \\\\" +
                "2 & 1 & 1" +
                "\\end{array} \\) \r\n" +
                "Тогда \\(V = \\{(<ans0>;<ans1>), (<ans2>;<ans3>), (<ans4>;<ans5>)\\}\\)"
            };
            public string[][] ans { get; } = {
                new string[] {"1", "2", "2", "2"},
                new string[] {"1", "1", "1", "2", "2", "2"},
                new string[] {"0", "0", "2", "0"},
                new string[] {"1", "2", "2", "1", "2", "2"},
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat02();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"Предикат {data.arr[num]}.\r\n" +
                $"Порядок написания пар скобок бинарных отношений важен.";

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
        public List<MemoryStream> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 90;
    }
}
