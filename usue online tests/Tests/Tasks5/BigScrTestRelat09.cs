using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Test_Wrapper;

namespace UserTest
{
    public class Relat09 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Вид отношения эквивалентности при известных классах элементов";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {
            
            public static string[][] basis;
            public string[][] text { get; set; } =
            {
                new string[] {"V", "\\{2;8\\}, \\{4\\}"},
                new string[] {"Q", "\\{2;4\\}, \\{6\\}"},
                new string[] {"P", "\\{1\\}, \\{3;5\\}"},
                new string[] {"R", "\\{3;7\\}, \\{5\\}"}
            };

            public string[][] ans { get; set; } = {
                new string[] { "2", "2", "2", "8", "8", "2", "8", "8", "4", "4" },
                new string[] { "2", "2", "2", "4", "4", "2", "4", "4", "6", "6" },
                new string[] { "1", "1", "3", "3", "3", "5", "5", "3", "5", "5" },
                new string[] { "3", "3", "3", "7", "7", "3", "7", "7", "5", "5" }

            };
            public string[] anstext { get; set; } =
            {
                "\\{ (<ans0>; <ans1>), (<ans2>; <ans3>), (<ans4>; <ans5>), (<ans6>; <ans7>), (<ans8>; <ans9>)  \\}",
                "\\{ (<ans0>; <ans1>), (<ans2>; <ans3>), (<ans4>; <ans5>), (<ans6>; <ans7>), (<ans8>; <ans9>)  \\}",
                "\\{ (<ans0>; <ans1>), (<ans2>; <ans3>), (<ans4>; <ans5>), (<ans6>; <ans7>), (<ans8>; <ans9>)  \\}",
                "\\{ (<ans0>; <ans1>), (<ans2>; <ans3>), (<ans4>; <ans5>), (<ans6>; <ans7>), (<ans8>; <ans9>)  \\}",
            };
        }
        public ITest CreateTest(int randomSeed)
        {
            ITest result = new Relat09();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);

            result.Text = $"\\({data.text[num][0]}\\) —  отношение эквивалентности, классы эквивалентных по \\({data.text[num][0]}\\) элементов имеют вид \\({data.text[num][1]}\\). \r\n" +
            $"Тогда \\({data.text[num][0]} = {data.anstext[num]}\\) \r\n" +
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
                for (int i = 0; i < 16; i++)
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
        public int TimeLimitSeconds { get; set; } = 90;
    }
}
