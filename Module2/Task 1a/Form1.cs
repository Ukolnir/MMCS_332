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
		private static Pen pen = new Pen(Color.Blue, 2);
		private static List<Point> points = new List<Point>();
		public Form1(){
			InitializeComponent();
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			if (points == null || points.Count == 0)
			{
				return;
			}
			e.Graphics.DrawCurve(pen, points.ToArray());
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			//points.Clear();
			points.Add(e.Location);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			points.Add(e.Location);
			if (points.Count > 2)
			{
				pictureBox1.Refresh();
			}
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			points.Add(e.Location);
			if (points.Count > 2)
			{
				pictureBox1.Refresh();
			}
		}
	}
}

/*
 
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            flag = true;            
        }
 
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            flag = false;
        }
*/
