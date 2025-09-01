using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

namespace usue_online_tests.Tests.VectorAlg;

public class Analit3 : ITestCreator, ITest
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
    public string Name { get; } = "Аналитическая геометрия13";
    public string Description { get; } = "Упражнение по аналитической геометрии";

    public ITest CreateTest(int randomSeed)
    {
        Random random = new Random(randomSeed);
        ITest test = new Analit3();

        var imageInfo = new SKImageInfo(510, 510);
        using var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        DrawCrosses(canvas);

        int centerX = imageInfo.Width / 2;
        int centerY = imageInfo.Height / 2;

        int sign = random.Next(0, 2) == 0 ? -1 : 1;

        int vectorEndX, vectorEndY, vectorStartX, vectorStartY;
        int pointAx, pointAy, pointBx, pointBy;

        vectorStartX = centerX - 30 * random.Next(4, 7);
        vectorStartY = centerY - 30 * random.Next(4, 7);
        vectorEndX = vectorStartX + 30 * random.Next(2, 4);
        vectorEndY = vectorStartY - 30 * random.Next(2, 4);

        if (vectorStartY < 105)
            vectorEndY = vectorStartY - 30 * 2;

        pointAx = centerX - 30 * random.Next(2, 3);
        pointAy = centerY + 30 * random.Next(2, 3);

        pointBx = pointAx + Math.Abs(vectorEndX - vectorStartX);
        pointBy = pointAy - Math.Abs(vectorEndY - vectorStartY);

        answer1 = -8 + pointAx / 30;
        answer2 = 8 - pointAy / 30;
        answer3 = Math.Abs(vectorEndX - vectorStartX) / 30;
        answer4 = Math.Abs(vectorEndY - vectorStartY) / 30;
        answer9 = sign;

        // Внимание: Этот блок, как и в оригинальном коде, меняет координаты,
        // если sign == -1. Ниже есть еще один такой же блок, что, вероятно, является ошибкой,
        // но сохранено для полного соответствия логике оригинала.
        if (sign == -1)
        {
            pointBx = pointAx;
            pointBy = pointAy;
            pointAx = pointBx - Math.Abs(vectorEndX - vectorStartX);
            pointAy = pointBy + Math.Abs(vectorEndY - vectorStartY);
        }

        // --- Настройка объектов SkiaSharp для рисования ---
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial"), 15);
        using var font1 = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Italic), 25);
        using var font2 = new SKFont(SKTypeface.FromFamilyName("Arial"), 23);

        using var blackPaint = new SKPaint { Color = SKColors.Black, IsAntialias = true };
        using var pinkPaint = new SKPaint { Color = SKColors.DeepPink, IsAntialias = true };

        using var axisPen = new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsAntialias = true, Style = SKPaintStyle.Stroke };
        using var vectorPen = new SKPaint { Color = SKColors.DeepPink, StrokeWidth = 3, IsAntialias = true, Style = SKPaintStyle.Stroke };

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
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackPaint);
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackPaint);
        }
        canvas.DrawText(0.ToString(), centerY + 3, centerX + 18, font, blackPaint);
        for (int i = 1; i < 9; i++)
        {
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackPaint);
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackPaint);
        }

        // Рисование подписей
        canvas.DrawText("x", 494, centerY - 15, font1, blackPaint);
        canvas.DrawText("y", centerX - 25, 20, font1, blackPaint);
        canvas.DrawText("v", vectorEndX + offsetX, vectorEndY + offsetY + 20, font2, blackPaint);

        if (sign == -1)
        {
            // Внимание: Этот блок, как и в оригинальном коде, ПОВТОРНО применяет
            // смещение координат, а затем пересчитывает ответы.
            pointBx = pointAx;
            pointBy = pointAy;
            pointAx = pointBx - Math.Abs(vectorEndX - vectorStartX);
            pointAy = pointBy + Math.Abs(vectorEndY - vectorStartY);
            answer1 = -8 + pointBx / 30;
            answer2 = 8 - pointBy / 30;
            canvas.DrawText("B", pointAx + offsetX, pointAy + offsetY + 20, font2, blackPaint);
            canvas.DrawText("A", pointBx + offsetX, pointBy + offsetY + 20, font2, blackPaint);
        }
        else
        {
            canvas.DrawText("A", pointAx + offsetX, pointAy + offsetY + 20, font2, blackPaint);
            canvas.DrawText("B", pointBx + offsetX, pointBy + offsetY + 20, font2, blackPaint);
        }

        answer5 = answer1;
        answer6 = answer3;
        answer7 = answer2;
        answer8 = answer4;

        // Рисование осей и вектора
        DrawArrow(canvas, new SKPoint(0, centerY), new SKPoint(510, centerY), axisPen);
        DrawArrow(canvas, new SKPoint(centerX, 510), new SKPoint(centerX, 0), axisPen);
        DrawArrow(canvas, new SKPoint(vectorStartX, vectorStartY), new SKPoint(vectorEndX, vectorEndY), vectorPen, 15, 20);

        // Рисование точек
        int radius = 4;
        canvas.DrawCircle(pointAx, pointAy, radius, pinkPaint);
        canvas.DrawCircle(pointBx, pointBy, radius, pinkPaint);

        // Сохранение изображения
        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0;
        test.Pictures.Add(ms);

        string questionText = $"Парамерическое уравнение данной прямой с начальной точкой А и направляющим вектором \\(\\overline{{v}}\\) имеет вид:\r\n" +
            $"\\( x \\overline{{i}} + y\\overline{{j}} = <answer1>\\overline{{i}} +<answer2>\\overline{{j}}+\\)" +
            $"\\(t\\left(<answer3>\\overline{{i}}+<answer4>\\overline{{j}}\\right)\\), т.е." +
            $"\\(\\left\\{{\\begin{{array}}{{l}}" +
            $"  x = <answer5>+<answer6>t,\\\\" +
            $"  y = <answer7>+<answer8>t.\\\\" +
            $" \\end{{array}}\\right.\\) " +
            $"Точка \\(B\\) получается при \\(t = <answer9>\\).";

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
                canvas.DrawLine(x - size / 2f, y, x + size / 2f, y, crossPaint);
                canvas.DrawLine(x, y - size / 2f, x, y + size / 2f, crossPaint);
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
        // Метод не использует графику и остается без изменений
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
}