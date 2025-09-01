using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace02 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Определение отношения, связывающего столбцы координат матрицы перехода из базиса в базис";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            public string[] arr { get; set; } = { 
                "Если \\(T_{Б→В}\\) - матрица перехода из базиса  \\(Б\\) в базис  \\(В\\), то столбцы координат  \\([x]_Б\\) и  \\([x]_В\\) связаны отношением...",
                "Если \\(T_{V'→V''}\\) - матрица перехода из базиса  \\(V'\\) в базис  \\(V''\\), то столбцы координат  \\([x]_V'\\) и  \\([x]_V''\\) связаны отношением...\r\n (Штрих пишется как апостроф: \" ' \")",
                "Если \\(T_{P→Q}\\) - матрица перехода из базиса  \\(P\\) в базис  \\(Q\\), то столбцы координат  \\([u]_P\\) и  \\([u]_Q\\) связаны отношением...",
                "Если \\(T_{M→N}\\) - матрица перехода из базиса  \\(M\\) в базис  \\(N\\), то столбцы координат  \\([v]_M\\) и  \\([v]_N\\) связаны отношением..."
            };
            public string[] column { get; set; } = { "[x]", "[x]", "[u]", "[v]" };
            public string[][] ans { get; set; } = {
                new string[] {"Б", "Б", "В", "В", "В", "Б", "В"},
                new string[] { "V'", "V'", "V''", "V''", "V''", "V'", "V''"},
                new string[] {"P", "P", "Q", "Q", "Q", "P", "Q"},
                new string[] {"M", "M", "N", "N", "N", "M", "N"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace02();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"{data.arr[num]} \r\n" +
                $"\\({data.column[num]}<ans0>  = T_{{\\left(<ans1>→<ans2>\\right)}}\\ {data.column[num]}<ans3> = T^{{-1}}_{{\\left(<ans4>→<ans5>\\right)}}\\ {data.column[num]}<ans6>\\)\r\n" +
                $"";

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
                for (int i = 0; i < 7; i++)
                {
                    if (answers["ans" + i] == ans[i].ToString()) total++;
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
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
