using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1{
    public partial class Form1 : Form{
        Bitmap bmp;
        Graphics g;
        public Form1(){
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
        }

        private void Clear(){
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }

        List<string> param;

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e){
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files(*.txt)|*.txt|All files (*.*)|*.*"; //формат загружаемого файла
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK){
                param = new List<string>();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName, Encoding.Default)){
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        param.Add(line);
                }
            }
        }

        Dictionary<char, string> rules;
        string axiom;
        int cnt;
        double angle;
        string direction;

        List<Point> fractal;
        List<int> nolinepoint;
        Stack<Tuple<Point,double>> memory;
        Stack<Tuple<Color, Tuple<int,int>>> _memory;
        Dictionary<Point, Tuple<Color, Tuple<int, int>>> fractal_dict;
        Color color;
        int width = 7;
        int height = 100;
        
        private double[,] matrix_multiplication(double[,] m1, double[,] m2){
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];
            for (int i = 0; i < m1.GetLength(0); ++i)
                for (int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }

        private Point rotation(double ang, int x0, int y0, int x, int y) {
            double p = ang * Math.PI / 180;
            double cos = Math.Cos(p);
            double sin = Math.Sin(p);
            double[,] transferalMatrix = new double[,] { {cos, sin, 0}, {-sin, cos, 0}, 
                        {cos*(-x0)+y0*sin+x0, (-x0)*sin-y0*cos+y0, 1}};
            double[,] point = new double[,] { { x, y, 1.0 } };
            double[,] res = matrix_multiplication(point, transferalMatrix);
            return new Point(Convert.ToInt32(res[0, 0]), Convert.ToInt32(res[0, 1]));
        }

        private void perform_fractal(string str) { 
            int len = 10;
            int ht = pictureBox1.Height;
            fractal = new List<Point>();
            fractal.Add(new Point(0, ht));
            int countPoint = 0; //Первая точка - нулевая
            memory = new Stack<Tuple<Point, double>>();
            nolinepoint = new List<int>();

            double ang = 0;
            //Здесь идет разветвление по режиму
            for (int i = 0; i < str.Length; ++i) {
                switch (str[i]) { 
                    case 'F':
                        Point p0 = fractal.Last();
                        int x, y;
                        x = p0.X + len;
                        y = p0.Y;
                        var p = rotation(ang, p0.X, p0.Y, x, y);
                        fractal.Add(p);
                        ++countPoint;
                        break;
                    case '+':
                        ang = (ang + angle);
                        break;
                    case '-':
                        ang = (ang - angle);
                        break;
                    case '[':
                        var temp = Tuple.Create(fractal.Last(), ang);
                        memory.Push(temp);
                        break;
                    case ']':
                        var t0 = memory.Pop();
                        nolinepoint.Add(countPoint);
                        fractal.Add(t0.Item1);
                        ang = t0.Item2;
                        ++countPoint;
                        break;
                    default:
                        break;
                }
            }
        }

        private void perform_fractal_ac(string str)
        {
            int len = height;
            int ht = pictureBox1.Height;
            int wh = width;
            color = Color.ForestGreen;
            fractal_dict = new Dictionary<Point, Tuple<Color, Tuple<int, int>>>();
            fractal_dict.Add(new Point(0, ht), Tuple.Create(color, Tuple.Create(len, wh)));            
            int countPoint = 0; //Первая точка - нулевая
            Stack<int> check_cobaka = new Stack<int>();
            memory = new Stack<Tuple<Point, double>>();
            nolinepoint = new List<int>();
            Random rand = new Random();
            _memory = new Stack<Tuple<Color, Tuple<int, int>>>();
            
            double ang = 0;
            //Здесь идет разветвление по режиму
            for (int i = 0; i < str.Length; ++i)
            {
                switch (str[i])
                {
                    case 'F':
                        Point p0 = fractal_dict.Last().Key;
                        int x, y;
                        x = p0.X + fractal_dict.Last().Value.Item2.Item1;
                        y = p0.Y;
                        var p = rotation(ang, p0.X, p0.Y, x, y);
                        fractal_dict.Add(p, Tuple.Create(color, Tuple.Create(len, wh)));
                        ++countPoint;
                        break;
                    case '+':
                        ang = ang + rand.Next(Convert.ToInt32(angle)); //Здесь аккуратнее
                        break;
                    case '-':
                        ang = ang - rand.Next(Convert.ToInt32(angle));
                        break;
                    case '[':
                        var t = check_cobaka.Pop();
                        ++t;
                        check_cobaka.Push(t);
                        var temp = Tuple.Create(fractal_dict.Last().Key, ang);
                        memory.Push(temp);
                        
                        break;
                    case ']':
                        var c = check_cobaka.Pop();
                        --c;
                        if (c == -1){
                            var clw = _memory.Pop();
                            color = clw.Item1;
                            len = clw.Item2.Item1;
                            wh = clw.Item2.Item2;
                        }
                        var t0 = memory.Pop();
                        nolinepoint.Add(countPoint);
                        fractal_dict.Add(t0.Item1, Tuple.Create(color, Tuple.Create(len, wh)));
                        ang = t0.Item2;
                        ++countPoint;
                        break;
                    case '@':
                        _memory.Push(Tuple.Create(color, Tuple.Create(len, wh)));
                        color = Color.FromArgb(color.R + 10, color.G + 10, color.B + 10);
                        len -= 10;
                        wh -= 1;
                        check_cobaka.Push(0);
                        break;
                    default:
                        break;
                }
            }
            /*string s = "";
            for (int j = 0; j < fractal.Count; ++j)
                s += "(" + fractal[j].X + " ; " + fractal[j].Y + ")";
            textBox1.Text = s;*/
        }

        private void render(string str) {
            int minx, maxx, miny, maxy;
            IEnumerable<Point> query = fractal.OrderBy(x => x.X);
            minx = query.First().X;
            maxx = query.Last().X;
            query = fractal.OrderBy(y => y.Y);
            miny = query.First().Y;
            maxy = query.Last().Y;

           // string s = "(" + minx + " ; " + miny + ") " + "(" + maxx + " ; " + maxy + ")";
          //  textBox1.Text = s;

            if (maxy == miny)
                fractal = fractal.Select(p => new Point(Convert.ToInt32((pictureBox1.Width - 1) * (p.X - minx) / (maxx - minx)),
                    pictureBox1.Height - 5)).ToList();
            else
                fractal = fractal.Select(p => new Point(Convert.ToInt32((pictureBox1.Width - 1) * (p.X - minx) / (maxx - minx)),
                    Convert.ToInt32((pictureBox1.Height - 1) * (p.Y - miny) / (maxy - miny)))).ToList();
            
            var pen = new Pen(Color.Black);
            if (nolinepoint.Count == 0)
                g.DrawLines(pen, fractal.ToArray());
            else
                for (int i = 0; i < fractal.Count-1; ++i) { 
                    if(nolinepoint.Any(x=>x == i)) continue;
                    g.DrawLine(pen, fractal[i], fractal[i + 1]);
                }
            pictureBox1.Image = pictureBox1.Image;
            pen.Dispose();
        }

        private void render_rand(string str)
        {
           
           // for (int i = 0; i < fractal_dict.Count - 1; ++i){
                //if (nolinepoint.Any(x => x == i)) continue;
                   // g.DrawLine(new Pen(fractal_dict), fractal[i], fractal[i + 1]);
           // }
            //pictureBox1.Image = pictureBox1.Image;
           // pen.Dispose();
        }





        private void button1_Click(object sender, EventArgs e)
        {
            cnt = Convert.ToInt32(numericUpDown1.Value);
            axiom = param[0];  //аксиома
            rules = new Dictionary<char, string>(); //правила
            angle = Convert.ToDouble(param[1]); // угол
            direction = param[2]; // направление
            for (int i = 3; i < param.Count; ++i){
                char key = param[i][0];
                rules.Add(key, param[i].Substring(3));
            }
            //Раскрытие фрактала
            string pred = direction + axiom; //Обрабатывается начальное направление, смотреть внимательней
            string result = axiom;
            for (int i = 0; i < cnt; ++i){
                result = "";
                for (int j = 0; j < pred.Length; ++j){
                    string temp = pred[j].ToString();
                    if (rules.TryGetValue(temp[0], out temp))
                        result += temp;
                    else
                        result += pred[j];
                }
                pred = result;
            }
            //textBox1.Text = result;
            if (result.Any(x => x == '@'))
                label2.Text = "Включен режим случайности";
            //perform_fractal(result);
            //render(result);
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e){
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
        }
    }
}
