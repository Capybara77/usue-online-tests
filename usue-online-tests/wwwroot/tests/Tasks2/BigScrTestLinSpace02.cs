using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace02 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Линейные пространства";
        public string Description { get; } = "Определение отношения, связывающего столбцы координат матрицы перехода из базиса в базис";

        public class Data
        {
            public string[] arr { get; set; } = { "Если \\(T_{Б→В}\\) - матрица перехода из базиса  \\(Б\\) в базис  \\(В\\), то столбцы координат  \\([x]_Б\\) и  \\([x]_В\\) связаны отношением...",
                "Если \\(T_{Б'→Б''}\\) - матрица перехода из базиса  \\(Б'\\) в базис  \\(Б''\\), то столбцы координат  \\([x]_Б'\\) и  \\([x]_Б''\\) связаны отношением...\r\n (Штрих пишется как апостроф: \" ' \")" };
            public string[][] ans { get; set; } = {
                new string[] {"Б", "Б", "В", "В", "В", "Б", "В"},
                new string[] { "Б'", "Б'", "Б''", "Б''", "Б''", "Б'", "Б''"}
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new LinSpace02();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(2);

            result.Text = $"{data.arr[num]} \r\n" +
                $"\\([x]<ans0>  = T_{{<ans1>→<ans2>}}\\ [x]<ans3> = T^{{-1}}_{{<ans4>→<ans5>}}\\ [x]<ans6>\\)\r\n" +
                $"";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            Data data = new Data();
            int num = random.Next(2);
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
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = true;
    }
}
