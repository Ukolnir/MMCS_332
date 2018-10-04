﻿using System;
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
        bool drawing; //рисуем ли мы на данный момент?
        int cnt; //счетчик для прорисовки ребра
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Clear();
            pictureBox1.Image = bmp;
        }

        //удаление всех объектов
        private void Clear()
        {
            var g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(pictureBox1.BackColor);
            pictureBox1.Image = pictureBox1.Image;
            list.Clear();
            primitiv.Clear();
            cnt = 0;
        }

        List<Tuple<double, double>> primitiv = new List<Tuple<double,double>>(); //список точек для примитива
        List<Point> list = new List<Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((radioButton2.Checked && cnt < 1) || radioButton3.Checked) //для ребра больше одной линии нельзя
            {
                drawing = true;
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                list.Add(new Point(e.X, e.Y));
                cnt++;
            }

            if (radioButton1.Checked)
            {
                primitiv.Add(Tuple.Create(e.X * 1.0, e.Y * 1.0));
                ((Bitmap)pictureBox1.Image).SetPixel(e.X, e.Y, Color.Black);
                pictureBox1.Image = pictureBox1.Image;
            }
        }


        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if ((radioButton2.Checked && cnt > 1) || ! drawing ) return;

            list.Add(new Point(e.X, e.Y));
            Point start = list.First();

            foreach (var p in list)
            {
                var pen = new Pen(Color.Black, 1);
                var g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(pen, start, p);
                pen.Dispose();
                g.Dispose();
                pictureBox1.Image = pictureBox1.Image;
                start = p;
            }
            drawing = false;
        }

        //перемножение матриц
        private double[,] matrix_multiplication(double[,] m1, double[,] m2)
        {
            double[,] res = new double[m1.GetLength(0), m2.GetLength(1)];

            for(int i = 0; i < m1.GetLength(0); ++i)
                for(int j = 0; j < m2.GetLength(1); ++j)
                    for (int k = 0; k < m2.GetLength(0); k++)
                    {
                        res[i, j] += m1[i, k] * m2[k, j];
                    }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            list.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            list.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            { 
                case "Смещение":
                    /*label2.Text = "Выберите точку смещения";
                    pictureBox1.MouseDown -= (MouseEventHandler)pictureBox1_MouseDown;
                    pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown1);*/
                    double[,] toMachineCoordsionMatrix = new double[,] { { 1.0, 0, 0 }, { 0, -1.0, 0 }, { 100 / 2, 100 / 2, 1.0 } }; //w, h


                    break;
                    

                case "Масштабирование":
                    
                    label2.Text = "Выберите коэффициент масштабирования";
                    while (textBox1.Text == "")
                        continue;
                    double cm = Double.Parse(textBox1.Text); //прочитали коэфициент
                    double[,] scaling_matrix = matrix_multiplication(new double[3, 3] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { 0, -pictureBox1.Height + 1, 1 } },
                        new double[3, 3] { { cm, 0, 0 }, { 0, cm, 0 }, { 0, 0, 1 } });
                    scaling_matrix = matrix_multiplication(scaling_matrix, new double[3, 3] { { 1.0, 0, 0 }, { 0, 1.0, 0 }, { 0, pictureBox1.Height - 1, 1 } });


                    break;
            }
        }
    }
}