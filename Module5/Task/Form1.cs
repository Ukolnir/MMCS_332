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
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
        }

        public Graphics g;
        public Bitmap bmp;

        public void Clear() {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        //Пока рисует объект многогранник (любой из)
        private void button2_Click(object sender, EventArgs e)
        {
            Polyhedron pol = new Polyhedron();

            var temp = pol.getEdges(pol.vertices[0]);
            textBox1.Text = "(" + temp[0].Item1.X + "; " + temp[0].Item1.Y +")   ("+
                temp[0].Item2.X + "; " + temp[0].Item2.Y +")   ("+
                temp[1].Item2.X + "; " + temp[1].Item2.Y +")   ("+
                temp[2].Item2.X + "; " + temp[2].Item2.Y +")";

            switch (comboBox1.SelectedItem.ToString()) { 
                case "Гексаэдр":
                   //Уже внутри объекта при инициализации
                break;
                case "Тетраэдр":
                    pol.Tetrahedron();
                    break;
                case "Октаэдр":
                    pol.Octahedron();

                    break;
                default:
                    break;
            }
            Pen p = new Pen(Color.Black);
            foreach (var t in pol.vis_edge)
                g.DrawLine(p, t.Item1, t.Item2);
            foreach (var t in pol.invis_edge)
                g.DrawLine(p, t.Item1, t.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button1_Click(object sender, EventArgs e){
            Clear();
        }
    }




    public class Polyhedron
    {
        Form1 _form = new Form1(); //Доступ к элементам формы
        int len = 100;
        public List<Point> vertices; //Список вершин многогранника
        public List<Tuple<Point, Point>> vis_edge; //Видимые ребра
        public List<Tuple<Point, Point>> invis_edge; //Невидимые ребра
        //Конструктор. Рисуется просто гексаэдр, 
        //обход вершин: A A1 B1 B C C1 D1 D A, где без индексов - основание куба

        public Polyhedron()
        {
            //Рисование куба! //Слабонервным не смотреть
            
            vis_edge = new List<Tuple<Point, Point>>();
            vertices = new List<Point>();
            invis_edge = new List<Tuple<Point, Point>>();
            //A
            Point start = new Point(_form.pictureBox1.Width / 2 - 100, _form.pictureBox1.Height / 2);
            vertices.Add(start);
            //A1
            vertices.Add(new Point(start.X, start.Y - len));
            //A-A1
            vis_edge.Add(Tuple.Create(start, vertices.Last()));
            //B1
            var p = rotation(-45, vertices.Last().X, vertices.Last().Y, vertices.Last().X + len / 2, vertices.Last().Y);
            vertices.Add(p);
            //A1-B1
            vis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //B
            vertices.Add(new Point(vertices.Last().X, vertices.Last().Y + 100));
            //B1-B
            invis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //A-B
            invis_edge.Add(Tuple.Create(start, vertices.Last()));
            //C
            vertices.Add(new Point(vertices.Last().X + len, vertices.Last().Y));
            //B-C
            invis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //C1
            vertices.Add(new Point(vertices.Last().X, vertices.Last().Y - 100));
            //C-C1
            vis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //C1-B1
            vis_edge.Add(Tuple.Create(vertices[2], vertices.Last()));
            //D1
            p = rotation(-45, vertices.Last().X, vertices.Last().Y, vertices.Last().X - len / 2, vertices.Last().Y);
            vertices.Add(p);
            //C1-D1
            vis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //A1-D1
            vis_edge.Add(Tuple.Create(vertices[1], vertices.Last()));
            //D
            vertices.Add(new Point(vertices.Last().X, vertices.Last().Y + 100));
            //D1-D
            vis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last()));
            //C-D
            vis_edge.Add(Tuple.Create(vertices[4], vertices.Last()));
            //A-D
            vis_edge.Add(Tuple.Create(start, vertices.Last()));
        }

        public List<Tuple<Point, Point>> getEdges(Point f) {
            return vis_edge.Where(x => x.Item1 == f || x.Item2 == f).Union(
                invis_edge.Where(y => y.Item1 == f || y.Item2 == f)).ToList();
        }

        private double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        private Point rotation(double ang, int x0, int y0, int x, int y)
        {
            double p = ang * Math.PI / 180;
            double cos = Math.Cos(p);
            double sin = Math.Sin(p);
            double[,] transferalMatrix = new double[,] { {cos, sin, 0}, {-sin, cos, 0}, 
                        {cos*(-x0)+y0*sin+x0, (-x0)*sin-y0*cos+y0, 1}};
            double[,] point = new double[,] { { x, y, 1.0 } };
            double[,] res = matrix_multiplication(point, transferalMatrix);
            return new Point(Convert.ToInt32(res[0, 0]), Convert.ToInt32(res[0, 1]));
        }

        //Тетраэдр
        public void Tetrahedron() {
            List<Point> temp = new List<Point>();
            for (int i = 0; i < vertices.Count; i += 2)
                temp.Add(vertices[i]);
            vertices = temp;
            vis_edge = new List<Tuple<Point, Point>>();
            vis_edge.Add(Tuple.Create(vertices[0], vertices[1]));
            vis_edge.Add(Tuple.Create(vertices[0], vertices[2]));
            vis_edge.Add(Tuple.Create(vertices[0], vertices[3]));
            vis_edge.Add(Tuple.Create(vertices[1], vertices[3]));
            vis_edge.Add(Tuple.Create(vertices[3], vertices[2]));
            invis_edge = new List<Tuple<Point, Point>>();
            invis_edge.Add(Tuple.Create(vertices[1], vertices[2]));
        }

        //Октаэдр
        public void Octahedron() {
            var edges = getEdges(vertices.First());

            var avg = (edges[0].Item1.Y - edges[0].Item2.Y) / 2 + edges[0].Item2.Y;
            var rot = rotation(-45, edges[0].Item1.X, avg, (edges[2].Item2.X - edges[2].Item1.X) / 2 + edges[2].Item2.X, avg);

            var pt1 = rot;
            //var pt1 = new Point((edges[1].Item2.X - edges[1].Item1.X) / 2 + edges[1].Item2.X,
                //(edges[0].Item2.Y - edges[0].Item1.Y) / 2 + edges[0].Item2.Y); //(edges[0].Item2.Y - edges[0].Item1.Y) / 2 + edges[0].Item2.Y)
            var pt2 = new Point((edges[2].Item2.X - edges[2].Item1.X) / 2 + edges[2].Item2.X,
                (edges[0].Item2.Y - edges[0].Item1.Y) / 2 + edges[0].Item2.Y); //(edges[0].Item2.Y - edges[0].Item1.Y) / 2 + edges[0].Item2.Y)
            var pt3 = new Point((edges[2].Item2.X - edges[2].Item1.X) / 2 + edges[2].Item2.X,
                (edges[1].Item2.Y - edges[1].Item1.Y) / 2 + edges[1].Item2.Y); //(edges[1].Item2.Y - edges[1].Item1.Y) / 2 + edges[1].Item2.Y)
            vis_edge.Clear();
            vertices.Clear();
            invis_edge.Clear();
            vertices.Add(pt1); vertices.Add(pt2); vertices.Add(pt3);
            vis_edge.Add(Tuple.Create(pt1, pt2));
            vis_edge.Add(Tuple.Create(pt2, pt3));
            vis_edge.Add(Tuple.Create(pt1, pt3));
            /*vertices.Add(new Point(vertices[0].X + len, vertices[0].Y));
            vis_edge.Add(Tuple.Create(pt2, vertices.Last()));
            vis_edge.Add(Tuple.Create(pt3, vertices.Last()));
            var p = rotation(45, pt2.X, pt2.Y, pt2.X + len / 2, pt2.Y);
            vertices.Add(p);
            invis_edge.Add(Tuple.Create(vertices[vertices.Count - 2], vertices.Last())); //Для проверки, потом переделать в invis
            invis_edge.Add(Tuple.Create(pt1, vertices.Last())); //
            invis_edge.Add(Tuple.Create(pt3, vertices.Last()));   //
            vertices.Add(new Point(pt3.X, pt3.Y + len));
            vis_edge.Add(Tuple.Create(pt1, vertices.Last())); 
            vis_edge.Add(Tuple.Create(pt2, vertices.Last()));
            vis_edge.Add(Tuple.Create(vertices[3], vertices.Last()));*/
        }

        //public virtual void shift() { }
    }
}
