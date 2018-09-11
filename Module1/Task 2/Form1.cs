using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Bitmap image2, image3, image4;

        /*private void Form1_Load(object sender, EventArgs e)
        {

        }*/

        private void ShowPictures()
        {
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;


            image2 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox2.Image = image2;
            image3 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox3.Image = image3;
            image4 = new Bitmap(openFileDialog1.FileName, true);
            pictureBox4.Image = image4;

            long r, g, b;
            r = 0;
            g = 0;
            b = 0;

            for (int x = 0; x < image2.Width; x++)
            {
                for (int y = 0; y < image2.Height; y++)
                {
                    Color pixelColor = image2.GetPixel(x, y);
                    Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                    image2.SetPixel(x, y, newColor);
                    r += pixelColor.R;
                    g += pixelColor.G;
                    b += pixelColor.B;
                }
            }

            for (int x = 0; x < image3.Width; x++)
            {
                for (int y = 0; y < image3.Height; y++)
                {
                    Color pixelColor = image3.GetPixel(x, y);
                    Color newColor = Color.FromArgb(0, pixelColor.G, 0);
                    image3.SetPixel(x, y, newColor);
                }
            }

            for (int x = 0; x < image4.Width; x++)
            {
                for (int y = 0; y < image4.Height; y++)
                {
                    Color pixelColor = image4.GetPixel(x, y);
                    Color newColor = Color.FromArgb(0, 0, pixelColor.B);
                    image4.SetPixel(x, y, newColor);
                }
            }


            long r1 = r / image2.Width / image2.Height;
            long g1 = g / image2.Width / image2.Height;
            long b1 = b / image2.Width / image2.Height;

            label1.Text = "r = " + r1 + " | g = " + g1 + " | b = " + b1;

            chart1.Series["Series1"].Points.Clear();

            chart1.Series["Series1"].Points.AddY(r1);
            chart1.Series["Series1"].Points[0].Color = Color.Red;
            chart1.Series["Series1"].Points.AddY(g1);
            chart1.Series["Series1"].Points[1].Color = Color.Green;
            chart1.Series["Series1"].Points.AddY(b1);
            chart1.Series["Series1"].Points[2].Color = Color.Blue;
            chart1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            /*if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName, Encoding.Default);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }*/

            ShowPictures();
        }
       
    }
}
