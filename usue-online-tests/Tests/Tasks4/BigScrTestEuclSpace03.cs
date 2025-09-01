using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Test_Wrapper;

namespace UserTest
{
    public class EuclSpace03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Типовые способы задания подпространства линейного пространства";
        public string Description { get; } = "Линейные пространства Eucl";

        public class Data
        {
            static int num;
            public Data(int n)
            {
                num = n;
                letter = new string[] { "MN", "PQ", "FG", "CD", };
                ans = new string[] {
                "ОСЛУ, т.е. утверждений о координатах произвольного вектора из " + letter[num][0] + " в базисе пр-ва " + letter[num][1],
                "Представление " + letter[num][0] + " в виде линейной оболочки базиса подпространства " + letter[num][1],
                "Представление " + letter[num][0] + " в виде линейного комбинации базисных векторов пространства " + letter[num][1],
                "Ортогональная проекция векторов из " + letter[num][0] + " на подпространство " + letter[num][1],
                "Линейная трансформация векторов из " + letter[num][0] + " в пространство " + letter[num][1]
                };
            }
            public static string[] letter { get; set; }
            public string[] ans { get; set; }
        }

        private int[] ShuffleSequence(int arrayLength, int randomSeed)
        {
            int[] sequence = Enumerable.Range(0, arrayLength).ToArray();
            Random rnd = new Random(randomSeed);
            for (int i = 0; i < sequence.Length; i++)
            {
                int randomIndex = rnd.Next(i, sequence.Length);
                (sequence[i], sequence[randomIndex]) = (sequence[randomIndex], sequence[i]);
            }

            return sequence;
        }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new EuclSpace03();

            Random random = new Random(randomSeed);
            int number = random.Next(4);
            Data data = new Data(number);

            var a = ShuffleSequence(data.ans.Length, randomSeed);
            string[] answersForUser = new string[data.ans.Length];

            for (int i = 0; i < data.ans.Length; i++)
            {
                answersForUser[i] = data.ans[a[i]];
            }

            result.Text = $"Перечислите типовые способы задания подпространства {Data.letter[number][0]} линейного пространства {Data.letter[number][1]}. Отметьте верные отверждения.";
            result.CheckBoxes = answersForUser;
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            int num = random.Next(4);
            Data data = new Data(num);

            try
            {
                if (answers.ContainsKey(data.ans[0]) && answers[data.ans[0]] == "on") total++;
                if (answers.ContainsKey(data.ans[1]) && answers[data.ans[1]] == "on") total++;
                if (!answers.ContainsKey(data.ans[2])) total++;
                if (!answers.ContainsKey(data.ans[3])) total++;
                if (!answers.ContainsKey(data.ans[4])) total++;
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
