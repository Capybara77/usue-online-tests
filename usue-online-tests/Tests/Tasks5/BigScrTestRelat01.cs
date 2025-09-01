using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class Relat01 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношение P задано на Ω, отметить верное высказывание";
        public string Description { get; } = "Отношения, предикаты";

        public class Data
        {
            public string[] text { get; set; } = {
                "\\( P = \\{(0;2), (1;3) \\}\\) задано на \\(Ω = \\{0;1;2;3\\}\\)",
                "\\( P = \\{(0;1), (0;2), (2;1) \\}\\) задано на \\(Ω = \\{0;1;2\\}\\)",
                "\\( P = \\{(0;2), (2;3) \\}\\) задано на \\(Ω = \\{0;1;2;3\\}\\)",
                "\\( P = \\{(0;2), (2;0), (1;2) \\}\\) задано на \\(Ω = \\{0;1;2\\}\\)"
            };
            public string[][] answ { get; set; } =
            {
                new string[] { "\\((0;1) ∈ P\\)", "\\((0;1) ∉ P\\)", "\\(𝒫(1;3)\\) (предикат)", "\\(𝒫(3;1)\\) (предикат)", "\\(p(1;3) = 1\\)", "\\(p(3;1) = 0\\)" },
                new string[] { "\\((1;2) ∈ P\\)", "\\((1;3) ∉ P\\)", "\\(𝒫(0;2)\\) (предикат)", "\\(𝒫(1;2)\\) (предикат)", "\\(p(0;2) = 0\\)", "\\(p(0;3) = 1\\)" },
                new string[] { "\\((0;2) ∈ P\\)", "\\((0;3) ∉ P\\)", "\\(𝒫(0;2)\\) (предикат)", "\\(𝒫(0;3)\\) (предикат)", "\\(p(0;3) = 1\\)", "\\(p(0;3) = 0\\)" },
                new string[] { "\\((1;2) ∈ P\\)", "\\((0;1) ∉ P\\)", "\\(𝒫(1;2)\\) (предикат)", "\\(𝒫(2;0)\\) (предикат)", "\\(p(1;2) = 0\\)", "\\(p(1;2) = 1\\)" },
            };
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
            ITest result = new Relat01();

            Data data = new Data();

            Random random = new Random(randomSeed);
            int option = random.Next(0, 4);

            var a = ShuffleSequence(data.answ[option].Length, randomSeed);
            string[] answersForUser = new string[data.answ[option].Length];

            for (int i = 0; i < data.answ[option].Length; i++)
            {
                answersForUser[i] = data.answ[option][a[i]];
            }

            result.Text = $"Отношение {data.text[option]}. Отметьте верные высказывания.";
            result.CheckBoxes = answersForUser;
            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            Data data = new Data();

            Random random = new Random(randomSeed);
            int option = random.Next(0, 4);

            try
            {
                switch (option)
                {
                    case 0:
                        {
                            if (!answers.ContainsKey(data.answ[option][0])) total++;
                            if (answers.ContainsKey(data.answ[option][1]) && answers[data.answ[option][1]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][2]) && answers[data.answ[option][2]] == "on") total++;
                            if (!answers.ContainsKey(data.answ[option][3])) total++;
                            if (answers.ContainsKey(data.answ[option][4]) && answers[data.answ[option][4]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][5]) && answers[data.answ[option][5]] == "on") total++;
                            break;
                        }
                    case 1:
                        {
                            if (!answers.ContainsKey(data.answ[option][0])) total++;
                            if (answers.ContainsKey(data.answ[option][1]) && answers[data.answ[option][1]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][2]) && answers[data.answ[option][2]] == "on") total++;
                            if (!answers.ContainsKey(data.answ[option][3])) total++;
                            if (!answers.ContainsKey(data.answ[option][4])) total++;
                            if (!answers.ContainsKey(data.answ[option][5])) total++;
                            break;
                        }
                    case 2:
                        {
                            if (answers.ContainsKey(data.answ[option][0]) && answers[data.answ[option][0]] == "on") total++;
                            if (!answers.ContainsKey(data.answ[option][1])) total++;
                            if (answers.ContainsKey(data.answ[option][2]) && answers[data.answ[option][2]] == "on") total++;
                            if (!answers.ContainsKey(data.answ[option][3])) total++;
                            if (!answers.ContainsKey(data.answ[option][4])) total++;
                            if (answers.ContainsKey(data.answ[option][5]) && answers[data.answ[option][5]] == "on") total++;
                            break;
                        }
                    case 3:
                        {
                            if (answers.ContainsKey(data.answ[option][0]) && answers[data.answ[option][0]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][1]) && answers[data.answ[option][1]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][2]) && answers[data.answ[option][2]] == "on") total++;
                            if (answers.ContainsKey(data.answ[option][3]) && answers[data.answ[option][3]] == "on") total++;
                            if (!answers.ContainsKey(data.answ[option][4])) total++;
                            if (answers.ContainsKey(data.answ[option][5]) && answers[data.answ[option][5]] == "on") total++;
                            break;
                        }
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
    }
}
