﻿using System;
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
        static Color penColor;
        OpenFileDialog open_dialog;
        private static Bitmap image;

        private static Color borderColor, innerColor, myBorderColor;

        private static Graphics g;
        Pen pen;

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
            //Цвет на который указала мышка
            innerColor = pixelColor;
            //Движение вправо, пока не встретится другой цвет
            while (colorsEqual(innerColor, currColor) && x < image.Width)
            {
                x += 1;
                currColor = image.GetPixel(x, y);
            }
            //Цвет границы - первый отличный от innerColor цвет
            borderColor = image.GetPixel(x, y);
            //Точка начала обхода границы
            firstX = x - 1;
            firstY = y;
        }

        //Движение по заданному направлению от точки Р
        //3 2 1
        //4 P 0
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
            //Сдвиг по заданному направлению
            Tuple<int, int> t = moveByDirection(x, y, direction);
            //Получение цвета
            return image.GetPixel(t.Item1, t.Item2);
        }

        //Получение слудующего пикселя при обходе границы
        private void getNextPixel(ref int x, ref int y, ref int whereBorder, ref int direction)
        {
            if (x > 0 && x < image.Width && y > 0 && y < image.Height)
            {
                int d = whereBorder;

                //Получение направления, на котором цвет отличается от цвета границы
                while (colorsEqual(borderColor, colorByDirection(x, y, d)))
                    d = (d + 2) % 8;
                //Выбор куда идти в зависимости от цвета по диагонали
                if (colorsEqual(borderColor, colorByDirection(x, y, (d - 1 + 8) % 8)))
                {
                    Tuple<int, int> t = moveByDirection(x, y, d);
                    x = t.Item1;
                    y = t.Item2;
                    whereBorder = (d + 8 - 2) % 8;
                    direction = d;
                }
                else
                {
                    Tuple<int, int> t = moveByDirection(x, y, (d - 1 + 8) % 8);
                    x = t.Item1;
                    y = t.Item2;
                    whereBorder = (d - 1 + 8 - 3) % 8;
                    direction = (d - 1 + 8) % 8;
                }
            }
        }

        private void drawLine(int x1, int x2, int y)
        {
            if (x1 > x2)
            {
                int t = x2;
                x2 = x1;
                x1 = t;
            }
                
            int xLeft = x1, xRight = x1;
            
            for (int i = x1; i < x2; ++i)
            {
                xRight = i;
                if (colorsEqual(borderColor, image.GetPixel(i, y)))
                {
                    break;
                }
            }
            g.DrawLine(pen, new Point(xLeft, y), new Point(xRight, y));
        }

        //Получение всей границы
        private List<Tuple<int, int>> getFullBorder(int x, int y)
        {
            //Список точек
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();
            List<Tuple<int, int>> points1 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> points2 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> points3 = new List<Tuple<int, int>>();
            Dictionary<int, int> d = new Dictionary<int, int>();
            

            int cntDirChanges = 0;
            int wb1 = 2, wb2 = 2, wb3 = 2;
            int direction = 2;
            int whereBorder = 0;
            
            do
            {
                /*
                if (cntDirChanges == 0)
                {
                    points1.Add(Tuple.Create(x, y));
                    if (direction != wb3)
                    {
                        wb1 = wb2;
                        wb2 = wb3;
                        wb3 = direction;
                        ++cntDirChanges;
                    }
                    
                }
                else if (cntDirChanges == 1)
                {
                    points2.Add(Tuple.Create(x, y));
                    if (direction != wb3)
                    {
                        wb1 = wb2;
                        wb2 = wb3;
                        wb3 = direction;
                        ++cntDirChanges;
                    }
                    
                }
                else if (cntDirChanges == 2)
                {
                    points3.Add(Tuple.Create(x, y));
                    if (direction != wb3)
                    {
                        wb1 = wb2;
                        wb2 = wb3;
                        wb3 = direction;
                        ++cntDirChanges;
                    }
                }
                else
                {
                    points3.Add(Tuple.Create(x, y));
                    if (direction != wb3)
                    {
                        points = points.Concat(points1).ToList();
                        points1 = points2;
                        points2 = points3;
                        points3 = new List<Tuple<int, int>>();
                        //points3.Add(Tuple.Create(x, y));

                        wb1 = wb2;
                        wb2 = wb3;
                        wb3 = direction;

                        if (((wb1 + 4) % 8 == (wb2 + 2) % 8) && ((wb2 + 2) % 8 == wb3) ||
                            ((wb1 + 2) % 8 == (wb2 + 1) % 8) && ((wb2 + 1) % 8 == wb3))
                        {
                            points2 = new List<Tuple<int, int>>();
                        }
                    }

                    //Добавление текущей точки
                    //Tuple<int, int> newt = Tuple.Create(x, y);
                    //if (points.Count() == 0 || points.Last() != newt)
                      //  points.Add(newt);
                    //Получение след пикселя
                    //getNextPixel(ref x, ref y, ref whereBorder);

                    //++cntSteps;
                }*/
                /*
                if (d.ContainsKey(y))
                {
                    drawLine(d[y], x, y);
                    for (int i_y = y; i_y <= y; ++ i_y)
                    {
                        if (d.ContainsKey(i_y))
                            d.Remove(i_y);
                    }
                    //d.Remove(y);
                }
                else
                    d.Add(y, x);
                    */
                points.Add(Tuple.Create(x, y));
                getNextPixel(ref x, ref y, ref whereBorder, ref direction);
            } while (((x != firstX) || (y != firstY)) && (points.Count() < (image.Width + image.Height) * 10));
            //points.Concat(points2);
            //points.Concat(points3);
            //pictureBox1.Image = pictureBox1.Image;
            return points;
        }

        private void pointsToFile(ref List<Tuple<int, int>> points, string fname = "points.txt")
        {
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter(fname))
            {
                foreach (var t in points)
                    writetext.WriteLine("x = " + t.Item1 + "| y = " + t.Item2);
            }
        }

        //Закраска границы
        private void fillMyBorderPoints(ref LinkedList<Tuple<int, int>> points)
        {
            myBorderColor = Color.FromArgb(255, 0, 0);
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
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> yBorders = 
                new LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>>();
            //Предыдущее значение
            int y = points.First().Item2;
            //Предыдущее значение
            int x = points.First().Item1;
            //Начало интервала
            int x_first = points.First().Item1;
            //Список интервалов для одного У
            LinkedList<Tuple<int, int>> borders = new LinkedList<Tuple<int, int>>();

            foreach (var t in points)
            {
                //Первая точка уже просмотрена
                if (t != points.First())
                {
                    //Текущие значения
                    int curry = t.Item2;
                    int currx = t.Item1;

                    //У не изменился
                    if (curry == y)
                    {
                        //текущее значение == предыдущее + 1
                        if (currx == x + 1)
                        {
                            x += 1;
                        }
                        else
                        {
                            //Добавление интервала
                            borders.AddLast(Tuple.Create(x_first, x));
                            x_first = currx;
                            x = currx;
                        }
                    }
                    else
                    {
                        //Запоминание предыдущего интервала и всего списка интервалов для предыдущего У
                        borders.AddLast(Tuple.Create(x_first, x));
                        yBorders.AddLast(Tuple.Create(y, borders));
                        borders = new LinkedList<Tuple<int, int>>();

                        x_first = currx;
                        x = currx;
                        y = curry;
                    }
                }
            }
            //Запоминание предыдущего интервала и всего списка интервалов для предыдущего У
            borders.AddLast(Tuple.Create(x_first, x));
            yBorders.AddLast(Tuple.Create(y, borders));
            return yBorders;
        }

        //Получение для одного У всех соответствующих ему Х
        private List<Tuple<int, List<int>>> getYandRelatedX(ref List<Tuple<int, int>> points)
        {
            List<Tuple<int, List<int>>> yXs =
                new List<Tuple<int, List<int>>>();
            //Предыдущее значение
            int prevy = points.First().Item2;
            int currx = 0;
            int curry = 0;

            //Список X для одного У
            List<int> xs = new List<int>();

            foreach (var t in points)
            {
                currx = t.Item1;
                curry = t.Item2;

                if (curry == prevy)
                {
                    xs.Add(currx);
                }
                else
                {
                    yXs.Add(Tuple.Create(prevy, xs));
                    xs = new List<int>();
                    xs.Add(currx);
                    prevy = curry;
                }
            }
            yXs.Add(Tuple.Create(curry, xs));
            
            return yXs;
        }

        public Form1()
        {
            InitializeComponent();

            colorDialog1.FullOpen = true;
            // установка начального цвета для colorDialog
            colorDialog1.Color = Color.Red;
            penColor = Color.Red;

            label2.BackColor = penColor;
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            // установка цвета формы
            penColor = colorDialog1.Color;
            label2.BackColor = penColor;
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

        private void fillByPoints(ref List<Tuple<int, int>> points)
        {
            List<Tuple<int, List<int>>> yXs = getYandRelatedX(ref points);

            foreach (var t in yXs)
            {
                int curry = t.Item1;
                List<int> xs = t.Item2;
                for (int i_x1 = 0; i_x1 < xs.Count; ++i_x1)
                {
                    for (int i_x2 = i_x1+1; i_x2 < xs.Count; ++i_x2)
                    {
                        drawLine(xs[i_x1], xs[i_x2], curry);
                    }
                }

            }
            pictureBox1.Image = pictureBox1.Image;
        }

        //находим внут. границу
        private void find_iner_right(Tuple<int, int> t2, int y, ref int n_x)
        {
            n_x = t2.Item1;

            //innerColor = ((Bitmap)pictureBox1.Image).GetPixel(n_x, y);
            Color currColor = innerColor;

            //пока нет границы идем вперед
            while (colorsEqual(innerColor, currColor) && n_x < t2.Item2)
            {
                n_x++;
                currColor = ((Bitmap)pictureBox1.Image).GetPixel(n_x, y);
            }
        }

        //ищем, пока внут. граница не закончится и мы не выйдем на фон заново
        private void find_our_start(Tuple<int, int> t2, int y, ref int n_x)
        {
            
            n_x = t2.Item1;
            Color currColor = ((Bitmap)pictureBox1.Image).GetPixel(n_x, y);

            while (currColor != innerColor && n_x < t2.Item2)
            {
                n_x += 1;
                currColor = ((Bitmap)pictureBox1.Image).GetPixel(n_x, y);
            }
        }

        //создает новый список со всеми внут. границами
        private LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> find_internal_borders(LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> yBorders)
        {
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> newBorders = new LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>>();
            foreach (Tuple<int, LinkedList<Tuple<int, int>>> t in yBorders)
            {
                int cnt = 0; //индекс четный - соед. линиями пред. и данную точку, иначе нет
                int count = yBorders.Count;
                int new_x = 0;
                int old_x = 0;
			//	bool old_xf = false;

                LinkedList<Tuple<int, int>> yList = new LinkedList<Tuple<int, int>>();

                foreach (Tuple<int, int> t2 in t.Item2)
                {
                    old_x = new_x; //new_x, оld_x используются для прохождения основных границ
                    if (cnt == 0 || (count % 2 == 0 && cnt % 2 == 0))
                    {
                        new_x = t2.Item2;
                        ++cnt;
                        continue;
                    }
                    new_x = t2.Item1;
               
                    int n_x = old_x; //n_x, o_x исользуются для поиска внутренних границ
                    //находим внутр. границу
                    find_iner_right(Tuple.Create(old_x, new_x), t.Item1, ref n_x);
                    if (n_x == new_x)
                    {
                       if (cnt % 2 != 0) //если границы нет, то просто добавляем старый интервал
                            yList.AddLast(Tuple.Create(old_x, new_x));
                    }
                    else
                    {
                        yList.AddLast(Tuple.Create(old_x, n_x)); //добавляем новый интервал
                        while (n_x < new_x) //пока не нашли самую правую границу
                        {
                            int o_x = n_x + 1;
                            find_our_start(Tuple.Create(o_x, new_x), t.Item1, ref n_x); //идем, пока не вылезем с внут. границы
                            o_x = n_x;

                            find_iner_right(Tuple.Create(o_x, new_x), t.Item1, ref n_x); //ищем новую границу
                            if (n_x != new_x || cnt % 2 != 0)
                                yList.AddLast(Tuple.Create(o_x, n_x)); // добавляем интервал
                        }
                    }

                    new_x = t2.Item2;
                    cnt++;
                }
                // добавляем в новый список данные по у
                newBorders.AddLast(Tuple.Create(t.Item1, yList));
            }
            return newBorders;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            g = Graphics.FromImage(pictureBox1.Image);
            pen = new Pen(penColor, 1);


            var loc = e.Location;
            var x = loc.X;
            var y = loc.Y;

            //Получение правой точки границы
            getRightBorder(x, y);
            //Получение всей границы
            List<Tuple<int, int>> points = getFullBorder(firstX, firstY);

            pointsToFile(ref points);

            //Отсортированные и уникальные точки
            List<Tuple<int, int>> pointsSorted = new List<Tuple<int, int>>(
                points.OrderBy(t => t.Item2).ThenBy(t => t.Item1).ToList().Distinct().ToList());

            fillByPoints(ref pointsSorted);

            //Получение У и соответствующих ему интервалов
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> yBorders = getYandBorders(ref pointsSorted);

            //Получаем список со всеми внутр. границами
            LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> newBorders = find_internal_borders(yBorders);

            //Соединяем точки
            //fill(newBorders);

            g.Dispose();
        }

        private void fill(LinkedList<Tuple<int, LinkedList<Tuple<int, int>>>> lst) {
         
            foreach (Tuple<int, LinkedList<Tuple<int, int>>> l in lst) {
                int y = l.Item1;
                foreach (Tuple<int, int> s in l.Item2) {
                    g.DrawLine(pen, s.Item1, y, s.Item2, y);
                    pictureBox1.Image = pictureBox1.Image;
                }
            }
            pen.Dispose();
        }
    }
}
