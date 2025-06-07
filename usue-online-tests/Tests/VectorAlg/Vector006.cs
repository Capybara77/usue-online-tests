using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

public class Vector006 : ITestCreator, ITest
{
    int answer1;

    public int TestID { get; set; }
    public string Name { get; } = "Векторная алгебра 006";
    public string Description { get; } = "Упражнение по векторной алгебре";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Vector006();

        var imageInfo = new SKImageInfo(510, 510);
        using var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        // --- Логика генерации номеров и координат оставлена без изменений ---
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

        // --- Рисование сетки чисел с помощью SkiaSharp ---
        using var numberFont = new SKFont(SKTypeface.FromFamilyName("Arial"), 8);
        using var numberPaint = new SKPaint(numberFont) { Color = SKColors.Black, IsAntialias = true, TextAlign = SKTextAlign.Center };

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

                // Центрируем текст по вертикали
                var textBounds = new SKRect();
                numberPaint.MeasureText(numberText, ref textBounds);
                float centeredY = y - textBounds.MidY;

                canvas.DrawText(numberText, x, centeredY, numberPaint);
            }
        }

        int centerX = imageInfo.Width / 2;
        int centerY = imageInfo.Height / 2;

        // --- Рисование осей ---
        using var axisPen = new SKPaint { Color = SKColors.Black, StrokeWidth = 3, Style = SKPaintStyle.Stroke, IsAntialias = true };
        DrawArrow(canvas, new SKPoint(0, centerY), new SKPoint(510, centerY), axisPen);
        DrawArrow(canvas, new SKPoint(centerX, 510), new SKPoint(centerX, 0), axisPen);

        // --- Вычисление координат вектора ---
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
        this.answer1 = numbers[indexA];

        // --- Рисование вектора ---
        using var vectorPen = new SKPaint { Color = SKColors.DeepPink, StrokeWidth = 3, Style = SKPaintStyle.Stroke, IsAntialias = true };
        DrawArrow(canvas, new SKPoint(vectorStartXA, vectorStartYA), new SKPoint(vectorEndXA, vectorEndYA), vectorPen);

        // Сохранение изображения
        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0;
        test.Pictures.Add(ms);

        string questionText = $"На четеже изображён направленный отрезок, полученный откладыванием вектора \\(\\overline{{a}}\\) от точки с номером {number}.\r\n" +
            $"Точка, координаты которой совпадают с координатами вектора \\(\\overline{{a}}\\), имеет номер \\(<answer1>\\).";

        test.Text = questionText;

        return test;
    }

    private void DrawArrow(SKCanvas canvas, SKPoint start, SKPoint end, SKPaint paint, float arrowHeadLength = 12f, float arrowHeadAngle = 25.0f)
    {
        canvas.DrawLine(start, end, paint);
        var angle = Math.Atan2(end.Y - start.Y, end.X - start.X);
        using var path = new SKPath();
        var radians = arrowHeadAngle * Math.PI / 180;
        var p1 = new SKPoint(
            (float)(end.X - arrowHeadLength * Math.Cos(angle - radians)),
            (float)(end.Y - arrowHeadLength * Math.Sin(angle - radians))
        );
        var p2 = new SKPoint(
            (float)(end.X - arrowHeadLength * Math.Cos(angle + radians)),
            (float)(end.Y - arrowHeadLength * Math.Sin(angle + radians))
        );
        path.MoveTo(p1);
        path.LineTo(end);
        path.LineTo(p2);
        path.Close();
        using var arrowPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = paint.Color,
            IsAntialias = true
        };
        canvas.DrawPath(path, arrowPaint);
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        // Логика проверки не использует графику и остается без изменений
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
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
}