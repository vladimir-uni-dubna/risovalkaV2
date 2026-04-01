using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace risovalka
{
    abstract class Shape
    {
        protected int x0;
        protected int y0;
        protected int x1;
        protected int y1;
        protected Color fillColour;
        protected Color lineColour;

        public int Width
        {
            get { return Width; }
            set { Width = value; }
        }

        public int Height
        {
            get {  return Height;}  
            set { Height = value; }

        public Shape(int x0, int y0, int x1, int y1, Color fillColour, Color lineColour)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
            this.fillColour = fillColour;
            this.lineColour = lineColour;
        }

        public abstract void Draw(Graphics canvas);
        public abstract double Square();
        public abstract bool IsIn();

        public string Info()
        {
            return "ima figure";
        }
    }
}
