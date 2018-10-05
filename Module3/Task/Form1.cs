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
      //  bool res = false; //для выведения результатов проверки

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
        }

        List<Tuple<double, double>> primitiv = new List<Tuple<double,double>>(); //список точек для примитива
        List<Point> list = new List<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((radioButton2.Checked && cnt < 1) || radioButton3.Checked) //для ребра больше одной линии нельзя
            {
                drawing = true;
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                list.Add(new Point(e.X, e.Y));
                cnt++;
            }

            if (radioButton1.Checked)
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

            if ((radioButton2.Checked && cnt > 1) || ! drawing ) return;

            list.Add(new Point(e.X, e.Y));
            primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
            Point start = list.First();

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
                    foreach (Tuple<double, double> p in primitiv)
                    {
                        double[,] point = new double[,] { { p.Item1, p.Item2, 1.0 } };
                        double[,] res = matrix_multiplication(point, transferalMatrix);
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
                            primitiv.Add(Tuple.Create(p1.X * 1.0, p1.Y * 1.0));
                        }
                    }
                }

                pictureBox1.Image = bmp;
            }
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Опасная секция
            pictureBox1.MouseDown -= (MouseEventHandler)pictureBox1_MouseDown1;
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);

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

            }
        }
    }
}
