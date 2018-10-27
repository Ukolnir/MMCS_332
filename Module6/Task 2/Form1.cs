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

        delegate double lambda(double a, double b, double c);
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

        public void write_axes()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.ScaleTransform(1, -1);
            g.TranslateTransform(pictureBox1.Width / 2, -pictureBox1.Height/2);

            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width/2, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width/2, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width/2);

            Point o = p0.To2D(pictureBox1.Width, pictureBox1.Height);
            Point x = p1.To2D(pictureBox1.Width, pictureBox1.Height);
            Point y = p2.To2D(pictureBox1.Width, pictureBox1.Height);
            Point z = p3.To2D(pictureBox1.Width, pictureBox1.Height);

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
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

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

        private void button1_Click(object sender, EventArgs e)
        {
            write_axes();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "x^2 + y^2 = z":
                    f = (x, y, z) => x * x + y * y - z;
                    break;
                case "sqrt( 1 - x^2 - y^2 ) = z":
                    f = (x, y, z) => Math.Sqrt(1 - x * x - y * y) - z;
                    break;
                case "x^2 - y^2 = z":
                    f = (x, y, z) => x*x - y*y - z;
                    break;
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

    public Point To2D(int width, int height)
    {
        var temp = _form.matrix_multiplication(displayMatrix, getP1());
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
        foreach (var v in poins)
            points.Add(v);
    }

}
}