using System;
using System.Collections.Generic;
using System.Drawing;

namespace risovalka
{
    // Класс для сериализации фигур в JSON
    [Serializable]
    public class ShapeData
    {
        public string Type { get; set; }
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int FillColorArgb { get; set; }
        public int LineColorArgb { get; set; }

        public ShapeData() { }

        public ShapeData(Shape shape)
        {
            Type = shape.GetType().Name;
            X0 = shape.GetX0();
            Y0 = shape.GetY0();
            X1 = shape.GetX1();
            Y1 = shape.GetY1();
            FillColorArgb = shape.FillColour.ToArgb();
            LineColorArgb = shape.LineColour.ToArgb();
        }

        public Shape ToShape()
        {
            Color fillColor = Color.FromArgb(FillColorArgb);
            Color lineColor = Color.FromArgb(LineColorArgb);

            switch (Type)
            {
                case "Rectangle":
                    return new Rectangle(X0, Y0, X1, Y1, fillColor, lineColor);
                case "Ellipse":
                    return new Ellipse(X0, Y0, X1, Y1, fillColor, lineColor);
                case "Triangle":
                    return new Triangle(X0, Y0, X1, Y1, fillColor, lineColor);
                default:
                    return new Rectangle(X0, Y0, X1, Y1, fillColor, lineColor);
            }
        }
    }

    [Serializable]
    public class DrawingData
    {
        public List<ShapeData> Shapes { get; set; } = new List<ShapeData>();
    }
}
