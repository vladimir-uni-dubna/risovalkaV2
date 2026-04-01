using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace risovalka
{
    internal class Triangle : Shape
    {
        public Triangle(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour) : base(x0, y0, x1, y1, fillColour, lineColour) { }

        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(fillColour);
            Brush brush = new SolidBrush(lineColour);

            int x00 = Math.Min(x0, x1);
            int y00 = Math.Max(y0, y1);

            int x11 = Math.Max(x0, x1);
            int y11 = y00;

            int x22 = (x00 + x11) / 2;
            int y22 = Math.Min(y0, y1);

            Point[] points = { new Point(x00, y00), new Point(x11, y11), new Point(x22, y22) };

            canvas.FillPolygon(brush, points);
            canvas.DrawPolygon(pen, points);
        }

        public new string Info()
        {
            return "ima triangle";
        }
    }
}
