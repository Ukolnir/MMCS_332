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

		private void label1_Click(object sender, EventArgs e)
		{

		}

        OpenFileDialog open_dialog;

		private void simple_grey(Bitmap bmp)
		{
			pictureBox2.Image = bmp;
			pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    Color pixelColor = bmp.GetPixel(i, j);
                    byte c = (byte)(((float)pixelColor.A + (float)pixelColor.B + (float)pixelColor.G) / 3.0f);
                    Color newColor = Color.FromArgb(c, c, c);
                    bmp.SetPixel(i, j, newColor);
                }

		}

		private void ChangePicture(DialogResult res)
		{

			if (res == DialogResult.OK) //если в окне была нажата кнопка "ОК"
			{
				try
				{
					Bitmap bmp = new Bitmap(open_dialog.FileName);


					pictureBox1.Image = bmp;
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

					simple_grey((Bitmap)bmp.Clone());


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
