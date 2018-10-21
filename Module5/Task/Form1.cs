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

            Pen col = new Pen(Color.Black);
            pol.Display();
            for(int i = 0; i < pol.vertices2D.Count; ++i)
                for (int j = 1; j < pol.vertices2D.Count; ++j)
                    g.DrawLine(col, pol.vertices2D[i], pol.vertices2D[j]);
              
            pictureBox1.Image = pictureBox1.Image;
        }

        private void button1_Click(object sender, EventArgs e){
            Clear();
        }
    }

    public class PointPol {
       public double X, Y, Z, W;
        
        public PointPol(double x, double y, double z){
            X = x; Y = y; Z = z; W = 1;
        }

        public double[,] getP(){
            return new double[3,1]{{X},{Y},{Z}};
        }
    }

    public class Polyhedron
    {
        Form1 _form = new Form1(); //Доступ к элементам формы
        int len = 100;
        public List<PointPol> vertices; //Список вершин многогранника
        public List<Point> vertices2D; 

        //Конструктор. Рисуется просто гексаэдр, 
        //обход вершин: A A1 B1 B C C1 D1 D A, где без индексов - основание куба

        public Polyhedron(){
            vertices = new List<PointPol>();
            vertices.Add(new PointPol(0, len, 0));
            vertices.Add(new PointPol(0, len, len));
            vertices.Add(new PointPol(0, 0, len));
            vertices.Add(new PointPol(0, 0, 0));
            vertices.Add(new PointPol(len, 0, 0));
            vertices.Add(new PointPol(len, 0, len));
            vertices.Add(new PointPol(len, len, len));
            vertices.Add(new PointPol(len, len, 0));
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

        }

        //Октаэдр
        public void Octahedron() {
            
        }

        public void Display() { 
            vertices2D = new List<Point>();
            double[,] transformMatrix = new double[3,3]{{Math.Sqrt(0.5),0,-Math.Sqrt(0.5)},{1/Math.Sqrt(6),Math.Sqrt(2)/3,1/Math.Sqrt(6)},{1/Math.Sqrt(3), -1/Math.Sqrt(3), 1/Math.Sqrt(3)}};
            foreach (var p in vertices) {
                var temp = matrix_multiplication(transformMatrix, p.getP());
                vertices2D.Add(new Point(Convert.ToInt32(temp[0,0]), Convert.ToInt32(temp[1,0])));
                
            }
        } 

        //public virtual void shift() { }
    }
}
