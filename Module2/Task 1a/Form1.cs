using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1a
{
    public partial class Form1 : Form
    {

        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();    
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        List<Point> list = new List<Point>();
        List<Point> lstfill = new List<Point>();
        bool flag = false;

        Graphics g;
        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                g = Graphics.FromImage(bmp);
                list.Add(new Point(e.X, e.Y));
                g.DrawLines(new Pen(Color.Black, 3), list.ToArray());
            }
            else
            {
                list.Add(new Point(e.X, e.Y));
                flag = true;
            }
            pictureBox1.Image = bmp;
        }
        
        Color formColor;
        Pen needColor = new Pen(Color.Red);
        private void pictureBox1_MouseDown2(object sender, MouseEventArgs e)
        {
            Point firstPoint = new Point(e.X, e.Y);
            fill(firstPoint);
        }

        private void fill(Point p) {
            formColor = bmp.GetPixel(p.X, p.Y);
            if (0 < p.X && p.X < bmp.Width && 0 < p.Y && p.Y < bmp.Height &&
                formColor.R != Color.Black.R && formColor.B != Color.Black.B && formColor.G != Color.Black.G &&
                formColor.R != Color.Red.R && formColor.B != Color.Red.B && formColor.G != Color.Red.G)
            {
                Point leftBound = new Point(p.X, p.Y);
                Point rightBound = new Point(p.X, p.Y);
                Color currentColor = formColor;
                while (leftBound.X > 0 && currentColor.R != Color.Black.R && currentColor.B != Color.Black.B && currentColor.G != Color.Black.G)
                {
                    leftBound.X -= 1;
                    currentColor = bmp.GetPixel(leftBound.X, p.Y);
                }
                currentColor = formColor;
                while (rightBound.X < pictureBox1.Width && currentColor.R != Color.Black.R && currentColor.B != Color.Black.B && currentColor.G != Color.Black.G)
                {
                    rightBound.X += 1;
                    currentColor = bmp.GetPixel(rightBound.X, p.Y);
                }
                leftBound.X += 1;
                rightBound.X -= 1;
                g.DrawLine(needColor, leftBound, rightBound);
                pictureBox1.Image = bmp;
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    fill(new Point(i, p.Y + 1));
                for (int i = leftBound.X; i < rightBound.X + 1; ++i)
                    fill(new Point(i, p.Y - 1));
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown -= (MouseEventHandler)pictureBox1_MouseDown1;
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown2);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            pictureBox1.Image = pictureBox1.Image;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}