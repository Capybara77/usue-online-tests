using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

namespace usue_online_tests.Tests.List;

public class Analit002 : ITestCreator, ITest
{
    int answer1;
    int answer2;
    int answer3;
    int answer4;
    int answer5;
    int answer6;
    int answer7;

    public int TestID { get; set; }
    public string Name { get; } = "Аналитическая геометрия 002";
    public string Description { get; } = "Упражнение по аналитической геометрии";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Analit002();

        var imageInfo = new SKImageInfo(510, 510);
        using var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;

        canvas.Clear(SKColors.White);

        DrawCrosses(canvas);

        int centerX = imageInfo.Width / 2;
        int centerY = imageInfo.Height / 2;

        int h = random.Next(1, 4);

        int vectorEndX, vectorEndY, vectorStartX, vectorStartY;
        int pointAx, pointAy;

        vectorStartX = centerX - 30 * random.Next(4, 8);
        vectorStartY = centerY + 30 * random.Next(5, 7);
        vectorEndX = vectorStartX + 30 * h;
        vectorEndY = vectorStartY - 30 * h;

        if (vectorStartY < 105)
            vectorEndY = vectorStartY - 30 * 2;
        if (vectorStartX > centerX + 150)
            vectorEndX = vectorStartX + 30 * random.Next(2, 3);

        pointAx = centerX - 30 * random.Next(2, 6);
        pointAy = centerY - 30 * random.Next(2, 6);

        answer1 = -8 + pointAx / 30;
        answer2 = 8 - pointAy / 30;
        answer3 = Math.Abs(vectorEndX - vectorStartX) / 30;
        answer4 = Math.Abs(vectorEndY - vectorStartY) / 30;
        answer5 = answer3;
        answer6 = answer4;
        answer7 = -(answer3 * answer1 + answer4 * answer2); // Обновленная логика для С: Ax + By + C = 0 -> C = -Ax - By

        // --- Настройка объектов SkiaSharp для рисования ---
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial"), 15);
        using var font1 = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Italic), 25);
        using var font2 = new SKFont(SKTypeface.FromFamilyName("Arial"), 23);

        using var blackTextPaint = new SKPaint { Color = SKColors.Black, IsAntialias = true };
        using var pinkFillPaint = new SKPaint { Color = SKColors.DeepPink, IsAntialias = true, Style = SKPaintStyle.Fill };

        using var axisPen = new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsAntialias = true, Style = SKPaintStyle.Stroke };
        using var vectorPen = new SKPaint { Color = SKColors.DeepPink, StrokeWidth = 3, IsAntialias = true, Style = SKPaintStyle.Stroke };
        using var perpendicularLinePen = new SKPaint { Color = SKColors.Black, StrokeWidth = 1, IsAntialias = true, Style = SKPaintStyle.Stroke };

        // --- Рисование ---

        int offsetX = 0;
        int offsetY = 0;

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

        // Рисование чисел на осях
        for (int i = -1; i > -9; i--)
        {
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackTextPaint);
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackTextPaint);
        }

        canvas.DrawText(0.ToString(), centerY + 3, centerX + 18, font, blackTextPaint);

        for (int i = 1; i < 9; i++)
        {
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackTextPaint);
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackTextPaint);
        }

        // Рисование подписей
        canvas.DrawText("x", 494, centerY - 15, font1, blackTextPaint);
        canvas.DrawText("y", centerX - 25, 20, font1, blackTextPaint);
        canvas.DrawText("v", vectorEndX - offsetX, vectorEndY + offsetY + 20, font2, blackTextPaint);
        canvas.DrawText("A", pointAx + offsetX, pointAy + offsetY + 20, font2, blackTextPaint);

        // Рисование перпендикулярной линии
        // Уравнение прямой Ax+By+C=0. Нормаль n={A,B}, т.е. {answer3, answer4}
        // A(x-x0) + B(y-y0) = 0. Направляющий вектор прямой s={-B, A} = {-answer4, answer3}
        if (answer3 != 0) // чтобы избежать деления на ноль
        {
            float slope = -(float)answer3 / answer4; // наклон s = {A,B}, а наклон перпендикуляра -A/B
            int x1 = 0;
            int y1 = (int)(slope * (x1 - pointAx) + pointAy);
            int x2 = imageInfo.Width;
            int y2 = (int)(slope * (x2 - pointAx) + pointAy);
            canvas.DrawLine(x1, y1, x2, y2, perpendicularLinePen);
        }
        else // Вертикальная линия x = pointAx
        {
            canvas.DrawLine(pointAx, 0, pointAx, imageInfo.Height, perpendicularLinePen);
        }


        // Рисование осей и вектора со стрелками
        DrawArrow(canvas, new SKPoint(0, centerY), new SKPoint(510, centerY), axisPen);
        DrawArrow(canvas, new SKPoint(centerX, 510), new SKPoint(centerX, 0), axisPen);
        DrawArrow(canvas, new SKPoint(vectorStartX, vectorStartY), new SKPoint(vectorEndX, vectorEndY), vectorPen, 15, 20);

        // Рисование точки
        int radius = 4;
        canvas.DrawCircle(pointAx, pointAy, radius, pinkFillPaint);

        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0;
        test.Pictures.Add(ms);

        string questionText = $"Общее уравнение прямой, проходящей через точку A перпендикулярно вектору \\(\\overline{{v}}\\) может быть представлено в виде:\r\n" +
                              $"\\(<answer3>\\)( x  - \\(<answer1>\\)) + \\(<answer4>\\)( y - \\(<answer2>\\)) = 0, т.е. приводя подобные члены \\(<answer5>\\)x + \\(<answer6>\\)y + \\(<answer7>\\) = 0";

        test.Text = questionText;

        return test;
    }

    private static void DrawCrosses(SKCanvas canvas)
    {
        var size = 17;
        var gap = 30;

        var horizontalCount = 530 / gap;
        var verticalCount = 530 / gap;

        var xOffset = 15;
        var yOffset = 15;

        using var crossPaint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1 };

        for (var i = 0; i < horizontalCount; i++)
        for (var j = 0; j < verticalCount; j++)
        {
            var x = i * gap + xOffset;
            var y = j * gap + yOffset;

            canvas.DrawLine(x - size / 2, y, x + size / 2, y, crossPaint);
            canvas.DrawLine(x, y - size / 2, x, y + size / 2, crossPaint);
        }
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
        int total = 0;

        foreach (var answer in answers)
        {
            if (answer.Key == "answer1" && answer.Value == answer1.ToString() ||
                answer.Key == "answer2" && answer.Value == answer2.ToString() ||
                answer.Key == "answer3" && answer.Value == answer3.ToString() ||
                answer.Key == "answer4" && answer.Value == answer4.ToString() ||
                answer.Key == "answer5" && answer.Value == answer5.ToString() ||
                answer.Key == "answer6" && answer.Value == answer6.ToString() ||
                answer.Key == "answer7" && answer.Value == answer7.ToString())
                total += 1;
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
}