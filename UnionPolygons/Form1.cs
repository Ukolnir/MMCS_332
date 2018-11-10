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
        Pen pen_union = new Pen(Color.Red, 2);
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            g.TranslateTransform(1, -1);
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
        bool direction = true;
        private void pictureBox1_MouseDown1(object sender, MouseEventArgs e)
        {
            if (!twoPolygons){
                if (flag){
                    int temp1 = newPolygon.Last().X;
                    int temp2 = newPolygon.Last().Y;
                    if (direction){
                        if (e.X > temp1)
                            for (int i = temp1 + 1; i <= e.X; ++i)
                                newPolygon.Add(new Point(i, temp2));
                        else
                            for (int i = temp1 - 1; i >= e.X; --i)
                                newPolygon.Add(new Point(i, temp2));
                        direction = false;
                    }
                    else{
                        if (e.Y > temp2)
                            for (int i = temp2 + 1; i <= e.Y; ++i)
                                newPolygon.Add(new Point(temp1, i));
                        else
                            for (int i = temp2 - 1; i >= e.Y; --i)
                                newPolygon.Add(new Point(temp1, i));
                        direction = true;
                    }
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
            if (newPolygon.First() != newPolygon.Last())
            {
               /* int xF = newPolygon.First().X;
                int yF = newPolygon.First().Y;
                int xL = newPolygon.Last().X;
                int yL = newPolygon.Last().Y;
                if (Math.Abs(xF - xL) > Math.Abs(yF - yL)) {
                    if (xL > xF)
                        for (int i = xL - 1; i >= xF; --i)
                            newPolygon.Add(new Point(i, yL));
                    else
                        for (int i = xL + 1; i <= xF ; ++i)
                            newPolygon.Add(new Point(i, yL));

                    if (yL > yF)
                        for (int i = temp2 + 1; i <= e.Y; ++i)
                            newPolygon.Add(new Point(temp1, i));
                    else
                        for (int i = temp2 - 1; i >= e.Y; --i)
                            newPolygon.Add(new Point(temp1, i));
                    direction = true;
                 }*/
                
                newPolygon.Add(newPolygon.First());
                g.DrawLines(new Pen(Color.Black), newPolygon.ToArray());
                pictureBox1.Image = bmp;
                
            }

            if (twoPolygons){
                label1.Text = "В данной реализации объединение работает только с двумя полигонами одновременно";
                return;
            }

            if (general.Count == 0){
                general = newPolygon;
                newPolygon = new List<Point>();
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
			return false;
		}

        //Функция берет список, стартовую точку в нем, ближайшее пересечение
        //Действие - добавляет точки в список
        private void add_points(ref List<Point> src, Point start, Point intersection) {
            Point temp = start;
            while (temp != intersection) {
                UNION.Add(temp);
                temp = next(ref src, temp);
            }
            UNION.Add(intersection);
        }
        
        //Функция возвращает следущую точку списка, если конец списка -> возвращает начало
        private Point next(ref List<Point> lst, Point p) {
            int index = lst.FindIndex(x => x == p);
            Point result;
            if (index + 1 == lst.Count)
                result = lst[0];
            else
                result = lst[index + 1];
            return result;
        }

        private Point prev(ref List<Point> lst, Point p)
        {
            int index = lst.FindIndex(x => x == p);
            Point result;
            if (index - 1 < 0)
                result = lst[lst.Count - 1];
            else
                result = lst[index - 1];
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var general_min = general.OrderBy(x => x.X).ThenBy(x => x.Y).First();
            var newPol_min = newPolygon.OrderBy(x => x.X).ThenBy(x => x.Y).First();
            bool flag_pol = true;
            var intersect = general.Intersect(newPolygon).ToList();
            Point start_point;
            if (general_min == newPol_min || general_min.X < newPol_min.X ||
                (general_min.X == newPol_min.X && general_min.Y < newPol_min.Y))
                start_point = general_min;
            else{
                start_point = newPol_min;
                flag_pol = false;
            }

            int cnt_op = 3;

            List<Point> work_lst;
            if (flag_pol)
                work_lst = general;
            else
                work_lst = newPolygon;
            Point t = start_point;
            foreach (var i in intersect){
                if (cnt_op == 0)
                    break;
                add_points(ref work_lst, t, i);
                Point current_point;
                if (flag_pol){
                    current_point = next(ref general, i);
                    Point check_now = next(ref newPolygon, i);
                    if (check_dir(check_now, Tuple.Create(i, current_point)))
                    {
                        work_lst = newPolygon;
                        flag_pol = false;
                        t = check_now;
                    }
                    else
                    {
                        check_now = prev(ref newPolygon, i);
                        if (check_dir(check_now, Tuple.Create(i, current_point)))
                        {
                            textBox2.Text = "Пошло сюда";
                            work_lst = newPolygon;
                            flag_pol = false;
                            t = check_now;
                        }
                        else
                            t = current_point;
                    }
                }
                else {
                    current_point = next(ref newPolygon, i);
                    Point check_now = next(ref general, i);
                    if (check_dir(check_now, Tuple.Create(i, current_point)))
                    {
                        work_lst = general;
                        flag_pol = true;
                        t = check_now;
                    }
                    else
                    {
                        check_now = prev(ref general, i);
                        if (check_dir(check_now, Tuple.Create(i, current_point)))
                        {
                            work_lst = general;
                            flag_pol = true;
                            t = check_now;
                        }
                        else
                            t = current_point;
                    }
                }
                --cnt_op;
            }

            if (cnt_op != 0)
            {

                if (flag_pol)
                {
                    if (UNION.Count == 0)
                        UNION.Add(next(ref general, general.First()));
                    add_points(ref general, UNION.Last(), general.First());
                }
                else
                {
                    if (UNION.Count == 0)
                        UNION.Add(newPolygon.First());
                    add_points(ref newPolygon, UNION.Last(), newPolygon.First());
                }
            }
            foreach (var i in UNION)
                textBox1.Text += "(" + i.X + "; " + i.Y + ")   ";

            g.DrawLines(pen_union, UNION.ToArray());
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
            general.Clear();
            newPolygon.Clear();
            UNION.Clear();
            twoPolygons = false;
            flag = false;
        }
    }
}














/*textBox1.Text = "";
           foreach (var i in general)
               textBox1.Text += "(" + i.X + "; " + i.Y + ")    ";

           textBox2.Text = "";
           foreach (var i in newPolygon)
               textBox2.Text += "(" + i.X + "; " + i.Y + ")    ";

           textBox3.Text = "";
           foreach (var i in intersect)
               textBox3.Text += "(" + i.X + "; " + i.Y + ")    ";*/