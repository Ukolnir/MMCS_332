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
        PointPol dot = new PointPol(-1, -1, -1);
        Tuple<PointPol, PointPol> line;
        List<PointPol> polygon = new List<PointPol>();
        double r = 0.002;
        double ind_scale;

        int ind_op;

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
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            trackBar1.Value = 0;
            trackBar1.Maximum = pictureBox1.Width;
            pictureBox1.Image = bmp;
            textBox2.Text = trackBar1.Value.ToString();
            textBox4.Text = "0,002";

        }

        public Graphics g;
        public Bitmap bmp;

        public void Clear()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            label3.Text = "";
            ind_op = -1;
            comboBox1.SelectedItem = "...";
            dot = new PointPol(-1, -1, -1);
            line = Tuple.Create(dot, dot);
            polygon.Clear();
        }

        public void ClearWithout()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        public void find_center(Tuple<PointPol, PointPol> line_1, ref double x, ref double y, ref double z)
        {
            x = (line_1.Item1.X + line_1.Item2.X) / 2;
            y = (line_1.Item1.Y + line_1.Item2.Y) / 2;
            z = (line_1.Item1.Z + line_1.Item2.Z) / 2;
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

        public double[,] matrix_for_point(PointPol p, double a, double b, double c)
        {
            double[,] transfer = new double[4, 4] { { 1, 0, 0, -a }, { 1, 0, 0, -b }, { 1, 0, 0, -c }, { 0, 0, 0, 1 } };
            var t1 = matrix_multiplication(p.getP(), transfer);

            t1 = matrix_multiplication(t1, new double[4, 4] { { ind_scale, 0, 0, 0 }, { 0, ind_scale, 0, 0 }, { 0, 0, ind_scale, 0 }, { 0, 0, 0, 1 } });
            transfer = new double[4, 4] { { 1, 0, 0, a }, { 1, 0, 0, b }, { 1, 0, 0, c }, { 0, 0, 0, 1 } };
            t1 = matrix_multiplication(t1, transfer);

            return t1;
        }

        public Tuple<PointPol, PointPol> scale(Tuple<PointPol, PointPol> line_1)
        {
            double a, b, c;
            a = b = c = 0;
            find_center(line_1, ref a, ref b, ref c);

            double[,] p1 = matrix_for_point(line_1.Item1, a, b, c);
            double[,] p2 = matrix_for_point(line_1.Item2, a, b, c);

            PointPol ps1 = new PointPol(p1[0, 0], p1[0, 1], p1[0, 2]);
            PointPol ps2 = new PointPol(p2[0, 0], p2[0, 1], p2[0, 2]);

            Tuple<PointPol, PointPol> new_line = Tuple.Create(ps1, ps2);
            return new_line;
        }

        public List<PointPol> scale(List<PointPol> list)
        {
            double a, b, c;
            a = b = c = 0;
            find_center(list, ref a, ref b, ref c);

            List<PointPol> new_list = new List<PointPol>();

            foreach (var p in list)
            {
                double[,] pn = matrix_for_point(p, a, b, c);
                PointPol ps1 = new PointPol(pn[0, 0], pn[0, 1], pn[0, 2]);
                new_list.Add(ps1);
            }

            return new_list;
            
        }

        //Пока рисует объект многогранник (любой из)
        private void button2_Click(object sender, EventArgs e)
        {
            Polyhedron pol = new Polyhedron();

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Гексаэдр":
                    //Уже внутри объекта при инициализации
                    break;
                case "Тетраэдр":
                    pol.Tetrahedron();
                    break;
                case "Октаэдр":
                    pol.Octahedron();
                    break;
                case "Точка":
                    ind_op = 1;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                case "Отрезок":
                    ind_op = 2;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                case "Полигон":
                    ind_op = 3;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                default:
                    break;
            }

            if (ind_op == -1)
            {
                Pen col = new Pen(Color.Black);
                pol.Display();
                foreach (var i in pol.edges)
                    g.DrawLine(col, i.Item1, i.Item2);
                textBox1.Text = "";
                foreach (var i in pol.vertices)
                    textBox1.Text += "(" + i.X + " " + i.Y + " " + i.Z + ")     ";

               /* pol.shift(-100, 0, 0);
                pol.Display();
                foreach (var i in pol.edges)
                    g.DrawLine(col, i.Item1, i.Item2);

                textBox3.Text = "";
                foreach (var i in pol.vertices)
                    textBox3.Text += "(" + i.X + " " + i.Y + " " + i.Z + ")     ";
                */pictureBox1.Image = pictureBox1.Image;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int v = Int32.Parse(textBox2.Text);
            trackBar1.Value = v;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            double z = trackBar1.Value;
            dot = new PointPol(e.X, e.Y, z);

            switch (ind_op)
            {
                case 1:
                    break;
                case 2:
                    if (line.Item1.X == -1)
                        line = Tuple.Create(dot, dot);
                    else
                        line = Tuple.Create(line.Item1, dot);
                    break;
                case 3:
                    polygon.Add(dot);
                    break;
            }
        }

        public void draw_elems(PointPol p)
        {
            //double[,] transformMatrix = new double[3, 3] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5) }, { 1 / Math.Sqrt(6), Math.Sqrt(2) / 3, 1 / Math.Sqrt(6) }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) } };
            double c = 1 + p.Z * r;
            double[,] transformMatrix = new double[4, 4] { { 1.0/c, 0, 0, 0 }, { 0, 1.0/c, 0, 0}, { 0, 0, 0, 0}, { 0, 0, -1.0/(r*c), 1.0/c} };
            var temp = matrix_multiplication(p.getP(), transformMatrix);
            Point dot2D = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[0, 1]));
            bmp = (Bitmap)pictureBox1.Image;
            bmp.SetPixel(dot2D.X, dot2D.Y, Color.Black);
            bmp.SetPixel(dot2D.X + 1, dot2D.Y, Color.Black);
            bmp.SetPixel(dot2D.X - 1, dot2D.Y, Color.Black);
            bmp.SetPixel(dot2D.X, dot2D.Y + 1, Color.Black);
            bmp.SetPixel(dot2D.X, dot2D.Y - 1, Color.Black);
            pictureBox1.Image = bmp;
        }

        public void draw_elems(Tuple<PointPol, PointPol> linel)
        {
            double c = 1 + linel.Item1.Z * r;
            double[,] transformMatrix = new double[4, 4] { { 1.0 / c, 0, 0, 0 }, { 0, 1.0 / c, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, -1.0 / (r * c), 1.0 / c } }; var temp = matrix_multiplication(linel.Item1.getP(), transformMatrix);
            Point p1 = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[0, 1]));

            c = 1 + linel.Item2.Z * r;
            transformMatrix = new double[4, 4] { { 1.0 / c, 0, 0, 0 }, { 0, 1.0 / c, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, -1.0 / (r * c), 1.0 / c } };
            var temp1 = matrix_multiplication(linel.Item2.getP(), transformMatrix);
            Point p2 = new Point(Convert.ToInt32(temp1[0, 0]), Convert.ToInt32(temp1[0, 1]));

            if (p1 != p2)
            {
                Pen mypen = new Pen(Color.Black);
                g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(mypen, p1, p2);
                pictureBox1.Image = pictureBox1.Image;
            }
        }

        public void draw_elems(List<PointPol> pol)
        {
            List<Point> polygon2D = new List<Point>();

            foreach (var p in pol)
            {
                double c = 1 + p.Z * r;
                double[,] transformMatrix = new double[4, 4] { { 1.0 / c, 0, 0, 0 }, { 0, 1.0 / c, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, -1.0 / (r * c), 1.0 / c } };
                var temp = matrix_multiplication(p.getP(), transformMatrix);
                polygon2D.Add(new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[0, 1])));
            }

            Point p1 = polygon2D.First();
            g = Graphics.FromImage(pictureBox1.Image);
            Pen mypen = new Pen(Color.Black);

            for (int i = 1; i < polygon2D.Count(); i++)
            {
                g.DrawLine(mypen, p1, polygon2D[i]);
                p1 = polygon2D[i];
            }

            pictureBox1.Image = pictureBox1.Image;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            double z = trackBar1.Value;

            switch (ind_op)
            {
                case 1:
                    draw_elems(dot);
                    break;
                case 2:
                    //line = Tuple.Create(dot, new PointPol(e.X, e.Y, z));
                    draw_elems(line);
                    break;
                case 3:
                    polygon.Add(new PointPol(e.X, e.Y, z));
                    draw_elems(polygon);
                    break;
            }

            label3.Text = "";
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
        }

        private void trackBar1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox2.Text = trackBar1.Value.ToString();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Точка":
                    ind_op = 1;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                case "Отрезок":
                    ind_op = 2;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                case "Полигон":
                    ind_op = 3;
                    label3.Text = "Выберите координаты точки х и у в окне, а также дополнтельно координату z";
                    break;
                default:
                    break;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            r = Double.Parse(textBox4.Text);
        }

        //direction(l,m,n)
        PointPol axisX = new PointPol(1, 0, 0);
        PointPol axisY = new PointPol(0, 1, 0);
        PointPol axisZ = new PointPol(0, 0, 1);

        private PointPol rotate(PointPol obj, PointPol direction, double phi)
        {
            double x = direction.X;
            double y = direction.Y;
            double z = direction.Z;

            double len = Math.Sqrt(x * x + y * y + z * z);

            double l = x / len;
            double m = y / len;
            double n = z / len;

            double[,] transfer = new double[4, 4] {
                { l*l+Math.Cos(phi)*(1 - l*l), l*(1-Math.Cos(phi))*m + n*Math.Sin(phi), l*(1-Math.Cos(phi))*n - m*Math.Sin(phi), 0 },
                { l*(1-Math.Cos(phi))*m - n*Math.Sin(phi), m*m+Math.Cos(phi)*(1 - m*m), m*(1-Math.Cos(phi))*n + l*Math.Sin(phi), 0 },
                { l*(1-Math.Cos(phi))*n + m*Math.Sin(phi), m*(1-Math.Cos(phi))*n - l*Math.Sin(phi), n*n+Math.Cos(phi)*(1 - n*n), 0 },
                { 0, 0, 0, 1 } };
            var t1 = matrix_multiplication(direction.getP(), transfer);

            t1 = matrix_multiplication(t1, transfer);

            PointPol p = new PointPol(t1[0, 0], t1[0, 1], t1[0, 2], t1[0, 3]);

            return p;
        }

        private Tuple<PointPol, PointPol> rotate(Tuple<PointPol, PointPol> obj, PointPol direction, double phi)
        {
            PointPol p1 = rotate(obj.Item1, direction, phi);
            PointPol p2 = rotate(obj.Item2, direction, phi);
            return Tuple.Create(p1, p2);
        }

        private List<PointPol> rotate(List<PointPol> obj, PointPol direction, double phi)
        {
            List<PointPol> l = new List<PointPol>();
            foreach (var p in obj)
            {
                PointPol newp = rotate(p, direction, phi);
                l.Add(newp);
            }
            return l;
        }

    }

    public class PointPol {
       public double X, Y, Z, W;
        
        public PointPol(double x, double y, double z){
            X = x; Y = y; Z = z; W = 1;
        }

        public PointPol(double x, double y, double z, double w){
            X = x; Y = y; Z = z; W = w;
        }

        public double[,] getP(){
            return new double[1,4]{{X, Y, Z, W}};
        }

        public double[,] getP1()
        {
            return new double[3, 1] { { X}, {Y}, { Z } };
        }

        public double[,] getPol()
        {
            return new double[4, 1] { { X }, { Y }, { Z }, { W } };
        }
    }

    public class Polyhedron
    {
        Form1 _form = new Form1(); //Доступ к элементам формы
        int len = 100;
        public List<PointPol> vertices; //Список вершин многогранника
        public List<Point> vertices2D;
        public Dictionary<PointPol, List<PointPol>> neighbors = new Dictionary<PointPol, List<PointPol>>();
        public List<Tuple<Point, Point>> edges;

        //Изометрическая проекция
        double[,] displayMatrix = new double[3, 3] { { Math.Sqrt(0.5), 0, -Math.Sqrt(0.5) }, { 1 / Math.Sqrt(6), Math.Sqrt(2) / 3, 1 / Math.Sqrt(6) }, { 1 / Math.Sqrt(3), -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) } };

        //Конструктор. Создается гексаэдр, 
        //обход вершин: A A1 B1 B C C1 D1 D A, где без индексов - основание куба

        public Polyhedron(){
            vertices = new List<PointPol>();

            vertices = new List<PointPol>();
            var h = _form.pictureBox1.Height / 2;
            var w = _form.pictureBox1.Width / 2;
            PointPol a = new PointPol(w, h + len, 0), a1 = new PointPol(w, h + len, len),
                b1 = new PointPol(w, h, len), b = new PointPol(w, h, 0),
                c = new PointPol(w + len, h, 0), c1 = new PointPol(w + len, h, len),
                d = new PointPol(w + len, h + len, 0), d1 = new PointPol(w + len, h + len, len);

            neighbors[a] = new List<PointPol>();
            neighbors[b] = new List<PointPol>();
            neighbors[c] = new List<PointPol>();
            neighbors[d] = new List<PointPol>();

            neighbors[a1] = new List<PointPol>();
            neighbors[b1] = new List<PointPol>();
            neighbors[c1] = new List<PointPol>();
            neighbors[d1] = new List<PointPol>();

            vertices.Add(a); //A
            neighbors[a1].Add(a);
            neighbors[b].Add(a);
            neighbors[d].Add(a);

            vertices.Add(a1); //A1
            neighbors[a].Add(a1);
            neighbors[b1].Add(a1);
            neighbors[d1].Add(a1);

            vertices.Add(b1); //B1
            neighbors[a1].Add(b1);
            neighbors[c1].Add(b1);
            neighbors[b].Add(b1);

            vertices.Add(b); // B
            neighbors[a].Add(b);
            neighbors[c].Add(b);
            neighbors[b1].Add(b);

            vertices.Add(c); // C
            neighbors[c1].Add(c);
            neighbors[d].Add(c);
            neighbors[b].Add(c);

            vertices.Add(c1);//C1
            neighbors[c].Add(c1);
            neighbors[d1].Add(c1);
            neighbors[b1].Add(c1);

            vertices.Add(d1);//D1
            neighbors[c1].Add(d1);
            neighbors[a1].Add(d1);
            neighbors[d].Add(d1);

            vertices.Add(d);//D
            neighbors[c].Add(d);
            neighbors[a].Add(d);
            neighbors[d1].Add(d);

        }

        //Тетраэдр
        public void Tetrahedron() {
            List<PointPol> temp = new List<PointPol>();
            temp.Add(vertices[0]); //A
            temp.Add(vertices[2]);
            temp.Add(vertices[4]);
            temp.Add(vertices[6]);
            Dictionary<PointPol, List<PointPol>> temp_n = new Dictionary<PointPol, List<PointPol>>();
            for (int i = 0; i < 4; ++i) {
                temp_n[temp[i]] = new List<PointPol>();
                temp_n[temp[i]].Add(temp[(i + 1) % 4]);
                temp_n[temp[i]].Add(temp[(i + 2) % 4]);
                temp_n[temp[i]].Add(temp[(i + 3) % 4]);
            }
            vertices = temp;
            neighbors = temp_n;
        }

        //Октаэдр
        public void Octahedron()
        {

        }

        //double[,] transformMatrix = new double[4, 4] { { 1 / Math.Sqrt(2), -1 / Math.Sqrt(6), 0, 0 }, { 0, Math.Sqrt(2 / 3), 0, 0 }, { -1 / Math.Sqrt(2), -1 / Math.Sqrt(6), 0, 0 }, { 0, 0, 0, 1 } };
        //double[,] transformMatrix = new double[4, 4] { { 1 / Math.Sqrt(2), -1 / Math.Sqrt(6), Math.Sqrt(1 / 3), 0 }, { 0, Math.Sqrt(2 / 3), Math.Sqrt(1 / 3), 0 }, { -1 / Math.Sqrt(2), -1 / Math.Sqrt(6), Math.Sqrt(1 / 3), 0 }, { 0, 0, 0, 1 } };
        //double[,] transformMatrix = new double[4, 4] { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { Math.Sqrt(2) / 4, Math.Sqrt(2) / 4, 0, 0 }, { 0, 0, 0, 1 } };
        public void Display()
        {
            edges = new List<Tuple<Point, Point>>();
            vertices2D = new List<Point>();
            foreach (var p in vertices)
            {
                //double c = 1 + p.Z * 0.002;
                //double[,] transformMatrix = new double[4, 4] { { 1.0 / c, 0, 0, 0 }, { 0, 1.0 / c, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, -1.0 / (0.002 * c), 1.0 / c } };
                //var temp = _form.matrix_multiplication(transformMatrix, p.getPol());
                var temp = _form.matrix_multiplication(displayMatrix, p.getP1());
                var temp2d = new Point(Convert.ToInt32(temp[0, 0]), Convert.ToInt32(temp[1, 0]));
                vertices2D.Add(temp2d);

                foreach (var t in neighbors[p])
                {
                    //c = 1 + t.Z * 0.00002;
                    //transformMatrix = new double[4, 4] { { 1.0 / c, 0, 0, 0 }, { 0, 1.0 / c, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, -1.0 / (0.00002 * c), 1.0 / c } };
                    //var temp = _form.matrix_multiplication(transformMatrix, t.getPol());
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
            //Показать в текстбоксах точку куда смещаем (или в текстбоксах будет задаваться точка, пока делаем так до интеграции)
            //double x = e.X, y = e.Y, z = 0;
            double[,] shiftMatrix = new double[4, 4] { { 1, 0, 0, x }, { 0, 1, 0, y }, { 0, 0, 1, z }, { 0, 0, 0, 1 } };

            List<PointPol> shift_vert = new List<PointPol>();

            var temp_vertices = vertices.Select(u => translatePol(_form.matrix_multiplication(shiftMatrix, u.getPol()))).ToList();

            Dictionary<PointPol, List<PointPol>> temp_dict = new Dictionary<PointPol, List<PointPol>>();

            for (int i = 0; i < neighbors.Count; ++i)
            {
                var key = temp_vertices[i];
                temp_dict[key] = neighbors[vertices[i]].Select(h => translatePol(_form.matrix_multiplication(shiftMatrix, h.getPol()))).ToList();
            }

            vertices = temp_vertices;
            neighbors = temp_dict;
        }
    }
}
