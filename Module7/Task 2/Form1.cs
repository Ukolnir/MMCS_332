using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<Polyhedron> figures = new List<Polyhedron>();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Clear();
        }

        public void find_center(List<PointPol> pList, ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;

            foreach (var p in pList)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }

            x /= pList.Count();
            y /= pList.Count();
            z /= pList.Count();
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
            figures.Clear();

            comboBox4.SelectedItem = "Гексаэдр";
            comboBox3.SelectedItem = "Ортогональная по Z";
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

        private Polygon parseString( string s, ref List<PointPol> points, ref int cnt)
        {
            List<Edge> e = new List<Edge>();
            var prs = s.Split(' ').Select(x => x.Split(';').Select(z => Convert.ToDouble(z)).ToArray());
            int wh = pictureBox1.Width / 2;

            foreach (var y in prs)
            {
                PointPol t0 = new PointPol(y[0] + wh, y[1], y[2]);
                points.Add(t0);
            }

            for (int i = cnt+1; i < points.Count(); ++i)
                e.Add(new Edge(i - 1, i));
            e.Add(new Edge(points.Count() - 1, cnt));

            cnt = points.Count();
            return new Polygon(e);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox4.SelectedText = "...";
            List<PointPol> points = new List<PointPol>();
            List<Polygon> pols = new List<Polygon>();
            int count = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*"; //формат загружаемого файла
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        pols.Add(parseString(line, ref points, ref count));
                }
            }

            figures.Add(new Polyhedron(pols, points));
            //pols.Clear();
            //points.Clear();
        }

        private Polyhedron createCubs()
        {
            List<PointPol> points = new List<PointPol>();
            List<Edge> e = new List<Edge>();
            List<Polygon> pols = new List<Polygon>();

            var w = pictureBox1.Width / 2;
            int len = 100;

            //обход вершин: A A1 B1 B C C1 D D1 A, где без индексов - основание куба
            points.Add(new PointPol(w, len, 0));  points.Add(new PointPol(w, len, len)); 
            points.Add(new PointPol(w, 0, len)); points.Add(new PointPol(w, 0, 0));
            points.Add(new PointPol(w + len, 0, 0)); points.Add(new PointPol(w + len, 0, len));
            points.Add(new PointPol(w + len, len, 0)); points.Add(new PointPol(w + len, len, len));

            e.Add(new Edge(0, 1)); e.Add(new Edge(1, 2)); e.Add(new Edge(2, 3)); e.Add(new Edge(3, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(0, 3)); e.Add(new Edge(3, 4)); e.Add(new Edge(4, 6)); e.Add(new Edge(6, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(1, 2)); e.Add(new Edge(2, 5)); e.Add(new Edge(5, 7)); e.Add(new Edge(7, 1));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(0, 1)); e.Add(new Edge(1, 7)); e.Add(new Edge(7, 6)); e.Add(new Edge(6, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(3, 2)); e.Add(new Edge(2, 5)); e.Add(new Edge(5, 4)); e.Add(new Edge(4, 3));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(4, 5)); e.Add(new Edge(5, 7)); e.Add(new Edge(7, 6)); e.Add(new Edge(6, 4));
            pols.Add(new Polygon(e));

            return new Polyhedron(pols, points);
        }

        private Polyhedron createT()
        {
            List<PointPol> points = new List<PointPol>();
            List<Edge> e = new List<Edge>();
            List<Polygon> pols = new List<Polygon>();

            var w = pictureBox1.Width / 2;
            int len = 100;

            
            points.Add(new PointPol(w, len, 0)); 
            points.Add(new PointPol(w, 0, len)); 
            points.Add(new PointPol(w + len, 0, 0));
            points.Add(new PointPol(w + len, len, 0));

            e.Add(new Edge(0, 1)); e.Add(new Edge(1, 2)); e.Add(new Edge(2, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(0, 1)); e.Add(new Edge(1, 3)); e.Add(new Edge(3, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(0, 2)); e.Add(new Edge(2, 3)); e.Add(new Edge(3, 0));
            pols.Add(new Polygon(e));

            e.Clear();
            e.Add(new Edge(1, 2)); e.Add(new Edge(2, 3)); e.Add(new Edge(3, 1));
            pols.Add(new Polygon(e));

            return new Polyhedron(pols, points);
        }

		private static int[] Interpolate(int i0, int d0, int i1, int d1)
		{
			if (i0 == i1)
			{
				return new int[] { d0 };
			}
			int[] res;
			double a = (double)(d1 - d0) / (i1 - i0);
            double val = d0;
            if (i0 > i1)
			{
				int c = i0;
				i0 = i1;
				i1 = c;
			}

			res = new int[i1 - i0 + 1];
		
            int ind = 0;
			for (int i = i0; i <= i1; i++)
			{
				res[ind] = (int)(val + 0.5);
				val += a;
				++ind;
			}


			return res;
		}

		private void swap(ref PointPol p1, ref PointPol p2)
		{
			PointPol c = p1;
			p1 = p2;
			p2 = c;
		}

		private void DrawShadedTriangle(PointPol p0, PointPol p1, PointPol p2, string s, ref List<Tuple<PointPol,double>> pixels )
		{
            PointPol P0 = p0;
            PointPol P1 = p1;
            PointPol P2 = p2;
            // Сортировка точек так, что y0 <= y1 <= y2
            if (P1.Y < P0.Y)
				{ swap(ref P1, ref P0); }
			if (P2.Y < P0.Y)
				{ swap(ref P2, ref P0); }
			if (P2.Y < P1.Y)
				{ swap(ref P2, ref P1); }

			int z0 = (int)P0.Z;
			/*if (z0 != 0)
				z0 = 1 / z0 * 1000000;
                */
			int z1 = (int)P1.Z;
		/*	if (z1 != 0)
				z1 = 1 / z1 * 1000000;

    */
			int z2 = (int)P2.Z;
		/*	if (z2 != 0)
				z2 = 1 / z2 * 1000000;
                */
			// Вычисление координат x и значений h для рёбер треугольника
			int[] x01 = Interpolate((int)P0.Y, (int)P0.X, (int)P1.Y, (int)P1.X);
			int[] h01 = Interpolate((int)P0.Y, z0, (int)P1.Y, z1);
			int[] x12 = Interpolate((int)P1.Y, (int)P1.X, (int)P2.Y, (int)P2.X);
			int[] h12 = Interpolate((int)P1.Y, z1, (int)P2.Y, z2);

			int[] x02 = Interpolate((int)P0.Y, (int)P0.X, (int)P2.Y, (int)P2.X);
			int[] h02 = Interpolate((int)P0.Y, z0, (int)P2.Y, z2);

			x01 = x01.Take(x01.Count() - 1).ToArray();
			int[] x012 = x01.Concat(x12).ToArray();


			h01 = h01.Take(h01.Count() - 1).ToArray();
			int[] h012 = h01.Concat(h12).ToArray();

			int m = x012.Count() / 2;

			int[] x_left, x_right, h_left, h_right;

			if (x02[m] < x012[m])
			{
				 x_left = x02;
				 x_right = x012;

				 h_left = h02;
				 h_right = h012;

			}
			else
			{
				x_left = x012;
				x_right = x02;

				h_left = h012;
				h_right = h02;
	  
		}

		// Отрисовка горизонтальных отрезков
			for (int y = (int)P0.Y; y <= (int)P2.Y; ++y)
			{
				int x_l = x_left[y - (int)P0.Y];
				int x_r = x_right[y - (int)P0.Y];

				int[] h_segment = Interpolate(x_l, h_left[y - (int)P0.Y], x_r, h_right[y - (int)P0.Y]);
		
				for (int x = x_l; x <= x_r; ++x)
				{
					
				    double ourZ = h_segment[x - x_l];
					pixels.Add(Tuple.Create(new PointPol(x, y, ourZ), ourZ));

				}
			}
		}



		private List<Tuple<PointPol, double>> rastr(Polygon p, List<PointPol> points, string ax)
        {
			List<Tuple<PointPol, double>> pixels = new List<Tuple<PointPol, double>>();
			if (p.edges.Count() == 4)
			{
				int n1 = p.edges[0].nP1;
				int n2 = p.edges[0].nP2;
				int n3 = p.edges[2].nP1;

				DrawShadedTriangle(points[n1], points[n2], points[n3], ax, ref pixels);

				n1 = p.edges[0].nP1;
				n2 = p.edges[2].nP1;
				n3 = p.edges[2].nP2;

				DrawShadedTriangle(points[n1], points[n2], points[n3], ax, ref pixels);
			}
			else
			{
				int n1 = p.edges[0].nP1;
				int n2 = p.edges[0].nP2;
				int n3 = p.edges[2].nP1;

				DrawShadedTriangle(points[n1], points[n2], points[n3], ax, ref pixels);
			}

			return pixels;
        }

        private List<List<Tuple<PointPol, double>>> rastrAll(string ax)
        {
            List<List<Tuple<PointPol,double>>> pixelsPol = new List<List<Tuple<PointPol,double>>>();

            foreach (var f in figures)
                foreach (var p in f.pol)
                    pixelsPol.Add(rastr(p, f.points, ax));

            return pixelsPol;
        }
		struct pix
		{
			public int z;
			public Color c;

			public pix(int z1, Color c1) { z = z1; c = c1; }
		}

		private pix[,] Z_buffer(List<List<Tuple<PointPol, double>>> pixels)
		{
			pix[,] buff = new pix[pictureBox1.Width, pictureBox1.Height];
			for (int i = 0; i < pictureBox1.Width; ++i)
				for (int j = 0; j < pictureBox1.Height; ++j)
					buff[i, j] = new pix(int.MaxValue, Color.White);

			int wh = 0;

            for (int i = 0; i < pixels.Count(); ++i)
                foreach (var p1 in pixels[i])
                {
                    Point p = p1.Item1.To2D(comboBox3.SelectedItem.ToString());

                    if (wh + p.X > -1 && wh + p.X < pictureBox1.Width && p.Y > -1 && p.Y < pictureBox1.Height && buff[wh + p.X, p.Y].z > p1.Item2)
                    {
                        buff[wh + p.X, p.Y].z = (int)p1.Item2;
                        buff[wh + p.X, p.Y].c = Color.FromArgb(((i + 1) * 2) % 255, ((i + 1) * 5) % 255, ((i + 1) * 5) % 255);
                    }
                }
			return buff;
		}


        private void print_figures()
        {
            ClearWithout();

			string axis = comboBox3.SelectedItem.ToString();

			Bitmap img = (Bitmap)pictureBox1.Image;
			List<List<Tuple<PointPol, double>>> pixels = rastrAll(axis);
			pix[,] b = Z_buffer(pixels);

			for (int i = 0; i < pictureBox1.Width; ++i)
				for (int j = 0; j < pictureBox1.Height; ++j)
					img.SetPixel(i, j, b[i, j].c);

			pictureBox1.Image = img;
		}

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedItem.ToString())
            {
                case "Гексаэдр":
                    figures.Add(createCubs());
                    break;
                case "Тетраэдр":
                    figures.Add(createT());
                    break;
            }

            print_figures();

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

        private void button7_Click(object sender, EventArgs e)
        {
            double ind_scale = Double.Parse(textBox9.Text.ToString());
            foreach (var f in figures)
                f.scale(ind_scale);

            print_figures();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x = Double.Parse(textBox6.Text.ToString());
            double y = Double.Parse(textBox7.Text.ToString());
            double z = Double.Parse(textBox8.Text.ToString());

            foreach (var f in figures)
                f.shift(x,y,z);

            print_figures();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = comboBox2.SelectedItem.ToString();
            foreach (var f in figures)
                f.reflection(s);

            print_figures();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double x1 = Double.Parse(textBoxX1.Text.ToString());
            double y1 = Double.Parse(textBoxY1.Text.ToString());
            double z1 = Double.Parse(textBoxZ1.Text.ToString());

            double x2 = Double.Parse(textBoxX2.Text.ToString());
            double y2 = Double.Parse(textBoxY2.Text.ToString());
            double z2 = Double.Parse(textBoxZ2.Text.ToString());

            double angle = Double.Parse(textBoxAngle.Text.ToString());
            PointPol p1 = new PointPol(x1, y1, z1);
            PointPol p2 = new PointPol(x2, y2, z2);

            foreach (var f in figures)
                f.rotate(Tuple.Create(p1, p2), angle);

            print_figures();
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
        public Point To2D(string display)
        {
            double[,] displayMatrix = new double[4, 4] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5), 0 }, { 1 / Math.Sqrt(6), 2 / Math.Sqrt(6), 1 / Math.Sqrt(6), 0 }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 0 }, { 0, 0, 0, 1 } }; ;

            if (display == "Ортогональная по Z")
                displayMatrix = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 1 } };

            var temp = matrix_multiplication(displayMatrix, getP1());
            var temp2d = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[1, 0]));
            return temp2d;
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

