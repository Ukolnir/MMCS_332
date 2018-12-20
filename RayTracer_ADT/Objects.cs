using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Objects
    {
    }

    public class Sphere{
        private const double Epsilon = 0.001;
        // Центр и радиус
        public Vector3 Center;
        public float Radius;
        public ColorT Color;
        public double Specular;
        public double Reflective;
        public double Refraction;

        public Sphere(Vector3 center, float radius, ColorT color, double spec, double refl, double refr = 0)
        {
            Center = center;
            Radius = radius;
            Color = color;
            //Зеркальность
            Specular = spec;
            //Отражение
            Reflective = refl;
            //Прозрачность
            Refraction = refr;
        }

        public Tuple<float, float> IntersectRaySphere(Vector3 O, Vector3 D) {
            float t1 = 0.0f, t2 = 0.0f;
            Vector3 OC = O - Center;
            var k1 = D * D;
            var k2 = OC * D * 2;
            var k3 = OC * OC - Radius * Radius;

            var dis = k2 * k2 - 4 * k1 * k3;
            if (dis < 0)
                t1 = t2 = float.NaN;
            else{
                t1 = (float)((-k2 + Math.Sqrt((double)dis)) / (2 * k1));
                t2 = (float)((-k2 - Math.Sqrt((double)dis)) / (2 * k1));
            }
            return Tuple.Create(t1, t2);
        }
    }

}
