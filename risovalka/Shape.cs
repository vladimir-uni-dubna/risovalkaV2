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

        // Методы для доступа к координатам
        public int GetX0() => x0;
        public int GetY0() => y0;
        public int GetX1() => x1;
        public int GetY1() => y1;

        // Метод для изменения размера фигуры
        public void Resize(int newX1, int newY1)
        {
            x1 = newX1;
            y1 = newY1;
        }

        // Получить границы фигуры для проверки попадания в угол
        public System.Drawing.Rectangle GetBounds()
        {
            int x = Math.Min(x0, x1);
            int y = Math.Min(y0, y1);
            return new System.Drawing.Rectangle(x, y, Width, Height);
        }

        // Проверка попадания в правый нижний угол (для изменения размера)
        public bool IsInResizeHandle(int px, int py, int handleSize = 10)
        {
            int xMax = Math.Max(x0, x1);
            int yMax = Math.Max(y0, y1);
            return Math.Abs(px - xMax) <= handleSize && Math.Abs(py - yMax) <= handleSize;
        }

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
