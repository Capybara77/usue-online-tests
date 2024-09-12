using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using Test_Wrapper;

public class Vector005 : ITestCreator, ITest
{
    int answer1;
    int answer2;
    int answer3;

    public int TestID { get; set; }
    public string Name { get; } = "Векторная алгебра 005";
    public string Description { get; } = "Упражнение по векторной алгебре";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Vector005();

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


        int vectorStartXA, vectorEndXA, vectorStartYA, vectorEndYA, vectorStartXB, vectorEndXB, vectorStartYB, vectorEndYB, vectorX, vectorY;


        vectorStartXA = centerX - 30 * random.Next(1, 5) + 3;
        vectorStartYA = centerY - 30 * random.Next(1, 5);
        vectorEndXA = vectorStartXA + 30 * random.Next(1, 4);
        vectorEndYA = vectorStartYA - 30 * random.Next(1, 3);


        vectorStartXB = centerX - 30 * random.Next(0, 5);
        vectorStartYB = centerY + 30 * random.Next(3, 8);
        vectorEndXB = vectorStartXB - 30 * random.Next(1, 4) + 3;
        vectorEndYB = vectorStartYB - 30 * random.Next(1, 3);


        vectorX = centerX + 30 * random.Next(2, 5);
        vectorY = centerY + 30 * random.Next(3, 9);




        var newVectorEndXA = vectorX + (vectorEndXA - vectorStartXA);
        var newVectorEndYA = vectorY + (vectorEndYA - vectorStartYA);

        var newVectorEndXB = vectorX + (vectorEndXB - vectorStartXB);
        var newVectorEndYB = vectorY + (vectorEndYB - vectorStartYB);

        var newVectorEndX = vectorX + 2 * (vectorEndXA - vectorStartXA) + 3 * (vectorEndXB - vectorStartXB);
        var newVectorEndY = vectorY + 2 * (vectorEndYA - vectorStartYA) + 3 * (vectorEndYB - vectorStartYB);

        int indexA = ((verticalCount - 1) - (newVectorEndYA - yOffset) / gap) * horizontalCount + (newVectorEndXA - xOffset) / gap;
        int indexB = ((verticalCount - 1) - (newVectorEndYB - yOffset) / gap) * horizontalCount + (newVectorEndXB - xOffset) / gap;
        int indexAB = ((verticalCount - 1) - (newVectorEndY - yOffset) / gap) * horizontalCount + (newVectorEndX - xOffset) / gap;
        int index = ((verticalCount - 1) - (vectorY - yOffset) / gap) * horizontalCount + (vectorX - xOffset) / gap;
        var number = numbers[index];

        answer1 = numbers[indexA];
        answer2 = numbers[indexB];
        answer3 = numbers[indexAB];

        Font font = new Font("Arial", 15, FontStyle.Bold);


        Pen vectorPen = new Pen(Color.DeepPink, 3);
        Pen pen = new Pen(Color.Red, 2);

        CustomLineCap bigArrow = new AdjustableArrowCap(5, 5, true);
        vectorPen.CustomEndCap = bigArrow;

        int radius = 11;

        graphics.DrawEllipse(pen, vectorX - radius - 1, vectorY - radius, 2 * radius, 2 * radius);

        graphics.DrawLine(vectorPen, vectorStartXA, vectorStartYA, vectorEndXA, vectorEndYA);

        graphics.DrawLine(vectorPen, vectorStartXB, vectorStartYB, vectorEndXB, vectorEndYB);

        graphics.DrawString("a", font, Brushes.Black, vectorEndXA, vectorEndYA - 20);
        graphics.DrawString("b", font, Brushes.Black, vectorEndXB - 20, vectorEndYB - 20);



        test.Pictures.Add(img);

        string questionText = $"Если векторы \\(\\overline{{a}}\\), \\(\\overline{{b}}\\) и 2\\(\\overline{{a}}\\) + 3\\(\\overline{{b}}\\) отложить от точки с номером {number} (обевдена кружком)," +
            $"то концы полученных направленных отрезков будут находиться в точках, соответсвенно \\(<answer1>\\), \\(<answer2>\\) и \\(<answer3>\\).";

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
                answer.Key == "answer3" && answer.Value == answer3.ToString())
                total += 1;
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<Image> Pictures { get; set; } = new List<Image>();
}
