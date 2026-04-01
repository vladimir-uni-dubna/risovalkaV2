using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace risovalka
{
    public partial class Form1 : Form
    {
        Graphics canvas;
        public int x0, y0, x1, y1;

        ShapeType currentFigure;
        Shape currentShape;
        List<Shape> shapes = new List<Shape>();

        bool isResizing = false;

        bool isMouseDown = false;

        public Form1()
        {
            InitializeComponent();
            canvas = panel1.CreateGraphics();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                currentFigure = ShapeType.Ellipse;
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                currentFigure = ShapeType.Triangle;
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                currentFigure = ShapeType.Rectangle;
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = colorDialog2.Color;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x0 = e.X;
            y0 = e.Y;

            isResizing = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (isResizing == true)
                {
                    x1 = e.X;
                    y1 = e.Y;

                    canvas = panel1.CreateGraphics();

                    if (currentFigure == ShapeType.Rectangle)
                    {
                        currentShape = new Rectangle(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                        Rectangle rec = (Rectangle)currentShape;
                    }
                    else if (currentFigure == ShapeType.Ellipse)
                    {
                        currentShape = new Ellipse(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                        Ellipse eli = (Ellipse)currentShape;
                    }
                    else if (currentFigure == ShapeType.Triangle)
                    {
                        currentShape = new Triangle(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                        Triangle tri = (Triangle)currentShape;
                    }

                    currentShape.Draw(canvas);
                    panel1.Refresh();
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            if (!isMouseDown) { return; }
            isResizing = false;

            x1 = e.X;
            y1 = e.Y;

            int width = e.X - x1;
            int height = e.Y - y1;

            currentFigure.Width = width;
            currentFigure.Height = height;

            try
            {
                x1 = e.X;
                y1 = e.Y;

                canvas = panel1.CreateGraphics();

                if (currentFigure == ShapeType.Rectangle)
                {
                    currentShape = new Rectangle(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                    Rectangle rec = (Rectangle)currentShape;
                    //MessageBox.Show(rec.Info());
                }
                else if (currentFigure == ShapeType.Ellipse)
                {
                    currentShape = new Ellipse(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                    Ellipse eli = (Ellipse)currentShape;
                    //MessageBox.Show(eli.Info());
                }
                else if (currentFigure == ShapeType.Triangle)
                {
                    currentShape = new Triangle(x0, y0, x1, y1, colorDialog1.Color, colorDialog2.Color);
                    Triangle tri = (Triangle)currentShape;
                    //MessageBox.Show(tri.Info());
                }

                currentShape.Draw(canvas);
                shapes.Add(currentShape);
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape s in shapes)
            {
                s.Draw(canvas);
            }
        }
    }
}