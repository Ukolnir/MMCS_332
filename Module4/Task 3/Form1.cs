using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			g = Graphics.FromImage(pictureBox1.Image);
			bmp = (Bitmap)pictureBox1.Image;
		}

		private static List<Point> points = new List<Point>();
		private static Graphics g;
		private static Bitmap bmp;

		private void drawPoint(int x, int y, Color c)
		{
			bmp.SetPixel(x, y, c);

			bmp.SetPixel(x + 1, y, c);
			bmp.SetPixel(x - 1, y, c);
			bmp.SetPixel(x, y + 1, c);
			bmp.SetPixel(x, y - 1, c);

			bmp.SetPixel(x + 1, y + 1, c);
			bmp.SetPixel(x - 1, y + 1, c);
			bmp.SetPixel(x + 1, y - 1, c);
			bmp.SetPixel(x - 1, y - 1, c);
		}

		private bool addPoint(int x, int y)
		{
			if (x > 0 && x < pictureBox1.Width && y > 0 && y < pictureBox1.Height)
			{
				points.Add(new Point(x, y));

				drawPoint(x, y, Color.Black);

				label1.Text = "Point added";
				pictureBox1.Image = bmp;

				return true;
			}
			else
			{
				label1.Text = "Point abroad borders";

				return false;
			}
		}

		private bool deletePoint(int x, int y)
		{
			if (x > 0 && x < pictureBox1.Width && y > 0 && y < pictureBox1.Height)
			{
				Point p;
				int diff = 7;
				if (points.Exists(point => ((point.X > x - diff) && (point.X < x + diff)) &&
					((point.Y > y - diff) && (point.Y < y + diff))))
				{
					p = points.Find(point => ((point.X > x - diff) && (point.X < x + diff)) &&
						((point.Y > y - diff) && (point.Y < y + diff)));
					points.Remove(p);

					drawPoint(p.X, p.Y, pictureBox1.BackColor);

					label1.Text = "Point deleted";
					pictureBox1.Image = bmp;

					return true;
				}
				else
				{
					label1.Text = "Point not found";

					return false;
				}
			}
			else
			{
				label1.Text = "Point abroad borders";

				return false;
			}
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;
			if (radioAdd.Checked)
			{
				addPoint(x, y);
			}
			if (radioDelete.Checked)
			{
				deletePoint(x, y);
			}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;

			if (radioMove.Checked)
			{
				deletePoint(x, y);
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			int x = e.Location.X;
			int y = e.Location.Y;

			if (radioMove.Checked)
			{
				if (addPoint(x, y))
				{
					label1.Text = "Point moved";
				}
			}
		}
	}
}
