using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private static Bitmap image;
        private static Color borderColor;

        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();

            Bitmap imageSource = new Bitmap(openFileDialog1.FileName, true);
            image = ResizeBitmap(imageSource, pictureBox1.Size.Width, pictureBox1.Size.Height);

            pictureBox1.Image = image;
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        
		}

        private int norma(Color c1, Color c2)
        {
            return (System.Math.Abs(c1.R - c2.R) +
                System.Math.Abs(c1.G - c2.G) +
                System.Math.Abs(c1.B - c2.B));
        }

        private bool colorsEqual(Color c1, Color c2)
        {
            return (norma(c1, c2) < 10);
        }

        private static int firstX;
        private static int firstY;

        private void getRightBorder(int x, int y)
        {
            Color pixelColor = image.GetPixel(x, y);
            var innerColor = pixelColor;
            var currColor = pixelColor;

            borderColor = Color.FromArgb(pixelColor.R, 0, 0);
            label1.Text += "CurrColor = " + currColor.ToString() + '\n';
            while (colorsEqual(innerColor, currColor))
            {
                image.SetPixel(x, y, borderColor);
                x += 1;
                currColor = image.GetPixel(x, y);
            }
            firstX = x;
            firstY = y;
            image.SetPixel(x, y, borderColor);
            label1.Text += "firstX = " + x + " firstY = " + y + "\n";
            label1.Text += "CurrColor = " + currColor.ToString() + '\n';
        }

        //1 2 3
        //4 5 6
        //7 8 9
        //position relative to 5
        private Tuple<int, int> getNextPixel(int x, int y, int position)
        {
            var currColor = image.GetPixel(x, y);

            Color nextColor;

            switch (position)
            {
                case 3:
                    nextColor = image.GetPixel(x + 1, y - 1);
                    if (colorsEqual())
                    break;
            }

            nextColor = image.GetPixel(x, y - 1);
            if (colorsEqual(currColor, nextColor))
                x = 0;

            return Tuple.Create(x, y);
        }

        private void getFullBorder(int x, int y)
        {
            while (true)
            {
                
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{

            var loc = e.Location;

            label1.Text = "Mouse x = " + loc.X + " | y = " + loc.Y + '\n';

            var x = loc.X;
            var y = loc.Y;

            getRightBorder(x, y);
            getFullBorder(firstX, firstY);              
       
			pictureBox1.Image = image;
		}
    }
}
