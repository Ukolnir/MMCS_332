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
        Scene scene;
        int recursion_depth = 5;

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

            public static Vec3d operator *(Vec3d v, double d)
            {
                return new Vec3d(v.x * d, v.y * d, v.z * d);
            }

            public static Vec3d operator /(Vec3d v, double d)
            {
                return new Vec3d(v.x / d, v.y / d, v.z / d);
            }

            public static Vec3d operator -(Vec3d v)
            {
                return new Vec3d(-v.x, -v.y, -v.z);
            }

            public double Length()
            {
                return Math.Sqrt(x*x + y*y+ z*z);
            }
        }

        private struct Sphere
        {
            public Vec3d center;
            public double radius;
            public Color col;
            public int specular;    // насколько блестящий
            public double reflective; // отражение

            public Sphere(Vec3d Center, double Radius, 
                Color Col, int Specular, double Reflective)
            {
                center = Center; radius = Radius; col = Col;
                specular = Specular; reflective = Reflective;
            }
        }

        enum LightType
        {
            Ambient, Point, Directional
        }

        private struct Light
        {
            public LightType type;
            public double intensity;
            public Vec3d? position_or_direction;

            public Light(LightType LType, double Intensity, Vec3d? PositionOrDirection = null)
            {
                type = LType;
                intensity = Intensity;
                position_or_direction = PositionOrDirection;
            }
        }

        private struct Scene
        {
            public List<Sphere> spheres;
            public List<Light> lights;

            public Scene(List<Sphere> Spheres, List<Light> Lights)
            {
                spheres = Spheres;
                lights = Lights;
            }
        }

        private void PutPixel(double x, double y, Color c)
        {
            x = Cw/2 + Math.Round(x);
            y = Ch/2 - Math.Round(y);
            bmp.SetPixel((int)x, (int)y, c);
        }

        private void Render()
        {
            Vec3d O = new Vec3d(0, 0, 0);
            for (int x = -Cw/2; x < Cw/2; ++x)
            {
                for (int y = -Ch/2+1; y < Ch/2; ++y)
                {
                    Vec3d D = CanvasToViewport(x, y);
                    Color color = TraceRay(O, D, 1, inf, recursion_depth);
                    PutPixel(x, y, color);
                }
            }


            pictureBox1.Image = bmp;
        }

        private Vec3d CanvasToViewport(double x, double y)
        {
            return new Vec3d(x * Vw / Cw, y * Vh / Ch, d);
        }

        private Color TraceRay(Vec3d O, Vec3d D, double t_min, double t_max, int depth)
        {
            InitScene();

            var pair = ClosestIntersection(O, D, t_min, t_max);
            double closest_t = pair.Item2;
            Sphere? closest_sphere = pair.Item1;

            if (closest_sphere.HasValue)
            {
                //Вычисление локального цвета
                Vec3d P = O + D * closest_t; // Вычисление точки пересечения
                Vec3d N = P - closest_sphere.Value.center; // Вычисление нормали к сфере в точке пересечения
                N = N / N.Length();

                double k = ComputeLightning(P, N, -D, closest_sphere.Value.specular);
                Color local_color = ColorDotDouble(closest_sphere.Value.col, k);
            
                //Если мы достигли предела рекурсии или объект не отражающий, то мы закончили
                double refl = closest_sphere.Value.reflective;
                if ((depth <= 0) || (refl <= 0))
                {
                    return local_color;
                }

                //Вычисление отражённого цвета
                Vec3d R = ReflectRay(-D, N);
                Color reflected_color = TraceRay(P, R, 0.001, inf, depth - 1);

                return ColorPlusColor(
                    ColorDotDouble(local_color, 1 - refl), 
                    ColorDotDouble(reflected_color, refl)
                    );
            }
            return Color.Black;
        }

        private Color ColorDotDouble(Color c, double k)
        {
            int r = Math.Min((int)Math.Round(c.R * k), 255);
            int g = Math.Min((int)Math.Round(c.G * k), 255);
            int b = Math.Min((int)Math.Round(c.B * k), 255);
            return Color.FromArgb(r, g, b);
        }

        private Color ColorPlusColor(Color c1, Color c2)
        {
            int r = Math.Min(c1.R + c2.R, 255);
            int g = Math.Min(c1.G + c2.G, 255);
            int b = Math.Min(c1.B + c2.B, 255);
            return Color.FromArgb(r, g, b);
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

        private void InitScene()
        {
            Vw = Vh = d = 1;

            List<Sphere> spheres = new List<Sphere>();
            spheres.Add(new Sphere(new Vec3d(0, -1, 3), 1, Color.Red, 500, 0.0));
            spheres.Add(new Sphere(new Vec3d(2, 0, 4), 1, Color.DarkBlue, 500, 0.3));
            spheres.Add(new Sphere(new Vec3d(-2, 0, 4), 1, Color.Green, 10, 0.4));
            //spheres.Add(new Sphere(new Vec3d(0, -0.5, 3), 0.5, Color.Red));
            //spheres.Add(new Sphere(new Vec3d(1.5, -0.5, 4), 0.5, Color.Blue));
            //spheres.Add(new Sphere(new Vec3d(-2, -0.5, 10), 0.5, Color.Green));
            spheres.Add(new Sphere(new Vec3d(0, -5001, 0), 5000, Color.Blue, 1000, 0.5));
            spheres.Add(new Sphere(new Vec3d(0, 0, 5006), 5000, Color.Yellow, 10, 0));


            List<Light> lights = new List<Light>();
            lights.Add(new Light(LightType.Ambient, 0.2));
            lights.Add(new Light(LightType.Point, 0.6, new Vec3d(2, 1, 0)));
            lights.Add(new Light(LightType.Directional, 0.6, new Vec3d(1, 4, 4)));

            scene = new Scene(spheres, lights);
        }

        private double Dot(Vec3d v1, Vec3d v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
        }

        private bool NumInRange(double num, double range_from, double range_to)
        {
            return ((range_from <= num) && (num <= range_to));
        }

        private double ComputeLightning(Vec3d P, Vec3d N, Vec3d V, int s)
        {
            double i = 0;       
            foreach (var light in scene.lights)
            {
                if (light.type == LightType.Ambient)
                {
                    i += light.intensity;
                }
                else
                {
                    Vec3d L = new Vec3d();
                    double t_max;
                    if (light.type == LightType.Point)
                    {
                        L = light.position_or_direction.Value - P;
                        t_max = 1;
                    }
                    else
                    {
                        L = light.position_or_direction.Value;
                        t_max = inf;
                    }

                    //Проверка тени
                    var pair = ClosestIntersection(P, L, 0.001, t_max);
                    double shadow_t = pair.Item2;
                    Sphere? shadow_sphere = pair.Item1;
                    if (shadow_sphere.HasValue)
                    {
                        continue;
                    }

                    //Диффузность
                    double n_dot_l = Dot(N, L);
                    if (n_dot_l > 0)
                    {
                        i += light.intensity * n_dot_l / (N.Length() * L.Length());
                    }

                    //Зеркальность
                    if (s != -1)
                    {
                        Vec3d R = N * Dot(N, L) * 2 - L;
                        double r_dot_v = Dot(R, V);
                        if (r_dot_v > 0)
                        {
                            double k = Math.Pow(r_dot_v / (R.Length() * V.Length()), s);
                            i += light.intensity * k;
                        }
                    }
                }
            }
            return i;
        }

        private Tuple<Sphere?, double> ClosestIntersection(Vec3d O, Vec3d D, double t_min, double t_max)
        {
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

            return Tuple.Create(closest_sphere, closest_t);
        }

        private Vec3d ReflectRay(Vec3d R, Vec3d N)
        {
            return N * Dot(N, R) * 2 - R;
        }
    }
}
