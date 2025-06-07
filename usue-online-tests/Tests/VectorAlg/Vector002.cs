using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
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

        var imageInfo = new SKImageInfo(510, 510);
        using var surface = SKSurface.Create(imageInfo);
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        DrawCrosses(canvas);

        int centerX = imageInfo.Width / 2;
        int centerY = imageInfo.Height / 2;

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

        // --- Вся логика вычислений оставлена без изменений ---
        var vector = Math.Abs(8 - (vectorEndX / 30));

        this.vectorA = 0; // Используем this.vectorA для соответствия оригиналу
        while (vector > 0)
        {
            this.vectorA++;
            vector -= Math.Abs(8 - (vectorEndX2 / 30));
        }

        var vector2 = Math.Abs(8 - (vectorEndY / 30));

        this.vectorB = 1;
        this.vectorA1 = 0;
        while (vector2 > 0)
        {
            this.vectorA1++;
            vector2 -= Math.Abs(8 - (vectorEndY2 / 30));
        }

        var sign2 = sign == -1 ? 1 : -1;
        var sign3 = sign1 == -1 ? 1 : -1;

        int vectorEndX3 = centerX + 30 * (int)vector * sign2;
        int vectorEndY3 = centerY + 30 * (int)vector2 * sign3;

        if (this.vectorA1 > this.vectorA)
        {
            this.vectorA = this.vectorA1;
            vectorEndX3 = centerX - 30 * (int)(-vector + Math.Abs(8 - (vectorEndX / 30))) * sign2;
        }

        // --- Настройка объектов SkiaSharp для рисования ---
        using var font = new SKFont(SKTypeface.FromFamilyName("Arial"), 25);
        using var textPaint = new SKPaint { Color = SKColors.Black, IsAntialias = true };

        using var redPen = new SKPaint { Color = SKColors.Red, StrokeWidth = 3, Style = SKPaintStyle.Stroke, IsAntialias = true };
        using var blackPen = new SKPaint { Color = SKColors.Black, StrokeWidth = 3, Style = SKPaintStyle.Stroke, IsAntialias = true };

        // --- Рисование ---
        int offsetX = 10;
        int offsetY_c;
        int offsetY_a;

        if (vectorEndY < imageInfo.Height / 2)
        {
            offsetY_c = vectorEndY - 30;
            offsetY_a = vectorEndY2 - 30;
        }
        else
        {
            offsetY_c = vectorEndY;
            offsetY_a = vectorEndY2;
        }

        canvas.DrawText("c", vectorEndX + offsetX, offsetY_c + font.Size, font, textPaint);
        canvas.DrawText("a", vectorEndX2 + offsetX, offsetY_a + font.Size, font, textPaint);
        canvas.DrawText("b", vectorEndX3 + offsetX, vectorEndY3 + font.Size, font, textPaint);

        // Рисуем векторы со стрелками
        DrawArrow(canvas, new SKPoint(centerX, centerY), new SKPoint(vectorEndX, vectorEndY), redPen);
        DrawArrow(canvas, new SKPoint(centerX, centerY), new SKPoint(vectorEndX2, vectorEndY2), blackPen);
        DrawArrow(canvas, new SKPoint(centerX, centerY), new SKPoint(vectorEndX3, vectorEndY3), blackPen);

        // Сохранение изображения
        var ms = new MemoryStream();
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        {
            data.SaveTo(ms);
        }
        ms.Position = 0;
        test.Pictures.Add(ms);

        string questionText = $"В разложении \\(\\overline{{c}}\\) в линейную комбинацию введите числа(коэффициенты):" +
            $" \\(\\overline{{c}}\\) = \\({this.vectorA}\\)\\(<vectorA>\\)\\(\\overline{{a}}\\) +   \\({this.vectorB}\\)\\(<vectorB>\\)\\(\\overline{{b}}\\).";

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
        // Логика проверки оставлена без изменений
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
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
}