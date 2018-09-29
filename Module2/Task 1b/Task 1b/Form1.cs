using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Task_1b
{
	public partial class Form1 : Form
	{	private Point start;
		private bool drawing = false;
		private Image orig;
		Color c;
		OpenFileDialog open_dialog;
        Bitmap back;

		public Form1()
		{
			InitializeComponent();;
            radioButton1.Checked = true;


            //создаем фон
            Bitmap b = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = b;
            Clear(); // очищаем pictureBox
            
        }

        //поиск границ
        private void find_borders(Point our_p, ref Point left_b, ref Point right_b, Bitmap b, Color c)
        {
            while (left_b.X > 0 && equalColors(b.GetPixel(left_b.X, left_b.Y), c))
            {
                left_b.X -= 1;
            }

            while (right_b.X < b.Width && equalColors(b.GetPixel(right_b.X, right_b.Y), c))
                right_b.X += 1;
        }

        //проверяем цвета на равенство
        private bool equalColors(Color c1, Color c2)
        {
                return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B;
        }

        //заливка
        private void filling(Point p, Color c)
        {
            Bitmap b = (Bitmap)pictureBox.Image;

            //если пиксель еще не был закрашен
            if (0 < p.X && p.X < b.Width && 0 < p.Y && p.Y < b.Height && equalColors(b.GetPixel(p.X, p.Y), c))
            {
                var g = Graphics.FromImage(b);
                Point left_b = p, right_b = p;
                find_borders(p, ref left_b, ref right_b, b, c); //поиск границ
                Rectangle r = new Rectangle(left_b.X + 1, p.Y, right_b.X - left_b.X - 1, 1); 
                Bitmap line = back.Clone(r, back.PixelFormat); //копируем линию из заданного изображения

                g.DrawImage(line, r);
                pictureBox.Image = b;
                
                for (int i = left_b.X + 1; i < right_b.X; ++i)
                        filling(new Point(i, p.Y + 1), c);

                for (int i = left_b.X + 1; i < right_b.X; ++i)
                        filling(new Point(i, p.Y - 1), c);
            }
        }

		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
            start = new Point(e.X, e.Y);
            if (radioButton1.Checked) //рисуем
            {
                orig = pictureBox.Image;
                drawing = true;
            }
            else
            {
                filling(start, pictureBox.BackColor); // заливаем
            }
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (!drawing) return;
			var finish = new Point(e.X, e.Y);
			var pen = new Pen(Color.Black, 1f);
            
			var g = Graphics.FromImage(pictureBox.Image); 
			g.DrawLine(pen, start, finish);
            pen.Dispose();
			g.Dispose();
            pictureBox.Image = pictureBox.Image;
			pictureBox1.Invalidate();
			start = finish;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			drawing = false;
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox.Image);
            g.Clear(pictureBox.BackColor);
            pictureBox.Image = pictureBox.Image;
        }

		private void button1_Click(object sender, EventArgs e)
		{
            Clear();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
			open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
			DialogResult dr = open_dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Bitmap b = new Bitmap(open_dialog.FileName);
                back = new Bitmap(b, pictureBox.Size);
                pictureBox1.Image = new Bitmap(b, pictureBox1.Size);
            }
		}


        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }
    }
}
