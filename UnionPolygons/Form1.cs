using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnionPolygons
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        List<Point> UNION = new List<Point>();
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }

        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        List<Point> general = new List<Point>();
        List<Point> newPolygon = new List<Point>();
        bool flag = false;
        bool twoPolygons = false;

        Graphics g;
        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            if (!twoPolygons){
                if (flag){
                    newPolygon.Add(new Point(e.X, e.Y));
                    g.DrawLines(new Pen(Color.Black), newPolygon.ToArray());
                }
                else
                {
                    newPolygon.Add(new Point(e.X, e.Y));
                    flag = true;
                }
                pictureBox1.Image = bmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (twoPolygons){
                label1.Text = "В данной реализации объединение работает только с двумя полигонами одновременно";
                return;
            }

            if (newPolygon.First() != newPolygon.Last()){
                newPolygon.Add(newPolygon.First());
                g.DrawLines(new Pen(Color.Black), newPolygon.ToArray());
                pictureBox1.Image = bmp;
            }

            if (general.Count == 0){
                general = newPolygon;
                newPolygon.Clear();
                flag = false;
            }
            else{
                twoPolygons = true;
                label1.Text = "В данной реализации объединение работает только с двумя полигонами одновременно";
            }
        }

		private bool check_dir(Point check, Tuple<Point, Point> cd) {
            double yb = cd.Item2.Y - cd.Item1.Y;
            double xb = cd.Item2.X - cd.Item1.X;
            double ya = check.Y - cd.Item1.Y;
            double xa = check.X - cd.Item1.X;
            
            if (yb * xa - xb * ya > 0)
                return true;
           /* else
                if (yb * xa - xb * ya < 0)
                    label5.Text += " правее";
                else
                    label5.Text += " лежит на прямой";*/

			return false;
		}

        //Функция берет список, стартовую точку в нем, ближайшее пересечение
        //Действие - добавляет точки в список

        //Функция возвращает следущую точку списка, если конец списка -> возвращает начало


        private void button2_Click(object sender, EventArgs e)
        {
            List<Point> UNION = new List<Point>();
            var general_min = general.OrderBy(x => x.X).ThenBy(x => x.Y).First();
            var newPol_min = newPolygon.OrderBy(x => x.X).ThenBy(x => x.Y).First();
            bool flag = true;
            Point start_point;
            if (general_min == newPol_min || general_min.X < newPol_min.X ||
                (general_min.X == newPol_min.X && general_min.Y < newPol_min.Y))
                start_point = general_min;
            else{
                start_point = newPol_min;
                flag = false;
            }

            UNION.Add(start_point);

            /*if (flag) {
                var index = general.FindIndex(p => p == start_point);
                Point temp = general[index];
                while (temp != general[index - 1]) {
                    var ff = general.FindIndex(p => p == temp);
                    if(newPolygon.Any(g=>g == temp)){
                        var ind = newPolygon.FindIndex(p => p == temp);
                        if (check_dir(newPolygon[ind + 1], Tuple.Create(temp, general[ff + 1]))) { 
                            
                        
                        }
                    }
                }
            }*/

        }
    }
}
