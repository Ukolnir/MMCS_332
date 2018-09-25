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
		public Form1()
		{
			InitializeComponent();
		}

		private static Bitmap image;

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();

			pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
			image = new Bitmap(openFileDialog1.FileName, true);
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			var loc = e.Location;
			var x = loc.X;
			var y = loc.Y;

			Color pixelColor = image.GetPixel(x, y);
			Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
			image.SetPixel(x, y, newColor);
			image.SetPixel(x+1, y, newColor);
			image.SetPixel(x-1, y, newColor);
			image.SetPixel(x, y+1, newColor);
			image.SetPixel(x, y-1, newColor);

			while (true)
			{
				break;
			}

			pictureBox1.Image = image;
		}
	}
}
