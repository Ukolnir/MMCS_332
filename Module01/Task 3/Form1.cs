using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task3
{
    public partial class Form1 : Form
    {
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
        }

        private void openPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*";
            openFileDialog1.Title = "Выберите изображение";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bmp;
                trackBar1.Enabled = true;
                trackBar2.Enabled = true;
                trackBar3.Enabled = true;
                button1.Enabled = true;
            }

           
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));
            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));  //vmin
            int a = Convert.ToInt32((value - p)* (hue % 60)/60); //a
            int q = Convert.ToInt32(value - a); //vdec
            int t = Convert.ToInt32(p + a);     //vinc

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int hue_change = trackBar1.Value;
            int sat_change = trackBar2.Value;
            int val_change = trackBar3.Value;

            double hue;
            double saturation;
            double value;

            Bitmap bmp1 = (Bitmap)bmp.Clone();
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j){
                    Color pixelColor = bmp.GetPixel(i, j);
                    ColorToHSV(pixelColor, out hue, out saturation, out value);
                    hue = (hue + hue_change) % 360;

                    saturation = (saturation * 100 + sat_change) * 0.01;
                    if (saturation > 1)
                        saturation = 1;
                    if (saturation < 0)
                        saturation = 0;

                    value = (value * 100 + val_change) * 0.01;
                    if (value > 1)
                        value = 1;
                    if (value < 0)
                        value = 0;

                    Color newpixelColor = ColorFromHSV(hue,saturation,value);
                    bmp1.SetPixel(i, j, newpixelColor);
                }
            pictureBox1.Image = bmp1;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}
