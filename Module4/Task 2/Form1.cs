using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2
{
	public partial class Form1 : Form
	{
        int cnt = 0;
        Color c = Color.White;
		Point pl, pr;
		List<Point> points;
		double R;
        int num_p = 0;
        bool star = false;

		public Form1()
		{
			InitializeComponent();
			Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			pictureBox1.Image = bmp;
            points = new List<Point>();
            Clear();
			cnt = 0;
            label1.Text = "Отметьте высоты точек в окне и выберите коэффициент шероховатости (0-1).";
            textBox5.Text = "0,5";
            label8.BackColor = Color.White;
            button4.Visible = false;
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            c = Color.White;
            label8.BackColor = Color.White;
            pictureBox1.Image = pictureBox1.Image;
            points.Clear();
            cnt = 0;
            num_p = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = (pictureBox1.Width).ToString();
            label1.Text = "Отметьте точке в окне и выберите коэффициент шероховатости (0,1).";
            button2.Text = "Создать";
            button4.Text = "Добавить звезды";
            button4.Visible = false;
            star = false;
        }

        private void ClearWithout()
		{
			var g = Graphics.FromImage(pictureBox1.Image);
			g.Clear(c);
            if (star)
            {
                Random rnd = new Random();
                int value = rnd.Next(10, 200);

                for (int i = 0; i < value; ++i)
                {
                    int x = rnd.Next(1, pictureBox1.Width - 1);
                    int y = rnd.Next(1, pictureBox1.Height - 1);
                    ((Bitmap)pictureBox1.Image).SetPixel(x, y, Color.White);
                }
            }

			pictureBox1.Image = pictureBox1.Image;
            num_p = 0;

		}


		private bool equalColors(Color c1, Color c2)
		{
			return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
		}

		
		private void button1_Click(object sender, EventArgs e)
		{
			Clear();
		}

		//добавляет новые точки
		private void addHights()
		{
            List<Point> l1 = new List<Point>();
            Point p = points.First();
            l1.Add(p);
            Random rnd = new Random();

            for (int i = 1; i < points.Count; ++i)
            {
                int x = (points[i].X + p.X) / 2;
                double length = Math.Sqrt(Math.Pow(points[i].X - p.X, 2) + Math.Pow(points[i].Y - p.Y, 2)); // длина отрезка

                double min = -R * length;
                double max = R * length;
                int  h = Convert.ToInt32(Math.Round((p.Y + points[i].Y) / 2 + rnd.NextDouble() * (max - min) + min));

                l1.Add(new Point(x, h));
                l1.Add(points[i]);
                p = points[i];
            }

            points.Clear();

            foreach (var p1 in l1)
                points.Add(p1);
            l1.Clear();
		}

       
		//прорисовка заданной прямой
		private void drawBorder()
		{
            ClearWithout();
			Graphics g = Graphics.FromImage(pictureBox1.Image);

            Point[] par = { };
            Array.Resize(ref par, points.Count() + 2);

            par[0] = new Point(0, pictureBox1.Height - 1);
            par[par.Count() - 1] = new Point(pictureBox1.Width, pictureBox1.Height);

            for (int i = 1; i < points.Count() + 1; ++i)
                    par[i] = points[i - 1];


            Brush b = Brushes.Black;
            pictureBox1.Image = pictureBox1.Image;
            g.FillPolygon(b, par);
            Array.Clear(par, 0, par.Count());
            pictureBox1.Image = pictureBox1.Image;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (num_p == 0)
            {
                textBox1.Text = e.Y.ToString();
            }
            else
            {
                textBox2.Text = e.Y.ToString();
                label1.Text = "Выбранные коэффициенты: ";
            }

            num_p++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            c = colorDialog1.Color;
            label8.BackColor = c;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!star)
            {
                star = true;
                button4.Text = "Удалить звезды";
            }
            else
            {
                button4.Text = "Добавить звезды";
                star = false;
            }

            drawBorder();
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                cnt++;

                if (cnt == 1)
                {
                    button2.Text = "Следующий шаг";
                    pl = new Point(int.Parse(textBox3.Text), int.Parse(textBox1.Text));
                    pr = new Point(int.Parse(textBox4.Text), int.Parse(textBox2.Text));

                    points.Add(pl);
                    points.Add(pr);
                    button4.Visible = true;

                    R = double.Parse(textBox5.Text);
                }
                else
                {
                    addHights();
                }
                drawBorder();

            }
        }
	}
}
