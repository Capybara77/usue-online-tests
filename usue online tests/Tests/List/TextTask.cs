using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace usue_online_tests.Tests.List
{
    public class TextTask : ITestCreater, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Текстовая задача про концентрацию";
        public string Description { get; } = "";
        public ITest CreateTest(int randomSeed)
        {
            ITest test = new TextTask();
            Random random = new Random(randomSeed);

            double luqMass = random.Next(20, 50);
            double saltMass = random.Next(10, (int)(luqMass - 5));
            double addSalt = random.Next(5, 20);

            string textTask = $"В {luqMass} г раствора содержится {saltMass} г соли. Если в этот раствор добавить {addSalt} г соли, то получим раствор с концентрацией \\(<in1> \\over <in2>\\)";

            test.Text = textTask;

            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);

            double luqMass = random.Next(20, 50);
            double saltMass = random.Next(10, (int)(luqMass - 5));
            double addSalt = random.Next(5, 20);

            try
            {
                double in1 = Convert.ToDouble(answers.FirstOrDefault(pair => pair.Key == "in1").Value);
                double in2 = Convert.ToDouble(answers.FirstOrDefault(pair => pair.Key == "in2").Value);
                return Math.Abs(in1 / in2 - (saltMass + addSalt) / (addSalt + luqMass)) < 0.001 ? 2 : 0;
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        public string Text { get; set; }
        public string[] Inputs { get; set; }
        public List<Image> Pictures { get; set; }
    }
}
