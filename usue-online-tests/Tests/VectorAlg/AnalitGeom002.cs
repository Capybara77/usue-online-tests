using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.IO;
using Test_Wrapper;

namespace usue_online_tests.Tests.VectorAlg2;

public class Analit3 : ITestCreator, ITest, ITimeLimit, IHidden, ITestGroup
{
    public int answer1;
    public int answer2;
    public int answer3;
    public int answer4;
    public int answer5;
    public int answer6;
    public int answer7;
    public int answer8;
    public int answer9;


    public int TestID { get; set; }
    public string GroupName { get; set; } = "Analitic Geometry and Vectors";
    public string Name { get; } = "Аналитическая геометрия 002";
    public string Description { get; } = "Упражнение по аналитической геометрии";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Analit3();

        Bitmap img = new Bitmap(510, 510);
        Graphics graphics = Graphics.FromImage(img);



        int size = 17;
        int gap = 30;

        int horizontalCount = 530 / gap;
        int verticalCount = 530 / gap;

        int xOffset = 15;
        int yOffset = 15;

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = 0; j < verticalCount; j++)
            {
                int x = i * gap + xOffset;
                int y = j * gap + yOffset;

                graphics.DrawLine(Pens.Black, x - size / 2, y, x + size / 2, y);

                graphics.DrawLine(Pens.Black, x, y - size / 2, x, y + size / 2);
            }
        }

        int centerX = img.Width / 2;
        int centerY = img.Height / 2;




        int sign = random.Next(0, 2) == 0 ? -1 : 1;
        int sign1 = random.Next(0, 2) == 0 ? -1 : 1;


        int vectorEndX, vectorEndY, vectorStartX, vectorStartY, vectorEndX2, vectorEndY2;
        int pointAx, pointAy, pointBx, pointBy;


        vectorStartX = centerX - 30 * random.Next(4, 7);
        vectorStartY = centerY - 30 * random.Next(4, 7);
        vectorEndX = vectorStartX + 30 * random.Next(2, 4);
        vectorEndY = vectorStartY - 30 * random.Next(2, 4);

        if (vectorStartY < 105)
            vectorEndY = vectorStartY - 30 * 2;



        var sign2 = sign == -1 ? 1 : -1;
        var sign3 = sign1 == -1 ? 1 : -1;

        pointAx = centerX - 30 * random.Next(2, 3);
        pointAy = centerY + 30 * random.Next(2, 3);

        pointBx = pointAx + Math.Abs(vectorEndX - vectorStartX);
        pointBy = pointAy - Math.Abs(vectorEndY - vectorStartY);

        answer1 = -8 + pointAx / 30;
        answer2 = 8 - pointAy / 30;
        answer3 = Math.Abs(vectorEndX - vectorStartX) / 30;
        answer4 = Math.Abs(vectorEndY - vectorStartY) / 30;

        answer9 = sign;

        if (sign == -1)
        {
            pointBx = pointAx;
            pointBy = pointAy ;
            pointAx = pointBx - Math.Abs(vectorEndX - vectorStartX);
            pointAy = pointBy + Math.Abs(vectorEndY - vectorStartY);
        }



        Font font = new Font("Arial", 15, FontStyle.Regular, GraphicsUnit.Pixel);
        Font font1 = new Font("Arial", 25, FontStyle.Italic, GraphicsUnit.Pixel);
        Font font2 = new Font("Arial", 23, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brush = Brushes.Black;
        Brush brush2 = Brushes.DeepPink;

        int offsetX = 0;
        int offsetY = 0;
        int offsetY_a;

        if (vectorEndY < 105)
        {
            offsetX = 15;
            offsetY = -20;
        }
        else if (vectorEndY > 255)
        {
            offsetY = -20;
            offsetX = 5;
        }

        for (int i = -1; i > -9; i--)
        {
            graphics.DrawString(i.ToString(), font, brush, centerX - 7 + 30 * i, centerY + 6);
            graphics.DrawString(i.ToString(), font, brush, centerY + 6, centerX - 7 - 30 * i);
        }

        graphics.DrawString(0.ToString(), font, brush, centerY + 3, centerX + 3);

        for (int i = 1; i < 9; i++)
        {
            graphics.DrawString(i.ToString(), font, brush, centerX - 7 + 30 * i, centerY + 6);
            graphics.DrawString(i.ToString(), font, brush, centerY + 6, centerX - 7 - 30 * i);
        }
        graphics.DrawString("x", font1, brush, 494, centerY - 30);
        graphics.DrawString("y", font1, brush, centerX - 25, -5);
        graphics.DrawString("v", font2, brush, vectorEndX + offsetX, vectorEndY + offsetY);

        if (sign == -1)
        {
            pointBx = pointAx;
            pointBy = pointAy;
            pointAx = pointBx - Math.Abs(vectorEndX - vectorStartX);
            pointAy = pointBy + Math.Abs(vectorEndY - vectorStartY);    
            answer1 = -8 + pointBx / 30;
            answer2 = 8 - pointBy / 30;
            graphics.DrawString("B", font2, brush, pointAx + offsetX, pointAy + offsetY);
            graphics.DrawString("A", font2, brush, pointBx + offsetX, pointBy + offsetY);
        }
        else
        {
            graphics.DrawString("A", font2, brush, pointAx + offsetX, pointAy + offsetY);
            graphics.DrawString("B", font2, brush, pointBx + offsetX, pointBy + offsetY);
        }

        answer5 = answer1;
        answer6 = answer3;
        answer7 = answer2;
        answer8 = answer4;

        Pen vectorPen = new Pen(Color.Black, 2);
        Pen vectorPen2 = new Pen(Color.Black, 3);
        Pen vectorPen3 = new Pen(Color.DeepPink, 3);
        Pen vectorPen4 = new Pen(Color.Black, 2);

        CustomLineCap bigArrow = new AdjustableArrowCap(6, 6, true);
        vectorPen.CustomEndCap = bigArrow;

        CustomLineCap bigArrow2 = new AdjustableArrowCap(5, 5, true);
        vectorPen2.CustomEndCap = bigArrow;

        CustomLineCap bigArrow3 = new AdjustableArrowCap(5, 5, true);
        vectorPen3.CustomEndCap = bigArrow3;



        graphics.DrawLine(vectorPen, 0, centerY, 510, centerX);
        graphics.DrawLine(vectorPen, centerX, 510, centerY, 0);
        graphics.DrawLine(vectorPen3, vectorStartX, vectorStartY, vectorEndX, vectorEndY);

        int radius = 4;
        graphics.FillEllipse(brush2, pointAx - radius, pointAy - radius, radius * 2, radius * 2);

        graphics.FillEllipse(brush2, pointBx - radius, pointBy - radius, radius * 2, radius * 2);




        //test.Pictures.Add(img);

        string questionText = $"Парамерическое уравнение данной прямой с начальной точкой А и направляющим вектором \\(\\overline{{v}}\\) имеет вид:\r\n" +
            $"\\( x \\overline{{i}} + y\\overline{{j}} = <answer1>\\overline{{i}} +<answer2>\\overline{{j}}+\\)" +
            $"\\(t\\left(<answer3>\\overline{{i}}+<answer4>\\overline{{j}}\\right)\\), т.е." +
            $"\\(\\left\\{{\\begin{{array}}{{l}}"+
            $"  x = <answer5>+<answer6>t,\\\\"+
            $"  y = <answer7>+<answer8>t.\\\\"+
            $" \\end{{array}}\\right.\\) "+
            $"Точка \\(B\\) получается при \\(t = <answer9>\\).";


        test.Text = questionText;

        return test;
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        int total = 0;

        foreach (var answer in answers)
        {
            if (answer.Key == "answer1" && answer.Value == answer1.ToString() ||
                answer.Key == "answer2" && answer.Value == answer2.ToString() ||
                answer.Key == "answer3" && answer.Value == answer3.ToString() ||
                answer.Key == "answer4" && answer.Value == answer4.ToString() ||
                answer.Key == "answer5" && answer.Value == answer5.ToString() ||
                answer.Key == "answer6" && answer.Value == answer6.ToString() ||
                answer.Key == "answer7" && answer.Value == answer7.ToString() ||
                answer.Key == "answer8" && answer.Value == answer8.ToString() ||
                answer.Key == "answer9" && answer.Value == answer9.ToString())
                total += 1;
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
    public int TimeLimitSeconds { get; set; } = 60;
    public bool IsHidden { get; set; } = false;
}
