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
        Graphics g, g2;

        List<PointPol> points = new List<PointPol>();
        List<Polygon> pols_rotate = new List<Polygon>();
		List<Polygon> fig = new List<Polygon>();
        bool fig_drawed = false;

		public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            g = Graphics.FromImage(pictureBox1.Image);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);

            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            g2 = Graphics.FromImage(pictureBox2.Image);
            g2.ScaleTransform(1, -1);
            g2.TranslateTransform(pictureBox2.Width / 2, -pictureBox2.Height / 2);

            write_axes();
			write_axes2();
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

		public void write_axes2()
		{
			g2.DrawLine(new Pen(Color.Red), -pictureBox1.Width / 2, 0, pictureBox1.Width / 2, 0);
			g2.DrawLine(new Pen(Color.Red), 0, -pictureBox1.Height / 2, 0, pictureBox1.Height / 2);
			pictureBox2.Image = pictureBox2.Image;
		}

		public void ClearPic1()
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

            write_axes();

            //comboBox1.SelectedItem = "...";
        }

		public void ClearPic2()
		{
			g2.Clear(pictureBox2.BackColor);
			pictureBox2.Image = pictureBox2.Image;

			write_axes2();

			//comboBox1.SelectedItem = "...";
		}

		private void button2_Click(object sender, EventArgs e)
        {
            ClearPic1();
            ClearPic2();
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

        private bool edgesEqual(Edge e1, Edge e2)
        {
            return (pointsEqual(e1.P1, e2.P1) && pointsEqual(e1.P2, e2.P2)) ||
                (pointsEqual(e1.P1, e2.P2) && pointsEqual(e1.P2, e2.P1));
        }

        private void buildPolsRotate(string axis, int cnt, ref PointPol rotate_around)
        {
            pols_rotate.Clear();
            List<PointPol> l1 = new List<PointPol>(points);
            double a = 0, b = 0, c = 0;

            Polygon pol = new Polygon(l1);
            //pol.find_center(ref a, ref b, ref c);
            pol.closest_to_zero(axis, ref a, ref b, ref c);

            Edge dir = new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
            if (axis == "X")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(0, -b, 0);
                rotate_around.X = a; rotate_around.Y = 0; rotate_around.Z = c;
            }
            if (axis == "Y")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(0, 1, 0));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(-a, 0, 0);
                rotate_around.X = 0; rotate_around.Y = b; rotate_around.Z = c;
            }
            if (axis == "Z")
            {
                dir = new Edge(new PointPol(0, 0, 0), new PointPol(0, 0, 1));
                for (int i = 0; i < l1.Count(); ++i)
                    l1[i] = l1[i].shift(0, -b, 0);
                rotate_around.X = a; rotate_around.Y = 0; rotate_around.Z = c;
            }

            double angle = 360.0 / cnt;
                    
            for (int i = 0; i < cnt; ++i)
            {
                List<PointPol> l2 = new List<PointPol>(l1);

                for (int j = 0; j < l2.Count(); ++j)
                {
                    l2[j] = l2[j].rotate(dir, angle, 0, 0, 0);
                }

                pols_rotate.Add(new Polygon(l1));

                l1 = l2;
            }
        }

        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (p1.X == p2.X) && (p1.Y == p2.Y) && (p1.Z == p2.Z);
        }

        private bool polygonsEqual(Polygon pol1, Polygon pol2)
        {
            if (pol1.points.Count() != pol2.points.Count())
                return false;
            if (pol1.edges.Count() != pol2.edges.Count())
                return false;

            for (int i = 0; i < pol1.points.Count(); ++i)
            {
                if (!pointsEqual(pol1.points[i], pol2.points[i]))
                    return false;
            }
            for (int i = 0; i < pol1.edges.Count(); ++i)
            {
                if ((pol1.edges[i].Item1 != pol2.edges[i].Item1 &&
                    pol1.edges[i].Item2 != pol2.edges[i].Item2) ||
                    (pol1.edges[i].Item1 != pol2.edges[i].Item2 &&
                    pol1.edges[i].Item2 != pol2.edges[i].Item1))
                {
                    return false;
                }
            }
            return true;
        }

        private Polygon littlePartOfFig(Edge e1, Edge e2)
        {
            
            List<PointPol> l1 = new List<PointPol>();

            l1.Add(e1.P1);
            l1.Add(e1.P2);

            if (!edgesEqual(e1, e2))
            {
                if (!pointsEqual(e1.P2, e2.P2))
                    l1.Add(e2.P2);
                if (!pointsEqual(e1.P1, e2.P1))
                    l1.Add(e2.P1);
            }

            Polygon pol = new Polygon(l1);
            return pol;

        }

        private void buildPartOfFig(Polygon p1, Polygon p2)
        {
            for (int i = 0; i < p1.edges.Count(); ++i)
            {
                Polygon pol = littlePartOfFig(
                    new Edge(p1.points[p1.edges[i].Item1],
                    p1.points[p1.edges[i].Item2]),
                    new Edge(p2.points[p2.edges[i].Item1],
                    p2.points[p2.edges[i].Item2])
                    );
                int ind = fig.FindIndex(polinl => polygonsEqual(polinl, pol));
                if (ind == -1 && pol.edges.Count() > 2)
                    fig.Add(pol);
            }
        }

        private void buildFig(PointPol rotate_around)
        {
            fig.Clear();
            if (pols_rotate.Count() > 1)
            {
                for (int i = 1; i < pols_rotate.Count(); ++i)
                {
                    buildPartOfFig(pols_rotate[i-1], pols_rotate[i]);
                }
                buildPartOfFig(pols_rotate.Last(), pols_rotate.First());
            }
        }

        private void drawPols(List<Polygon> pols)
        {
            foreach (var item in pols)
            {
                drawPolygon(item, Color.Black);
            }
            pictureBox1.Image = pictureBox1.Image;
        }

		public void find_center(List<Polygon> pols, ref double x, ref double y, ref double z)
		{
			x = 0; y = 0; z = 0;

			double a = 0, b = 0, c = 0;

			foreach (var p in pols)
			{
				p.find_center(ref a, ref b, ref c);
				x += a;
				y += b;
				z += c;
			}

			x /= pols.Count();
			y /= pols.Count();
			z /= pols.Count();
		}

        private void drawPointsOnPic2()
        {
            Color c = Color.Green;
            for (int i = 1; i < points.Count(); ++i)
            {
                g2.DrawLine(new Pen(c), (float)points[i - 1].X, (float)points[i - 1].Y, (float)points[i].X, (float)points[i].Y);
                //g2.DrawEllipse(new Pen(Color.Black), (float)points[i - 1].X - 1, (float)points[i - 1].X - 1, 2, 2);
            }
            g2.DrawLine(new Pen(c), (float)points.Last().X, (float)points.Last().Y, (float)points.First().X, (float)points.First().Y);
            g2.DrawEllipse(new Pen(Color.Black), (float)points.First().X - 1, (float)points.First().Y - 1, 2, 2);
        }

        private string save()
        {
            string result = "";
            foreach (var pol in fig)
            {
                foreach (var p in pol.points)
                {
                    //result += p.X.ToString() + ";" + p.Y.ToString() + ";" + p.Z.ToString() + " ";
                    result += p.X.ToString() + ";" + p.Y.ToString() + ";" + p.Z.ToString();
                    if (p != pol.points.Last())
                    {
                        result += " ";
                    }
                }

                if (pol != fig.Last())
                    result += Environment.NewLine;
            }
            //textBox1.Text = result;
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Polygon pol = new Polygon(points);
            //drawPolygon(pol, Color.Black);
            ClearPic1();

            string ax = comboBoxBuildAxis.SelectedItem.ToString();
            int cnt = Int32.Parse(textBoxBuildCount.Text);

            PointPol rotate_around = new PointPol(0, 0, 0);
            buildPolsRotate(ax, cnt, ref rotate_around);
            drawPols(pols_rotate);

            buildFig(rotate_around);
            buildFig(rotate_around);
            //drawPols(fig);
            fig_drawed = false;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox1.Width / 2;
            int y = pictureBox1.Height / 2 - e.Y;
            int z = Int32.Parse(textBoxBuildZ.Text);

            points.Add(new PointPol(x, y, z));

            if (points.Count() > 1)
            {
                ClearPic1();
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

        private void reDrawPols()
        {
            if (fig_drawed)
            {
                ClearPic1();
                drawPols(fig);
            }
            else
            {
                ClearPic1();
                drawPols(pols_rotate);
            }
        }

        private void scalePols(List<Polygon> pols, double ind)
        {
            double a = 0, b = 0, c = 0;
            find_center(pols, ref a, ref b, ref c);
            foreach (var item in pols)
            {
                item.scale(ind, a, b, c);
            }
        }

        private void buttonScale_Click(object sender, EventArgs e)
        {
            double ind = Double.Parse(textBoxScale.Text);

            scalePols(pols_rotate, ind);
            scalePols(fig, ind);

            reDrawPols();
        }

        private void shiftPols(List<Polygon> pols, int x, int y, int z)
        {
            foreach (var item in pols)
            {
                item.shift(x, y, z);
            }
        }

        private void buttonShift_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBoxShiftX.Text);
            int y = Int32.Parse(textBoxShiftY.Text);
            int z = Int32.Parse(textBoxShiftZ.Text);

            shiftPols(pols_rotate, x, y, z);
            shiftPols(fig, x, y, z);

            reDrawPols();
        }

        private void reflectionPols(List<Polygon> pols, string axis)
        {
            foreach (var item in pols)
            {
                item.reflection(axis);
            }
        }

        private void buttonReflection_Click(object sender, EventArgs e)
        {
            string axis = comboBoxReflection.SelectedItem.ToString();

            reflectionPols(pols_rotate, axis);
            reflectionPols(fig, axis);

            reDrawPols();
        }

        private void rotatePols(List<Polygon> pols, int x1, int y1, int z1,
            int x2, int y2, int z2, double angle)
        {
            PointPol p1 = new PointPol(x1, y1, z1);
            PointPol p2 = new PointPol(x2, y2, z2);
            Edge ed = new Edge(p1, p2);

            double a = 0, b = 0, c = 0;
            find_center(pols, ref a, ref b, ref c);

            foreach (var item in pols)
            {
                item.rotate(ed, angle, a, b, c);
            }
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

            rotatePols(pols_rotate, x1, y1, z1, x2, y2, z2, angle);
            rotatePols(fig, x1, y1, z1, x2, y2, z2, angle);

            reDrawPols();
        }

        private void buttonBuild2_Click(object sender, EventArgs e)
        {
            ClearPic1();

            string ax = comboBoxBuildAxis.SelectedItem.ToString();
            int cnt = Int32.Parse(textBoxBuildCount.Text);

            PointPol rotate_around = new PointPol(0, 0, 0);
            buildPolsRotate(ax, cnt, ref rotate_around);
            //drawPols(pols_rotate);

            buildFig(rotate_around);
            buildFig(rotate_around);
            drawPols(fig);
            fig_drawed = true;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox2.Width / 2;
            int y = pictureBox2.Height / 2 - e.Y;
            labelDebug2.Text = "x = " + x.ToString() + " | y = " + y.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default))
                {
                    string text = save();
                    sw.WriteLine(text);
                }
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - pictureBox2.Width/2;
            int y = pictureBox2.Height/2 - e.Y;
            int z = 0;

            points.Add(new PointPol(x, y, z));
            ClearPic2();
            drawPointsOnPic2();
            pictureBox2.Image = pictureBox2.Image;

            if (points.Count() > 1)
            {
                ClearPic1();
                Polygon pol = new Polygon(points);
                drawPolygon(pol, Color.Black);
                pictureBox1.Image = pictureBox1.Image;
            }

            labelDebug.Text = "x = " + x.ToString() +
                " | y = " + y.ToString() + " | z = " + z.ToString();
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

        public PointPol reflection(string axis)
        {

            if (axis == "X")
            {
                return new PointPol(-X, Y, Z);
            }
            if (axis == "Y")
            {
                return new PointPol(X, -Y, Z);
            }
            if (axis == "Z")
            {
                return new PointPol(X, Y, -Z);
            }
            return new PointPol(X, Y, Z);
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
        double[,] displayMatrix = new double[4, 4] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5), 0 }, { 1 / Math.Sqrt(6), 2 / Math.Sqrt(6), 1 / Math.Sqrt(6), 0 }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 0 }, { 0, 0, 0, 1 } };

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

        public void reflection(string axis)
        {

            if (axis == "X")
            {
				P1.X = -P1.X;
            }
            if (axis == "Y")
            {
				P1.Y = -P1.Y;
			}
            if (axis == "Z")
            {
				P1.Z = -P1.Z;
			}
        }

        public Tuple<Point, Point> to2d()
        {
            return Tuple.Create(P1.To2D(), P2.To2D());
        }
    }

    public class Polygon
    {
        public List<PointPol> points = new List<PointPol>();
        public List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

        /*public Polygon(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
                //points.Add(el.P1);
                //points.Add(el.P2);
            }
        }*/
        //грани
        public Polygon(List<PointPol> ps)
        { 
            for (int i = 0; i < ps.Count(); ++i)
            {
                PointPol p = ps[i];
                int ind = points.FindIndex(pinl => pointsEqual(p, pinl));
                if (ind == -1)
                    points.Add(p);
            }

            if (points.Count() > 1)
            {
                PointPol p2, p1 = ps.First();
                int indp, ind2, ind1 = points.FindIndex(pinl => pointsEqual(p1, pinl));
                for (int i = 1; i < ps.Count(); ++i)
                {
                    p2 = ps[i];
                    ind2 = points.FindIndex(pinl => pointsEqual(p2, pinl));
                    indp = edges.FindIndex(pair =>
                        (pair.Item1 == ind2) && (pair.Item2 == ind1));
                    if ((ind1 != ind2) && (indp == -1))
                        edges.Add(Tuple.Create(ind1, ind2));
                    ind1 = ind2;
                }

                p2 = ps.First();
                ind2 = points.FindIndex(pinl => pointsEqual(p2, pinl));
                indp = edges.FindIndex(pair =>
                    (pair.Item1 == ind2) && (pair.Item2 == ind1));
                if ((ind1 != ind2) && (indp == -1))
                    edges.Add(Tuple.Create(ind1, ind2));
            }

            deleteDuplicates();
        }

        private void deleteDuplicates()
        {
            edges = edges.Distinct().ToList();
        }

        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (p1.X == p2.X) && (p1.Y == p2.Y) && (p1.Z == p2.Z);
        }
        private bool edgesEqual(Edge e1, Edge e2)
        {
            return (pointsEqual(e1.P1, e2.P1) && pointsEqual(e1.P2, e2.P2)) ||
                (pointsEqual(e1.P1, e2.P2) && pointsEqual(e1.P2, e2.P1));
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

        public void closest_to_zero(string axis, ref double x, ref double y, ref double z)
        {
            x = points.First().X; y = points.First().Y; z = points.First().Z;

            double min_dist;
            if (axis == "X")
            {
                min_dist = Math.Abs(y);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.Y);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }
            if (axis == "Y")
            {
                min_dist = Math.Abs(x);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.X);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }
            if (axis == "Z")
            {
                min_dist = Math.Abs(y);

                foreach (var p in points)
                {
                    double dist = Math.Abs(p.Y);
                    if (dist < min_dist)
                    {
                        min_dist = dist;
                        x = p.X; y = p.Y; z = p.Z;
                    }
                }
            }

        }

        public void scale(double ind_scale, double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].scale(ind_scale, a, b, c);
            deleteDuplicates();
        }

        public void shift(double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].shift(a, b, c);
            deleteDuplicates();
        }

        public void rotate(Edge edge, double angle, double a, double b, double c)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].rotate(edge, angle, a, b, c);
            deleteDuplicates();
        }

        public void reflection(string axis)
        {
            for (int i = 0; i < points.Count(); ++i)
                points[i] = points[i].reflection(axis);
        }

        public List<Tuple<Point, Point>> to2d()
        {
            List<Tuple<Point, Point>> l = new List<Tuple<Point, Point>>();
            foreach (var item in edges)
            {
                l.Add(Tuple.Create(points[item.Item1].To2D(), points[item.Item2].To2D()));
            }
            l.Add(Tuple.Create(points[edges.Last().Item2].To2D(), points[edges.First().Item2].To2D()));
            return l;
        }
    }
}