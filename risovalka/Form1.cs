using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;

namespace risovalka
{
    public partial class Form1 : Form
    {
        ShapeType currentFigure = ShapeType.Rectangle;
        List<Shape> shapes = new List<Shape>();
        Shape previewShape = null;

        bool isDrawing = false;
        bool isResizing = false;
        int startX, startY;
        Shape selectedShape = null;

        Color fillColor = Color.LightBlue;
        Color lineColor = Color.Black;

        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.Text = "Рисовалка v2";
        }

        // =================== КНОПКИ ФИГУР ===================

        private void button4_Click(object sender, EventArgs e)
        {
            currentFigure = ShapeType.Rectangle;
            HighlightButton(button4);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentFigure = ShapeType.Ellipse;
            HighlightButton(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentFigure = ShapeType.Triangle;
            HighlightButton(button3);
        }

        private void HighlightButton(Button active)
        {
            button4.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            active.BackColor = Color.LightGreen;
        }

        // =================== ЦВЕТА ===================

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = fillColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                fillColor = colorDialog1.Color;
                button1.BackColor = fillColor;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog2.Color = lineColor;
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                lineColor = colorDialog2.Color;
                button5.BackColor = lineColor;
            }
        }

        // =================== РИСОВАНИЕ МЫШЬЮ ===================

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            // Проверяем, попали ли в угол существующей фигуры для изменения размера
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].IsInResizeHandle(e.X, e.Y))
                {
                    isResizing = true;
                    selectedShape = shapes[i];
                    return;
                }
            }

            // Иначе начинаем рисовать новую фигуру
            isDrawing = true;
            startX = e.X;
            startY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // Изменение размера существующей фигуры
            if (isResizing && selectedShape != null)
            {
                selectedShape.Resize(e.X, e.Y);
                panel1.Invalidate();
                return;
            }

            // Рисование новой фигуры
            if (!isDrawing) return;

            previewShape = CreateShape(startX, startY, e.X, e.Y);
            panel1.Invalidate();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                isResizing = false;
                selectedShape = null;
                panel1.Invalidate();
                return;
            }

            if (!isDrawing) return;

            isDrawing = false;

            Shape shape = CreateShape(startX, startY, e.X, e.Y);
            if (shape.Width > 1 && shape.Height > 1)
            {
                shapes.Add(shape);
            }
            previewShape = null;
            panel1.Invalidate();
        }

        private Shape CreateShape(int x0, int y0, int x1, int y1)
        {
            switch (currentFigure)
            {
                case ShapeType.Rectangle:
                    return new Rectangle(x0, y0, x1, y1, fillColor, lineColor);
                case ShapeType.Ellipse:
                    return new Ellipse(x0, y0, x1, y1, fillColor, lineColor);
                case ShapeType.Triangle:
                    return new Triangle(x0, y0, x1, y1, fillColor, lineColor);
                default:
                    return new Rectangle(x0, y0, x1, y1, fillColor, lineColor);
            }
        }

        // =================== ДОПОЛНИТЕЛЬНЫЕ КНОПКИ ===================

        private void btnClear_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            previewShape = null;
            panel1.Invalidate();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (shapes.Count == 0)
            {
                MessageBox.Show("Нет фигур. Нарисуйте что-нибудь!", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Shape last = shapes[shapes.Count - 1];
            MessageBox.Show(last.Info(), "Информация о фигуре", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // =================== СОХРАНЕНИЕ И ЗАГРУЗКА ===================

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "JSON файлы (*.json)|*.json",
                DefaultExt = "json",
                Title = "Сохранить рисунок"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DrawingData data = new DrawingData();
                    foreach (Shape shape in shapes)
                    {
                        data.Shapes.Add(new ShapeData(shape));
                    }

                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string json = serializer.Serialize(data);
                    File.WriteAllText(saveDialog.FileName, json);
                    MessageBox.Show("Рисунок сохранён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "JSON файлы (*.json)|*.json",
                Title = "Загрузить рисунок"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string json = File.ReadAllText(openDialog.FileName);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DrawingData data = serializer.Deserialize<DrawingData>(json);

                    shapes.Clear();
                    foreach (ShapeData shapeData in data.Shapes)
                    {
                        shapes.Add(shapeData.ToShape());
                    }

                    panel1.Invalidate();
                    MessageBox.Show("Рисунок загружен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =================== ОТРИСОВКА ===================

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем все сохранённые фигуры
            foreach (Shape s in shapes)
            {
                s.Draw(e.Graphics);
            }

            // Рисуем превью (текущая фигура под мышью)
            if (previewShape != null)
            {
                previewShape.Draw(e.Graphics);
            }
        }
    }
}
