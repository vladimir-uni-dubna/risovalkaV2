using System;
using System.Drawing;

namespace risovalka
{
    internal class Ellipse : Shape
    {
        public Ellipse(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour) : base(x0, y0, x1, y1, fillColour, lineColour) { }

        public override void Draw(Graphics canvas)
        {
            int x = Math.Min(x0, x1);
            int y = Math.Min(y0, y1);
            int w = Width;
            int h = Height;

            using (Brush brush = new SolidBrush(fillColour))
            using (Pen pen = new Pen(lineColour))
            {
                canvas.FillEllipse(brush, x, y, w, h);
                canvas.DrawEllipse(pen, x, y, w, h);
            }
        }

        public override double Square()
        {
            return Math.PI * Width * Height / 4.0;
        }

        public override bool IsIn(int px, int py)
        {
            int cx = (x0 + x1) / 2;
            int cy = (y0 + y1) / 2;
            double a = Width / 2.0;
            double b = Height / 2.0;
            if (a == 0 || b == 0) return false;
            return ((px - cx) * (px - cx)) / (a * a) + ((py - cy) * (py - cy)) / (b * b) <= 1;
        }

        public override string Info()
        {
            return $"Эллипс: ширина={Width}, высота={Height}, площадь={Square():F1}";
        }
    }
}
