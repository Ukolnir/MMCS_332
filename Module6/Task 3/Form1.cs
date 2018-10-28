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
            g = Graphics.FromImage(pictureBox1.Image);

            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width);

            Pen my_pen = new Pen(Color.Blue);
            g.DrawLine(my_pen, p0.To2D(), p1.To2D());
            my_pen.Color = Color.Red;
            g.DrawLine(my_pen, p0.To2D(), p2.To2D());
            my_pen.Color = Color.Green;
            g.DrawLine(my_pen, p0.To2D(), p3.To2D());

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

        private void make_curve(ref List<Edge> l, double x, double y0, double y1, double step)
        {
            PointPol p1 = new PointPol(x, y0, f(x, y0));
            for (double y = y0 + step ; y <= y1; y += step)
            {
                PointPol p2 = new PointPol(x, y, f(x, y));
                l.Add(new Edge(p1, p2));
                p1 = p2;
            }
        }

        private List<Edge> merdge_curves(List<Edge> l1, List<Edge > l2)
        {
            List<Edge> l = new List<Edge>();

            foreach (var e in l1)
                l.Add(e);
            for (int i = l2.Count() - 1; i > 0; --i)
                l.Add(l2[i]);

            return l;
        }

        private void create_pol()
        {
            double x0 = Double.Parse(textBox1.Text.ToString());
            double x1 = Double.Parse(textBox2.Text.ToString());
            double y0 = Double.Parse(textBox3.Text.ToString());
            double y1 = Double.Parse(textBox4.Text.ToString());
            double step = Double.Parse(textBox5.Text.ToString());

            List<Edge> l1 = new List<Edge>(), l2 = new List<Edge>();
            List<Polygon> polygons = new List<Polygon>();

            make_curve(ref l1, x0, y0, y1, step);

            for (double x = x0 + step; x <= x1; x += step)
            {
                make_curve(ref l2, x, y0, y1, step);
                List<Edge> l = merdge_curves(l1, l2);
                polygons.Add(new Polygon(l));
                l1 = l2;
            }

            figure = new Polyhedron(polygons);
        }

        private void print_figure()
        {
            ClearWithout();
            pictureBox1.Image = figure.print(pictureBox1.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            write_axes();

            create_pol();
            print_figure();

            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;

            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;

            textBoxZ2.Visible = true;
            textBoxX1.Visible = true;
            textBoxY1.Visible = true;
            textBoxY2.Visible = true;
            textBoxZ1.Visible = true;
            textBoxX2.Visible = true;
            comboBox2.Visible = true;

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
                case "sqrt( 1 - x^2 - y^2 ) = z":
                    f = (x, y) => Math.Sqrt(1 - x * x - y * y);
                    break;
                case "x^2 - y^2 = z":
                    f = (x, y) => x*x - y*y;
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

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

        public PointPol rotate(Edge direction, double angle, double a, double b, double c)
        {
            double phi = Math.PI / 180 * angle;
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
        double[,] displayMatrix = new double[3, 3] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5) }, { 1 / Math.Sqrt(6), Math.Sqrt(2) / 3, 1 / Math.Sqrt(6) }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) } };

        public Point To2D()
        {
            var temp = _form.matrix_multiplication(displayMatrix, getP1());
            var temp2d = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[1, 0]));
            return temp2d;
        }
    }

    public class Edge
    {
        public PointPol P1;
        public PointPol P2;

        public Edge(PointPol p1, PointPol p2) { P1 = p1; P2 = p2; }

        public Image print(Image I)
        {
            Point p1 = P1.To2D();
            Point p2 = P2.To2D();

            Graphics g = Graphics.FromImage(I);
            Pen my_pen = new Pen(Color.Black);
            g.DrawLine(my_pen, p1, p2);
            return I;
        }

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

    }

    public class Polygon
    {
        public List<PointPol> points = new List<PointPol>();
        public List<Edge> edges = new List<Edge>();

        public Polygon(List<Edge> edg)
        {
            foreach (var el in edg)
            {
                edges.Add(el);
                points.Add(el.P1);
                points.Add(el.P2);
            }
        }

        //грани
        public Polygon(List<PointPol> poins)
        {
            PointPol p1 = poins.First();
            points.Add(p1);
            for (int i = 1; i < poins.Count(); ++i)
            {
                points.Add(poins[i]);
                edges.Add(new Edge(p1, poins[i]));
                p1 = poins[i];
            }

            edges.Add(new Edge(p1, poins.First()));
        }


        public Image print(Image I)
        {
            foreach (var e in edges)
                I = e.print(I);

            return I;
        }

        public void scale(double ind_scale, double a, double b, double c)
        {
            foreach (var e in edges)
                e.scale(ind_scale, a, b, c);
        }

        public void shift(double a, double b, double c)
        {
            foreach (var e in edges)
                e.shift(a, b, c);
        }

        public void rotate(Edge edge, double angle, double a, double b, double c)
        {
            foreach (var e in edges)
                e.rotate(edge, angle, a, b, c);
        }

        public void reflection(string axis, double a, double b, double c)
        {

            foreach (var e in edges)
                e.reflection(axis, a, b, c);
        }

    }

    public class Polyhedron
    {
       Form1 _form = new Form1();

        public List<PointPol> points = new List<PointPol>();
        public List<Polygon> pol = new List<Polygon>();

        public Polyhedron(List<Polygon> pl)
        {
            foreach (var el in pl)
            {
                pol.Add(el);
                foreach (var e in el.edges)
                {
                    points.Add(e.P1);
                    points.Add(e.P2);
                }
            }
        }

        public Image print(Image I)
        {
            foreach (var el in pol)
                I = el.print(I);

            return I;
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

            _form.find_center(points, ref a, ref b, ref c);
            foreach (var e in pol)
                e.scale(ind_scale, a, b, c);
        }

        public void shift(double a, double b, double c)
        { 
            foreach (var e in pol)
                e.shift(a, b, c);
        }

        public void rotate(Edge edge, double angle)
        {
            double a = 0;
            double b = 0;
            double c = 0;

            _form.find_center(points, ref a, ref b, ref c);
            foreach (var e in pol)
                e.rotate(edge, angle, a, b, c);
        }

        public void reflection(string axis)
        {
            double a = 0;
            double b = 0;
            double c = 0;

            _form.find_center(points, ref a, ref b, ref c);
            foreach (var e in pol)
                e.reflection(axis, a, b, c);
        }
    }

}