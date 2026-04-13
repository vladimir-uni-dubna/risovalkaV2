using System;
using System.Drawing;

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
            get { return Math.Abs(x1 - x0); }
        }

        public int Height
        {
            get { return Math.Abs(y1 - y0); }
        }

        public Color FillColour { get => fillColour; set => fillColour = value; }
        public Color LineColour { get => lineColour; set => lineColour = value; }

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

        /// <summary>
        /// Проверяет, находится ли точка внутри фигуры
        /// </summary>
        public abstract bool IsIn(int px, int py);

        public virtual string Info()
        {
            return $"Фигура: ширина={Width}, высота={Height}, площадь={Square():F1}";
        }
    }
}
