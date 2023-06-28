using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    public class RelatLS04 : ITestCreator, ITest, ITimeLimit, IHidden
    {
        public bool IsHidden { get; set; } = true;
        public int TestID { get; set; }
        public string Name { get; } = "Отношения и предикаты. Линейные пространства. Линейная комбинация векторов";
        public string Description { get; } = "Линейная комбинация векторов";

        public class Data
        {
            public string[] vectors { get; set; } = { "uvw", "kmn", "pqr", "abc" };
            public string[] vals { get; set; } = { "abc", "pqr", "uvw", "mnk" };
        }

        public ITest CreateTest(int randomSeed)
        {
            ITest result = new RelatLS04();

            Random random = new Random(randomSeed);
            Data data = new Data();

            int num = random.Next(4);
            string vector = data.vectors[num];
            string val = data.vals[num];

            string str = "";
            for (int i = 0; i < vector.Length; i++)
            {
                if (i + 1 != vector.Length)
                {
                    str += $"<val{i}>*<vec{i}> + ";
                }
                else
                {
                    str += $"<val{i}>*<vec{i}>";
                }
            }
            string vecs = string.Join(", ", vector.ToCharArray());
            string vals = string.Join("; ", val.ToCharArray());
            result.Text = $" Заполните поля для ввода, записав линейную комбинацию векторов \\( {vecs} \\) с коэффициентами \\( {vals} \\)... . \r\nФормула имеет вид «коэффициент*вектор». \r\n" + $"\\({str}\\)";

            return result;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;

            Random random = new Random(randomSeed);
            string[] answer = answers.Values.ToArray();

            Data data = new Data();

            int num = random.Next(4);
            string vector = data.vectors[num];
            string val = data.vals[num];

            for (int i = 0; i < vector.Length; i++)
            {
                try
                {
                    if (vector[i].ToString() == answers["vec" + i]) total += 1;
                }
                catch
                {

                }

                try
                {
                    if (val[i].ToString() == answers["val" + i]) total += 1;
                }
                catch
                {

                }
            }
            return total;
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 100;
    }
}
