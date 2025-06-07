using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

namespace usue_online_tests.Tests.List;

public class Analit001 : ITestCreator, ITest
{
    private int answer1;
    private int answer2;
    private int answer3;
    private int answer4;
    private int answer5;
    private int answer6;
    private int answer7;
    private int answer8;
    private int answer9;

    public int TestID { get; set; }
    public string Name { get; } = "Аналитическая геометрия 001";
    public string Description { get; } = "Упражнение по аналитической геометрии";

    public ITest CreateTest(int randomSeed)
    {
        var random = new Random(randomSeed);
        ITest test = new Analit001();

        var imageInfo = new SKImageInfo(510, 510);
        using var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;

        // Фон делаем белым для наглядности
        canvas.Clear(SKColors.White);

        DrawCrosses(canvas);

        var centerX = imageInfo.Width / 2;
        var centerY = imageInfo.Height / 2;

        var h1 = random.Next(1, 4);
        var h2 = random.Next(1, 4);
        while (h1 == h2) h2 = random.Next(1, 4);

        var sign = random.Next(0, 2) == 0 ? -1 : 1;

        var vectorStartX = centerX - 30 * random.Next(4, 7);
        var vectorStartY = centerY - 30 * random.Next(4, 7);
        var vectorEndX = vectorStartX + 30 * h1;
        var vectorEndY = vectorStartY - 30 * h2;

        if (vectorStartY < 105) vectorEndY = vectorStartY - 30 * 2;
        if (vectorStartX > centerX + 150) vectorEndX = vectorStartX + 30 * random.Next(2, 3);

        var pointAx = centerX - 30 * random.Next(2, 4);
        var pointAy = centerY + 30 * random.Next(2, 4);
        if (-8 + pointAx / 30 == 8 - pointAy / 30) pointAy = centerY + 30 * (-1 + pointAx / 30);

        var pointBx = pointAx + Math.Abs(vectorEndX - vectorStartX);
        var pointBy = pointAy - Math.Abs(vectorEndY - vectorStartY);

        answer1 = -8 + pointAx / 30;
        answer2 = 8 - pointAy / 30;
        answer3 = Math.Abs(vectorEndX - vectorStartX) / 30;
        answer4 = Math.Abs(vectorEndY - vectorStartY) / 30;

        answer9 = sign;

        // --- Настройка объектов SkiaSharp для рисования ---
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial"), 15);
        using var font1 = new SKFont(SKTypeface.FromFamilyName("Arial", SKFontStyle.Italic), 25);
        using var font2 = new SKFont(SKTypeface.FromFamilyName("Arial"), 23);

        using var blackPaint = new SKPaint { Color = SKColors.Black, IsAntialias = true };
        using var pinkPaint = new SKPaint { Color = SKColors.DeepPink, IsAntialias = true };

        using var axisPen = new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsAntialias = true, Style = SKPaintStyle.Stroke };
        using var vectorPen = new SKPaint { Color = SKColors.DeepPink, StrokeWidth = 3, IsAntialias = true, Style = SKPaintStyle.Stroke };

        // --- Рисование ---

        var offsetX = 0;
        var offsetY = 0;

        if (vectorEndY < 105)
        {
            offsetX = 20;
            offsetY = -20;
        }
        else if (vectorEndY > 255)
        {
            offsetY = -20;
            offsetX = 5;
        }

        // Рисование чисел на осях
        for (var i = -1; i > -9; i--)
        {
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackPaint); // Y-координата текста в SkiaSharp - это базовая линия
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackPaint);
        }

        canvas.DrawText(0.ToString(), centerY + 3, centerX + 18, font, blackPaint);

        for (var i = 1; i < 9; i++)
        {
            canvas.DrawText(i.ToString(), centerX - 7 + 30 * i, centerY + 20, font, blackPaint);
            canvas.DrawText(i.ToString(), centerY + 6, centerX - 5 - 30 * i, font, blackPaint);
        }

        canvas.DrawText("x", 494, centerY - 15, font1, blackPaint);
        canvas.DrawText("y", centerX - 25, 20, font1, blackPaint);
        canvas.DrawText("v", vectorEndX - offsetX, vectorEndY + offsetY + 20, font2, blackPaint);

        if (sign == -1)
        {
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

        // Рисование осей со стрелками
        DrawArrow(canvas, new SKPoint(0, centerY), new SKPoint(510, centerY), axisPen);
        DrawArrow(canvas, new SKPoint(centerX, 510), new SKPoint(centerX, 0), axisPen);

        // Рисование вектора со стрелкой
        DrawArrow(canvas, new SKPoint(vectorStartX, vectorStartY), new SKPoint(vectorEndX, vectorEndY), vectorPen, 15, 20);

        // Рисование точек
        var radius = 4;
        canvas.DrawCircle(pointAx, pointAy, radius, pinkPaint);
        canvas.DrawCircle(pointBx, pointBy, radius, pinkPaint);

        // Сохранение изображения в MemoryStream
        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0; // Сбрасываем позицию стрима для последующего чтения
        test.Pictures.Add(ms);

        test.Text =
            $"Парамерическое уравнение данной прямой с начальной точкой А и направляющим вектором \\(\\overline{{v}}\\) имеет вид:\r\n" +
            $"\\( x \\overline{{i}} + y\\overline{{j}} = <answer1>\\overline{{i}} + <answer2>\\overline{{j}} +\\)" +
            $"\\(t\\left(<answer3>\\overline{{i}}+<answer4>\\overline{{j}}\\right)\\), т.е." +
            $"\\(\\left\\{{\\begin{{array}}{{l}}" +
            $"  x = <answer5>+<answer6>t,\\\\" +
            $"  y = <answer7>+<answer8>t.\\\\" +
            $" \\end{{array}}\\right.\\) " +
            $"Точка \\(B\\) получается при \\(t = <answer9>\\).";

        return test;
    }

    /// <summary>
    /// Рисует линию со стрелкой на конце.
    /// </summary>
    /// <param name="canvas">Канва для рисования.</param>
    /// <param name="start">Начальная точка.</param>
    /// <param name="end">Конечная точка (где будет стрелка).</param>
    /// <param name="paint">Стиль линии.</param>
    /// <param name="arrowHeadLength">Длина "усиков" стрелки.</param>
    /// <param name="arrowHeadAngle">Угол "усиков" стрелки относительно линии.</param>
    private void DrawArrow(SKCanvas canvas, SKPoint start, SKPoint end, SKPaint paint, float arrowHeadLength = 12f, float arrowHeadAngle = 25.0f)
    {
        // Рисуем основную линию
        canvas.DrawLine(start, end, paint);

        // Вычисляем угол основной линии
        var angle = Math.Atan2(end.Y - start.Y, end.X - start.X);

        // Создаем путь для стрелки
        using var path = new SKPath();

        // Вычисляем точки для "усиков" стрелки
        var radians = arrowHeadAngle * Math.PI / 180;
        var p1 = new SKPoint(
            (float)(end.X - arrowHeadLength * Math.Cos(angle - radians)),
            (float)(end.Y - arrowHeadLength * Math.Sin(angle - radians))
        );
        var p2 = new SKPoint(
            (float)(end.X - arrowHeadLength * Math.Cos(angle + radians)),
            (float)(end.Y - arrowHeadLength * Math.Sin(angle + radians))
        );

        // Строим треугольный путь
        path.MoveTo(p1);
        path.LineTo(end);
        path.LineTo(p2);
        path.Close(); // Замыкаем путь, чтобы получился треугольник

        // Рисуем стрелку, используя отдельную кисть для заливки
        using var arrowPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = paint.Color,
            IsAntialias = true
        };
        canvas.DrawPath(path, arrowPaint);
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

                // Горизонтальная линия крестика
                canvas.DrawLine(x - size / 2, y, x + size / 2, y, crossPaint);
                // Вертикальная линия крестика
                canvas.DrawLine(x, y - size / 2, x, y + size / 2, crossPaint);
            }
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        var total = 0;

        foreach (var answer in answers)
            if ((answer.Key == "answer1" && answer.Value == answer1.ToString()) ||
                (answer.Key == "answer2" && answer.Value == answer2.ToString()) ||
                (answer.Key == "answer3" && answer.Value == answer3.ToString()) ||
                (answer.Key == "answer4" && answer.Value == answer4.ToString()) ||
                (answer.Key == "answer5" && answer.Value == answer5.ToString()) ||
                (answer.Key == "answer6" && answer.Value == answer6.ToString()) ||
                (answer.Key == "answer7" && answer.Value == answer7.ToString()) ||
                (answer.Key == "answer8" && answer.Value == answer8.ToString()) ||
                (answer.Key == "answer9" && answer.Value == answer9.ToString()))
                total += 1;

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<MemoryStream> Pictures { get; set; } = new();
}