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
        public string Name { get; } = "Текстовая задача про соль";
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

        public bool CheckAnswer(int randomSeed, string input, string value)
        {
            Random random = new Random(randomSeed);

            double luqMass = random.Next(20, 50);
            double saltMass = random.Next(10, (int)(luqMass - 5));
            double addSalt = random.Next(5, 20);

            switch (input)
            {
                case "in1":
                {
                    return Math.Abs(saltMass + addSalt - Convert.ToDouble(value)) < 0.0001;
                }
                case "in2":
                {
                    return Math.Abs(addSalt + luqMass - Convert.ToDouble(value)) < 0.0001;
                }
            }

            return false;
        }

        public string Text { get; set; }
        public string[] Inputs { get; set; }
        public List<Bitmap> Pictures { get; set; }
    }
}
