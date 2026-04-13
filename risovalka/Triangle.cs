using System;
using System.Drawing;

namespace risovalka
{
    internal class Triangle : Shape
    {
        public Triangle(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour) : base(x0, y0, x1, y1, fillColour, lineColour) { }

        private Point[] GetPoints()
        {
            int xLeft = Math.Min(x0, x1);
            int xRight = Math.Max(x0, x1);
            int yBottom = Math.Max(y0, y1);
            int yTop = Math.Min(y0, y1);

            return new Point[]
            {
                new Point(xLeft, yBottom),   // левый нижний
                new Point(xRight, yBottom),  // правый нижний
                new Point((xLeft + xRight) / 2, yTop)  // верхний центр
            };
        }

        public override void Draw(Graphics canvas)
        {
            Point[] points = GetPoints();

            using (Brush brush = new SolidBrush(fillColour))
            using (Pen pen = new Pen(lineColour))
            {
                canvas.FillPolygon(brush, points);
                canvas.DrawPolygon(pen, points);
            }
        }

        public override double Square()
        {
            return Width * Height / 2.0;
        }

        public override bool IsIn(int px, int py)
        {
            Point[] p = GetPoints();

            // Метод барицентрических координат
            double d1 = (px - p[1].X) * (p[0].Y - p[1].Y) - (p[0].X - p[1].X) * (py - p[1].Y);
            double d2 = (px - p[2].X) * (p[1].Y - p[2].Y) - (p[1].X - p[2].X) * (py - p[2].Y);
            double d3 = (px - p[0].X) * (p[2].Y - p[0].Y) - (p[2].X - p[0].X) * (py - p[0].Y);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        public override string Info()
        {
            return $"Треугольник: ширина={Width}, высота={Height}, площадь={Square():F1}";
        }
    }
}
