using System;
using System.IO;
using SkiaSharp;

namespace Spatial.RenderEngine
{
    public class Renderer
    {
        public Renderer()
        {

        }

        // https://washamdev.com/skiasharp-getting-started-and-simple-tutorial/
        public void Generate()
        {
            SKImageInfo imageInfo = new SKImageInfo(300, 250);
            using (SKSurface surface = SKSurface.Create(imageInfo))
            {
                SKCanvas canvas = surface.Canvas;

                //canvas.DrawColor(SKColors.Red);
                canvas.Clear();
                canvas.Clear(SKColors.Empty); //same thing but also erases anything else on the canvas first

                using (SKPaint paint = new SKPaint())
                {
                    paint.Color = SKColors.Blue;
                    paint.IsAntialias = true;
                    paint.StrokeWidth = 15;
                    paint.Style = SKPaintStyle.Stroke;
                    SKPoint[] points = new SKPoint[] { };
                    canvas.DrawPoints(SKPointMode.Lines, points, paint);
                    //canvas.DrawLine(new SKPoint(0, 0), new SKPoint(30, 30), paint);
                    //canvas.DrawCircle(50, 50, 30, paint); //arguments are x position, y position, radius, and paint
                }

                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                }

            }
        }
    }
}
