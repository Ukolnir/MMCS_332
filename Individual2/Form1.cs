using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Individual2
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int Cw, Ch;     //Размер экрана
        double Ww, Wh;  //Окно просмотра
        double distance;       //Расстояние до камеры
        Scene scene;
        Camera camera;
        Color Back;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;

            Cw = pictureBox1.Image.Width;
            Ch = pictureBox1.Image.Height;
            Ww = Wh = distance = 1;

            createObjs();

        }

        //создаем объекты
        public void createObjs()
        {
            Ww = 2;
            Wh = 1;
            distance = 1;
            camera = new Camera(new Vec(0, 0, 0));
            camera.rotate(30);
            Back = Color.SkyBlue;

            List<Object> objs = new List<Object>();
            objs.Add(new Object(new Sphere(new Vec(-3, -1, 10), 2), new Material(Color.White)));
            objs.Add(new Object(new Sphere(new Vec(-3, 1.5, 10), 1.2), new Material(Color.White)));
            objs.Add(new Object(new Sphere(new Vec(-3, 3, 9.5), 0.8), new Material(Color.White)));
            objs.Add(new Object(new Sphere(new Vec(-4.4, 1.5, 10), 0.5), new Material(Color.White)));
            objs.Add(new Object(new Sphere(new Vec(-1.65, 1.5, 10), 0.5), new Material(Color.White)));

            objs.Add(new Object(new Sphere(new Vec(-3, 2.93, 8.7), 0.1), new Material(Color.DarkBlue, 100)));
            objs.Add(new Object(new Sphere(new Vec(-2.5, 2.93, 8.7), 0.1), new Material(Color.DarkBlue, 100)));
            objs.Add(new Object(new Sphere(new Vec(-2.75, 2.7, 8.7), 0.12), new Material(Color.Red)));

            objs.Add(new Object(new Sphere(new Vec(-3, 2.5, 8.7), 0.08), new Material(Color.Black)));
            objs.Add(new Object(new Sphere(new Vec(-2.5, 2.5, 8.7), 0.08), new Material(Color.Black)));
            objs.Add(new Object(new Sphere(new Vec(-2.75, 2.4, 8.7), 0.08), new Material(Color.Black)));

            objs.Add(new Object(new Sphere(new Vec(5, 0, 14), 1), new Material(Color.SpringGreen, 1000)));
            objs.Add(new Object(new Sphere(new Vec(5.5, 0, 11), 1), new Material(Color.MediumSlateBlue, 500)));
            objs.Add(new Object(new Sphere(new Vec(2, -0.5, 9), 0.8), new Material(Color.Violet, 1000, 0.001, 0.7)));
            objs.Add(new Object(new Sphere(new Vec(0, -5001, 0), 5000), new Material(Color.White, 1200, 0.65)));

            objs.Add(new Object(new Cube(new Vec(-50, 0, -50), 100), new Material(Color.Red)));
            objs.Add(new Object(new Cube(new Vec(3, 0, 2), 2), new Material(Color.Red)));

            List<Light> lights = new List<Light>();
            lights.Add(new Light("ambient", 0.2));
            lights.Add(new Light("point", 0.6, new Vec(2,1,0)));

            scene = new Scene(objs, lights);
        }

        

        //вычисляем отраженный луч
        public Vec ReflectRay(Vec R, Vec N)
        {
            return 2 * N * N.dot(R) - R;
        }

        public double ComputeLighting(Vec P, Vec N, Vec V, int s)
        {
            double i = 0.0;
            double t_max = 0;
            foreach (var light in scene.lights)
            {
                Vec L = new Vec(0, 0, 0);
                if (light.type == "ambient")
                {
                    i += light.intensive;
                }
                else
                {
                    if (light.type == "point")
                    {
                        L = light.position.Value - P;
                        t_max = 1;
                    }
                    else
                    {
                        L = light.direction.Value;
                        t_max = Double.MaxValue;
                    }

                    //проверка того, является ли данное место в тени
                    var p = ClosestIntersection(P, L, 0.001, t_max);
                    if (p.Item2 != null)
                        continue;

                    //диффузное освещение
                    var n_dot_l = N.dot(L);
                    if (n_dot_l > 0)
                        i += light.intensive * n_dot_l / (N.Length() * L.Length());
                    //зеркальное отражение
                    if (s != -1)
                    {
                        var R = 2.0 * N * N.dot(L) - L;
                        var r_dot_v = R.dot(V);
                        if (r_dot_v > 0)
                            i += light.intensive * Math.Pow(r_dot_v / (R.Length() * V.Length()), s);
                    }
                }
            }
            return i;
        }

        //ближайшее пересечение луча с каким-нибудь объектом
        private Tuple<double, Object> ClosestIntersection(Vec O, Vec D, double t_min, double t_max)
        {
            double closest_t = Double.MaxValue;
            Object closest_obj = null;


            foreach (var o in scene.objects)
            {
                var pair = o.shape.Intersect(O, D);

                if (pair.Item1 >= t_min && pair.Item1 <= t_max && pair.Item1 < closest_t)
                {
                    closest_t = pair.Item1;
                    closest_obj = o;
                }

                if (pair.Item2 >= t_min && pair.Item2 <= t_max && pair.Item2 < closest_t)
                {
                    closest_t = pair.Item2;
                    closest_obj = o;
                }
            }

            return Tuple.Create(closest_t, closest_obj);
        }

        public Color ColorMult(Color c, double k)
        {
            int r = Math.Max(Math.Min((int)Math.Round(c.R * k), 255), 0);
            int g = Math.Max(Math.Min((int)Math.Round(c.G * k), 255), 0);
            int b = Math.Max(Math.Min((int)Math.Round(c.B * k), 255), 0);
            return  Color.FromArgb(r, g, b);
        }

        public Color ColorSum(Color c1, Color c2)
        {
            int r = Math.Min((c1.R + c2.R), 255);
            int g = Math.Min((c1.G + c2.G), 255);
            int b = Math.Min((c1.B + c2.B), 255);
            return Color.FromArgb(r, g, b);
        }

         private Color TraceRay(Vec O, Vec D, double t_min, double t_max, int depth)
        {
            var closest = ClosestIntersection(O, D, t_min, t_max);

            if (closest.Item2 != null)
            {
                Vec P = O + D * closest.Item1; // Вычисление точки пересечения
                Vec N = P - closest.Item2.shape.getParams().Item1; // Вычисление нормали к сфере в точке пересечения
                N = N / N.Length();

                double k = ComputeLighting(P, N, -D, closest.Item2.material.Specular);
                Color return_c = ColorMult(closest.Item2.material.Diffuse, k);

                if (depth <= 0 || closest.Item2.material.Reflective == 0)
                    return return_c;

                var R = ReflectRay(-D, N);

                //отражение
                var refl_c = TraceRay(P, R, t_min, t_max, depth - 1);
                //прозрачность
                var refrec = TraceRay(P, D, 0.0001, Double.MaxValue, 1);
                return ColorSum(
                    ColorSum(ColorMult(return_c, 1 - closest.Item2.material.Reflective),
                    ColorMult(refl_c, closest.Item2.material.Reflective)),
                    ColorMult(refrec, closest.Item2.material.Refraction));
            }

            return Back;
        }

        //для проекции
        private Vec CanvasToViewport(double x, double y)
        {
            return new Vec(x * Ww / Cw, y * Wh / Ch, distance);
        }


        public void drawPicture()
        {
            camera.position = new Vec(Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            scene.lights.Add(new Light("point", 0.1, camera.position));

            for (int x = -Cw / 2; x < Cw / 2; ++x)
            {
                for (int y = -Ch / 2 + 1; y < Ch / 2; ++y)
                {

                    Vec D = camera.rotation * CanvasToViewport(x, y);
                    //Определяем цвет пикселя в нашем окне
                    Color color = TraceRay(camera.position, D, 1, Double.MaxValue, 5);
                    PutPixel(x, y, color);
                }
                pictureBox1.Image = bmp;
            }

            scene.lights.RemoveAt(scene.lights.Count() - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawPicture();
        }

        //закраска пикселя
        private void PutPixel(double x, double y, Color c)
        {
            x = Cw / 2 + Math.Round(x);
            y = Ch / 2 - Math.Round(y);
            bmp.SetPixel((int)x, (int)y, c);
        }
    } 




    //================================= Struct Vector ===============================================




    public struct Vec
    {
        public double x, y, z;
        public Vec(double X, double Y, double Z)
        {
            x = X; y = Y; z = Z;
        }

        public static Vec operator -(Vec v1, Vec v2)
        {
            return new Vec(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vec operator +(Vec v1, Vec v2)
        {
            return new Vec(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vec operator *(Vec v1, Vec v2)
        {
            return new Vec(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vec operator *(Vec v, double d)
        {
            return new Vec(v.x * d, v.y * d, v.z * d);
        }

        public static Vec operator *(double d, Vec v)
        {
            return new Vec(v.x * d, v.y * d, v.z * d);
        }

        public static Vec operator /(Vec v, double d)
        {
            return new Vec(v.x / d, v.y / d, v.z / d);
        }

        public static Vec operator -(Vec v)
        {
            return new Vec(-v.x, -v.y, -v.z);
        }

        public static Vec operator *(List<List<double>> rotation, Vec v)
        {
            double x = 0, y = 0, z = 0;

            for (int i = 0; i < 3; ++i)
            {
                x += rotation[0][i] * v.x;
                y += rotation[1][i] * v.y;
                z += rotation[2][i] * v.z;
            }

            return new Vec(x, y, z);
        }

        public double Length()
        {
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public double dot(Vec v)
        {
            return (x * v.x) + (y * v.y) + (z * v.z);
        }
    }
}
