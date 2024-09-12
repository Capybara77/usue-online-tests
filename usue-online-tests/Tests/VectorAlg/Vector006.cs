using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using Test_Wrapper;

public class Vector006 : ITestCreator, ITest
{
    int answer1;
    int answer2;
    int answer3;


    public int TestID { get; set; }
    public string Name { get; } = "Векторная алгебра 006";
    public string Description { get; } = "Упражнение по векторной алгебре";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Vector006();

        Bitmap img = new Bitmap(510, 510);
        Graphics graphics = Graphics.FromImage(img);
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;



        int[] numbers = new int[289];
        for (int i = 0; i <= 16; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < 17; j++)
                {
                    numbers[i * 17 + j] = (i * 17) + j + 1;
                }
            }
            else
            {
                for (int j = 16; j >= 0; j--)
                {
                    numbers[i * 17 + (16 - j)] = (i * 17) + j + 1;
                }
            }
        }

        int gap = 30;

        int horizontalCount = 530 / gap;
        int verticalCount = 530 / gap;

        int xOffset = 15;
        int yOffset = 15;

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = verticalCount - 1; j >= 0; j--)
            {
                if (i == horizontalCount / 2 || j == verticalCount / 2)
                    continue;

                int x = i * gap + xOffset;
                int y = (verticalCount - 1 - j) * gap + yOffset;

                int numberIndex = j * horizontalCount + i;
                string numberText = numbers[numberIndex].ToString();
                Font font2 = new Font("Arial", 8);

                SizeF textSize = graphics.MeasureString(numberText, font2);

                float centeredX = x - textSize.Width / 2;
                float centeredY = y - textSize.Height / 2;

                graphics.DrawString(numberText, font2, Brushes.Black, centeredX, centeredY);
            }
        }


        int centerX = img.Width / 2;
        int centerY = img.Height / 2;



        Pen vectorPen2 = new Pen(Color.Black, 3);
        CustomLineCap bigArrow2 = new AdjustableArrowCap(5, 5, true);
        vectorPen2.CustomEndCap = bigArrow2;

        graphics.DrawLine(vectorPen2, 0, centerY, 510, centerX);
        graphics.DrawLine(vectorPen2, centerX, 510, centerY, 0);


        int vectorStartXA, vectorEndXA, vectorStartYA, vectorEndYA;


        vectorStartXA = centerX - 30 * random.Next(1, 7);
        vectorStartYA = centerY - 30 * random.Next(1, 7);
        vectorEndXA = vectorStartXA + 30 * random.Next(1, 5);
        vectorEndYA = vectorStartYA + 30 * random.Next(1, 5);


        var newVectorEndXA = centerX + (vectorEndXA - vectorStartXA);
        var newVectorEndYA = centerY + (vectorEndYA - vectorStartYA);


        int indexA = ((verticalCount - 1) - (newVectorEndYA - yOffset) / gap) * horizontalCount + (newVectorEndXA - xOffset) / gap;
        int index = ((verticalCount - 1) - (vectorStartYA - yOffset) / gap) * horizontalCount + (vectorStartXA - xOffset) / gap;

        var number = numbers[index];
        answer1 = numbers[indexA];



        Pen vectorPen = new Pen(Color.DeepPink, 3);

        CustomLineCap bigArrow = new AdjustableArrowCap(5, 5, true);
        vectorPen.CustomEndCap = bigArrow;




        graphics.DrawLine(vectorPen, vectorStartXA, vectorStartYA, vectorEndXA, vectorEndYA);


        test.Pictures.Add(img);

        string questionText = $"На четеже изображён направленный отрезок, полученный откладыванием вектора \\(\\overline{{a}}\\) от точки с номером {number}.\r\n" +
            $"Точка, координаты которой совпадают с координатами вектора \\(\\overline{{a}}\\), имеет номер \\(<answer1>\\).";

        test.Text = questionText;

        return test;
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        int total = 0;

        foreach (var answer in answers)
        {
            if (answer.Key == "answer1" && answer.Value == answer1.ToString())
                total += 1;
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<Image> Pictures { get; set; } = new List<Image>();
}
