using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Test_Wrapper;

namespace UserTest
{
    public class ComplexPlaneDivTest : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
    {
        int answer1, answer2, answer3, answer4, answer5, answer6;

        public int TestID { get; set; }
        public string GroupName { get; set; } = "Complex Numbers";
        public string Name { get; } = "Деление комплексных чисел в комплексной плоскости";
        public string Description { get; } = "Тест на определение параметров векторов на комплексной плоскости";

        public class Data
        {
            public string LetterR = "ABCDFGHKMPRSUVWXYZ"; //"abcbdfghkmprsuwxyz";
            /// </summary>
            public int[] answ = new int[16];
            public int[] Aa = new int[8];
            public int[] Ab = new int[8];
            public int[] Ac = new int[5];
            //public int[] Ad = new int[3];
            //public int[] FractA = new int[2];
            public int n = 10;
            public int[] Numbers = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //public int[] Numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            public int i, j, k;
            public string VectA = "";
            public string VectB = "";
            // public int[] MatrB = new int[6];
            // public int[] MatrC = new int[6];
            // public int[] MatrD = new int[6];
            // public int iA, jA, iB, jB, iCx, jCx, iCy, jCy, Vvv, Vvy;
            // public string ElemMatrA = "";
            // public string ElemMatrB = "";
            // public string ElemMatrC = "";
            // public string MCa = "";
            // public string MCb = "";
            // public string MCc = "";
            // public string IndAa = "";
            // public string IndAb = "";
            // public string IndAc = "";
            // public string IndBa = "";
            // public string IndBb = "";
            // public string IndBc = "";
            // public string IndexC = "";
            // public string MatrEqA = "";
            // public int alpha;
            // public int beta;

            public Data(Random random)
            {
                CreateVectors(random);
            }
            public void CreateVectors(Random random)
            {
                int letterCount = LetterR.Length;
                VectA = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(VectA, "");
                VectA = VectA.ToLower();
                letterCount = LetterR.Length;
                VectB = $"{LetterR[random.Next(letterCount)]}";
                LetterR = LetterR.Replace(VectB, "");
                VectB = VectB.ToLower();
            }
        }
        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new ComplexPlaneDivTest();
            Data data = new Data(random);


            int Ax = random.Next(1, 7);
            int Ay = random.Next(1, 24);
            int By = 6;
            for(;(By==6)||(By==12)||(By==18)||(By==(24-2*Ay));){
                By = random.Next(1, 24);
            }

            int Bx=-1;

            if (Ax == 1)
            {
                Bx = random.Next(2, 7); 
            }
            else if (Ax == 2)
            {
                Bx = random.Next(2, 4); 
            }
            else if (Ax == 3)
            {
                Bx = random.Next(1, 3); 
            }
            else if (Ax >= 4 && Ax <= 6)
            {
                Bx = 1;
            }
           

            int Cx = Ax * Bx;
            int Cy = Ay + By < 24 ? Ay + By : Ay + By - 24;
            answer1 = Bx;
            answer2 = By;
           
             
      
    Bitmap img = new Bitmap(650, 650);
    using (Graphics graphics = Graphics.FromImage(img))
    {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.Clear(Color.White);

        int centerX = 325;
        int centerY = 325;
        int maxRadius = 300;
        int labelRadius = 320; 

        
        using (Pen axisPen = new Pen(Color.Black, 2))
        {
            graphics.DrawLine(axisPen, centerX, 0, centerX, 650);
            graphics.DrawLine(axisPen, 0, centerY, 650, centerY);
        }

        
        for (int i = 0; i < 6; i++)
        {
            int radius = 50 * (i + 1);
            
            
            using (Pen circlePen = new Pen(Color.LightGray, 1))
            {
                graphics.DrawEllipse(circlePen, 
                    centerX - radius, 
                    centerY - radius, 
                    2 * radius, 
                    2 * radius);
            }

           
            using (Font numberFont = new Font("Arial", 12, FontStyle.Bold))
            using (Brush numberBrush = new SolidBrush(Color.Black))
            {
          
                PointF numberPos = new PointF(
                    centerX + radius + 3,  
                    centerY +10            
                );
                
                graphics.DrawString(
                    (i + 1).ToString(), 
                    numberFont, 
                    numberBrush, 
                    numberPos);
            }
        }


       
        int numberOfStripes = 24;
        double angleStep = 360.0 / numberOfStripes;
        using (Pen stripePen = new Pen(Color.FromArgb(50, Color.Blue), 1))
        using (Font labelFont = new Font("Arial", 10, FontStyle.Bold))
        using (Brush textBrush = new SolidBrush(Color.DarkBlue))
        {
            for (int i = 0; i < numberOfStripes; i++)
            {
                double angle = angleStep * i;
                double radians = angle * Math.PI / 180.0;
                
              
                int endX = centerX + (int)(maxRadius * Math.Cos(radians));
                int endY = centerY - (int)(maxRadius * Math.Sin(radians));
                graphics.DrawLine(stripePen, centerX, centerY, endX, endY);

               
                int labelX = centerX + (int)(labelRadius * Math.Cos(radians));
                int labelY = centerY - (int)(labelRadius * Math.Sin(radians));
                
          
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

              
                int label = i;
                int offsetX = 0, offsetY = 0;
                
                if (i == 0 || i == 6 || i == 12 || i == 18)
                {
                    offsetX = (i == 0 || i == 12) ? 0 : (i == 6 ? 15 : -15);
                    offsetY = (i == 0 || i == 12) ? (i == 0 ? -15 : 15) : 0;
                }

                graphics.TranslateTransform(labelX + offsetX, labelY + offsetY);
                graphics.RotateTransform((float)-angle);
                graphics.DrawString(
                    $"{label}", 
                    labelFont, 
                    textBrush, 
                    0, 0, 
                    format);
                graphics.ResetTransform();
            }
        }

  
        int vectorScale = 15;
        using (Pen vectorPen = new Pen(Color.Red, 3) { 
                   CustomEndCap = new AdjustableArrowCap(6, 6) 
               })
        {
            
            Point endB = new Point(
                centerX + (int)(Cx*50 * Math.Cos((15.0*Cy)* Math.PI / 180.0)),
                centerY - (int)(Cx*50 * Math.Sin((15.0*Cy)* Math.PI / 180.0))
            );
            graphics.DrawLine(vectorPen, centerX, centerY, endB.X, endB.Y);

          
            Point endA = new Point(
                centerX + (int)(Ax*50 * Math.Cos((15.0*Ay)* Math.PI / 180.0)),
                centerY - (int)(Ax*50 * Math.Sin((15.0*Ay)* Math.PI / 180.0))
            );
            graphics.DrawLine(vectorPen, centerX, centerY, endA.X, endA.Y);

            
            using (Font labelFont = new Font("Arial", 12, FontStyle.Bold))
            using (Brush textBrush = new SolidBrush(Color.DarkRed))
            {
               
                PointF labelPosB = new PointF(
                    endB.X + (endB.X > centerX ? 5 : -35),
                    endB.Y + (endB.Y < centerY ? -25 : 5)
                );
                graphics.DrawString($"{data.VectB}", labelFont, textBrush, labelPosB);

          
                PointF labelPosA = new PointF(
                    endA.X ,
                    endA.Y 
                );
                
               
                
                graphics.DrawString($"{data.VectA}", labelFont, textBrush, labelPosA);
            }
        }
    }

    //test.Pictures.Add(img);


            string questionText =
                $"На комплексной плоскости с изображенной полярной осью отношение " +// частное чисел " +
                $"\\(\\displaystyle\\frac{{ {data.VectB} }}{{ {data.VectA} }} \\) " +
                //$"(векторов комплексной плоскости) \\(\\overrightarrow{{ {data.VectB} }}\\) и " +
                //$" \\(\\overrightarrow{{ {data.VectA} }}\\) " +
                $"имеет направление, обозначенное числом " +
                //$"\\(\\begin{{array}}{{r}}  <ans[0]:3> \\end{{array}}\\)" +
                $"\\( <ans[0]:3> \\)" +
                $" и имеет длину" +
                //$"\\(\\begin{{array}}{{r}}  <ans[1]:3> \\end{{array}}\\)"+
                $"\\( <ans[1]:3> \\)" +
                $"."; //{answer2},{answer1}";
            test.Text = questionText;
            return test;
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            int total = 0;
            Random random = new Random(randomSeed);
            Data data = new Data(random);
            if (answers.TryGetValue("ans[0]", out string cx) && cx == answer2.ToString()) total++;
            if (answers.TryGetValue("ans[1]", out string cy) && cy == answer1.ToString()) total++;
            return total;
        }

        public string Text { get; set; }
        
        public string[] CheckBoxes { get; set; }

        public List<MemoryStream> Pictures { get; set; }

        //public List<Image> Pictures { get; set; } = new List<Image>();
        public int TimeLimitSeconds { get; set; } = 60;
        public bool IsHidden { get; set; } = false;
    }
}
