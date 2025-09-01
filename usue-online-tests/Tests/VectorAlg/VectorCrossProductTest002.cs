using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Test_Wrapper;

namespace UserTest
{
    /// <summary>
    /// Тест на геометрическое вычисление векторного произведения.
    /// </summary>
    public class VectorCrossProductTest002 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        // Свойства теста
        public int TestID { get; set; }
        public string GroupName { get; set; } = "Analytic Geometry and Vectors"; // Название группы тестов
        public string Name { get; } = "Геометрическое вычисление векторного произведения 002"; // Название теста
        public string Description { get; } = "Упражнение по векторной алгебре"; // Описание теста

        /// <summary>
        /// Внутренний класс для хранения исходных данных теста.
        /// </summary>
        public class Data
        {
            // Списки буквенных меток и числовых значений сторон
            public List<string> LetterA = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "K", "M", "N", "P", "Q", "R", "U", "V", "W" };
            public List<int> NumberA = new List<int> { 10, 20, 40, 50 };
            //public List<int> NumberA = new List<int> {  40 };
            // Названия вершин параллелепипеда
            public string PAa, PBa, PCa, PDa, PAb, PBb, PCb, PDb, PU,PV,PW;
            // Длины рёбер
            public int LAaBa, LAaCa, LAaAb;
            // Координаты результирующих векторных произведений
            public int AaBaAaCaX, AaBaAaCaY, AaBaAaCaZ;
            public int AaBaAaAbX, AaBaAaAbY, AaBaAaAbZ;
            public int AaCaAaAbX, AaCaAaAbY, AaCaAaAbZ;

            /// <summary>
            /// Генерация случайных данных на основе переданного генератора случайных чисел.
            /// </summary>
            public Data(Random random)
            {
                // Выбор разных букв для каждой вершины (с удалением уже выбранных)
                PAa = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PAa);
                
                PBa = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PBa);
                
                PCa = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PCa);
                
                PDa = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PDa);
                
                PAb = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PAb);
                
                PBb = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PBb);
                
                PCb = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PCb);
                
                PDb = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PDb);
                PU = LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PU);
                PV= LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PV);
                PW= LetterA[random.Next(LetterA.Count)];
                LetterA.Remove(PW);
                // Выбор длин сторон
                LAaBa = NumberA[random.Next(NumberA.Count)];
                LAaCa = NumberA[random.Next(NumberA.Count)];
                LAaAb = NumberA[random.Next(NumberA.Count)];
                Console.WriteLine($"Случайно выбранные длины рёбер:");
                Console.WriteLine($" (LAaBa) = {LAaBa}");
                Console.WriteLine($" (LAaCa) = {LAaCa}");
                Console.WriteLine($" (LAaAb) = {LAaAb}");
                // Вычисление компонентов векторных произведений
                AaBaAaCaX = 0;
                AaBaAaCaY = 0;
                AaBaAaCaZ = (LAaBa/10) * (LAaCa/10) / (LAaAb/10);

                AaBaAaAbX = 0;
                AaBaAaAbZ = 0;
                AaBaAaAbY = -(LAaBa/10) * (LAaAb/10) / (LAaCa/10);

                AaCaAaAbY = 0;
                AaCaAaAbZ = 0;
                AaCaAaAbX = (LAaCa/10) * (LAaAb/10) / (LAaBa/10);
            }
        }

        /// <summary>
        /// Метод создания теста.
        /// </summary>
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new VectorCrossProductTest();
            Data data = new Data(random);

            // Определение начальных векторов на основе размеров параллелепипеда
             int[] vectorD= { data.LAaBa, data.LAaCa/2, 0 }; // Вектор AaBa направлен вдоль оси X
             int[] vectorE = { data.LAaBa/2, 0,data.LAaAb  }; // Вектор AaCa вдоль оси Y
             int[] vectorF = { 0,  data.LAaCa/2, data.LAaAb }; // Вектор AaAb вдоль оси Z

             int[] vectorA = { data.LAaBa, 0, 0 }; // Вектор AaBa направлен вдоль оси X
             int[] vectorB = { 0, data.LAaCa, 0 }; // Вектор AaCa вдоль оси Y
             int[] vectorC = { 0, 0, data.LAaAb }; // Вектор AaAb вдоль оси Z
            
            // Вычисляем векторные произведения
            int[] crossAB = CrossProduct(vectorA, vectorB);
            int[] crossAC = CrossProduct(vectorA, vectorC);
            int[] crossBC = CrossProduct(vectorB, vectorC);

            // Создание изображения параллелепипеда
            Bitmap img = new Bitmap(1000, 1000);
using (Graphics graphics = Graphics.FromImage(img))
{
    graphics.SmoothingMode = SmoothingMode.AntiAlias;
    graphics.Clear(Color.White);

    int width = data.LAaBa * 9;
    int height = data.LAaCa * 9;
    int depth = data.LAaAb * 9;
    int xOffset = 250;
    int yOffset = 260;

    Point[] frontFace = new Point[]
    {
        new Point(xOffset, yOffset),
        new Point(xOffset + width, yOffset+ width/4),
        new Point(xOffset + width, yOffset + height+ width/4),
        new Point(xOffset, yOffset + height)
    };

    Point[] backFace = new Point[]
    {
        new Point(xOffset + depth/2, yOffset - depth/2),
        new Point(xOffset + width + depth/2, yOffset - depth/2+ width/4),
        new Point(xOffset + width + depth/2, yOffset + height - depth/2+ width/4),
        new Point(xOffset + depth/2, yOffset + height - depth/2)
    };
    
    Point vectorCStart = frontFace[3];
    Point vectorCEnd = new Point(vectorCStart.X + (vectorC[0]+vectorC[2]/2  )*9, vectorCStart.Y - (vectorC[1]+vectorC[2]/2  )*9);
        using (Pen vectorCPen = new Pen(Color.DarkGreen, 3) { CustomEndCap = new AdjustableArrowCap(6, 6) }){
           
        graphics.DrawLine(vectorCPen, vectorCStart, vectorCEnd);
        }
        
        
        
        
        
        
        
        // Векторы
       
        Point vectorEStart = frontFace[3];
        Point vectorEEnd = new Point(vectorEStart.X + (vectorE[0]+vectorE[2]/2 ) * 9, vectorEStart.Y - (vectorE[1]+vectorE[2]/2 ) * 9+ width/8);
        Point vectorFStart = frontFace[3];
        Point vectorFEnd = new Point(vectorFStart.X + (vectorF[0]+vectorF[2]/2 ) * 9, vectorFStart.Y - (vectorF[1]+vectorF[2]/2 ) * 9);
        
        using (Pen vectorAPen = new Pen(Color.FromArgb(200, 128, 0, 128), 3) { CustomEndCap = new AdjustableArrowCap(6, 6) })
        using (Pen vectorEPen = new Pen(Color.FromArgb(200, 255, 190, 0), 3) { CustomEndCap = new AdjustableArrowCap(6, 6),DashStyle = DashStyle.Dash })
        using (Pen vectorFPen = new Pen(Color.FromArgb(200, 144, 238, 144), 3) { CustomEndCap = new AdjustableArrowCap(6, 6),DashStyle = DashStyle.Dash })
        {
            
            
            
            graphics.DrawLine(vectorEPen, vectorEStart, vectorEEnd);
            graphics.DrawLine(vectorFPen, vectorFStart, vectorFEnd);
           

            
        }
        
        
        
        
        
        
        
        
    using (Pen frontPenBlue = new Pen(Color.Blue, 2))
    using (Pen backPenBlue = new Pen(Color.Blue, 2) { DashStyle = DashStyle.Dash })
    using (Pen backPenGreen = new Pen(Color.Green, 2) { DashStyle = DashStyle.Dash }) 
    using (Pen frontPenGreen = new Pen(Color.Green, 2))
    using (Pen backPenRed = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dash }) 
    using (Pen frontPenRed = new Pen(Color.Red, 2))
    
    using (Pen sidePen = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dash })
    using (Font labelFont = new Font("Arial", 12, FontStyle.Bold))
    using (Brush labelBrush = new SolidBrush(Color.Black))
    {
        // Передняя грань (сплошными линиями)
        for (int i = 0; i < 4; i++)
        {
            graphics.DrawLine(frontPenBlue, frontFace[i], frontFace[(i + 1) % 4]);
        }

        // Задняя грань (пунктирными линиями)
        graphics.DrawLine(frontPenGreen, backFace[1], backFace[(1 + 1) % 4]);
        graphics.DrawLine(frontPenGreen, backFace[0], backFace[(0 + 1) % 4]);
        graphics.DrawLine(backPenGreen, backFace[2], backFace[(2 + 1) % 4]);
        graphics.DrawLine(backPenGreen, backFace[3], backFace[(3 + 1) % 4]);
        

        // Соединяющие линии между передней и задней гранью (тоже пунктирные)
        graphics.DrawLine(frontPenRed, frontFace[0], backFace[0]);
        graphics.DrawLine(frontPenRed, frontFace[1], backFace[1]);
        graphics.DrawLine(frontPenRed, frontFace[2], backFace[2]);
        graphics.DrawLine(sidePen, frontFace[3], backFace[3]);
        

        // Подписи вершин
        graphics.DrawString(data.PAb, labelFont, labelBrush, frontFace[0].X - 20, frontFace[0].Y - 20);//
        graphics.DrawString(data.PBb, labelFont, labelBrush, frontFace[1].X + 10, frontFace[1].Y - 20);//
        graphics.DrawString(data.PBa, labelFont, labelBrush, frontFace[2].X + 10, frontFace[2].Y + 10);//
        graphics.DrawString(data.PAa, labelFont, labelBrush, frontFace[3].X - 20, frontFace[3].Y + 10);//

        graphics.DrawString(data.PCb, labelFont, labelBrush, backFace[0].X - 20, backFace[0].Y - 20);//
        graphics.DrawString(data.PDb, labelFont, labelBrush, backFace[1].X + 10, backFace[1].Y - 20);//
        graphics.DrawString(data.PDa, labelFont, labelBrush, backFace[2].X + 10, backFace[2].Y + 10);//
        graphics.DrawString(data.PCa, labelFont, labelBrush, backFace[3].X - 20, backFace[3].Y + 10);//

        // Векторы
        Point vectorAStart = frontFace[3];
        Point vectorAEnd = new Point(vectorAStart.X + vectorA[0] * 9, vectorAStart.Y - vectorA[1] * 9+ width/4);
        Point vectorBStart = frontFace[3];
        Point vectorBEnd = new Point(vectorBStart.X + vectorB[0] * 9, vectorBStart.Y - vectorB[1] * 9);
        
        Point vectorDStart = frontFace[3];
        Point vectorDEnd = new Point(vectorAStart.X +(vectorD[0]+vectorD[2]/3 ) * 9, vectorAStart.Y -(vectorD[1]+vectorD[2]/3 ) * 9+ width/4
            
            );
        
        using (Pen vectorAPen = new Pen(Color.Purple, 3) { CustomEndCap = new AdjustableArrowCap(6, 6) })
        using (Pen vectorBPen = new Pen(Color.Orange, 3) { CustomEndCap = new AdjustableArrowCap(6, 6) })
        using (Pen vectorDPen = new Pen(Color.FromArgb(200, 146,110,174), 3) { CustomEndCap = new AdjustableArrowCap(6, 6) })
        {
            graphics.DrawLine(vectorAPen, vectorAStart, vectorAEnd);
            graphics.DrawLine(vectorBPen, vectorBStart, vectorBEnd);
            
            graphics.DrawLine(vectorDPen, vectorDStart, vectorDEnd);
            
            graphics.DrawString("f", labelFont, labelBrush, vectorFEnd.X - 15, vectorFEnd.Y - 19);
            graphics.DrawString("d", labelFont, labelBrush, vectorDEnd.X -15, vectorDEnd.Y - 19);
            
            
            graphics.DrawString(data.PV, labelFont, labelBrush, vectorFEnd.X + 15, vectorFEnd.Y - 19);
            graphics.DrawString(data.PW, labelFont, labelBrush, vectorDEnd.X +15, vectorDEnd.Y - 19);
            graphics.DrawString(data.PU, labelFont, labelBrush, vectorEEnd.X +15, vectorEEnd.Y - 19);
            
            graphics.DrawString("e", labelFont, labelBrush, vectorEEnd.X - 15, vectorEEnd.Y - 19);
            graphics.DrawString("a", labelFont, labelBrush, vectorAEnd.X - 15, vectorAEnd.Y - 19);
            graphics.DrawString("b", labelFont, labelBrush, vectorBEnd.X - 15, vectorBEnd.Y + 19);
            graphics.DrawString("c", labelFont, labelBrush, vectorCEnd.X - 15, vectorCEnd.Y - 19);
        }
    }
}

                
            

            test.Pictures.Add(img); // Добавляем картинку в тест

            // Формирование текста задания
            test.Text = $"В прямоугольном параллелепипеде {data.PAa}{data.PBa}{data.PCa}{data.PDa}{data.PAb}{data.PBb}{data.PCb}{data.PDb} " +
                        $"имеем $|a| = {(data.LAaBa/10)}$, $|c| =  {(data.LAaAb/10)}$, " +
                        $"$|b| ={(data.LAaCa/10)}$. Тогда:\n" +
                        $"1. Векторное произведение $[\\vec{{{data.PAa}{data.PU}}} \\times \\vec{{{data.PAa}{data.PCa}}}] $ = " +
                        $"\\(\\begin{{array}}{{r}} <ans[0]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PBa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[1]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PCa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[2]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PAb}}}$ \n" +
                        $"2. Векторное произведение $[\\vec{{{data.PAa}{data.PW}}} \\times \\vec{{{data.PAa}{data.PAb}}}]$ = " +
                        $"\\(\\begin{{array}}{{r}} <ans[4]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PBa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[5]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PCa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[6]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PAb}}}$ \n" +
                        $"3. Векторное произведение $[\\vec{{{data.PAa}{data.PV}}} \\times \\vec{{{data.PAa}{data.PAb}}}]$ = " +
                        $"\\(\\begin{{array}}{{r}} <ans[7]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PBa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[8]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PCa}}}$ + " +
                        $"\\(\\begin{{array}}{{r}} <ans[9]:3> \\\\ \\end{{array}}\\) * $\\vec{{{data.PAa}{data.PAb}}}$ \n" ;


            answer1 = data.AaBaAaCaX;
            answer2 = data.AaBaAaCaY;
            answer3 = data.AaBaAaCaZ;
            answer4 = data.AaBaAaAbX;
            answer5 = data.AaBaAaAbY;
            answer6 = data.AaBaAaAbZ;
            answer7 = data.AaCaAaAbX;
            answer8= data.AaCaAaAbY;
            answer9 = data.AaCaAaAbZ;


            
            
            Console.WriteLine("Векторное произведение [AaBa × AaCa]:");
            Console.WriteLine($"X = {data.AaBaAaCaX}, Y = {data.AaBaAaCaY}, Z = {data.AaBaAaCaZ}");

            Console.WriteLine("\nВекторное произведение [AaBa × AaAb]:");
            Console.WriteLine($"X = {data.AaBaAaAbX}, Y = {data.AaBaAaAbY}, Z = {data.AaBaAaAbZ}");

            Console.WriteLine("\nВекторное произведение [AaCa × AaAb]:");
            Console.WriteLine($"X = {data.AaCaAaAbX}, Y = {data.AaCaAaAbY}, Z = {data.AaCaAaAbZ}");
            return test;
        }

        /// <summary>
        /// Вычисление векторного произведения двух векторов.
        /// </summary>
        private int[] CrossProduct(int[] a, int[] b)
        {
            return new int[]
            {
                a[1] * b[2] - a[2] * b[1],
                a[2] * b[0] - a[0] * b[2],
                a[0] * b[1] - a[1] * b[0]
            };
        }

        // Правильные ответы
        private int answer1, answer2, answer3, answer4, answer5, answer6, answer7, answer8, answer9;
        private int answer1_1, answer2_1, answer3_1, answer4_1, answer5_1, answer6_1, answer7_1, answer8_1, answer9_1;
        /// <summary>
        /// Проверка ответов пользователя.
        /// </summary>
        /*
        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            if (answers.TryGetValue("ans[0]", out string ans1) && ans1 == answer1.ToString()) total++;
            if (answers.TryGetValue("ans[1]", out string ans2) && ans2 == answer2.ToString()) total++;
            if (answers.TryGetValue("ans[2]", out string ans3) && ans3 == answer3.ToString()) total++;
            
            if (answers.TryGetValue("ans[3]", out string ans4) && ans4 == answer4.ToString()) total++;
            if (answers.TryGetValue("ans[4]", out string ans5) && ans5 == answer5.ToString()) total++;
            if (answers.TryGetValue("ans[5]", out string ans6) && ans6 == answer6.ToString()) total++;
            
            if (answers.TryGetValue("ans[6]", out string ans7) && ans7 == answer7.ToString()) total++;
            if (answers.TryGetValue("ans[7]", out string ans8) && ans8 == answer8.ToString()) total++;
            if (answers.TryGetValue("ans[8]", out string ans9) && ans9 == answer9.ToString()) total++;
            return total;
        }
        */

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        
            {
                int total = 0;

                if (answers.TryGetValue("ans[0]", out string ans1))
                {
                                  //  Console.WriteLine($"Проверка ans[0]: введено = {ans1}, ожидается = {answer1}");
                    if (ans1 == answer1.ToString()) total++;
                }

                if (answers.TryGetValue("ans[1]", out string ans2))
                {
                    //Console.WriteLine($"Проверка ans[1]: введено = {ans2}, ожидается = {answer2}");
                    if (ans2 == answer2.ToString()) total++;
                }

                if (answers.TryGetValue("ans[2]", out string ans3))
                {
                    //Console.WriteLine($"Проверка ans[2]: введено = {ans3}, ожидается = {answer3}");
                    if (ans3 == answer3.ToString()) total++;
                }

                if (answers.TryGetValue("ans[3]", out string ans4))
                {
                   // Console.WriteLine($"Проверка ans[3]: введено = {ans4}, ожидается = {answer4}");
                    if (ans4 == answer4.ToString()) total++;
                }

                if (answers.TryGetValue("ans[4]", out string ans5))
                {
                  //  Console.WriteLine($"Проверка ans[4]: введено = {ans5}, ожидается = {answer5}");
                    if (ans5 == answer5.ToString()) total++;
                }

                if (answers.TryGetValue("ans[5]", out string ans6))
                {
                    //Console.WriteLine($"Проверка ans[5]: введено = {ans6}, ожидается = {answer6}");
                    if (ans6 == answer6.ToString()) total++;
                }

                if (answers.TryGetValue("ans[6]", out string ans7))
                {
                    //Console.WriteLine($"Проверка ans[6]: введено = {ans7}, ожидается = {answer7}");
                    if (ans7 == answer7.ToString()) total++;
                }

                if (answers.TryGetValue("ans[7]", out string ans8))
                {
                   // Console.WriteLine($"Проверка ans[7]: введено = {ans8}, ожидается = {answer8}");
                    if (ans8 == answer8.ToString()) total++;
                }

                if (answers.TryGetValue("ans[8]", out string ans9))
                {
                  //  Console.WriteLine($"Проверка ans[8]: введено = {ans9}, ожидается = {answer9}");
                    if (ans9 == answer9.ToString()) total++;
                }

               // Console.WriteLine($"Итого правильных ответов: {total} из 9");

                return total;
            }
        
        // Дополнительные свойства теста
        public string Text { get; set; }
        public string[] CheckBoxes { get; set; }
        public List<Image> Pictures { get; set; } = new List<Image>();
        public int TimeLimitSeconds { get; set; } = 120; // Ограничение по времени
        public bool IsHidden { get; set; } = false; // Видимость теста
    }
}
