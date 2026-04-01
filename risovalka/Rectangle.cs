using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace risovalka
{
    internal class Rectangle : Shape
    {
        public Rectangle(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour) : base(x0, y0, x1, y1, fillColour, lineColour) { }

        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(fillColour);
            Brush brush = new SolidBrush(lineColour);

            int x = Math.Min(x0, x1);
            int y = Math.Min(y0, y1);

            int width = Math.Abs(x1 - x0);
            int height = Math.Abs(y1 - y0);

            canvas.FillRectangle(brush, x, y, width, height);
            canvas.DrawRectangle(pen, x, y, width, height);
        }

        public new string Info()
        {
            return "ima rectangle";
        }
    }
}
