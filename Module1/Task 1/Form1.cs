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
        List<int> l1, l2; //для хранения кол-во пикселей с оттенкам серого в 1ом и 2м изображении (для гистограммы)


        //оттенки серого (Y = 0.333R + 0.333G + 0.333B)
        private void simpleGrey(Bitmap bmp) 
		{
			pictureBox2.Image = bmp;
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            //проходимся по пикселям, изменяя цвет
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    Color pixelColor = bmp.GetPixel(i, j); // получаем пиксель исходного ихображения
                    int c = (int)(((float)pixelColor.R + (float)pixelColor.B + (float)pixelColor.G) / 3.0f); // расчитываем оттенок серого
                    l1[c]++; //увеличиваем число пикселей данного оттенка на 1 в контейнере
                    Color newColor = Color.FromArgb(c, c, c);
                    bmp.SetPixel(i, j, newColor); //устанавливаем новый цвет пикселя
                }
        }

        //HDTV Model
        private void intensiveGrey(Bitmap bmp)
        {
            pictureBox3.Image = bmp;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            //проходимся по пикселям, изменяя цвет
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    Color pixelColor = bmp.GetPixel(i, j); // получаем пиксель исходного ихображения
                    int c = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B); // расчитываем оттенок серого
                    l2[c]++; //увеличиваем число пикселей данного оттенка на 1 в контейнере
                    Color newColor = Color.FromArgb(c, c, c);
                    bmp.SetPixel(i, j, newColor); //устанавливаем новый цвет пикселя
                }
        }


        //разность двух полученных полутоновых изображений
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
                    byte d = (byte)Math.Max(d1, d2); //выбираем максимум из двух сумм, т.к. значение d1 или d2 м.б. отрицательным
                    Color newColor = Color.FromArgb(d, d, d);
                    bmp3.SetPixel(i, j, newColor);
                }
        }

        //построение гистограммы
        private void Histogram()
        {
            
            chart1.Series["image 1"].Points.Clear(); //интенсивность (оттенки серого) первого изображения
            chart1.Series["image 2"].Points.Clear(); //интенсивность (оттенки серого) второго изображения

            //строим гистограмму по кол-ву пикселей оттенков серого (от 0 до 255) 1го и 2го изображений
            for (int i = 0; i < 256; ++i)
            {
                chart1.Series["image 1"].Points.AddY(l1[i]);
                chart1.Series["image 1"].Points[i].Color = Color.Blue; //цвет столбцов, относящихся к 1му изображению
                chart1.Series["image 2"].Points.AddY(l2[i]);
                chart1.Series["image 2"].Points[i].Color = Color.Orange; //цвет столбцов, относящихся к 2му изображению
            }
            chart1.Update();
        }

        private void ChangePicture(DialogResult res)
		{

			if (res == DialogResult.OK) //если в окне была нажата кнопка "ОК"
			{
				
			    Bitmap bmp = new Bitmap(open_dialog.FileName); 
                    

			    pictureBox1.Image = bmp;
			    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bmp1 = (Bitmap)bmp.Clone(); //создаем копию исх-го изображения для simpleGrey
                Bitmap bmp2 = (Bitmap)bmp.Clone(); //создаем копию исх-го изображения для intensiveGrey

                l1 = new List<int>(); //инициализируем контейнеры для последующего построения гистограммы
                l2 = new List<int>();
 
                for (int i = 0; i < 256; ++i)
                {
                    l1.Add(0);
                    l2.Add(0);
                }

                simpleGrey(bmp1);
                intensiveGrey(bmp2);
                imageDifference((Bitmap)bmp1.Clone(), (Bitmap)bmp2.Clone());
                Histogram();
           
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
