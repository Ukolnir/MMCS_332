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
		int left, right, up, down;
		Color c;
		OpenFileDialog open_dialog;
        Bitmap back;
		List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();

		public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
		{
			Bitmap result = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(result))
			{
				g.DrawImage(bmp, 0, 0, width, height);
			}

			return result;
		}

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

		private void byFilling(Point p)
		{
			int back_av = back.Width / 2;
			int back_yav = back.Height / 2;

			int x_av = back_av - p.X;
			int y_av = back_yav - p.Y;

			var g = Graphics.FromImage(pictureBox.Image);
			foreach (var t in l)
			{
				if (t.Item1.X < t.Item2.X)
				{
					Rectangle r = new Rectangle(t.Item1.X + 1 + x_av, t.Item1.Y + y_av, t.Item2.X - t.Item1.X - 1, 1);
					Bitmap line = back.Clone(r, back.PixelFormat); //копируем линию из заданного изображения

					r = new Rectangle(t.Item1.X + 1, t.Item1.Y, t.Item2.X - t.Item1.X - 1, 1);
					g.DrawImage(line,r);
					pictureBox.Image = pictureBox.Image;
				}
			}
		}

        //заливка
        private void filling(Point p, Color c)
        {
            Bitmap b = (Bitmap)pictureBox.Image;

			if (l.Exists(t => t.Item1.Y == p.Y && t.Item1.X <= p.X && p.X <= t.Item2.X))
				return;
            //если пиксель еще не был закрашен
            if (0 < p.X && p.X < b.Width && 0 < p.Y && p.Y < b.Height)// && equalColors(b.GetPixel(p.X, p.Y), c))
            {
               // var g = Graphics.FromImage(b);
                Point left_b = p, right_b = p;
                find_borders(p, ref left_b, ref right_b, b, c); //поиск границ
				if (left_b.X < left)
					left = left_b.X;
				if (right_b.X > right)
					right = right_b.X;

				if (left_b.Y < down)
					down = left_b.Y;
				if (right_b.Y > right)
					up = right_b.Y;

				l.Add(Tuple.Create(left_b, right_b));
               // Rectangle r = new Rectangle(left_b.X + 1, p.Y, right_b.X - left_b.X - 1, 1); 
               // Bitmap line = back.Clone(r, back.PixelFormat); //копируем линию из заданного изображения

               // g.DrawImage(line, r);
               // pictureBox.Image = b;
                
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
				left = e.Location.X ;
				right = e.Location.X;
				up = e.Location.Y;
				down = e.Location.Y;

				filling(start, pictureBox.BackColor); // заливаем
				//back = ResizeBitmap(back, right - left, up - down);
				byFilling(start);
				l.Clear();
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

        //Заменяет кортинку на cntByWidth её копий вширину и cntByHeight ей копий ввысоту
        private Bitmap multiplyImage(Bitmap im, int cntByWidth, int cntByHeight)
        {
            Bitmap res = new Bitmap(im);

            int width = res.Size.Width / cntByWidth;
            int height = res.Size.Height / cntByHeight;

            //Создаём фрагмент - изначальную картинку, но меньшей ширины и высоты
            Bitmap fragment = ResizeBitmap(res, width, height);
            Rectangle r;
            var g = Graphics.FromImage(res);
            for (int w = 0; w < cntByWidth; ++w)
            {
                for (int h = 0; h < cntByHeight; ++h)
                {
                    //Находим координаты для рисования
                    int x = width*w;
                    int y = height*h;
                    
                    r = new Rectangle(x, y, width, height);
                    //Рисуем поверх исходного изображения
                    g.DrawImage(fragment, r);
                }
            }
            res = res;
            return res;
        }

		private void button2_Click(object sender, EventArgs e)
		{
			open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
			open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
			DialogResult dr = open_dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Bitmap b = new Bitmap(open_dialog.FileName);
                //Получаем увеличенную картинку
                back = new Bitmap(b, pictureBox.Size.Width * 3, pictureBox.Size.Height * 3);
                pictureBox1.Image = new Bitmap(b, pictureBox1.Size);

                //Получаем расклонированную картинку
                back = new Bitmap(multiplyImage(back, 3, 3));
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
