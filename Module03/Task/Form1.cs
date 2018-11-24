using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task
{
    public partial class Form1 : Form
    {
        bool drawing; //рисуем ли мы на данный момент?
        int cnt; //счетчик для прорисовки ребра
        Bitmap bmp;
        double[,] transferalMatrix;
        Tuple<double, double> dot;
        bool method = false; //применимость(true) или проверка
        bool t5 = false;


        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
            label5.Visible = false;
        }

        //удаление всех объектов
        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            list.Clear();
            primitiv.Clear();
            dot = Tuple.Create(-1.0, -1.0);
            cnt = 0;
            label5.Text = "";
            comboBox1.SelectedItem = "...";
            t5 = false;
        }

        List<Tuple<double, double>> primitiv = new List<Tuple<double,double>>(); //список точек для примитива
        List<Point> list = new List<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((radioButton2.Checked && (cnt < 1 || cnt < 2 && t5)) || radioButton3.Checked) //для ребра больше одной линии нельзя
            {
                drawing = true;
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                list.Add(new Point(e.X, e.Y));
                cnt++;
            }

            if (radioButton1.Checked && dot.Item1 == -1)
            {
                dot = Tuple.Create(e.X * 1.0, e.Y * 1.0);
                ((Bitmap)pictureBox1.Image).SetPixel(e.X, e.Y, Color.Black);

                pictureBox1.Image = pictureBox1.Image;
            }
        }


        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            button2.Text = "Применить";
            button2.Visible = true;

            if ((radioButton2.Checked && (cnt > 1 && !t5 || cnt > 2 && t5)) || ! drawing ) return;

            list.Add(new Point(e.X, e.Y));
            primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
            Point start = list.First();

            if (!t5)
            {
                foreach (var p in list)
                {
                    var pen = new Pen(Color.Black, 1);
                    var g = Graphics.FromImage(pictureBox1.Image);
                    g.DrawLine(pen, start, p);
                    pen.Dispose();
                    g.Dispose();
                    pictureBox1.Image = pictureBox1.Image;

                    start = p;
                }
            }
            else
            {
                var pen = new Pen(Color.Black, 1);
                var g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(pen, start, new Point(Convert.ToInt32(Math.Round(primitiv[1].Item1)), Convert.ToInt32(Math.Round(primitiv[1].Item2))));
                g.DrawLine(pen, new Point(Convert.ToInt32(Math.Round(primitiv[2].Item1)), Convert.ToInt32(Math.Round(primitiv[2].Item2))), 
                    new Point(Convert.ToInt32(Math.Round(primitiv[3].Item1)), Convert.ToInt32(Math.Round(primitiv[3].Item2))));
                pen.Dispose();
                g.Dispose();
                pictureBox1.Image = pictureBox1.Image;
            }

            drawing = false;
        }

        //перемножение матриц
        private double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];

            for(int i = 0; i < m1.GetLength(0); ++i)
                for(int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            list.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            list.Clear();
        }

        //поиск центра примитива
        private void find_center(ref double x, ref double y)
        {

            foreach (var c in primitiv)
            {
                x += c.Item1;
                y += c.Item2;
            }

            x /= primitiv.Count;
            y /= primitiv.Count;
        }

        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }

        bool less1(double p1, double p2)
        {
	        return (p1<p2 && Math.Abs(p1 - p2) >= 0.00001);
        }

    bool less_or_equal(double x, double y)
        {
            return Math.Abs(x - y) < 0.0001 || less1(x, y);
        }

        private bool vert_intersect(Tuple<double, double> c1, Tuple<double, double> c2, Tuple<double, double> d1, Tuple<double, double> d2, ref Point p)
        {
	        double x1 = c1.Item1;
            //Ax+b=y, A - tang
            double tan2 = (d1.Item2 - d2.Item2) / (d1.Item1 - d2.Item1);
            double b2 = d1.Item2 - tan2 * d1.Item1;
            double y1 = tan2 * x1 + b2;

	        //x,y лежат в заданных диапазонах 
	        if (less_or_equal(d1.Item1, x1) && less_or_equal(x1, d2.Item1) && less_or_equal(Math.Min(c1.Item2, c2.Item2), y1) 
                        && less_or_equal(y1, Math.Max(c1.Item2, c2.Item2)))
	        {
		        p.X = Convert.ToInt32(Math.Round(x1));
                p.Y = Convert.ToInt32(Math.Round(y1));
                return true;
	        }
	        return false;
        }


        bool intersect(Tuple<double, double> r1, Tuple<double, double> r2, Tuple<double, double> d1, Tuple<double, double> d2, ref Point p)
        {
            if (r1.Item1 == d1.Item1 && r1.Item2 == d1.Item2 || r1.Item1 == d2.Item1 && r1.Item2 == d2.Item2)
            {
                p = new Point(Convert.ToInt32(Math.Round(r1.Item1)), Convert.ToInt32(Math.Round(r1.Item2)));
                return true;
            }

            if (r2.Item1 == d1.Item1 && d1.Item2 == r2.Item2 || d1.Item1 == r2.Item1 && d1.Item2 == r2.Item2)
            {
                p = new Point(Convert.ToInt32(Math.Round(d1.Item1)), Convert.ToInt32(Math.Round(d1.Item2)));
                return true;
            }

            if (Math.Abs(d1.Item1 - d2.Item1) < 0.0001)
                return vert_intersect(d1, d2, r1, r2, ref p);

            if (Math.Abs(r1.Item1 - r2.Item1) < 0.0001)
                return vert_intersect(r1, r2, d1, d2, ref p);

                //Ax+b = y
            double a1 = (r1.Item2 - r2.Item2) / (r1.Item1 - r2.Item1);
            double a2 = (d1.Item2 - d2.Item2) / (d1.Item1 - d2.Item1);
            double b1 = r1.Item2 - a1 * r1.Item1;
            double b2 = d1.Item2 - a2 * d1.Item1;

            if (a1 == a2)
                return false; //отрезки параллельны


            //x - абсцисса точки пересечения двух прямых
            double x = (b2 - b1) / (a1 - a2);
            double y = a1 * x + b1;

            if (less1(x, Math.Min(r1.Item1, r2.Item1)) || less1(Math.Max(r1.Item1, r2.Item1), x) ||
                less1(x, Math.Min(d1.Item1, d2.Item1)) || less1(Math.Max(d1.Item1, d2.Item1), x) ||
                less1(y, Math.Min(r1.Item2, r2.Item2)) || less1(Math.Max(r1.Item2, r2.Item2), y) ||
                less1(y, Math.Min(d1.Item2, d2.Item2)) || less1(Math.Max(d1.Item2, d2.Item2), y))
                return false; //точка x находится вне пересечения проекций отрезков на ось X 

            p.X = Convert.ToInt32(Math.Round(x));
            p.Y = Convert.ToInt32(Math.Round(y));
            return true;
        }

        //поиск точки пересечения
        private bool intersection_point_search(ref Point p)
        {
            return intersect(primitiv[0], primitiv[1], primitiv[2], primitiv[3], ref p);
        }

        //принадлежность точки к многоугольнику
        private bool point_affiliation()
        {
            Tuple<double, double> dot1 = Tuple.Create(pictureBox1.Width - 1.0, dot.Item2);
            int cnt = 0;
            Point p = new Point(0, 0);
            Tuple<double, double> p1 = primitiv.First();

            int num = 0;

            foreach (var p2 in primitiv)
            {
                if (p1 == p2) continue;

                if (dot.Item1 <= p1.Item1 && Math.Abs(dot.Item2 - p1.Item2) < 0.0001 && num == 0)
                {
                    num++;
                    continue;
                }
                else
                    num = 0;

                if (intersect(dot, dot1, p1, p2, ref p))
                    cnt++;

                p1 = p2;
            }

            if (!(dot.Item1 <= p1.Item1 && Math.Abs(dot.Item2 - p1.Item2) < 0.0001 && num == 0))
            {

                if (intersect(dot, dot1, p1, primitiv.First(), ref p))
                    cnt++;
            }
            return cnt % 2 != 0;
        }

        private void choose_method()
        {
            //Выбор матрицы афинного преобразования
            switch (comboBox1.SelectedItem.ToString())
            { 
                case "Смещение":
                    double tX = System.Convert.ToDouble(textBox1.Text);
                    double tY = System.Convert.ToDouble(textBox2.Text);
                    transferalMatrix = new double[,] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { tX, tY, 1.0 } };

                    break;
                
                case "Масштабирование":
                    double cm = System.Convert.ToDouble(textBox1.Text); //прочитали коэфициент
                 
                    if (!radioButton1.Checked)
                    {
                        double a = 0, b = 0;
                        find_center(ref a, ref b);
                        transferalMatrix = new double[3, 3] { { cm, 0, 0 }, { 0, cm, 0 }, { (1 - cm) * a, (1 - cm)*b, 1 } };
                    }
                    else
                        transferalMatrix = new double[3, 3] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { 0, 0, 1.0 } };
                    break;

                case "Поворот":
                    double c = System.Convert.ToDouble(textBox1.Text);
                    double d = System.Convert.ToDouble(textBox2.Text);
                    double p = System.Convert.ToDouble(textBox3.Text) * Math.PI / 180;
					radioButton3.Checked = true;
                    double cos = Math.Cos(p);
                    double sin = Math.Sin(p);
                    transferalMatrix = new double[,] { {cos, sin, 0}, {-sin, cos, 0}, 
                        {cos*(-c)+d*sin+c, (-c)*sin-d*cos+d, 1}};
                    break;

                case "Положение точки относительно ребра":
                    if (dot.Item1 == -1 || primitiv.Count != 2)
                        return;
                    Tuple<double, double> cm1 = primitiv.First();

                    label5.Text = "Точка лежит относительно ребра: ";

                    double yb = primitiv.Last().Item2 - cm1.Item2;
                    double xb = primitiv.Last().Item1 - cm1.Item1;
                    double ya = dot.Item2 - cm1.Item2;
                    double xa = dot.Item1 - cm1.Item1;

                    if (yb * xa - xb * ya > 0)
                        label5.Text += " левее";
                    else
                        if (yb * xa - xb * ya < 0)
                            label5.Text += " правее";
                        else
                            label5.Text += " лежит на прямой";
                    break;

                case "Принадлежит ли точка многоугольнику":
                    if (dot.Item1 == -1 || primitiv.Count < 3)
                        return;

                    if (point_affiliation())
                        label5.Text += " да";
                    else
                        label5.Text += " нет";

                    break;

                case "Поиск точки пересечения двух ребер":
                    t5 = true;
                    if (primitiv.Count == 4)
                    {
                        label5.Text = "Точка пересечения найдена: ";
                        Point p1 = new Point(-1,-1);
                        bool f = intersection_point_search(ref p1);
                        if (f)
                        {
                            label5.Text += " да;  Координаты: " + p1.X.ToString() + " " + p1.Y.ToString();
                            ((Bitmap)pictureBox1.Image).SetPixel(p1.X, p1.Y, Color.Red);

                            if (p1.X - 1 > 0)
                                ((Bitmap)pictureBox1.Image).SetPixel(p1.X - 1, p1.Y, Color.Red);
                            if (p1.Y - 1 > 0)
                                ((Bitmap)pictureBox1.Image).SetPixel(p1.X, p1.Y - 1, Color.Red);

                            if (p1.X + 1 < pictureBox1.Width)
                                ((Bitmap)pictureBox1.Image).SetPixel(p1.X + 1, p1.Y, Color.Red);
                            if (p1.Y + 1 < pictureBox1.Height)
                                ((Bitmap)pictureBox1.Image).SetPixel(p1.X, p1.Y + 1, Color.Red);
                            pictureBox1.Image = pictureBox1.Image;
                        }
                        else
                            label5.Text += " нет";
                    }
                    break;

                default:
                    break;
            }
        }

        //для очищения экрана для перерисовке при применении алгоритма 
        private void ClearWithout()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button2_Click1(object sender, EventArgs e)
        {
            choose_method();
            if (method)
            {
                List<Point> newprimitiv = new List<Point>();

                if (radioButton1.Checked)
                {
                    double[,] point = new double[,] { { dot.Item1, dot.Item2, 1.0 } };
                    double[,] res = matrix_multiplication(point, transferalMatrix);
                    newprimitiv.Add(new Point(Convert.ToInt32(Math.Round(res[0, 0])), Convert.ToInt32(Math.Round(res[0, 1]))));
                    ClearWithout();
                    bmp.SetPixel(newprimitiv.First().X, newprimitiv.First().Y, Color.Black);
                    dot = Tuple.Create(newprimitiv.First().X * 1.0, newprimitiv.First().Y * 1.0);
                }
                else
                {
					List<Tuple<double, double>> l1 = new List<Tuple<double, double>>();
                    foreach (Tuple<double, double> p in primitiv)
                    {
                        double[,] point = new double[,] { { p.Item1, p.Item2, 1.0 } };
                        double[,] res = matrix_multiplication(point, transferalMatrix);
						l1.Add(Tuple.Create(res[0, 0], res[0, 1]));
						newprimitiv.Add(new Point(Convert.ToInt32(Math.Round(res[0, 0])), Convert.ToInt32(Math.Round(res[0, 1]))));
                    }

                    ClearWithout();

                    primitiv.Clear();

                    Point p1 = newprimitiv.First();
                    primitiv.Add(Tuple.Create(p1.X * 1.0, p1.Y * 1.0));
                    foreach (Point c in newprimitiv)
                    {
                        if (c != p1)
                        {
                            var g = Graphics.FromImage(bmp);
                            Pen p = new Pen(Color.Black, 1);
                            g.DrawLine(p, p1, c);
                            p1 = c;
                            p.Dispose();
                            g.Dispose();
                        }
                    }

					primitiv = l1;

                }

                pictureBox1.Image = bmp;
            }
           
        }

		private void fff(object sender, EventArgs e) {


		}


		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Опасная секция
            pictureBox1.MouseDown -= (MouseEventHandler)pictureBox1_MouseDown1;
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
			pictureBox1.MouseUp += new MouseEventHandler(pictureBox_MouseUp);

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Смещение":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку смещения";
                    textBox2.Visible = true;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = true;
                    label5.Visible = false;
                    break;

                case "Поворот":
                    label2.Visible = true;
                    textBox1.Visible = true;
                    label2.Text = "Выберите точку поворота(укажите мышкой)";
                    textBox2.Visible = true;
                    label3.Visible = true;
                    textBox3.Visible = true;
                    label5.Visible = false;
                    method = true;
                    //Опасная секция
                    pictureBox1.MouseDown -= (MouseEventHandler)pictureBox1_MouseDown;
                    pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown1);
					pictureBox1.MouseUp += new MouseEventHandler(fff);
					pictureBox1.MouseUp -= (MouseEventHandler)pictureBox_MouseUp;
                    break;

                case "Масштабирование":
                    label2.Visible = true;
                    label5.Visible = false;
                    textBox1.Visible = true;
                    label2.Text = "Выберите коэффициент масштабирования";
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = true;
                    break;

                case "Положение точки относительно ребра":
                    label2.Visible = false;
                    label5.Visible = true;
                  //  label5.Text = "Точка лежит относительно ребра: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                    break;

                case "Принадлежит ли точка многоугольнику":
                    label2.Visible = false;
                    label5.Visible = true;
                    label5.Text = "Принадлежит точка многоугольнику: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                    break;

                case "Поиск точки пересечения двух ребер":
                    label5.Visible = true;
                    label5.Text = "Нарисуйте новую прямую: ";
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label3.Visible = false;
                    textBox3.Visible = false;
                    method = false;
                   // cnt = 0;
                    t5 = true;
                    break;

                default:
                    break;
            }
        }
    }
}
