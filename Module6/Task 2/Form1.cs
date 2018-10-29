﻿using System;
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
        Graphics g;

        List<PointPol> points = new List<PointPol>();

        List<Polygon> fig = new List<Polygon>();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);

            write_axes();
        
        }

        public void find_center(List<PointPol> pol, ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var p in pol)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }

            x /= pol.Count();
            y /= pol.Count();
            z /= pol.Count();
        }

        public void write_axes()
        {
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width/2, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width/2, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width/2);

            Point o = p0.To2D();
            Point x = p1.To2D();
            Point y = p2.To2D();
            Point z = p3.To2D();

            Font font = new Font("Arial", 8);
            SolidBrush brush = new SolidBrush(Color.Black);
            
            g.DrawString("X", font, brush, x);
            g.DrawString("Y", font, brush, y);
            g.DrawString("Z", font, brush, z);

            Pen my_pen = new Pen(Color.Blue);
            g.DrawLine(my_pen, o, x);
            my_pen.Color = Color.Red;
            g.DrawLine(my_pen, o, y);
            my_pen.Color = Color.Green;
            g.DrawLine(my_pen, o, z);

            pictureBox1.Image = pictureBox1.Image;
        }

        public void Clear()
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

            write_axes();

            //comboBox1.SelectedItem = "...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
            points.Clear();
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

        private void drawPolygon(Polygon pol, Color c)
        {
            List<Tuple<Point, Point>> l = pol.to2d();
            foreach (var p in l)
            {
                g.DrawLine(new Pen(c), p.Item1, p.Item2);
            }
            //g.DrawLine(new Pen(c), l.Last().Item2, l.First().Item1);
            //g.DrawLine(new Pen(c), l.First().Item1, l.First().Item2);
            
        }

        private void buildFigure(string axis, int cnt)
        {
            fig.Clear();
            List<PointPol> l1 = new List<PointPol>(points);


            Edge dir = new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
            if (axis == "X")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
            }
            if (axis == "Y")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(0, 1, 0));
            }
            if (axis == "Z")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(0, 0, 1));
            }

            double angle = 360.0 / cnt;

            for (int i = 0; i < cnt; ++i)
            {
                List<PointPol> l2 = new List<PointPol>(l1);
                Polygon pol = new Polygon(l2);
                double a = 0, b = 0, c = 0;
                pol.find_center(ref a, ref b, ref c);
                
                for (int j = 0; j < l2.Count(); ++j)
                {
                    l2[j] = l2[j].rotate(dir, angle, a, b, c);
                }

                List<PointPol> lsum = new List<PointPol>(l1);
                lsum.AddRange(l2);
                //fig.Add(new Polygon(lsum));
                fig.Add(new Polygon(l1));

                l1 = l2;
            }
        }

        private void drawFig()
        {
            foreach (var item in fig)
            {
                drawPolygon(item, Color.Black);
            }
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Polygon pol = new Polygon(points);
            //drawPolygon(pol, Color.Black);

            string ax = comboBoxBuildAxis.SelectedItem.ToString();
            int cnt = Int32.Parse(textBoxBuildCount.Text);

            buildFigure(ax, cnt);
            drawFig();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2 - e.Y;
            int z = Int32.Parse(textBoxBuildZ.Text);

            points.Add(new PointPol(x, y, z));

            if (points.Count() > 1)
            {
                Clear();
                Polygon pol = new Polygon(points);
                drawPolygon(pol, Color.Black);
                pictureBox1.Image = pictureBox1.Image;
            }

            labelDebug.Text = "x = " + x.ToString() +
                " | y = " + y.ToString() + " | z = " + z.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2 - e.Y;
            labelDebug2.Text = "x = " + x.ToString() + " | y = " + y.ToString();
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            double ind = Double.Parse(textBoxScale.Text);
            foreach (var item in fig)
            {
                item.scale(ind);
            }
            Clear();
            drawFig();
        }

        private void buttonShift_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBoxShiftX.Text);
            int y = Int32.Parse(textBoxShiftY.Text);
            int z = Int32.Parse(textBoxShiftZ.Text);
            foreach (var item in fig)
            {
                item.shift(x, y, z);
            }
            Clear();
            drawFig();
        }

        private void buttonReflection_Click(object sender, EventArgs e)
        {
            string axis = comboBoxReflection.SelectedItem.ToString();
            foreach (var item in fig)
            {
                item.reflection(axis);
            }
            Clear();
            drawFig();
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            int x1 = Int32.Parse(textBoxX1.Text);
            int y1 = Int32.Parse(textBoxY1.Text);
            int z1 = Int32.Parse(textBoxZ1.Text);
            int x2 = Int32.Parse(textBoxX2.Text);
            int y2 = Int32.Parse(textBoxY2.Text);
            int z2 = Int32.Parse(textBoxZ2.Text);
            double angle = Double.Parse(textBoxAngle.Text);

            PointPol p1 = new PointPol(x1, y1, z1);
            PointPol p2 = new PointPol(x2, y2, z2);
            Edge ed = new Edge(p1, p2);

            foreach (var item in fig)
            {
                item.rotate(ed, angle);
            }
            Clear();
            drawFig();
        }
    }


    public class PointPol
    {
        public double X, Y, Z, W;

        public PointPol(double x, double y, double z)
        {
            X = x; Y = y; Z = z; W = 1;
        }

        public PointPol(double x, double y, double z, double w)
        {
            X = x; Y = y; Z = z; W = w;
        }

        public double[,] getP()
        {
            return new double[1, 4] { { X, Y, Z, W } };
        }

        public double[,] getP1()
        {
            return new double[3, 1] { { X }, { Y }, { Z } };
        }

        public double[,] getPol()
        {
            return new double[4, 1] { { X }, { Y }, { Z }, { W } };
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



        public PointPol scale(double ind_scale, double a, double b, double c)
        {
            double[,] transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -a, -b, -c, 1 } };
            var t1 = matrix_multiplication(getP(), transfer);

            t1 = matrix_multiplication(t1, new double[4, 4] { { ind_scale, 0, 0, 0 }, { 0, ind_scale, 0, 0 }, { 0, 0, ind_scale, 0 }, { 0, 0, 0, 1 } });
            transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { a, b, c, 1 } };
            t1 = matrix_multiplication(t1, transfer);

            return translatePol(t1);
        }

        public PointPol rotate(Edge direction, double angle, double a, double b, double c)
        {
            double phi = Math.PI / 360 * angle;
            PointPol p = shift(-a, -b, -c);

            double x1 = direction.P1.X;
            double y1 = direction.P1.Y;
            double z1 = direction.P1.Z;

            double x2 = direction.P2.X;
            double y2 = direction.P2.Y;
            double z2 = direction.P2.Z;

            double vecx = x2 - x1;
            double vecy = y2 - y1;
            double vecz = z2 - z1;

            double len = Math.Sqrt(vecx * vecx + vecy * vecy + vecz * vecz);

            double l = vecx / len;
            double m = vecy / len;
            double n = vecz / len;

            double[,] transfer = new double[4, 4] {
                    { l*l+Math.Cos(phi)*(1 - l*l), l*(1-Math.Cos(phi))*m + n*Math.Sin(phi), l*(1-Math.Cos(phi))*n - m*Math.Sin(phi), 0 },
                    { l*(1-Math.Cos(phi))*m - n*Math.Sin(phi), m*m+Math.Cos(phi)*(1 - m*m), m*(1-Math.Cos(phi))*n + l*Math.Sin(phi), 0 },
                    { l*(1-Math.Cos(phi))*n + m*Math.Sin(phi), m*(1-Math.Cos(phi))*n - l*Math.Sin(phi), n*n+Math.Cos(phi)*(1 - n*n), 0 },
                    { 0, 0, 0, 1 } };
            var t1 = matrix_multiplication(p.getP(), transfer);

            t1 = matrix_multiplication(t1, transfer);

            PointPol p2 = translatePol(t1);
            PointPol p3 = p2.shift(a, b, c);

            return p3;
        }

        private PointPol translatePol(double[,] f)
        {
            return new PointPol(f[0, 0], f[0, 1], f[0, 2], f[0, 3]);
        }

        private PointPol translatePol1(double[,] f)
        {
            return new PointPol(f[0, 0], f[1, 0], f[2, 0], f[3, 0]);
        }

        public PointPol shift(double x, double y, double z)
        {
            double[,] shiftMatrix = new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };
            return translatePol1(matrix_multiplication(shiftMatrix, getPol()));
        }
            //Изометрическая проекция
        double[,] displayMatrix = new double[3, 3] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5) }, { 1 / Math.Sqrt(6), Math.Sqrt(2) / 3, 1 / Math.Sqrt(6) }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) } };

        public Point To2D()
        {
            var temp = matrix_multiplication(displayMatrix, getP1());
            int x = Convert.ToInt32(temp[0, 0]);
            int y = Convert.ToInt32(temp[1, 0]);
   
            var temp2d = new Point(x, y);
            return temp2d;
        }
    }

    public class Edge
    {
        public PointPol P1;
        public PointPol P2;

        public Edge(PointPol p1, PointPol p2) { P1 = p1; P2 = p2; }

        public void scale(double ind_scale, double a, double b, double c)
        {
            P1 = P1.scale(ind_scale, a, b, c);
            P2 = P2.scale(ind_scale, a, b, c);
        }

        public void shift(double a, double b, double c)
        {
            P1 = P1.shift(a, b, c);
            P2 = P2.shift(a, b, c);
        }

        public void rotate(Edge e, double angle, double a, double b, double c)
        {
            P1 = P1.rotate(e, angle, a, b, c);
            P2 = P2.rotate(e, angle, a, b, c);
        }

        public void reflection(string axis, double a, double b, double c)
        {

            if (axis == "X")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0)), 180, a, b, c);
                shift(-a * 2, 0, 0);
            }
            if (axis == "Y")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(0, 1, 0)), 180, a, b, c);
                shift(0, -b * 2, 0);
            }
            if (axis == "Z")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(0, 0, 1)), 180, a, b, c);
                shift(0, 0, -c * 2);
            }
        }

        public Tuple<Point, Point> to2d()
        {
            return Tuple.Create(P1.To2D(), P2.To2D());
        }
    }

    public class Polygon
    {
        //public List<PointPol> points = new List<PointPol>();
        public List<Edge> edges = new List<Edge>();

        public Polygon(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
                //points.Add(el.P1);
                //points.Add(el.P2);
            }
        }
        //грани
        public Polygon(List<PointPol> ps)
        {
            PointPol p1 = ps.First();
            //points.Add(p1);
            for (int i = 1; i < ps.Count(); ++i)
            {
                //points.Add(ps[i]);
                edges.Add(new Edge(p1, ps[i]));
                p1 = ps[i];
            }

            edges.Add(new Edge(p1, ps.First()));
        }
        /*
        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var p in points)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }

            x /= points.Count();
            y /= points.Count();
            z /= points.Count();
        }
        */

        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var e in edges)
            {
                x += (e.P1.X + e.P2.X) /2;
                y += (e.P1.Y + e.P2.Y) /2;
                z += (e.P1.Z + e.P2.Z) /2;
            }

            x /= edges.Count();
            y /= edges.Count();
            z /= edges.Count();
        }

        public void scale(double ind_scale)
        {
            double a = 0, b = 0, c = 0;
            find_center(ref a, ref b, ref c);
            foreach (var e in edges)
                e.scale(ind_scale, a, b, c);
        }

        public void shift(double a, double b, double c)
        {
            foreach (var e in edges)
                e.shift(a, b, c);
        }

        public void rotate(Edge edge, double angle)
        {
            double a = 0, b = 0, c = 0;
            find_center(ref a, ref b, ref c);
            foreach (var e in edges)
                e.rotate(edge, angle, a, b, c);
        }

        public void reflection(string axis)
        {
            double a = 0, b = 0, c = 0;
            find_center(ref a, ref b, ref c);
            foreach (var e in edges)
                e.reflection(axis, a, b, c);
        }

        public List<Tuple<Point, Point>> to2d()
        {
            List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();
            foreach (var item in edges)
            {
                l.Add(item.to2d());
            }
            l.Add(Tuple.Create(edges.Last().P2.To2D(), edges.First().P1.To2D()));
            return l;
        }
    }
}