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
        Polyhedron pol;
        Pen col;

        public double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        public Form1()
        {
            col = new Pen(Color.Black);
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
        }

        public Graphics g;
        public Bitmap bmp;

        public void Clear()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        public void ClearWithout()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
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

        private void button2_Click(object sender, EventArgs e)
        {
            pol.Display();
            foreach (var i in pol.edges)
                g.DrawLine(col, i.Item1, i.Item2);
            textBox1.Text = "";
            foreach (var i in pol.vertices)
                textBox1.Text += "(" + i.X + " " + i.Y + " " + i.Z + ")     ";

            pictureBox1.Image = pictureBox1.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double ind_scale = Double.Parse(textBox5.Text);
            pol.scale(ind_scale);
            pol.Display();

            ClearWithout();
            foreach (var i in pol.edges)
                g.DrawLine(col, i.Item1, i.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x = Double.Parse(textBox6.Text);
            double y = Double.Parse(textBox7.Text);
            double z = Double.Parse(textBox8.Text);

            pol.shift(x, y, z);
            pol.Display();
            ClearWithout();
            foreach (var i in pol.edges)
                g.DrawLine(col, i.Item1, i.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string axis = comboBox2.SelectedItem.ToString();

            pol.reflection(axis);
            pol.Display();

            ClearWithout();
            foreach (var i in pol.edges)
                g.DrawLine(col, i.Item1, i.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double x1 = Double.Parse(textBoxX1.Text);
            double y1 = Double.Parse(textBoxY1.Text);
            double z1 = Double.Parse(textBoxZ1.Text);

            double x2 = Double.Parse(textBoxX2.Text);
            double y2 = Double.Parse(textBoxY2.Text);
            double z2 = Double.Parse(textBoxZ2.Text);

            double angle = Double.Parse(textBoxAngle.Text);

            Edge e1 = new Edge(new PointPol(x1, y1, z1), new PointPol(x2, y2, z2));
            pol.rotate(e1, angle);

            pol.Display();
            ClearWithout();
            foreach (var i in pol.edges)
                g.DrawLine(col, i.Item1, i.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pol = new Polyhedron();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*"; //формат загружаемого файла
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        pol.AddPolygon(line);
                }
            }
        }

        private string save()
        {
            string result = "";
            foreach (var p in pol.polygons)
            {
                foreach (var t in p.points)
                {
                    t.X -= pictureBox1.Width / 2;
                    if (p.points.Last() == t)
                        result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString();
                    else
                        result += t.X.ToString() + ";" + t.Y.ToString() + ";" + t.Z.ToString() + " ";
                    t.X += pictureBox1.Width / 2;
                }
                if (p != pol.polygons.Last())
                    result += Environment.NewLine;
            }
            textBox1.Text = result;
            return result;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
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

        public PointPol rotate(Edge direction, double angle, double a, double b, double c)
        {
            double phi = angle * Math.PI / 180;
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

    public class Polyhedron
    {
        Form1 _form = new Form1(); //Доступ к элементам формы
        public List<PointPol> vertices; //Список вершин многогранника
        public List<Point> vertices2D;
        public Dictionary<PointPol, List<PointPol>> neighbors;
        public List<Tuple<Point, Point>> edges; //Список ребер в 2d
        public List<Polygon> polygons;
        //Изометрическая проекция
        public double[,] displayMatrix = new double[4, 4] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5), 0 }, { 1 / Math.Sqrt(6), 2 / Math.Sqrt(6), 1 / Math.Sqrt(6), 0 }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 0 }, { 0, 0, 0, 1 } };

        public Polyhedron()
        {
            vertices = new List<PointPol>();
            neighbors = new Dictionary<PointPol, List<PointPol>>();
            polygons = new List<Polygon>();
        }

        public void AddPolygon(string line)
        {
            var prs = line.Split(' ').Select(x => x.Split(';').Select(z => Convert.ToDouble(z)).ToArray());
            List<PointPol> temp = new List<PointPol>();
            int wh = _form.pictureBox1.Width / 2;
            foreach (var y in prs)
            {
                PointPol t0 = new PointPol(y[0] + wh, y[1], y[2]);
                temp.Add(t0);
                if (vertices.Count == 0 || vertices.Any(v => v != t0))
                    vertices.Add(t0);
            }
            for (int i = 0; i < temp.Count - 1; ++i)
            {
                neighbors[temp[i]] = new List<PointPol>();
                neighbors[temp[i]].Add(temp[i + 1]);
            }
            neighbors[temp.Last()] = new List<PointPol>();
            neighbors[temp.Last()].Add(temp[0]);

            polygons.Add(new Polygon(temp));
        }

        public void Display()
        {
            edges = new List<Tuple<Point, Point>>();
            vertices2D = new List<Point>();
            foreach (var p in vertices)
            {
                var temp = _form.matrix_multiplication(displayMatrix, p.getP1());
                var temp2d = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[1, 0]));
                vertices2D.Add(temp2d);

                foreach (var t in neighbors[p])
                {
                    var t1 = _form.matrix_multiplication(displayMatrix, t.getP1());
                    vertices2D.Add(new Point(Convert.ToInt32(t1[0, 0]), Convert.ToInt32(t1[1, 0])));
                    edges.Add(Tuple.Create(temp2d, vertices2D.Last()));
                }
            }
        }

        private PointPol translatePol(double[,] f)
        {
            return new PointPol(f[0, 0], f[1, 0], f[2, 0], f[3, 0]);
        }

        public void shift(double x, double y, double z)
        {
            double[,] shiftMatrix = new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };

            List<PointPol> shift_vert = new List<PointPol>();

            var temp_vertices = vertices.Select(u => u.shift(x, y, z)).ToList();

            Dictionary<PointPol, List<PointPol>> temp_dict = new Dictionary<PointPol, List<PointPol>>();
            foreach (var f in polygons)
                f.points = f.points.Select(f1 => f1.shift(x, y, z)).ToList();

            for (int i = 0; i < neighbors.Count; ++i)
            {
                var key = temp_vertices[i];
                temp_dict[key] = neighbors[vertices[i]].Select(h => h.shift(x, y, z)).ToList();
            }

            vertices = temp_vertices;
            neighbors = temp_dict;
        }

        public void scale(double ind_scale)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            _form.find_center(vertices, ref a, ref b, ref c);

            List<PointPol> temp_vertices = new List<PointPol>();

            foreach (var p in vertices)
                temp_vertices.Add(p.scale(ind_scale, a, b, c));
            foreach (var f in polygons)
                f.points = f.points.Select(f1 => f1.scale(ind_scale, a, b, c)).ToList();
            Dictionary<PointPol, List<PointPol>> temp_dict = new Dictionary<PointPol, List<PointPol>>();

            for (int i = 0; i < neighbors.Count; ++i)
            {
                var key = temp_vertices[i];
                temp_dict[key] = neighbors[vertices[i]].Select(h => h.scale(ind_scale, a, b, c)).ToList();
            }
            vertices = temp_vertices;
            neighbors = temp_dict;
        }

        public void rotate(Edge direction, double phi)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            _form.find_center(vertices, ref a, ref b, ref c);
            List<PointPol> temp_vertices = new List<PointPol>();

            foreach (var p in vertices)
                temp_vertices.Add(p.rotate(direction, phi, a, b, c));
            foreach (var f in polygons)
                f.points = f.points.Select(f1 => f1.rotate(direction, phi, a, b, c)).ToList();
            Dictionary<PointPol, List<PointPol>> temp_dict = new Dictionary<PointPol, List<PointPol>>();

            for (int i = 0; i < neighbors.Count; ++i)
            {
                var key = temp_vertices[i];
                temp_dict[key] = neighbors[vertices[i]].Select(h => h.rotate(direction, phi, a, b, c)).ToList();
            }
            vertices = temp_vertices;
            neighbors = temp_dict;
        }

        public void reflection(string axis)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            _form.find_center(vertices, ref a, ref b, ref c);

            if (axis == "X")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(1, 0, 0)), 180);
                shift(-a * 2, 0, 0);
            }
            if (axis == "Y")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(0, 1, 0)), 180);
                shift(0, -b * 2, 0);
            }
            if (axis == "Z")
            {
                rotate(new Edge(new PointPol(0, 0, 0), new PointPol(0, 0, 1)), 180);
                shift(0, 0, -c * 2);
            }
        }
    }

    public class Camera {
        PointPol coords;
        //направление обзора
        double[,] projection;
    
    }
}
