using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class LinSpace03 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public int TestID { get; set; }
        public string Name { get; } = "Линейные пространства";
        public string Description { get; } = "Типовые способы задания подпространства линейного пространства";

        public class Data
        {
            public string[] arr { get; set; } = { "𝑎^2 − |𝑎 · 𝑏| = 1", "𝑎^2𝑏^2 = 0", "𝑎^2 + 𝑏^2 − |𝑎 · 𝑏| = 0", "𝑏^2 − |𝑎 · 𝑏| = 1" };
            public string[] ans { get; set; } = { "ОСЛУ, т.е. утверждений о координатах произвольного вектора из M в базисе пр-ва N", 
            "Представление M в виде линейной оболочки базиса подпространства M.", 
            "Представление M в виде линейного комбинации базисных векторов пространства N.", 
            "Ортогональная проекция векторов из M на подпространство N.",
            "Линейная трансформация векторов из M в пространство N." };
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
            ITest result = new RelatLS03();

            Data data = new Data();

            var a = ShuffleSequence(data.ans.Length, randomSeed);
            string[] answersForUser = new string[data.ans.Length];

            for (int i = 0; i < data.ans.Length; i++)
            {
                answersForUser[i] = data.ans[a[i]];
            }

            result.Text = $"Перечислите типовые способы задания подпространства . Отметьте верные отверждения.";
            result.CheckBoxes = answersForUser;
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            Data data = new Data();
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
