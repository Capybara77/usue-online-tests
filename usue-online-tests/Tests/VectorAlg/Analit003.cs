using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using Test_Wrapper;

namespace usue_online_tests.Tests.List
{
    public class Analit003 : ITestCreator, ITest
    {
        public int TestID { get; set; }
        public string Name { get; } = "Аналитическая геометрия 003";
        public string Description { get; } = "Упражнение по аналитической геометрии";

        public ITest CreateTest(int randomSeed)
        {
            Random random = new Random(randomSeed);
            ITest test = new Analit003();

            int startV = random.Next(126, 131);
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

            int positionYV1 = random.Next(0, 9);
            int positionXV1 = random.Next(0, 9);

            int positionYV2 = random.Next(0, 7);
            int positionXV2 = random.Next(0, 7);

            if (positionYV2 == 3 && positionXV2 == 3) positionYV2 = 5;

            var imagePath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "generators", "alalitGeom.png");

            using var stream = File.OpenRead(imagePath);
            using var backgroundImage = SKImage.FromEncodedData(stream);

            using var surface = SKSurface.Create(new SKImageInfo(backgroundImage.Width, backgroundImage.Height));
            var canvas = surface.Canvas;

            canvas.DrawImage(backgroundImage, 0, 0);

            using var ellipsePaint = new SKPaint
            {
                Color = SKColors.Blue,
                StrokeWidth = 4,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            using var linePaint = new SKPaint
            {
                Color = SKColors.Green,
                StrokeWidth = 5,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            // --- ИСПРАВЛЕННЫЙ БЛОК РИСОВАНИЯ ЭЛЛИПСА ---
            // System.Drawing.DrawEllipse использует координаты верхнего левого угла и размеры.
            // SKCanvas.DrawOval использует координаты центра и радиусы.
            // Необходимо скорректировать координаты для соответствия оригиналу.

            float topLeftX = (startV - 120) * 40;
            float topLeftY = 280;
            float width = 40;
            float height = 40;

            // Вычисляем центр (cx, cy) и радиусы (rx, ry) из координат верхнего левого угла
            float centerX = topLeftX + width / 2;
            float centerY = topLeftY + height / 2;
            float radiusX = width / 2;
            float radiusY = height / 2;

            canvas.DrawOval(centerX, centerY, radiusX, radiusY, ellipsePaint);
            // --- КОНЕЦ ИСПРАВЛЕННОГО БЛОКА ---

            var startPoint = new SKPoint(
                positionXV1 * 40 - 20 + 40 * 3,
                positionYV1 * 40 - 20 + 40 * 4
            );
            var endPoint = new SKPoint(
                (positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3,
                (positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4
            );

            DrawArrow(canvas, startPoint, endPoint, linePaint);

            var ms = new MemoryStream();
            using (var image = surface.Snapshot())
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                data.SaveTo(ms);
            }
            ms.Position = 0;
            test.Pictures.Add(ms);

            test.Text = $"Прямая задана параметрическими уравнениями с начальной точкой с номером {startV}," +
                        $" и направляющим вектором \\(\\overline{{p}}\\). Тогда значению параметра {par1} соответствует точка с" +
                        $" номером \\(<point1>\\), а значению параметра {par2} соответствует точка с номером \\(<point2>\\)";

            return test;
        }

        private void DrawArrow(SKCanvas canvas, SKPoint start, SKPoint end, SKPaint paint, float arrowHeadLength = 15f, float arrowHeadAngle = 30.0f)
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

            canvas.DrawPath(path, paint);
        }

        public int CheckAnswer(int randomSeed, Dictionary<string, string> answers)
        {
            Random random = new Random(randomSeed);

            int total = 0;
            int startV = random.Next(126, 131);
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

            int positionYV1 = random.Next(0, 9);
            int positionXV1 = random.Next(0, 9);

            int positionYV2 = random.Next(0, 7);
            int positionXV2 = random.Next(0, 7);

            if (positionYV2 == 0 && positionXV2 == 0) positionYV2 = 5;

            try
            {
                int point = Convert.ToInt32(answers["point1"]);
                int x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
                int y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
                if (point == (startV + (x * par1) + (y * par1 * 17))) total++;
            }
            catch
            {
                // ignored
            }

            try
            {
                int point = Convert.ToInt32(answers["point2"]);
                int x = ((positionXV1 * 40 - 20 + 40 * 3) - ((positionXV1 + positionXV2 - 3) * 40 - 20 + 40 * 3)) / 40 * -1;
                int y = ((positionYV1 * 40 - 20 + 40 * 4) - ((positionYV1 + positionYV2 - 3) * 40 - 20 + 40 * 4)) / 40 * -1;
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
        public List<MemoryStream> Pictures { get; set; } = new();
    }
}