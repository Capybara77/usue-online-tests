using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using Test_Wrapper;

public class Vector002 : ITestCreator, ITest
{
    public int vectorA;
    public int vectorA1;
    public int vectorB;

    public int TestID { get; set; }
    public string Name { get; } = "Векторная аглебра 002";
    public string Description { get; } = "Упражнение по векторной алгебре";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Vector002();

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


        int vectorEndX, vectorEndY, vectorEndX2, vectorEndY2;

        do
        {
            vectorEndX = centerX - 30 * random.Next(4, 8) * sign;
            vectorEndY = centerY - 30 * random.Next(4, 6) * sign1;
            vectorEndX2 = centerX - 30 * random.Next(2, 5) * sign;
            vectorEndY2 = centerY - 30 * random.Next(2, 5) * sign1;
        } while (vectorEndX == vectorEndX2 && vectorEndY == vectorEndY2);

        var vector = Math.Abs(8 - (vectorEndX / 30));

        vectorA = 0;
        while (vector > 0)
        {
            vectorA++;
            vector -= Math.Abs(8 - (vectorEndX2 / 30));
        }


        var vector2 = Math.Abs(8 - (vectorEndY / 30));

        vectorB = 1;
        vectorA1 = 0;
        while (vector2 > 0)
        {
            vectorA1++;
            vector2 -= Math.Abs(8 - (vectorEndY2 / 30));
        }

        var sign2 = sign == -1 ? 1 : -1;
        var sign3 = sign1 == -1 ? 1 : -1;

        int vectorEndX3 = centerX + 30 * vector * sign2;
        int vectorEndY3 = centerY + 30 * vector2 * sign3;


        if (vectorA1 > vectorA)
        {
            vectorA = vectorA1;
            vectorEndX3 = centerX - 30 * (-vector + Math.Abs(8 - (vectorEndX / 30))) * sign2;
        }


        Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
        Brush brush = Brushes.Black;

        int offsetX = 10;
        int offsetY_c;
        int offsetY_a;

        if (vectorEndY < img.Height / 2)
        {
            offsetY_c = vectorEndY - 30;
            offsetY_a = vectorEndY2 - 30;
        }
        else
        {
            offsetY_c = vectorEndY;
            offsetY_a = vectorEndY2;
        }

        graphics.DrawString("c", font, brush, vectorEndX + offsetX, offsetY_c);
        graphics.DrawString("a", font, brush, vectorEndX2 + offsetX, offsetY_a);
        graphics.DrawString("b", font, brush, vectorEndX3 + offsetX, vectorEndY3);




        Pen vectorPen = new Pen(Color.Red, 3);
        Pen vectorPen2 = new Pen(Color.Black, 3);
        Pen vectorPen3 = new Pen(Color.Black, 3);

        CustomLineCap bigArrow = new AdjustableArrowCap(5, 5, true);
        vectorPen.CustomEndCap = bigArrow;

        CustomLineCap bigArrow2 = new AdjustableArrowCap(5, 5, true);
        vectorPen2.CustomEndCap = bigArrow;

        CustomLineCap bigArrow3 = new AdjustableArrowCap(5, 5, true);
        vectorPen3.CustomEndCap = bigArrow;

        graphics.DrawLine(vectorPen, centerX, centerY, vectorEndX, vectorEndY);
        graphics.DrawLine(vectorPen2, centerX, centerY, vectorEndX2, vectorEndY2);
        graphics.DrawLine(vectorPen3, centerX, centerY, vectorEndX3, vectorEndY3);





        test.Pictures.Add(img);

        string questionText = $"В разложении \\(\\overline{{c}}\\) в линейную комбинацию введите числа(коэффициенты):" +
            $" \\(\\overline{{c}}\\) = \\({vectorA}\\)\\(<vectorA>\\)\\(\\overline{{a}}\\) +   \\({vectorB}\\)\\(<vectorB>\\)\\(\\overline{{b}}\\).";

        test.Text = questionText;

        return test;
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        int total = 0;

        foreach (var answer in answers)
        {
            if (answer.Key == "vectorA" && answer.Value == vectorA.ToString() ||
                answer.Key == "vectorB" && answer.Value == vectorB.ToString())
                total += 1;
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<Image> Pictures { get; set; } = new List<Image>();
}