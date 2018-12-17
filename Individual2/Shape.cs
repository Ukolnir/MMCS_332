using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual2
{

    public class Shape
    {
        string Type;
        public Shape(string name) { Type = name; }
        public virtual Tuple<double, double> Intersect(Vec o, Vec d) { return Tuple.Create(0.0, 0.0); }
        public virtual Tuple<Vec, double> getParams() { return Tuple.Create(new Vec(0,0,0), 0.0); }
    }

    public class Sphere : Shape
    {
        public Vec c;
        public double r;
        public Sphere(Vec center, double radius) : base("Sphere")
        {
            c = center;
            r = radius;
        }

        public override Tuple<double, double> Intersect(Vec o, Vec d)
        {
            Vec oc = o - c;
            double k1 = d.dot(d);
            double k2 = 2* oc.dot(d);
            double k3 = oc.dot(oc) - Math.Pow(r, 2);

            var discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0)
                return Tuple.Create(Double.MaxValue, Double.MaxValue);

            var t1 = (-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            var t2 = (-k2 - Math.Sqrt(discriminant)) / (2 * k1);
            return Tuple.Create(t1, t2);
        }

        public override Tuple<Vec, double> getParams()
        {
            return Tuple.Create(c, r);
        }
    }

    public class Cube : Shape
    {
        public Vec position;
        public int dist;
        private List<List<Vec>> pols = new List<List<Vec>>();

        public void CreatePols()
        {
            List<Vec> points = new List<Vec>();

            points.Add(position);
            points.Add(new Vec(position.x, position.y + dist, position.z));
            points.Add(new Vec(position.x + dist, position.y, position.z));
            points.Add(new Vec(position.x + dist, position.y + dist, position.z));
        }
    
        public Cube(Vec pos, int d) : base("Cube")
        {
            position = pos;
            dist = d;
            CreatePols();
        }

        public double IntersectPol(Vec o, Vec d, List<Vec> p)
        {
            double intersect = -1;

            p = p.OrderByDescending(e => e.x).ToList();
            Vec edge1 = p[0] - p[2];
            Vec edge2 = p[1] - p[2];
            Vec h = d * edge2;
            double a = edge1.dot(h);

            if (a > -0.001 && a < 0.001)
                return intersect;       //луч параллелен полигону

            double f = 1.0 / a;
            var s = o - p[2];
            double u = f * s.dot(h);

            if (u < 0 || u > 1)
                return intersect;

            var q = s * edge1;
            var v = f * d.dot(q);

            if (v < 0 || u + v > 1)
                return intersect;
            
            var t = f * edge2.dot(q);
            if (t > 0.001)
            {
                intersect = t;
            }

            return intersect;
        }

        public override Tuple<double, double> Intersect(Vec o, Vec d)
        {
            double t;
            double res = -1;
            int c = 0;
        
            foreach(var l in pols)
            {
                t = IntersectPol(o, d, l);
                if (t != -1 && (res == 0 || t < res))
                {
                    res = t;
                }
            }

            if (res != -1)
               return Tuple.Create(0.0, res);

            return Tuple.Create(Double.MaxValue, Double.MaxValue);
        }
    }

}
