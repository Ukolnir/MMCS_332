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
        Polyhedron figure;
        delegate double lambda(double a, double b);
        lambda f;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Clear();
 
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "1";

            comboBox1.SelectedItem = "x^2 + y^2 = z";
            comboBox3.SelectedItem = "Изометрическая";
        }

        public void find_center(List<PointPol> pList, ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

			int count = 0;

			foreach (var p in pList)
			{
				count ++;
                x += p.X;
                y += p.Y;
                z += p.Z;
			}

            x /= count;
            y /= count;
            z /= count;
        }

        public void write_axes()
        {
            g = Graphics.FromImage(pictureBox1.Image);

            g.ScaleTransform(1, -1);
            g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height / 2);

            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width);

            String s = comboBox3.SelectedItem.ToString();
            Pen my_pen = new Pen(Color.Blue);
            g.DrawLine(my_pen, p0.To2D(s), p1.To2D(s));
            my_pen.Color = Color.Red;
            g.DrawLine(my_pen, p0.To2D(s), p2.To2D(s));
            my_pen.Color = Color.Green;

            g.DrawLine(my_pen, p0.To2D(s), p3.To2D(s));

            pictureBox1.Image = pictureBox1.Image;
        }
        public void ClearWithout()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }


        public void Clear()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

            comboBox1.SelectedItem = "...";
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;

            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;

            textBoxZ2.Visible = false;
            textBoxX1.Visible = false;
            textBoxY1.Visible = false;
            textBoxY2.Visible = false;
            textBoxZ1.Visible = false;
            textBoxX2.Visible = false;
            comboBox2.Visible = false;

            label7.Visible = false;
            label8.Visible = false;
            textBoxAngle.Visible = false;
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

        private void make_curve(ref List<PointPol> l, double x, double y0, double y1, double step)
        {
            for (double y = y0; y <= y1; y += step)
            {
                PointPol p = new PointPol(x, y, f(x, y));
                l.Add(p);
            }
        }


        private void create_pol()
        {
            double x0 = Double.Parse(textBox1.Text.ToString());
            double x1 = Double.Parse(textBox2.Text.ToString());
            double y0 = Double.Parse(textBox3.Text.ToString());
            double y1 = Double.Parse(textBox4.Text.ToString());
            double step = Double.Parse(textBox5.Text.ToString());

            List<PointPol> l1 = new List<PointPol>(), l2 = new List<PointPol>(), l = new List<PointPol>();
            List<Polygon> polygons = new List<Polygon>();

            make_curve(ref l1, x0, y0, y1, step);
            int num = 0;

            for (double x = x0 + step; x <= x1; x += step)
            {
                make_curve(ref l2, x, y0, y1, step);

				for(int i = 0; i < l1.Count() - 1; ++i)
                {
					List<Edge> gList = new List<Edge>();
                    int n1 = num + i;
					gList.Add(new Edge(n1, n1 + 1));
                    gList.Add(new Edge(n1 + 1, n1 + 1 + l1.Count));

                    gList.Add(new Edge(n1 + 1 + l1.Count, n1 + l1.Count()));
					gList.Add(new Edge(n1 + l1.Count(), n1));

					polygons.Add(new Polygon(gList));
                }

                num += l1.Count();

                foreach (var p in l1)
                    l.Add(p);

                l1.Clear();
                foreach (var e in l2)
                    l1.Add(e);

                l2.Clear();
            }

            foreach (var p in l1)
                l.Add(p);

            figure = new Polyhedron(polygons, l);
        }

        private void print_figure()
        {
            Pen my_pen = new Pen(Color.Black);

            string s = comboBox3.SelectedItem.ToString();
            foreach (var pl in figure.pol)
				foreach (var e in pl.edges)
				{
					Point p1 = figure.points[e.nP1].To2D(s), p2 = figure.points[e.nP2].To2D(s);
					g.DrawLine(my_pen, p1, p2);
				}

            pictureBox1.Image = pictureBox1.Image;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {

            create_pol();
            ClearWithout();
            write_axes();
            print_figure();

            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;

            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            textBox6.Text = "10";
            textBox7.Text = "10";
            textBox8.Text = "10";
            textBox9.Text = "0,5";

            textBoxZ2.Visible = true;
            textBoxX1.Visible = true;
            textBoxY1.Visible = true;
            textBoxY2.Visible = true;
            textBoxZ1.Visible = true;
            textBoxX2.Visible = true;
            comboBox2.Visible = true;
            comboBox2.SelectedItem = "X";

            label7.Visible = true;
            label8.Visible = true;
            textBoxAngle.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "x^2 + y^2 = z":
                    f = (x, y) => x * x + y * y;
                    break;
                case "1/(1+x^2) + 1/(1+y^2) = z":
                    f = (x, y) => Math.Sin(x) * Math.Cos(y);
                    break;
                case "x^2 - y^2 = z":
                    f = (x, y) => x*x - y*y;
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearWithout();
            write_axes();
            figure.reflection(comboBox2.SelectedItem.ToString());
            print_figure();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearWithout();
            write_axes();
            double ind_scale = Double.Parse(textBox9.Text.ToString());
            figure.scale(ind_scale);
            print_figure();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearWithout();
            write_axes();
            double x = Double.Parse(textBox6.Text.ToString());
            double y = Double.Parse(textBox7.Text.ToString());
            double z = Double.Parse(textBox8.Text.ToString());

            figure.shift(x, y, z);
            print_figure();
        }

		private string save()
		{
			string result = "";
			foreach (var p in figure.pol)
			{
				foreach (var e in p.edges)
				{
                    //t.X -= pictureBox1.Width / 2;
                    if (p.edges.Last() == e)
                    {
                        var t = figure.points[e.nP1];
                        result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString();
                       // t = figure.points[e.nP2];
                        //result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString();
                    }
                    else
                    {
                        var t = figure.points[e.nP1];
                        result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString() + " ";
                       // t = figure.points[e.nP2];
                        //result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString() + " ";
                    }
                }

				if (p != figure.pol.Last())
					result += Environment.NewLine;
			}
			//textBox1.Text = result;
			return result;
		}

        private void button6_Click(object sender, EventArgs e)
        {
            ClearWithout();
            write_axes();

            double x1 = Double.Parse(textBoxX1.Text.ToString());
            double y1 = Double.Parse(textBoxY1.Text.ToString());
            double z1 = Double.Parse(textBoxZ1.Text.ToString());
            double x2 = Double.Parse(textBoxX2.Text.ToString());
            double y2 = Double.Parse(textBoxY2.Text.ToString());
            double z2 = Double.Parse(textBoxZ2.Text.ToString());

            double angle = Double.Parse(textBoxAngle.Text.ToString());

            PointPol p1 = new PointPol(x1, y1, z1);
            PointPol p2 = new PointPol(x2, y2, z2);

            figure.rotate(Tuple.Create(p1,p2), angle);
            print_figure();
        }

		private void button3_Click(object sender, EventArgs e)
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

      
    }


    public class PointPol
    {
        public double X, Y, Z, W;

        Form1 _form = new Form1();

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


        public PointPol scale(double ind_scale, double a, double b, double c)
        {
            double[,] transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { -a, -b, -c, 1 } };
            var t1 = _form.matrix_multiplication(getP(), transfer);

            t1 = _form.matrix_multiplication(t1, new double[4, 4] { { ind_scale, 0, 0, 0 }, { 0, ind_scale, 0, 0 }, { 0, 0, ind_scale, 0 }, { 0, 0, 0, 1 } });
            transfer = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { a, b, c, 1 } };
            t1 = _form.matrix_multiplication(t1, transfer);

            return translatePol(t1);
        }

        public PointPol rotate(Tuple<PointPol, PointPol> direction, double angle, double a, double b, double c)
        {
            double phi = Math.PI / 360 * angle;
            PointPol p = shift(-a, -b, -c);

            double x1 = direction.Item1.X;
            double y1 = direction.Item1.Y;
            double z1 = direction.Item1.Z;

            double x2 = direction.Item2.X;
            double y2 = direction.Item2.Y;
            double z2 = direction.Item2.Z;

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
            var t1 = _form.matrix_multiplication(p.getP(), transfer);

            t1 = _form.matrix_multiplication(t1, transfer);

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
            return translatePol1(_form.matrix_multiplication(shiftMatrix, getPol()));
        }
        //Изометрическая проекция
        public Point To2D(string display)
        {
            double[,] displayMatrix = new double[4, 4] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5), 0 }, { 1 / Math.Sqrt(6), 2 / Math.Sqrt(6), 1 / Math.Sqrt(6), 0 }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 0 }, { 0, 0, 0, 1 } };;
  
            if (display == "Ортогональная по Z")
                displayMatrix = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };

            var temp = _form.matrix_multiplication(displayMatrix, getP1());
            var temp2d = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[1, 0]));
            return temp2d;
        }
    }

    public class Edge
    {
        public int  nP1;
        public int  nP2;


        public Edge(int p1, int p2) { nP1 = p1; nP2 = p2; }
	}

    public class Polygon
    {
        public List<Edge> edges = new List<Edge>();
        
        //по граням
        public Polygon(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
            }
        }
    }

    public class Polyhedron
    {
        Form1 _form = new Form1();
        public List<PointPol> points = new List<PointPol>();
        public List<Polygon> pol = new List<Polygon>();

        public Polyhedron(List<Polygon> pl, List<PointPol> p)
        {
            foreach (var el in pl)
            {
                pol.Add(el);
			}

            points = p;
        }

        public void AddPolygon(Polygon p)
        {
            pol.Add(p);
        }

        public void scale(double ind_scale)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            List<PointPol> ch_points = new List<PointPol>();

            _form.find_center(points, ref a, ref b, ref c);
            foreach (var p in points)
                ch_points.Add(p.scale(ind_scale, a, b, c));

            points = ch_points;
        }

        public void shift(double a, double b, double c)
        {
            List<PointPol> ch_points = new List<PointPol>();

            foreach (var e in points)
                ch_points.Add(e.shift(a, b, c));

            points = ch_points;
        }

        public void rotate(Tuple<PointPol, PointPol> dir, double angle)
        {
            double a = 0;
            double b = 0;
            double c = 0;

            _form.find_center(points, ref a, ref b, ref c);
            List<PointPol> ch_points = new List<PointPol>();

            foreach (var e in points)
                ch_points.Add(e.rotate(dir, angle, a, b, c));

            points = ch_points;
        }

        public void reflection(string axis)
        {
            foreach (var p in points)
            {
                if (axis == "X")
                    p.X = -p.X;
                if (axis == "Y")
                    p.Y = -p.Y;
                if (axis == "Z")
                    p.Z = -p.Z;
            }
        }
    }

}