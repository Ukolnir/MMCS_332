using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Alex
{
	public partial class Form1 : Form
	{
		float ymin, ymax;
		float step, cnt = 300;
		delegate float del(float i);

		public Form1()
		{
			InitializeComponent();
		}

		private static void graph_x2()
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void Calc(float xmin, float xmax, del f)
		{
            ymin = 0;
			ymax = ymin;

			for (float x = xmin; x <= xmax; x += step)
			{
				float y = f(x);

				if (y < ymin)
					ymin = y;
				if (y > ymax)
					ymax = y;
			}


		}

        private void Graphic(float xmin, float xmax)
		{
			Graphics g = pictureBox1.CreateGraphics();
			Pen p = new Pen(Color.Black);
            g.Clear(pictureBox1.BackColor);

            del f = x => 0;

            if (radioButton1.Checked)
            {
                f = x => (float)Math.Sin(x);
            }
            else
                if (radioButton2.Checked)
            {
                f = x => x * x;
            }
            else
            if (radioButton3.Checked)
            {
                f = x => (float)Math.Cos(x);
            }
            else
            if ( radioButton4.Checked)
            {
                f = x => x * x * (x - 1);
            }



			Calc(xmin, xmax, f);


            float dif_x = xmax - xmin;
            float dif_y = ymax - ymin;
			float x1 = xmin;
            float y1 = f(x1);


			for (int i = 0; i <= cnt; ++i)
			{
				float x2 = x1 + step;
                float y2 = f(x2);

                float nx1 = (x1 - xmin) / dif_x * pictureBox1.Width;
                float nx2 = (x2 - xmin) / dif_x * pictureBox1.Width;
                float ny1 = (y1 - ymin) / dif_y * pictureBox1.Height;
                float ny2 = (y2 - ymin) / dif_y * pictureBox1.Height;

                g.DrawLine(p, nx1, pictureBox1.Height - ny1, nx2, pictureBox1.Height - ny2);

				x1 = x2;
                y1 = y2;
			}


        }

		private void button1_Click(object sender, EventArgs e)
		{

			float xmin = float.Parse(textBox1.Text);
			float xmax = float.Parse(textBox2.Text);
            
            step = (xmax - xmin) / cnt;

			Graphic(xmin, xmax);
		

		}


		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}
	}
}
