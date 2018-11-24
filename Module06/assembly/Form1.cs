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
        public Polyhedron pol;
        Graphics g;
        Bitmap bmp;
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            pol = new Polyhedron();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
            pen = new Pen(Color.Black);
        }

        public void Clear()
        {
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
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
                print();
            }
        }

        public void print() {
            var res = pol.Display();
            foreach (var i in res)
                g.DrawLine(pen, i.Item1, i.Item2);
            pictureBox1.Image = pictureBox1.Image;
        }

        private string save()
        {
            string result = "";
            foreach (var p in pol.polygons)
            {
                foreach (var t in p.vertices)
                    if (p.vertices.Last() == t)
                        result += "" + pol.vertices[t].X + ';' + pol.vertices[t].Y + ';' + pol.vertices[t].Z;
                    else
                        result += "" + pol.vertices[t].X + ';' + pol.vertices[t].Y + ';' + pol.vertices[t].Z + ' ';
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
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName, false, System.Text.Encoding.Default))
                    sw.WriteLine(save());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double ind_scale = Double.Parse(textBox5.Text);
            pol.scale(ind_scale);
            Clear();
            print();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double x = Double.Parse(textBox6.Text);
            double y = Double.Parse(textBox7.Text);
            double z = Double.Parse(textBox8.Text);
            pol.shift(x, y, z);
            Clear();
            print();
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

            Tuple<PointPol, PointPol> e1 = Tuple.Create(new PointPol(x1, y1, z1), new PointPol(x2, y2, z2));
            pol.rotate(e1, angle);
            Clear();
            print();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string axis = comboBox2.SelectedItem.ToString();
            pol.reflection(axis);
            Clear();
            print();
        }
    }
}
