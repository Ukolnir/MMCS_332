using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1b
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private Point start;
		private bool drawing = false;
		private Image orig;
		Color c;

		private void pictureBox_Click(object sender, EventArgs e)
		{
			
		}
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			start = new Point(e.X, e.Y);
			orig = pictureBox.Image;
			drawing = true;
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (!drawing) return;
			var finish = new Point(e.X, e.Y);
			var pen = new Pen(Color.Black, 1f);
			var g = Graphics.FromHwnd(pictureBox.Handle);
			g.DrawLine(pen, start, finish);
			g.Dispose();
			pictureBox1.Invalidate();
			start = finish;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			drawing = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var g = Graphics.FromHwnd(pictureBox.Handle);
			g.Clear(pictureBox.BackColor);
		}
	}
}
