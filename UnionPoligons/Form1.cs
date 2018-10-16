using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnionPoligons
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
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        List<Point> list = new List<Point>();
        bool flag = false;

        Graphics g;
        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                list.Add(new Point(e.X, e.Y));
                g.DrawLines(new Pen(Color.Black), list.ToArray());
            }
            else
            {
                list.Add(new Point(e.X, e.Y));
                flag = true;
            }
            pictureBox1.Image = bmp;
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