using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracer
{
    public partial class Form1 : Form
    {
        int Cw, Ch;
        Scene scene = new Scene();
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp = (Bitmap)pictureBox1.Image;
            Cw = pictureBox1.Image.Width;
            Ch = pictureBox1.Image.Height;
            render();
        }

        ColorT background = new ColorT(135, 206, 250);

        private void InitScene() {
            Sphere s1 = new Sphere(new Vector3(0.0f, 0.0f, 3.0f), 1.0f, 
                new ColorT(255, 0, 0), 500, 0.2, 0.7);
            Sphere s2 = new Sphere(new Vector3(2.0f, 0.0f, 4.0f), 1.0f, 
                new ColorT(0, 0, 255), 500, 0.3);
            Sphere s3 = new Sphere(new Vector3(-2.0f, 0.0f, 4.0f), 1.0f, 
                new ColorT(0, 255, 0), 10, 0.4);
            Sphere s4 = new Sphere(new Vector3(0.0f, -5001.0f, 0.0f), 5000.0f, 
                new ColorT(255, 255, 0), 1000, 0.5);
            scene.Spheres.Add(s1);
            scene.Spheres.Add(s2);
            scene.Spheres.Add(s3);
            scene.Spheres.Add(s4);

            Light l1 = new Light("ambient", 0.2);
            Light l2 = new Light("point", 0.6, new Vector3(2.0f, 1.0f, 0.0f));

            scene.Lights.Add(l1);
            scene.Lights.Add(l2);
        }

        private void render() {
            ViewPort Port = new ViewPort(1, 1, 1, Cw, Ch);
            Camera camera = new Camera(new Vector3(0.0f, 0.0f, 0.0f));

            InitScene();

            //Отрисовка
            for(int x = -Cw/2; x < Cw/2; ++x)
                for (int y = -Ch/2 + 1; y < Ch/2; ++y) {
                    Vector3 D = Port.PictureToViewPort(x,y);
                    ColorT c = TraceRay(camera.Position, D, 1, float.MaxValue, 3);
                    PutPixel(x, y, c.Trunc());
                }
            pictureBox1.Image = bmp;
        }

        private Tuple<Sphere, float> ClosestIntersection(Vector3 O, Vector3 D, float t_min, float t_max) { 
            float closest_t = float.MaxValue;
            Sphere closest_sphere = null;
            foreach (var sphere in scene.Spheres){
                var temp = sphere.IntersectRaySphere(O, D);
                float t1 = temp.Item1, t2 = temp.Item2;
                if (t1 != float.NaN && t1 > t_min && t1 < t_max && t1 < closest_t){
                    closest_t = t1;
                    closest_sphere = sphere;
                }
                if (t2 != float.NaN && t2 > t_min && t2 < t_max && t2 < closest_t){
                    closest_t = t2;
                    closest_sphere = sphere;
                }
            }
            return Tuple.Create(closest_sphere, closest_t);
        }

        public ColorT TraceRay(Vector3 O, Vector3 D, float t_min, float t_max, int Depth) {

            var ResultIntersect = ClosestIntersection(O, D, t_min, t_max);

            if (ResultIntersect.Item1 == null)
                return background;

            Vector3 P = O + D * ResultIntersect.Item2;
            Vector3 N = P - ResultIntersect.Item1.Center;
            N = N / (float)N.Length();

            ColorT local_color = ResultIntersect.Item1.Color * ComputeLight(P, N, -D, ResultIntersect.Item1.Specular);

            double r = ResultIntersect.Item1.Reflective;

            if (Depth < 0 || r < 0) return local_color;

            Vector3 R = ReflectRay(-D, N);

            ColorT reflected_color = TraceRay(P, R, t_min, t_max, Depth - 1);

            ColorT refracted_color = TraceRay(P, D, 0.1f, t_max, 0);

            return local_color * (1 - r) + reflected_color * r + refracted_color * ResultIntersect.Item1.Refraction;
        }

        private double ComputeLight(Vector3 P, Vector3 N, Vector3 V, double s) {
            double i = 0;
            foreach(var l in scene.Lights){
                if (l.typeSource == "ambient")
                    i += l.intensity;
                else {
                    Vector3 L = l.position.Value - P;

                    var ResInter = ClosestIntersection(P, L, 0.001f, 1.0f);
                    if (ResInter.Item1 != null)
                        continue;

                    var temp = N.dot(L);
                    if (temp > 0)
                        i += l.intensity * temp / (N.Length() * L.Length());
                    if (s != -1) {
                        Vector3 R = ReflectRay(N, L);
                        var rtemp = R.dot(V);
                        if (rtemp > 0)
                            i += l.intensity * Math.Pow(rtemp / (R.Length() * V.Length()), s);
                    }
                }
            }
            return i;
        }

        public Vector3 ReflectRay(Vector3 R, Vector3 N){
            return N * (float)N.dot(R) * 2.0f - R;
        }

        private void PutPixel(double x, double y, Color c)
        {
            x = Cw / 2 + Math.Round(x);
            y = Ch / 2 - Math.Round(y);
            bmp.SetPixel((int)x, (int)y, c);
        }
    }
}
