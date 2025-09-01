using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Test_Wrapper;

namespace UserTest
{
    public class AnalyticGeometryProjectionTest : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        int answerCx;
        int answerCy;
        int coefA, coefB, coefC;

        public int TestID { get; set; }
        public string GroupName { get; set; } = "Analytic Geometry and Vectors";
        public string Name { get; } = "Проекция точки на прямую, заданную общим уравнением";
        public string Description { get; } = "Упражнение по аналитической геометрии";

        public class Data
        {
            public string Aname, Bname, Dname;
            public int Ax, Ay, Bx, By, Cx, Cy, Dx, Dy, Vx, Vy, Dd;

            public Data(Random random)
            {
                string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "K", "M", "N", "P", "Q", "R", "U", "V", "W" };
                
                Aname = letters[random.Next(letters.Length)];
                Bname = letters[random.Next(letters.Length)];
                while (Bname == Aname)
                    Bname = letters[random.Next(letters.Length)];
                
                Dname = letters[random.Next(letters.Length)];
                while (Dname == Aname || Dname == Bname)
                    Dname = letters[random.Next(letters.Length)];

                int[] numbers = { -3, -2, -1, 1, 2, 3 };
                
                bool validPoints = false;
                while (!validPoints)
                {
                   
                    Cx = numbers[random.Next(numbers.Length)];
                    Cy = numbers[random.Next(numbers.Length)];

                 
                    Vx = numbers[random.Next(numbers.Length)];
                    Vy = numbers[random.Next(numbers.Length)];
                    while (Vy == Vx)
                        Vy = numbers[random.Next(numbers.Length)];

                    int nod = GCD(Math.Abs(Vx), Math.Abs(Vy));
                    Vx /= nod;
                    Vy /= nod;

                    
                    int BCk = numbers[random.Next(numbers.Length)];
                    int CAk = numbers[random.Next(numbers.Length)];

                    Ax = Cx + BCk * (-Vy);
                    Ay = Cy + BCk * (Vx);
                    Bx = Cx + CAk * (Vx);
                    By = Cy + CAk * (Vy);

                   
                    Dx = numbers[random.Next(numbers.Length)];
                    Dy = numbers[random.Next(numbers.Length)];

                    
                    validPoints = IsPointInBounds(Ax, Ay) && 
                                IsPointInBounds(Bx, By) && 
                                IsPointInBounds(Cx, Cy) && 
                                IsPointInBounds(Dx, Dy) && 
                                IsPointInBounds(Cx + Vx, Cy + Vy) &&
                                IsPointInBounds(Bx + Vx, By + Vy);
                }

                Dd = -(Vx * Ax + Vy * Ay);
            }

            private bool IsPointInBounds(int x, int y)
            {
                return x >= -6 && x <= 6 && y >= -6 && y <= 6;
            }

            private int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int temp = a % b;
                    a = b;
                    b = temp;
                }
                return a;
            }
        }

        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new AnalyticGeometryProjectionTest();
            Data data = new Data(random);

            answerCx = data.Cx;
            answerCy = data.Cy;
            coefA = data.Vx;
            coefB = data.Vy;
            coefC = data.Dd;

            
            // Console.WriteLine($"1. Уравнение прямой через точку {data.Aname}:");
            // Console.WriteLine($"{data.Vx}(x - {data.Ax}) + {data.Vy}(y - {data.Ay}) = 0");
            // Console.WriteLine($"2. Приведенное уравнение:");
            // Console.WriteLine($"{data.Vx}x + {data.Vy}y + {data.Dd} = 0");
            // Console.WriteLine($"3. Проекция точки {data.Bname}:");
            // Console.WriteLine($"Cx = {data.Cx}, Cy = {data.Cy}");
            

            Bitmap img = new Bitmap(600, 600);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                int centerX = 300;
                int centerY = 300;
                int scale = 50;

                Pen gridPen = new Pen(Color.FromArgb(240, 240, 240), 1);
                for (int i = 0; i <= 600; i += scale)
                {
                    g.DrawLine(gridPen, i, 0, i, 600);
                    g.DrawLine(gridPen, 0, i, 600, i);
                }
                Pen axisPen = new Pen(Color.FromArgb(100, 100, 100), 2);
                g.DrawLine(axisPen, 0, centerY, 600, centerY);
                g.DrawLine(axisPen, centerX, 0, centerX, 600);
                Font axisFont = new Font("Arial", 12, FontStyle.Bold);
                g.DrawString("X", axisFont, Brushes.Black, centerX + 280, centerY + 20);
                g.DrawString("Y", axisFont, Brushes.Black, centerX - 20, centerY - 280);
                
                Font font = new Font("Arial", 10);
                for (int i = -6; i <= 6; i++)
                {
                    int x = centerX + i * scale;
                    int y = centerY - i * scale;
                    g.DrawLine(axisPen, x, centerY - 3, x, centerY + 3);
                    g.DrawLine(axisPen, centerX - 3, y, centerX + 3, y);
                    if (i != 0)
                    {
                        g.DrawString(i.ToString(), font, Brushes.Black, x - 10, centerY + 5);
                        g.DrawString(i.ToString(), font, Brushes.Black, centerX + 5, y - 10);
                    }
                }

                
                g.FillEllipse(Brushes.Black, centerX + data.Ax * scale - 4, centerY - data.Ay * scale - 4, 8, 8);
                g.DrawString($"{data.Aname} ({data.Ax},{data.Ay})", font, Brushes.Black, 
                            centerX + data.Ax * scale + 5, centerY - data.Ay * scale - 20);

               
               
                g.FillEllipse(Brushes.Black, centerX + data.Dx * scale - 4, centerY - data.Dy * scale - 4, 8, 8);
                g.DrawString($"{data.Dname} ({data.Dx},{data.Dy})", font, Brushes.Black, 
                            centerX + data.Dx * scale + 5, centerY - data.Dy * scale - 20);

               
                Pen vectorPen = new Pen(Color.Black, 2);
                vectorPen.CustomEndCap = new AdjustableArrowCap(5, 5);
                PointF start = new PointF(centerX + data.Bx * scale, centerY - data.By * scale);
                PointF end = new PointF(centerX + (data.Bx + data.Vx) * scale, centerY - (data.By + data.Vy) * scale);
                g.DrawLine(vectorPen, start, end);
            }

            test.Pictures.Add(img);

            test.Text =
                $"Прямая, проходящая через точку {data.Aname} ортогонально изображённому вектору (из точки ), " +
                $"может быть задана уравнением (не сокращайте на общий множитель и не меняйте знак!):" +
                $"\\[" +
                $"\\begin{{array}}{{r}} <ans[5]:2> \\end{{array}} \\left(x - \\begin{{array}}{{r}} <ans[7]:2> \\end{{array}}\\right) + " +
                $"\\begin{{array}}{{r}} <ans[6]:2> \\end{{array}} \\left(y - \\begin{{array}}{{r}} <ans[8]:2> \\end{{array}}\\right) = 0" +
                $"\\]" +
                $"то есть, после приведения подобных слагаемых:" +
                $"\\[" +
                $"\\begin{{array}}{{r}} <ans[2]:2> \\end{{array}} x + " +
                $"\\begin{{array}}{{r}} <ans[3]:2> \\end{{array}} y + " +
                $"\\begin{{array}}{{r}} <ans[4]:2> \\end{{array}} = 0" +
                $"\\]" +
                $"Проекция точки \\( {data.Bname}\\) на эту прямую имеет координаты:" +
                $"\\[" +
                $"C_x = \\begin{{array}}{{r}} <ans[0]:3> \\end{{array}}, \\quad " +
                $"C_y = \\begin{{array}}{{r}} <ans[1]:3> \\end{{array}}" +
                $"\\]";

            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Data data = new Data(new Random(randomSeed));
            int score = 0;

            if (answers.TryGetValue("ans[0]", out string cx) && NormalizeSigns(cx) == data.Cx.ToString()) score++;
            if (answers.TryGetValue("ans[1]", out string cy) && NormalizeSigns(cy) == data.Cy.ToString()) score++;
            if (answers.TryGetValue("ans[2]", out string a) && NormalizeSigns(a) == data.Vx.ToString()) score++;
            if (answers.TryGetValue("ans[3]", out string b) && NormalizeSigns(b) == data.Vy.ToString()) score++;
            if (answers.TryGetValue("ans[4]", out string c) && NormalizeSigns(c) == data.Dd.ToString()) score++;
            if (answers.TryGetValue("ans[5]", out string vxa) && NormalizeSigns(vxa) == data.Vx.ToString()) score++;
            if (answers.TryGetValue("ans[6]", out string vya) && NormalizeSigns(vya) == data.Vy.ToString()) score++;
            if (answers.TryGetValue("ans[7]", out string ax) && NormalizeSigns(ax) == data.Ax.ToString()) score++;
            if (answers.TryGetValue("ans[8]", out string ay) && NormalizeSigns(ay) == data.Ay.ToString()) score++;

            return score;
        }

        private string NormalizeSigns(string input) =>
            input.Replace("--", "+").Replace("+-", "-").Replace("-+", "-").Replace("++", "+");

        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; } = new List<Image>();
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = false;
    }
}