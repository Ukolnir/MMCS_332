using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnionPolygons
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

        List<Point> general = new List<Point>();
        List<Point> newPolygon = new List<Point>();
        bool flag = false;
        bool twoPolygons = false;

        Graphics g;
        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            if (!twoPolygons){
                if (flag){
                    newPolygon.Add(new Point(e.X, e.Y));
                    g.DrawLines(new Pen(Color.Black), newPolygon.ToArray());
                }
                else
                {
                    newPolygon.Add(new Point(e.X, e.Y));
                    flag = true;
                }
                pictureBox1.Image = bmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (twoPolygons){
                label1.Text = "В данной реализации объединение работает только с двумя полигонами одновременно";
                return;
            }

            if (newPolygon.First() != newPolygon.Last()){
                newPolygon.Add(newPolygon.First());
                g.DrawLines(new Pen(Color.Black), newPolygon.ToArray());
                pictureBox1.Image = bmp;
            }

            if (general.Count == 0){
                general = newPolygon;
                newPolygon.Clear();
                flag = false;
            }
            else{
                twoPolygons = true;
                label1.Text = "Объединение данной реализации работает только с двумя полигонами одновременно";
            }
        }
    }
}
