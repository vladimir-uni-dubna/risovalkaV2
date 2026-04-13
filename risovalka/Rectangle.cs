using System;
using System.Drawing;

namespace risovalka
{
    internal class Rectangle : Shape
    {
        public Rectangle(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour) : base(x0, y0, x1, y1, fillColour, lineColour) { }

        public override void Draw(Graphics canvas)
        {
            int x = Math.Min(x0, x1);
            int y = Math.Min(y0, y1);
            int w = Width;
            int h = Height;

            using (Brush brush = new SolidBrush(fillColour))
            using (Pen pen = new Pen(lineColour))
            {
                canvas.FillRectangle(brush, x, y, w, h);
                canvas.DrawRectangle(pen, x, y, w, h);
            }
        }

        public override double Square()
        {
            return Width * Height;
        }

        public override bool IsIn(int px, int py)
        {
            int xMin = Math.Min(x0, x1);
            int xMax = Math.Max(x0, x1);
            int yMin = Math.Min(y0, y1);
            int yMax = Math.Max(y0, y1);
            return px >= xMin && px <= xMax && py >= yMin && py <= yMax;
        }

        public override string Info()
        {
            return $"Прямоугольник: ширина={Width}, высота={Height}, площадь={Square():F1}";
        }
    }
}
