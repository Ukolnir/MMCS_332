using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Individual_ASR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;

            Cw = pictureBox1.Image.Width;
            Ch = pictureBox1.Image.Height;
            Vw = Vh = d = 1;


            Render();
        }

        Bitmap bmp;
        int Cw, Ch, Vw, Vh, d;
        double inf = Double.MaxValue;

        static double d_eps = 0.00001;

        private struct Vec3d
        {
            public double x, y, z;
            public Vec3d(double X, double Y, double Z)
            {
                x = X; y = Y; z = Z;
            }

            public static Vec3d operator -(Vec3d v1, Vec3d v2)
            {
                return new Vec3d(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
            }

            public static Vec3d operator +(Vec3d v1, Vec3d v2)
            {
                return new Vec3d(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
            }
        }

        private struct Sphere
        {
            public Vec3d center;
            public double radius;
            public Color col;

            public Sphere(Vec3d Center, double Radius, Color Col)
            {
                center = Center; radius = Radius; col = Col;
            }
        }

        private struct Scene
        {
            public List<Sphere> spheres;

            public Scene(List<Sphere> Spheres)
            {
                spheres = Spheres;
            }
        }

        private void PutPixel(double x, double y, Color c)
        {
            x = Cw/2 + Math.Round(x);
            y = Ch/2 + Math.Round(y);
            bmp.SetPixel((int)x, (int)y, c);
        }

        private void Render()
        {
            Vec3d O = new Vec3d(0, 0, 0);
            for (int x = -Cw/2; x < Cw/2; ++x)
            {
                for (int y = -Ch/2; y < Ch/2; ++y)
                {
                    Vec3d D = CanvasToViewport(x, y);
                    Color color = TraceRay(O, D, 1, inf);
                    PutPixel(x, y, color);
                }
            }


            pictureBox1.Image = bmp;
        }

        private Vec3d CanvasToViewport(int x, int y)
        {
            return new Vec3d(x * Vw / Cw, y * Vh / Ch, d);
        }

        private Color TraceRay(Vec3d O, Vec3d D, double t_min, double t_max)
        {
            Scene scene = InitScene();

            double closest_t = inf;
            Sphere? closest_sphere = null;

            foreach (var sphere in scene.spheres)
            {
                Tuple<double, double> t = IntersectRaySphere(O, D, sphere);
                if (NumInRange(t.Item1, t_min, t_max) && t.Item1 < closest_t)
                {
                    closest_t = t.Item1;
                    closest_sphere = sphere;
                }
                if (NumInRange(t.Item2, t_min, t_max) && t.Item2 < closest_t)
                {
                    closest_t = t.Item2;
                    closest_sphere = sphere;
                }
            }

            if (closest_sphere == null)
            {
                return Color.White;
            }
            return closest_sphere.Value.col;
        }

        private Tuple<double, double> IntersectRaySphere(Vec3d O, Vec3d D, Sphere sphere)
        {
            Vec3d C = sphere.center;
            double r = sphere.radius;

            Vec3d OC = O - C;

            double k1 = Dot(D, D);
            double k2 = 2 * Dot(OC, D);
            double k3 = Dot(OC, OC) - r * r;

            double discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
            {
                return Tuple.Create(inf, inf);
            }

            double t1 = (-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            double t2 = (-k2 - Math.Sqrt(discriminant)) / (2 * k1);

            return Tuple.Create(t1, t2);
        }

        private Scene InitScene()
        {
            Vw = Vh = d = 1;

            Sphere s1 = new Sphere(new Vec3d(0, -1, 3), 1, Color.Red);
            Sphere s2 = new Sphere(new Vec3d(2, 0, 4), 1, Color.Blue);
            Sphere s3 = new Sphere(new Vec3d(-2, 0, 4), 1, Color.Green);

            Scene sc = new Scene(new List<Sphere>() { s1, s2, s3 });
            return sc;
        }

        private double Dot(Vec3d v1, Vec3d v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
        }

        private bool NumInRange(double num, double range_from, double range_to)
        {
            return ((range_from <= num) && (num <= range_to));
        }
    }
}
