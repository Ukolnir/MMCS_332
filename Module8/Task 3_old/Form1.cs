using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3
{
    public partial class Form1 : Form
    {
        Graphics g2;
        delegate double lambda(double a, double b);
        lambda f;
        Camera cam;

        Dictionary<double, double> Ymax = new Dictionary<double, double>();
        Dictionary<double, double> Ymin = new Dictionary<double, double>();


        public Form1()
        {
            InitializeComponent();
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(pictureBox2.Image);

            g2.ScaleTransform(1, -1);
            g2.TranslateTransform(pictureBox2.Width / 2, -pictureBox2.Height / 2);
            pictureBox2.Image = pictureBox2.Image;

            //Clear();

            textBox1.Text = "0";
            textBox2.Text = "40";
            textBox3.Text = "-10";
            textBox4.Text = "35";
            textBox5.Text = "1";

            comboBox1.SelectedItem = "Sin(x)*Cos(x) = z";

        }



        public void ClearWithout()
        {
            g2.Clear(pictureBox2.BackColor);
            pictureBox2.Image = pictureBox2.Image;
        }


        public void Clear()
        {
            Ymax.Clear();
            Ymin.Clear();

            g2.Clear(pictureBox2.BackColor);
            pictureBox2.Image = pictureBox2.Image;

            comboBox1.SelectedItem = "...";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }


        private void DrawLine(Point p1, Point p2)
        {
            Bitmap bmp = (Bitmap)pictureBox2.Image;
            int dx = Math.Abs(p2.X - p1.X);
            int dy = Math.Abs(p2.Y - p1.Y);

            int sx = p2.X >= p1.X ? 1 : -1;
            int sy = p2.Y >= p1.Y ? 1 : -1;


            int xs = p2.X > p1.X ? p1.X : p2.X;
            int xe = p2.X > p1.X ? p2.X : p1.X;
            for (int i = xs; i <= 2 * xe + dx; ++i)
                if (!Ymax.Keys.Contains(i))
                { Ymax[i] = Ymin[i] = Int32.MaxValue; }

            if (dy <= dx)
            {
                int d = -dx;
                int d1 = dy << 1;
                int d2 = (dy - dx) << 1;
                for (int x = p1.X, y = p1.Y, i = 0; i <= dx; i++, x += sx)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymin.Add(x, Int32.MaxValue);
                        Ymax.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.BlueViolet);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < Ymin[x])
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.BlueViolet);
                        Ymin[x] = y;
                    }
                    else
                    if (y > Ymax[x])
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.Azure);
                        Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        y += sy;
                    }
                    else
                        d += d1;
                }
            }
            else
            {
                int d = dy;
                int d1 = dx << 1;
                int d2 = (dx - dy) << 1;

                if (!Ymin.Keys.Contains(p1.X))
                {
                    Ymin.Add(p1.X, Int32.MaxValue);
                    Ymax.Add(p1.X, Int32.MaxValue);
                }


                double m1 = Ymin[p1.X];
                double m2 = Ymax[p1.X];
                for (int x = p1.X, y = p1.Y, i = 0; i <= dy; i++, y += sy)
                {
                    if (!Ymin.Keys.Contains(x))
                    {
                        Ymax.Add(x, Int32.MaxValue);
                        Ymin.Add(x, Int32.MaxValue);
                    }

                    if (Ymin[x] == Int32.MaxValue) // YMin, YMax not inited
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.BlueViolet);
                        Ymin[x] = Ymax[x] = y;
                    }
                    else
                    if (y < m1)
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.BlueViolet);
                        if (y < Ymin[x])
                            Ymin[x] = y;

                    }
                    else
                    if (y > m2)
                    {
                        if (x >= 0 && x < pictureBox2.Width && y >= 0 && y < pictureBox2.Height)
                            bmp.SetPixel(x, y, Color.Azure);
                        if (y > Ymax[x])
                            Ymax[x] = y;
                    }

                    if (d > 0)
                    {
                        d += d2;
                        x += sx;

                        if (!Ymin.Keys.Contains(x))
                        {
                            Ymax.Add(x, Int32.MaxValue);
                            Ymin.Add(x, Int32.MaxValue);
                        }

                        m1 = Ymin[x];
                        m2 = Ymax[x];
                    }
                    else
                        d += d1;
                }
            }

            pictureBox2.Image = bmp;
        }

        void PlotSurface(double x1, double y1, double x2, double y2, double fmin, double fmax, int n1, int n2)
        {
            List<Point> CurrLine = new List<Point>();
            List<Point> NextLine = new List<Point>();

            double phiz = Double.Parse(textBox6.Text) * 3.1415926 / 180;
            double psiz = Double.Parse(textBox7.Text) * 3.1415926 / 180;

            double phi = 30 * 3.1415926 / 180 + phiz;
            double psi = 10 * 3.1415926 / 180 + psiz;
            double sphi = Math.Sin(phi);
            double cphi = Math.Cos(phi);
            double spsi = Math.Sin(psi);
            double cpsi = Math.Cos(psi);
            double[] e1 = { cphi, sphi, 0 };
            double[] e2 = { spsi * sphi, spsi * cphi, cpsi };

            double x, y, z;
            double hx = (x2 - x1) / n1;
            double hy = (y2 - y1) / n2;

            double xmin = (e1[0] >= 0 ? x1 : x2) * e1[0] + (e1[1] >= 0 ? y1 : y2) * e1[1];
            double xmax = (e1[0] >= 0 ? x2 : x1) * e1[0] + (e1[1] >= 0 ? y2 : y1) * e1[1];
            double ymin = (e2[0] >= 0 ? x1 : x2) * e2[0] + (e2[1] >= 0 ? y1 : y2) * e2[1];
            double ymax = (e2[0] >= 0 ? x2 : x1) * e2[0] + (e2[1] >= 0 ? y2 : y1) * e2[1];


            if (e2[2] >= 0)
            {
                ymin += fmin * e2[2];
                ymax += fmax * e2[2];
            }
            else
            {
                ymin += fmax * e2[2];
                ymax += fmin * e2[2];
            }
            double ax = 10 - pictureBox2.Width * xmin / (xmax - xmin);
            double bx = pictureBox2.Width / (xmax - xmin);
            double ay = 10 - pictureBox2.Height * ymin / (ymax - ymin);
            double by = pictureBox2.Height / (ymax - ymin);

            for (int i = 0; i < Math.Abs(x2 - x1); i++)
                Ymin[i] = Ymax[i] = Int32.MaxValue;

            for (int i = 0; i < n1; i++)
            {
                x = x1 + i * hx;
                y = y1 + (n2 - 1) * hy;
                z = f(x, y);


                var t0 = matrix_multiplication(cam.translateAtPosition(), new double[4, 1] { { x }, { y }, { z }, { 1 } });
                var t1 = matrix_multiplication(cam.translateAtAngles(),
                new double[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });
                PointPol p = new PointPol(t1[0, 0], t1[1, 0], t1[2, 0]);

                int X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                int Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                if (CurrLine.Count() > i)
                    CurrLine[i] = new Point(X, Y);
                else
                    CurrLine.Add(new Point(X, Y));
            }

            for (int i = n2 - 1; i > -1; i--)
            {
                for (int j = 0; j < n1 - 1; j++)
                    DrawLine(CurrLine[j], CurrLine[j + 1]);
                if (i > 0)
                    for (int j = 0; j < n1; j++)
                    {
                        x = x1 + j * hx;
                        y = y1 + (i - 1) * hy;
                        z = f(x, y);


                        var t0 = matrix_multiplication(cam.translateAtPosition(), new double[4, 1] { { x }, { y }, { z }, { 1 }});
                        var t1 = matrix_multiplication(cam.translateAtAngles(),
                        new double[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });
                        PointPol p = new PointPol(t1[0, 0], t1[1, 0], t1[2, 0]);

                        int X = (int)(ax + bx * (p.X * e1[0] + p.Y * e1[1]));
                        int Y = (int)(ay + by * (p.X * e2[0] + p.Y * e2[1] + p.Z * e2[2]));
                        if (NextLine.Count() > j)
                            NextLine[j] = new Point(X, Y);
                        else
                            NextLine.Add(new Point(X, Y));
                        DrawLine(CurrLine[j], NextLine[j]);
                        CurrLine[j] = NextLine[j];
                    }
            }
            CurrLine.Clear();
            NextLine.Clear();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            double x0 = Double.Parse(textBox1.Text.ToString());
            double x1 = Double.Parse(textBox2.Text.ToString());
            double y0 = Double.Parse(textBox3.Text.ToString());
            double y1 = Double.Parse(textBox4.Text.ToString());
            double step = Double.Parse(textBox5.Text.ToString());

            double x = double.Parse(textBox6.Text);
            double y = double.Parse(textBox8.Text);
            double z = double.Parse(textBox7.Text);

            double t = double.Parse(textBoxT.Text);
            double ya = double.Parse(textBoxY.Text);
            double r = double.Parse(textBoxR.Text);

            cam = new Camera(x, y, z, t, ya, r);

            PlotSurface(x0, y0, x1, y1, -1, 1, (int)((x1 - x0) / step), (int)((y1 - y0) / step));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "x^2 + y^2 = z":
                    f = (x, y) => x * x + y * y;
                    break;
                case "Sin(x)*Cos(x) = z":
                    f = (x, y) => Math.Sin(x) * Math.Cos(y);
                    break;
                case "((1 – x)^2 + 100 * (1 + y – x^2)^2) / 500 - 2.5 = z":
                    f = (x, y) => ((1 - x) * (1 - x) + 100 * (1 + y - x * x) * (1 + y - x * x)) / 500 - 2.5;
                    break;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Clear();
            double x0 = Double.Parse(textBox1.Text.ToString());
            double x1 = Double.Parse(textBox2.Text.ToString());
            double y0 = Double.Parse(textBox3.Text.ToString());
            double y1 = Double.Parse(textBox4.Text.ToString());
            double step = Double.Parse(textBox5.Text.ToString());

            PlotSurface(x0, y0, x1, y1, -1, 1, (int)((x1 - x0) / step), (int)((y1 - y0) / step));

        }


        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
            double x0 = Double.Parse(textBox1.Text.ToString());
            double x1 = Double.Parse(textBox2.Text.ToString());
            double y0 = Double.Parse(textBox3.Text.ToString());
            double y1 = Double.Parse(textBox4.Text.ToString());
            double step = Double.Parse(textBox5.Text.ToString());

            double x = double.Parse(textBox1.Text);
            double y = double.Parse(textBox2.Text);
            double z = double.Parse(textBox3.Text);

            double t = double.Parse(textBoxT.Text);
            double ya = double.Parse(textBoxY.Text);
            double r = double.Parse(textBoxR.Text);

            cam = new Camera(x, y, z, t, ya, r);
            PlotSurface(x0, y0, x1, y1, -1, 1, (int)((x1 - x0) / step), (int)((y1 - y0) / step));
        }

        private void groupBox16_Enter(object sender, EventArgs e)
        {

        }
    }

    public struct PointPol
    {
        public double X, Y, Z;
        public PointPol(double x, double y, double z) { X = x; Y = y; Z = z; }
    }

    public class Camera
    {
        public PointPol position;
        public double pitch, yaw, roll; //Тангаж, рысканье и крен
        //public PointPol view;

        public Camera(double x, double y, double z, double p, double ya, double r)
        {
            position = new PointPol(x, y, z);
            pitch = p * Math.PI / 180;
            yaw = ya * Math.PI / 180;
            roll = r * Math.PI / 180;
        }

        public double[,] translateAtPosition()
        {
            return new double[4, 4]{{1,0,0,-(position.X)},{0,1,0,-(position.Y)}, {0,0,1,-(position.Z)},
            {0,0,0,1}};
        }

        public double[,] translateAtAngles()
        {
            return new double[3, 3] {
                { Math.Cos(yaw) * Math.Cos(roll), -Math.Cos(yaw) * Math.Sin(roll), Math.Sin(yaw) },
                {Math.Sin(pitch)*Math.Sin(yaw)*Math.Cos(roll) + Math.Sin(roll)*Math.Cos(pitch),
                                                            -Math.Sin(pitch)*Math.Sin(yaw)*Math.Sin(roll) + Math.Cos(roll)*Math.Cos(pitch),
                                                                        -Math.Sin(pitch)*Math.Cos(yaw)},
                {-Math.Cos(pitch)*Math.Sin(yaw)*Math.Cos(roll) + Math.Sin(pitch)*Math.Sin(roll),
                    Math.Cos(pitch)*Math.Sin(yaw)*Math.Sin(roll) + Math.Sin(pitch)*Math.Cos(roll),
                                                                        Math.Cos(yaw)*Math.Cos(pitch)} };
        }
    }
}