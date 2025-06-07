using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

namespace usue_online_tests.Tests.List;

public class AnalitGeom : ITestCreator, ITest
{
    public int TestID { get; set; }
    public string Name { get; } = "Аналитическая геометрия";
    public string Description { get; } = "Упражнение по аналитической геометрии";

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы",
        Justification = "<Ожидание>")]
    public ITest CreateTest(int randomSeed)
    {
        //randomSeed = 1028797245;
        var random = new Random(randomSeed);
        ITest test = new AnalitGeom();

        var startV = random.Next(126, 131);
        int par1, par2;
        if (random.Next(0, 2) == 0)
        {
            par1 = 1;
            par2 = -2;
        }
        else
        {
            par1 = 2;
            par2 = -1;
        }

        var positionYV1 = random.Next(0, 9);
        var positionXV1 = random.Next(0, 9);

        var positionYV2 = random.Next(0, 7);
        var positionXV2 = random.Next(0, 7);

        if (positionYV2 == 3 && positionXV2 == 3) positionYV2 = 5;

        using var input = File.OpenRead(Environment.CurrentDirectory + "\\wwwroot\\generators\\alalitGeom.png");
        using var bitmap = SKBitmap.Decode(input);
        using var canvas = new SKCanvas(bitmap);

        //var img = Image.FromFile(Environment.CurrentDirectory + "\\wwwroot\\generators\\alalitGeom.png");

        using var paint = new SKPaint
        {
            Color = SKColors.Blue,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4
        };

        //var graphics = Graphics.FromImage(img);
        //darw ellipse
        //graphics.DrawEllipse(new Pen(Color.Blue, 4), (startV - 120) * 40, 280, 40, 40);

        canvas.DrawOval((startV - 120) * 40 - 20, 260, 20, 20, paint);

        //draw line
        paint.Color = SKColors.Green;
        paint.StrokeWidth = 5;
        canvas.DrawLine(positionXV1 * 40 - 20 + 40 * 3, positionYV1 * 40 - 20 + 40 * 4,
            (positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3, (positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4,
            paint);

        var stream = new MemoryStream();
        bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);

        test.Pictures.Add(stream);

        test.Text = $"Прямая задана параметрическими уравнениями с начальной точкой с номером {startV}," +
                    $" и направляющим вектором \\(\\overline{{p}}\\). Тогда значению параметра {par1} соответствует точка с" +
                    $" номером \\(<point1>\\), а значению параметра {par2} соответствует точка с номером \\(<point2>\\)";


        return test;
    }

    public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
    {
        var random = new Random(randomSeed);

        var total = 0;
        var startV = random.Next(126, 131);
        int par1, par2;
        if (random.Next(0, 2) == 0)
        {
            par1 = 1;
            par2 = -2;
        }
        else
        {
            par1 = 2;
            par2 = -1;
        }

        var positionYV1 = random.Next(0, 9);
        var positionXV1 = random.Next(0, 9);

        var positionYV2 = random.Next(0, 7);
        var positionXV2 = random.Next(0, 7);

        if (positionYV2 == 0 && positionXV2 == 0) positionYV2 = 5;

        try
        {
            var point = Convert.ToInt32(answers["point1"]);
            var x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
            var y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
            if (point == (startV + (x * par1) + (y * par1 * 17))) total++;
        }
        catch
        {
            // ignored
        }

        try
        {
            var point = Convert.ToInt32(answers["point2"]);
            var x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
            var y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
            if (point == (startV + (x * par2) + (y * par2 * 17))) total++;
        }
        catch
        {
            // ignored
        }

        return total;
    }

    public string Text { get; set; }
    public string[] CheckBoxes { get; set; }
    public List<MemoryStream> Pictures { get; set; } = new List<MemoryStream>();
}