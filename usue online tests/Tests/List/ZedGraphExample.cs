using System;
using System.Collections.Generic;
using System.Drawing;
using Test_Wrapper;
using ZedGraph;

namespace usue_online_tests.Tests.List
{
    public class ZedGraphExample : ITestCreator, ITest, ITimeLimit
    {
        public int TestID { get; set; }
        public string Name { get; } = "Пример создания графиков";
        public string Description { get; } = "ZedGraph";
        public ITest CreateTest(int randomSeed)
        {
            MasterPane masterPane = new MasterPane("Заголовок", new RectangleF(0, 0, 1000, 1000));

            GraphPane graphPane = new GraphPane(new RectangleF(0, 0, 1000, 500), "График функции sin", "ось x", "ось y");
            AddCurve(graphPane, new Random(randomSeed));

            GraphPane secPane = new GraphPane(new RectangleF(0, 500, 1000, 500), "График функции sin с помехами", "ось x", "ось y");
            AddCurve(secPane, new Random(randomSeed), true);

            GraphScaleSettings(graphPane);
            GraphScaleSettings(secPane);

            masterPane.Add(graphPane);
            masterPane.Add(secPane);

            return new ZedGraphExample
            {
                Text = string.Empty,
                Pictures = new List<Image> { masterPane.GetImage(true) }
            };
        }

        private static void GraphScaleSettings(GraphPane graphPane)
        {
            graphPane.XAxis.Scale.Max = 20;
            graphPane.XAxis.Scale.MajorStep = 0.5;

            graphPane.YAxis.Scale.Min = -1;
            graphPane.YAxis.Scale.MajorStep = 0.5;
            graphPane.YAxis.Scale.MinorStep = 0.1;
        }

        private static void AddCurve(GraphPane graphPane, Random rand, bool random = false)
        {
            double[] x = new double[200];
            double[] y = new double[200];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = 0.1 * i;
                y[i] = random ? Math.Sin(x[i]) * rand.NextDouble() : Math.Sin(x[i]);
            }

            graphPane.AddCurve("sin(x)", x, y, Color.FromArgb(rand.Next(50, 255), rand.Next(50, 255), rand.Next(50, 255)));
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            throw new NotImplementedException();
        }

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; }
        public int TimeLimitSeconds { get; set; } = 20;
    }
}
