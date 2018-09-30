using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Additional
{
    public partial class Form1 : Form
    {
        Color c;
        OpenFileDialog open_dialog;
        private static Bitmap image;
        private static Color borderColor, innerColor, myBorderColor;

        //Изменение размера изображения
        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        private int differenceBetweenColors = 125;

        //Эквивалентность цветов
        private bool colorsEqual(Color c1, Color c2)
        {
            return (System.Math.Abs(c1.R - c2.R) < differenceBetweenColors &&
                System.Math.Abs(c1.G - c2.G) < differenceBetweenColors &&
                System.Math.Abs(c1.B - c2.B) < differenceBetweenColors);
        }

        private static int firstX;
        private static int firstY;

        //Поиск правой границы
        private void getRightBorder(int x, int y)
        {
            Color pixelColor = image.GetPixel(x, y);
            Color currColor = pixelColor;
            innerColor = pixelColor;

            myBorderColor = Color.FromArgb(255, 0, 0);
            while (colorsEqual(innerColor, currColor) && x < image.Width)
            {
                x += 1;
                currColor = image.GetPixel(x, y);
            }
            borderColor = image.GetPixel(x, y);
            firstX = x - 1;
            firstY = y;
        }

        //3 2 1
        //4 X 0
        //5 6 7
        private Tuple<int, int> moveByDirection(int x, int y, int direction)
        {
            switch (direction)
            {
                case 0:
                    x += 1;
                    break;
                case 1:
                    x += 1;
                    y -= 1;
                    break;
                case 2:
                    y -= 1;
                    break;
                case 3:
                    x -= 1;
                    y -= 1;
                    break;
                case 4:
                    x -= 1;
                    break;
                case 5:
                    x -= 1;
                    y += 1;
                    break;
                case 6:
                    y += 1;
                    break;
                case 7:
                    x += 1;
                    y += 1;
                    break;
            }
            return Tuple.Create(x, y);
        }

        //Цвет, который получится если сдвинуться в сторону direction
        private Color colorByDirection(int x, int y, int direction)
        {
            Tuple<int, int> t = moveByDirection(x, y, direction);
            return image.GetPixel(t.Item1, t.Item2);
        }

        //Получение слудующего пикселя при обходе границы
        private void getNextPixel(ref int x, ref int y, ref int whereBorder)
        {
            if (x > 0 && x < image.Width && y > 0 && y < image.Height)
            {
                int d = whereBorder;
                while (colorsEqual(borderColor, colorByDirection(x, y, d)))
                    d = (d + 2) % 8;
                if (colorsEqual(borderColor, colorByDirection(x, y, (d - 1 + 8) % 8)))
                {
                    Tuple<int, int> t = moveByDirection(x, y, d);
                    x = t.Item1;
                    y = t.Item2;
                    whereBorder = (d + 8 - 2) % 8;
                }
                else
                {
                    Tuple<int, int> t = moveByDirection(x, y, (d - 1 + 8) % 8);
                    x = t.Item1;
                    y = t.Item2;
                    whereBorder = (d - 1 + 8 - 3) % 8;
                }
            }
        }

        //Получение всей границы
        private LinkedList<Tuple<int, int>> getFullBorder(int x, int y)
        {
            LinkedList<Tuple<int, int>> points = new LinkedList<Tuple<int, int>>();
            int whereBorder = 0;
            do
            {
                Tuple<int, int> newt = Tuple.Create(x, y);
                if (points.Count() == 0 || points.Last() != newt)
                    points.AddLast(newt);
                getNextPixel(ref x, ref y, ref whereBorder);
            } while (((x != firstX) || (y != firstY)) && (points.Count() < (image.Width + image.Height) * 10));
            return points;
        }

        //Закраска границы
        private void fillMyBorderPoints(ref LinkedList<Tuple<int, int>> points)
        {
            foreach (var t in points)
            {
                image.SetPixel(t.Item1, t.Item2, myBorderColor);
            }
        }

        //Возвращает список, соедержаций след элементы:
        //  Значение Y и список из пар границ (x1, x2), 
        //      где каждая пара границ получена из следующей последовательности пикселей: 
        //      (x1, y), (x1+1, y), ... , (x2, y).
        //      Из-за того, что одному Y может соответствовать не одна пара границ (x1, x2), а несколько,
        //      был использован двусвязный список.
        private LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> getYandBorders(ref List<Tuple<int, int>> points)
        {
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> yBorders = new LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>>();
            int y = points.First().Item2;
            int x = points.First().Item1;
            int x_first = points.First().Item1;
            LinkedList<Tuple<int, int>> borders = new LinkedList<Tuple<int, int>>();

            foreach (var t in points)
            {
                if (t != points.First())
                {
                    int curry = t.Item2;
                    int currx = t.Item1;

                    if (curry == y)
                    {
                        if (currx == x + 1)
                        {
                            x += 1;
                        }
                        else
                        {
                            borders.AddLast(Tuple.Create(x_first, x));
                            x_first = currx;
                            x = currx;
                        }
                    }
                    else
                    {
                        borders.AddLast(Tuple.Create(x_first, x));
                        yBorders.AddLast(Tuple.Create(y, borders));
                        borders = new LinkedList<Tuple<int, int>>();

                        x_first = currx;
                        x = currx;
                        y = curry;
                    }
                }
            }
            borders.AddLast(Tuple.Create(x_first, x));
            yBorders.AddLast(Tuple.Create(y, borders));
            return yBorders;
        }

        public Form1()
        {
            InitializeComponent();

            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = Color.Red;
            c = Color.Red;

            label2.BackColor = c;
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            // установка цвета формы
            c = colorDialog1.Color;
            label2.BackColor = c;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;**.PNG)|*.BMP;*.JPG;**.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            DialogResult dr = open_dialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Bitmap b = new Bitmap(open_dialog.FileName);
                pictureBox1.Image = new Bitmap(b, pictureBox1.Size);

                //Для работы с Bitmap
                Bitmap imageSource = new Bitmap(open_dialog.FileName, true);
                //Изменение размера изображения до размера pictureBox
                image = ResizeBitmap(imageSource, pictureBox1.Size.Width, pictureBox1.Size.Height);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            var x = loc.X;
            var y = loc.Y;

            //Получение правой точки границы
            getRightBorder(x, y);
            //Получение всей границы
            LinkedList<Tuple<int, int>> points = getFullBorder(firstX, firstY);

            //Отсортированные и уникальные точки
            List<Tuple<int, int>> pointsSorted = new List<Tuple<int, int>>(
                points.OrderBy(t => t.Item2).ThenBy(t => t.Item1).ToList().Distinct().ToList());

            //Получение У и соответствующих ему интервалов
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> yBorders = getYandBorders(ref pointsSorted);
        }
    }
}
