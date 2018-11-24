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
        Graphics g, g2;

        Camera cam;

        PointPol[] points;
        List<Polygon> pols_rotate = new List<Polygon>();
        List<Polygon> fig = new List<Polygon>();
        Polyhedron figure;
        delegate double lambda(double a, double b);
        lambda f;

        int step;

        public Form1()
        {
            InitializeComponent();

            double phi_a = double.Parse(textBox9.Text);
            double psi_a = double.Parse(textBox10.Text);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g2 = Graphics.FromImage(pictureBox2.Image);
            //g2.ScaleTransform(1, -1);
            g2.TranslateTransform(pictureBox2.Width / 2, pictureBox2.Height / 2);

            write_axes1();
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

        public void write_axes1()
        {
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width / 2, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width / 2, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width / 2);

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
            PointPol p0 = new PointPol(0, 0, 0);
            PointPol p1 = new PointPol(pictureBox1.Width / 2, 0, 0);
            PointPol p2 = new PointPol(0, pictureBox1.Width / 2, 0);
            PointPol p3 = new PointPol(0, 0, pictureBox1.Width / 2);

            Point o = p0.To2D();
            Point x = p1.To2D();
            Point y = p2.To2D();
            Point z = p3.To2D();

            Font font = new Font("Arial", 8);
            SolidBrush brush = new SolidBrush(Color.Black);

            g2.DrawString("X", font, brush, x);
            g2.DrawString("Y", font, brush, y);
            g2.DrawString("Z", font, brush, z);

            Pen my_pen = new Pen(Color.Blue);
            g2.DrawLine(my_pen, o, x);
            my_pen.Color = Color.Red;
            g2.DrawLine(my_pen, o, y);
            my_pen.Color = Color.Green;
            g2.DrawLine(my_pen, o, z);

            pictureBox2.Image = pictureBox2.Image;
        }



        public void ClearPic1()
        {
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;

            write_axes1();
        }

        public void ClearPic2()
        {
            g2.Clear(pictureBox2.BackColor);
            pictureBox2.Image = pictureBox2.Image;

            write_axes2();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            double phi_a = double.Parse(textBox9.Text);
            double psi_a = double.Parse(textBox10.Text);

            ClearPic1();
            ClearPic2();
            //points.Clear();

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


        private bool vectorsEqual(Vector e1, Vector e2)
        {
            return (pointsEqual(e1.P1, e2.P1) && pointsEqual(e1.P2, e2.P2)) ||
                (pointsEqual(e1.P1, e2.P2) && pointsEqual(e1.P2, e2.P1));
        }

       
        private bool pointsEqual(PointPol p1, PointPol p2)
        {
            return (p1.X == p2.X) && (p1.Y == p2.Y) && (p1.Z == p2.Z);
        }

       


        public void find_center(List<Polygon> pols, ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            double a = 0, b = 0, c = 0;

            foreach (var p in pols)
            {
                find_center(pols, ref a, ref b, ref c);
                x += a;
                y += b;
                z += c;
            }

            x /= pols.Count();
            y /= pols.Count();
            z /= pols.Count();
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

        private void make_curve(ref List<PointPol> l, double x, double y0, double y1, double step)
        {
            for (double y = y0; y <= y1; y += step)
            {
                PointPol p = new PointPol(x, y, f(x, y));
                l.Add(p);
            }
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

                for (int i = 0; i < l1.Count() - 1; ++i)
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

        public struct point3
        {
            public int x, y, z;
            PointPol p;
            public point3(int x1, int y1, int z1)
            { x = x1; y = y1; z = z1; p = new PointPol(0, 0, 0); }
            public point3(int x1, int y1, int z1, PointPol p1)
            { x = x1; y = y1; z = z1; p = p1; }
        }

        public point3 ViewPortTranform(PointPol p)
        {
            int h = pictureBox2.Image.Height;
            int w = pictureBox2.Image.Width;

            return new point3((int)((1 + p.X) * w / 2),
                              (int)((1 + p.Y) * h / 2),
                              (int)(p.Z * 100000000), p);
        }


        public PointPol PointToView(PointPol p)
        {
            return new PointPol(p.X / p.Z, p.Y / p.Z, p.Z);
        }

        private static int[] Interpolate(int i0, int d0, int i1, int d1)
        {
            if (i0 == i1)
            {
                return new int[] { d0, d1 };
            }
            int[] res;
            int a = (d1 - d0) / (i1 - i0);
            res = new int[i1 - i0 + 1];
            d1 = 0;
            for (int i = i0; i <= i1; i++)
            {
                res[d1] = d0;
                d0 += a;
                ++d1;
            }


            return res;

        }

        public void Horizont_figure()
        {
            float x0 = float.Parse(textBox1.Text.ToString());
            float x1 = float.Parse(textBox2.Text.ToString());
            float y0 = float.Parse(textBox3.Text.ToString());
            float y1 = float.Parse(textBox4.Text.ToString());
            CameraRender(new PointF(x0, x1), new PointF(y0, y1));
        }

        public void CameraRender(PointF borderx, PointF bordexy)
        {

            int h = pictureBox2.Image.Height;
            int w = pictureBox2.Image.Width;

            double xstep = double.Parse(textBox5.Text);
            double ystep = xstep;
            var YMax = new int[w];
            var YMin = new int[w];

            for (int i = 0; i < w; i++)
            {
                YMax[i] = -1;
                YMin[i] = pictureBox2.Height;
            }
            for (double x = borderx.X; x <= borderx.Y; x += xstep)
            {
                var hor = new List<PointPol>();
                for (double y = bordexy.X; y <= bordexy.Y; y += ystep)
                    hor.Add(PointToView(new PointPol(x, y, f(x, y))));


                var cur_hor = new List<Point>();
                foreach (PointPol p in hor)
                {
                    point3 vp = ViewPortTranform(p);
                    cur_hor.Add(new Point((int)p.X, (int)p.Y));
                }
                cur_hor = cur_hor.OrderBy(p => p.X).ToList();
                for (int i = 0; i < cur_hor.Count - 1; i++)
                {

                    var line = Interpolate(cur_hor[i].X, cur_hor[i].Y, cur_hor[i + 1].X, cur_hor[i + 1].Y);
                    int index = -1;
                    for (int linex = cur_hor[i].X; linex < cur_hor[i + 1].X; ++linex)
                    {
                        index++;
                        if (linex >= 0 && linex < w && line[index] > YMax[linex])
                        {
                            YMax[linex] = line[index];

                        }

                        if (linex >= 0 && linex < w && line[index] < YMin[linex])
                        {
                            YMin[linex] = line[index];
                        }
                    }

                }
                for (int i = 0; i < w - 1; i++)
                {
                    if (YMax[i] >= 0 && YMax[i] < h && YMax[i + 1] >= 0 && YMax[i + 1] < h)
                        g2.DrawLine(new Pen(Color.Green), new Point(i, YMax[i]), new Point(i + 1, YMax[i + 1]));
                    if (YMin[i] <= h && YMax[i] < pictureBox2.Width && YMin[i + 1] < h && YMin[i + 1] < pictureBox2.Width)
                        g2.DrawLine(new Pen(Color.GreenYellow), new Point(i, YMin[i]), new Point(i + 1, YMin[i + 1]));
                }

            }

            pictureBox2.Image = pictureBox2.Image;
        }


        private void old_print_figure()
        {
            Pen my_pen = new Pen(Color.BlueViolet);

            foreach (var pl in figure.points)
                {
                    Point p1 = figure.points[e.nP1].To2D(), p2 = figure.points[e.nP2].To2D();
                    g.DrawLine(my_pen, p1, p2);
                }

            pictureBox1.Image = pictureBox1.Image;
        }

        private void Create_point_fors_Hor()
        {
            double x0 = Double.Parse(textBox1.Text);
            double x1 = Double.Parse(textBox2.Text);
            double y0 = Double.Parse(textBox3.Text);
            double y1 = Double.Parse(textBox4.Text);

            step = Int32.Parse(textBox5.Text);

            double dx = (x1 - x0) / step;
            double dy = (y1 - y0) / step;


            points = new PointPol[(step + 1) * (step + 1)];
            for (int i = 0; i < step + 1; ++i)
            {
                for (int j = 0; j < step + 1; ++j)
                {
                    points[i * (step + 1) + j] = new PointPol(x0 + i * dx, y0 + j * dy, f(x0 + i * dx, y0 + j * dy));
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearPic1();
            create_pol();
            Init();
            old_print_figure();
        }






        private void buttonScale_Click(object sender, EventArgs e)
        {
            double ind = Double.Parse(textBoxScale.Text);
            double phi_a = double.Parse(textBox9.Text);
            double psi_a = double.Parse(textBox10.Text);

            // scalePols(pols_rotate, ind);
            // scalePols(fig, ind);
        }

        private void buttonShift_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBoxShiftX.Text);
            int y = Int32.Parse(textBoxShiftY.Text);
            int z = Int32.Parse(textBoxShiftZ.Text);

           // shiftPols(pols_rotate, x, y, z);
           // shiftPols(fig, x, y, z);

        }


        private void buttonReflection_Click(object sender, EventArgs e)
        {
            string axis = comboBoxReflection.SelectedItem.ToString();

            //reflectionPols(pols_rotate, axis);
            //reflectionPols(fig, axis);
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

           // rotatePols(pols_rotate, x1, y1, z1, x2, y2, z2, angle);
           // rotatePols(fig, x1, y1, z1, x2, y2, z2, angle);
        }

        private void buttonBuild2_Click(object sender, EventArgs e)
        {

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


        private PointPol normVecOfPlane(PointPol p1, PointPol p2, PointPol p3)
        {
            double kx, ky, kz, d;
            double xa = p1.X, ya = p1.Y, za = p1.Z;
            double a21 = p2.X - xa, a22 = p2.Y - ya, a23 = p2.Z - za;
            double a31 = p3.X - xa, a32 = p3.Y - ya, a33 = p3.Z - za;

            kx = a22 * a33 - a23 * a32;
            ky = -(a21 * a33 - a23 * a31);
            kz = a21 * a32 - a22 * a31;
            d = (kx * -xa) + (ky * -ya) + (kz * -za);

            return new PointPol(kx, ky, kz);
        }

        private double scalarProduct(PointPol vec1, PointPol vec2)
        {
            return (vec1.X * vec2.X + vec1.Y * vec2.Y + vec1.Z * vec2.Z);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textBox1.Text);
            double y = double.Parse(textBox2.Text);
            double z = double.Parse(textBox3.Text);

            double t = double.Parse(textBoxT.Text);
            double ya = double.Parse(textBoxY.Text);
            double r = double.Parse(textBoxR.Text);

            cam = new Camera(x, y, z, t, ya, r);

            pointInit();
        }


        public void Init()
        {
            double x = double.Parse(textBox1.Text);
            double y = double.Parse(textBox2.Text);
            double z = double.Parse(textBox3.Text);

            double t = double.Parse(textBoxT.Text);
            double ya = double.Parse(textBoxY.Text);
            double r = double.Parse(textBoxR.Text);

            cam = new Camera(x, y, z, t, ya, r);

            for (int i = 0; i < figure.points.Count(); ++i)
            {

                var t0 = matrix_multiplication(cam.translateAtPosition(), figure.points[i].getPol());
                var t1 = matrix_multiplication(cam.translateAtAngles(),
                new double[3, 1] { { t0[0, 0] }, { t0[1, 0] }, { t0[2, 0] } });
                figure.points[i] = new PointPol(t1[0, 0], t1[1, 0], t1[2, 0]);

            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void pointInit()
        {
            for(int i = 0; i < figure.points.Count(); ++i)
            {
                var t1 = matrix_multiplication(cam.translateAtAngles(),
                        figure.points[i].getP1());
                figure.points[i] = new PointPol(t1[0, 0], t1[1, 0], t1[2, 0]);
                
            }
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

        public PointPol rotate(Vector direction, double angle, double a, double b, double c)
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

        public Point To2D()
        {
            double phi = Math.PI / 180 * 145;
            double psi = Math.PI / 180 * 45;
            double[,] displayMatrix1 = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, Math.Cos(phi), Math.Sin(phi), 0 },
                { 0, -Math.Sin(phi), Math.Cos(phi), 0 },
                { 0, 0, 0, 1 } };
            double[,] displayMatrix2 = new double[4, 4] {
                { Math.Cos(psi), 0, -Math.Sin(psi), 0 },
                { 0, 1, 0, 0 },
                { Math.Sin(psi), 0, Math.Cos(psi), 0 },
                { 0, 0, 0, 1 } };

            var displayMatrix = matrix_multiplication(displayMatrix1, displayMatrix2);
            var temp = matrix_multiplication(displayMatrix, getP1());
            int x = Convert.ToInt32(temp[0, 0]);
            int y = Convert.ToInt32(temp[1, 0]);

            var temp2d = new Point(x, y);
            return temp2d;
        }

        public Point To2D(double phi_a, double psi_a)
        {
            double phi = Math.PI / 180 * phi_a;
            double psi = Math.PI / 180 * psi_a;
            double[,] displayMatrix1 = new double[4, 4] {
                { 1, 0, 0, 0 },
                { 0, Math.Cos(phi), Math.Sin(phi), 0 },
                { 0, -Math.Sin(phi), Math.Cos(phi), 0 },
                { 0, 0, 0, 1 } };
            double[,] displayMatrix2 = new double[4, 4] {
                { Math.Cos(psi), 0, -Math.Sin(psi), 0 },
                { 0, 1, 0, 0 },
                { Math.Sin(psi), 0, Math.Cos(psi), 0 },
                { 0, 0, 0, 1 } };

            var displayMatrix = matrix_multiplication(displayMatrix1, displayMatrix2);
            var temp = matrix_multiplication(displayMatrix, getP1());
            int x = Convert.ToInt32(temp[0, 0]);
            int y = Convert.ToInt32(temp[1, 0]);

            var temp2d = new Point(x, y);
            return temp2d;
        }
    }

    public class Vector
    {
        public PointPol P1;
        public PointPol P2;

        public Vector(PointPol p1, PointPol p2) { P1 = p1; P2 = p2; }

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

        public void rotate(Vector e, double angle, double a, double b, double c)
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

    public class Edge
    {
        public int nP1;
        public int nP2;


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

        public void rotate(Vector dir, double angle)
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