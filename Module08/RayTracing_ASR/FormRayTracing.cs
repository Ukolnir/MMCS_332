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
    public partial class FormRayTracing : Form
    {
        public FormRayTracing()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;

            Cw = pictureBox1.Image.Width;
            Ch = pictureBox1.Image.Height;
            Vw = Vh = d = 1;
        }

        Bitmap bmp;
        int Cw, Ch;     //Размер экрана
        double Vw, Vh;  //Окно просмотра
        double d;       //Расстояние до камеры
        double inf = Double.MaxValue;
        Scene scene;
        int recursion_depth = 5;
        Camera camera;

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

            public static Vec3d operator *(List<List<double>> rotation, Vec3d v)
            {
                double x = 0, y = 0, z = 0;

                for (int i = 0; i < 3; ++i)
                {
                    x += rotation[0][i] * v.x;
                    y += rotation[1][i] * v.y;
                    z += rotation[2][i] * v.z;
                }

                return new Vec3d(x, y, z);
            }

            public double Length()
            {
                return Math.Sqrt(x*x + y*y+ z*z);
            }
        }

        private struct Camera
        {
            public Vec3d position;
            public List<List<double>> rotation;

            public Camera(Vec3d Position)
            {
                position = Position;
                rotation = new List<List<double>>();

                for (int i = 0; i < 3; ++i)
                {
                    rotation.Add(new List<double>());
                    for (int j = 0; j < 3; ++j)
                    {
                        rotation[i].Add((i == j) ? 1.0 : 0.0);
                    }
                }
            }

            public void rotate(double ay)
            {
                double a = ay * Math.PI / 360;

                rotation[0][0] = Math.Cos(a);
                rotation[0][1] = 0;
                rotation[0][2] = -Math.Sin(a);
                rotation[1][0] = 0;
                rotation[1][1] = 1;
                rotation[1][2] = 0;
                rotation[2][0] = Math.Sin(a);
                rotation[2][1] = 0;
                rotation[2][2] = Math.Cos(a);
            }
        }

        private struct Sphere
        {
            public Vec3d center;
            public double radius;
            public Color col;
            public int specular;        // насколько блестящий
            public double reflective;   // отражение
            public double refraction;   // прозрачность

            public Sphere(Vec3d Center, double Radius, 
                Color Col, int Specular, double Reflective, 
                double Refraction)
            {
                center = Center; radius = Radius; col = Col;
                specular = Specular; reflective = Reflective;
                refraction = Refraction;
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
            Init();

            Vec3d O = camera.position;
            for (int x = -Cw/2; x < Cw/2; ++x)
            {
                for (int y = -Ch/2+1; y < Ch/2; ++y)
                {
                    //Определить квадрат сетки, соответствующий этому пикселю
                    Vec3d D = camera.rotation * CanvasToViewport(x, y);
                    //Определить цвет, видимый сквозь этот квадрат
                    Color color = TraceRay(O, D, 1, inf, recursion_depth);
                    //Закрасить пиксель этим цветом
                    PutPixel(x, y, color);
                }
                if (x % 9 == 0)
					UpdateProgress((double)(x + Cw/2) / Cw);
            }


            pictureBox1.Image = bmp;
        }

        private Vec3d CanvasToViewport(double x, double y)
        {
            return new Vec3d(x * Vw / Cw, y * Vh / Ch, d);
        }

        private Color TraceRay(Vec3d O, Vec3d D, double t_min, double t_max, int depth)
        {
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

                //Прозрачность
                double refr = closest_sphere.Value.refraction;
                //Color refrected_color = TraceRay(P, D, 0.001, inf, 1);
                //local_color = ColorPlusColor(local_color, ColorDotDouble(refrected_color, refr));
                
                //Если мы достигли предела рекурсии или объект не отражающий, то мы закончили
                double refl = closest_sphere.Value.reflective;
                if ((depth <= 0) || (refl <= 0))
                {
                    return local_color;
                }

                //Вычисление отражённого цвета
                Vec3d ReflRay = ReflectRay(-D, N);
                Color reflected_color = TraceRay(P, ReflRay, 0.001, inf, depth - 1);

                //Вычисление цвета от прозрачности
                Color refrected_color = TraceRay(P, D, 0.001, inf, 1);

                return ColorPlusColor(
                    ColorPlusColor(
                        ColorDotDouble(local_color, 1 - refl),
                        ColorDotDouble(reflected_color, refl)
                        ),
                    ColorDotDouble(refrected_color, refr)
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

        private void Init()
        {
            Vw = 2;
            Vh = 1;
            d = 1;
            camera = new Camera(new Vec3d(0, 3, -5));
            camera.rotate(0);

            List<Sphere> spheres = new List<Sphere>();
            spheres.Add(new Sphere(new Vec3d(-4, 0, 5), 1, Color.Aqua, 500, 0.001, 0.5));
			spheres.Add(new Sphere(new Vec3d(-2, 0, 6), 1, Color.Green, 10, 0.3, 0));

			//body
			spheres.Add(new Sphere(new Vec3d(3, 1, 7), 2, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(3, 4.5, 7), 1.5, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(3, 7, 7), 1, Color.LightBlue, 500, 1, 0));

			//hand 1
			spheres.Add(new Sphere(new Vec3d(4.5, 4, 6), 0.8, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(5, 3.5, 5), 0.5, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(5.2, 3.2, 4), 0.3, Color.LightBlue, 500, 1, 0));

			//hand 2
			//spheres.Add(new Sphere(new Vec3d(3, 4.5, 7), 1.5, Color.DarkBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(1, 4, 6.5), 0.8, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(0, 3.5, 5.9), 0.5, Color.LightBlue, 500, 1, 0));
			spheres.Add(new Sphere(new Vec3d(-0.6, 3.2, 5.5), 0.3, Color.LightBlue, 500, 1, 0));

			
            //spheres.Add(new Sphere(new Vec3d(0, -0.5, 3), 0.5, Color.Red));
            //spheres.Add(new Sphere(new Vec3d(1.5, -0.5, 4), 0.5, Color.Blue));
            //spheres.Add(new Sphere(new Vec3d(-2, -0.5, 10), 0.5, Color.Green));
            spheres.Add(new Sphere(new Vec3d(0, -5001, 0), 5000, Color.Blue, 1000, 0.2, 0));
            spheres.Add(new Sphere(new Vec3d(0, 0, 0), 15, Color.GreenYellow, 10, 0.7, 0));


            List<Light> lights = new List<Light>();
            lights.Add(new Light(LightType.Ambient, 0.3));
            lights.Add(new Light(LightType.Point, 0.7, 
				new Vec3d(camera.position.x+5, camera.position.y-3, camera.position.z)));
            //lights.Add(new Light(LightType.Directional, 0.2, new Vec3d(1, 4, 4)));

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

        private void UpdateProgress(double progress)
        {
            if (progress <= 1)
            {
                int p = (int)Math.Round(progress * 100);

                progressBar1.Value = p;
                labelProgress.Text = p.ToString() + "%";

                progressBar1.Invalidate();
                progressBar1.Update();

                pictureBox1.Invalidate();
                pictureBox1.Update();

                Invalidate();
                Update();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Render();
        }

            
    }
}
