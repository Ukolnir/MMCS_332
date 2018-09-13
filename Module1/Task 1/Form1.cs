using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        OpenFileDialog open_dialog;
        long g1, g2, g3;

        private void simpleGrey(Bitmap bmp)
		{
			pictureBox2.Image = bmp;
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    byte c = (byte)(((float)pixelColor.R + (float)pixelColor.B + (float)pixelColor.G) / 3.0f);
                    g1 += c;
                    Color newColor = Color.FromArgb(c, c, c);
                    bmp.SetPixel(i, j, newColor);
                }
            g1 = g1 / bmp.Height / bmp.Width;
        }

        //HDTV Model
        private void intensiveGrey(Bitmap bmp)
        {
            pictureBox3.Image = bmp;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    byte c = (byte)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                    g2 += c;
                    Color newColor = Color.FromArgb(c, c, c);
                    bmp.SetPixel(i, j, newColor);
                }

            g2 = g2 / bmp.Height / bmp.Width;
        }

        private void imageDifference(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp3 = (Bitmap)bmp2.Clone();
            pictureBox4.Image = bmp3;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

            for (int i = 0; i < bmp3.Width; ++i)
                for (int j = 0; j < bmp3.Height; ++j)
                {
                    Color pixC1 = bmp1.GetPixel(i, j);
                    Color pixC2 = bmp2.GetPixel(i, j);
                    int d1 = pixC2.R - pixC1.R + pixC2.G - pixC1.G + pixC2.B - pixC1.B;
                    int d2 = pixC1.R - pixC2.R + pixC1.G - pixC2.G + pixC1.B - pixC2.B;
                    byte d = (byte)Math.Max(d1, d2);
                    g3 += d;
                    Color newColor = Color.FromArgb(d, d, d);
                    bmp3.SetPixel(i, j, newColor);
                }

            g3 = g3 / bmp3.Height / bmp3.Width;
        }

        private void Histogram()
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddY(g1);
            chart1.Series["Series1"].Points[0].Color = Color.LightGray;
            chart1.Series["Series1"].Points.AddY(g2);
            chart1.Series["Series1"].Points[1].Color = Color.DarkGray;
            chart1.Series["Series1"].Points.AddY(g3);
            chart1.Series["Series1"].Points[2].Color = Color.Gray;
            chart1.Update();
        }

        private void ChangePicture(DialogResult res)
		{

			if (res == DialogResult.OK) //если в окне была нажата кнопка "ОК"
			{
				try
				{
					Bitmap bmp = new Bitmap(open_dialog.FileName);
                    g1 = g2 = g3 = 0;

					pictureBox1.Image = bmp;
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    Bitmap bmp1 = (Bitmap)bmp.Clone();
                    Bitmap bmp2 = (Bitmap)bmp.Clone();

                    simpleGrey(bmp1);
                    intensiveGrey(bmp2);
                    imageDifference((Bitmap)bmp1.Clone(), (Bitmap)bmp2.Clone());
                    Histogram();
                }
				catch
				{
					DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

                
            }
		}

		private void button1_Click(object sender, EventArgs e)
		{
            open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            DialogResult dr = open_dialog.ShowDialog();
         

            ChangePicture(dr);
		}

    }
}
