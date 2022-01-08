using System;
using System.Collections.Generic;
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

            string textTask = $"В {luqMass} г раствора содержится {saltMass} г соли. Если в этот раствор добавить {addSalt} г соли, то получим раствор с концентрацией ";

            ITestBox box1 = new SimpleTestBox
            {
                Texts = new List<string> { textTask },
                Numbers = new List<double> { saltMass + addSalt }
            };

            ITestBox box2 = new SimpleTestBox
            {
                Texts = new List<string> { "---------" },
                Numbers = new List<double> { addSalt + luqMass }
            };

            test.Boxes = new List<ITestBox> { box1, box2 };

            return test;
        }

        public List<ITestBox> Boxes { get; set; } = new();
    }
}
