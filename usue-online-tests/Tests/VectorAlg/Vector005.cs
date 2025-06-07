using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
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
        using var numberPaint = new SKPaint(numberFont) { Color = SKColors.Black, IsAntialias = true };

        for (int i = 0; i < horizontalCount; i++)
        {
            for (int j = verticalCount - 1; j >= 0; j--)
            {
                int x = i * gap + xOffset;
                int y = (verticalCount - 1 - j) * gap + yOffset;

                int numberIndex = j * horizontalCount + i;
                string numberText = numbers[numberIndex].ToString();

                // Измеряем текст, чтобы его центрировать
                var textBounds = new SKRect();
                numberPaint.MeasureText(numberText, ref textBounds);

                // Вычисляем координаты для центрирования
                float centeredX = x - textBounds.MidX;
                float centeredY = y - textBounds.MidY;

                canvas.DrawText(numberText, centeredX, centeredY, numberPaint);
            }
        }

        int centerX = imageInfo.Width / 2;
        int centerY = imageInfo.Height / 2;

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

        this.answer1 = numbers[indexA];
        this.answer2 = numbers[indexB];
        this.answer3 = numbers[indexAB];

        // --- Настройка объектов SkiaSharp для рисования ---
        using var labelFont = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold), 15);
        using var labelPaint = new SKPaint(labelFont) { Color = SKColors.Black, IsAntialias = true };

        using var vectorPen = new SKPaint { Color = SKColors.DeepPink, StrokeWidth = 3, Style = SKPaintStyle.Stroke, IsAntialias = true };
        using var circlePen = new SKPaint { Color = SKColors.Red, StrokeWidth = 2, Style = SKPaintStyle.Stroke, IsAntialias = true };

        // --- Рисование фигур ---
        float radius = 11;
        // В оригинале центр круга был немного смещен (vectorX - 1, vectorY). Сохраняем это.
        canvas.DrawCircle(vectorX - 1, vectorY, radius, circlePen);

        // Рисуем векторы со стрелками
        DrawArrow(canvas, new SKPoint(vectorStartXA, vectorStartYA), new SKPoint(vectorEndXA, vectorEndYA), vectorPen);
        DrawArrow(canvas, new SKPoint(vectorStartXB, vectorStartYB), new SKPoint(vectorEndXB, vectorEndYB), vectorPen);

        // Рисуем подписи к векторам
        canvas.DrawText("a", vectorEndXA, vectorEndYA - 10, labelPaint);
        canvas.DrawText("b", vectorEndXB - 20, vectorEndYB - 10, labelPaint);

        // Сохранение изображения
        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0;
        test.Pictures.Add(ms);

        string questionText = $"Если векторы \\(\\overline{{a}}\\), \\(\\overline{{b}}\\) и 2\\(\\overline{{a}}\\) + 3\\(\\overline{{b}}\\) отложить от точки с номером {number} (обевдена кружком)," +
            $"то концы полученных направленных отрезков будут находиться в точках, соответсвенно \\(<answer1>\\), \\(<answer2>\\) и \\(<answer3>\\).";

        test.Text = questionText;

        return test;
    }

    private void DrawArrow(SKCanvas canvas, SKPoint start, SKPoint end, SKPaint paint, float arrowHeadLength = 10f, float arrowHeadAngle = 25.0f)
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
        // Рисуем стрелку тем же пером, что и линию
        canvas.DrawPath(path, paint);
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        // Логика проверки не использует графику и остается без изменений
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
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
}