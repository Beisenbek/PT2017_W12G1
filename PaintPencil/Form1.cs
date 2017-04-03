using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintPencil
{
    public partial class Form1 : Form
    {
        Color prevColor = Color.Red;
        Point prevPoint;
        Point currentPoint;
        Shapes currentShape = Shapes.Free;
        GraphicsPath gp = new GraphicsPath();
        Graphics g;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (currentShape)
                {
                    case Shapes.Free:
                        currentPoint = e.Location;
                        g.DrawLine(new Pen(prevColor), prevPoint, currentPoint);
                        prevPoint = currentPoint;
                        break;
                    case Shapes.Line:
                        currentPoint = e.Location;
                        gp.Reset();
                        gp.AddLine(prevPoint, currentPoint);
                        break;
                    case Shapes.Ellipse:
                        break;
                    case Shapes.Rectangle:
                        break;
                    case Shapes.Triangle:
                        break;
                    default:
                        break;
                }
            }
            mouseLocationLabel.Text = string.Format("X = {0}; Y = {1}", e.X, e.Y);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g.DrawPath(new Pen(Color.Red), gp);
        }


        private void colorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                prevColor = c.Color;
            }
        }

        private void lineBtn_Click(object sender, EventArgs e)
        {
            currentShape = Shapes.Line;
        }

        private void freeBtn_Click(object sender, EventArgs e)
        {
            currentShape = Shapes.Free;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawPath(new Pen(Color.Red), gp);
        }
    }


    public enum Shapes
    {
        Free,
        Line,
        Ellipse,
        Rectangle,
        Triangle
    }
}
