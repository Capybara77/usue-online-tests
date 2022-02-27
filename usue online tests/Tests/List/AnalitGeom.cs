using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class AnalitGeom : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Аналитическая геометрия";
        public string Description { get; } = "Упражнение по аналитической геометрии";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
        public ITest CreateTest(int randomSeed)
        {
            //randomSeed = 1028797245;
            Random random = new Random(randomSeed);
            ITest test = new AnalitGeom();

            int startV = random.Next(126, 131);
            int par1, par2;
            if (random.Next(0, 2) == 0)
            {
                par1 = 1;
                par2 = -2;
            }
            else
            {
                par1 = 2;
                par2 = -1;
            }

            int positionYV1 = random.Next(0, 9);
            int positionXV1 = random.Next(0, 9);

            int positionYV2 = random.Next(0, 7);
            int positionXV2 = random.Next(0, 7);

            if (positionYV2 == 3 && positionXV2 == 3) positionYV2 = 5;

            Image img = Image.FromFile(Environment.CurrentDirectory + "\\wwwroot\\generators\\alalitGeom.png");

            Graphics graphics = Graphics.FromImage(img);
            //darw ellipse
            graphics.DrawEllipse(new Pen(Color.Blue, 4), (startV - 120) * 40, 280, 40, 40);

            //draw line
            Pen pen = new Pen(Color.Green, 5);
            pen.EndCap = LineCap.ArrowAnchor;
            graphics.DrawLine(pen, positionXV1 * 40 - 20 + 40 * 3, positionYV1 * 40 - 20 + 40 * 4,
                (positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3, (positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4);
            test.Pictures.Add(img);

            test.Text = $"Прямая задана параметрическими уравнениями с начальной точкой с номером {startV}," +
                        $" и направляющим вектором \\(\\overline{{p}}\\). Тогда значению параметра {par1} соответствует точка с" +
                        $" номером \\(<point1>\\), а значению параметра {par2} соответствует точка с номером \\(<point2>\\)";


            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);

            int total = 0;
            int startV = random.Next(126, 131);
            int par1, par2;
            if (random.Next(0, 2) == 0)
            {
                par1 = 1;
                par2 = -2;
            }
            else
            {
                par1 = 2;
                par2 = -1;
            }

            int positionYV1 = random.Next(0, 9);
            int positionXV1 = random.Next(0, 9);

            int positionYV2 = random.Next(0, 7);
            int positionXV2 = random.Next(0, 7);

            if (positionYV2 == 0 && positionXV2 == 0) positionYV2 = 5;

            try
            {
                int point = Convert.ToInt32(answers["point1"]);
                int x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
                int y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
                if (point == (startV + (x * par1) + (y * par1 * 17))) total++;
            }
            catch
            {
            }

            try
            {
                int point = Convert.ToInt32(answers["point2"]);
                int x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
                int y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
                if (point == (startV + (x * par2) + (y * par2 * 17))) total++;
            }
            catch
            {
            }

            return total;
        }

        public string Text { get; set; }
        public List<Image> Pictures { get; set; } = new List<Image>();
    }
}
