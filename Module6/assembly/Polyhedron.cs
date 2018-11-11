using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Task_3
{
    public class Polyhedron
    {
        public Dictionary<int, PointPol> vertices; //Список вершин многогранника
        public List<Polygon> polygons;
        public Polyhedron() {
            vertices = new Dictionary<int, PointPol>();
            polygons = new List<Polygon>();
        }
        
        public void AddPolygon(string line) {
            var prs = line.Split(' ').Select(x => x.Split(';').Select(z => Convert.ToDouble(z)).ToArray());
            List<int> numbers = new List<int>();
            foreach (var y in prs) {
                PointPol t = new PointPol(y[0], y[1], y[2]);
                if (!vertices.Values.Any(f=>f.X == y[0] && f.Y == y[1] && f.Z == y[2]))
                    vertices.Add(vertices.Count, t);
                numbers.Add(find_index(t));
            }
            polygons.Add(new Polygon(numbers));
        }

        private int find_index(PointPol p) {
            int i = vertices.Keys.First(x => vertices[x].Equal(p));    
            return i;
        }

        public List<Tuple<Point, Point>> Display() {
            List<Tuple<Point, Point>> result = new List<Tuple<Point, Point>>();
            Dictionary<int, Point> select = new Dictionary<int, Point>();
            foreach (var i in vertices)
                select.Add(i.Key, i.Value.To2D(""));

            foreach(var i in polygons)
                foreach (var j in i.edges) {
                    result.Add(Tuple.Create(select[j.E1], select[j.E2]));
                }
            return result;
        }

        public string checkin() {
            string res = "";
            foreach (var i in vertices)
                res += i.Key + ": (" + i.Value.X + "; " + i.Value.Y + "; " + i.Value.Z + ")  ";
            res += Environment.NewLine;
            foreach (var i in polygons)
                res += i.checkin() + "              ";
            return res;
        }

        //------------------affine transformation

        public void find_center(ref double x, ref double y, ref double z)
        {
            x = 0; y = 0; z = 0;
            foreach (var p in vertices.Values)
            {
                x += p.X;
                y += p.Y;
                z += p.Z;
            }
            x /= vertices.Count();
            y /= vertices.Count();
            z /= vertices.Count();
        }

        public void scale(double ind_scale)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            find_center(ref a, ref b, ref c);
            Dictionary<int, PointPol> temp = new Dictionary<int, PointPol>();
            foreach (var i in vertices)
                temp.Add(i.Key, i.Value.scale(ind_scale, a, b, c));
            vertices = temp;
        }

        public void shift(double x, double y, double z)
        {
            Dictionary<int, PointPol> temp = new Dictionary<int, PointPol>();
            foreach (var i in vertices)
                temp.Add(i.Key, i.Value.shift(x,y,z));
            vertices = temp;
        }

        public void rotate(Tuple<PointPol, PointPol> direction, double phi)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            find_center(ref a, ref b, ref c);
            Dictionary<int, PointPol> temp = new Dictionary<int, PointPol>();
            foreach (var i in vertices)
                temp.Add(i.Key, i.Value.rotate(direction, phi, a, b, c));
            vertices = temp;
        }

        public void reflection(string axis)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            find_center(ref a, ref b, ref c);

            if (axis == "X"){
                rotate(Tuple.Create(new PointPol(0, 0, 0), new PointPol(1, 0, 0)), 180);
                shift(-a * 2, 0, 0);
            }
            if (axis == "Y")
            {
                rotate(Tuple.Create(new PointPol(0, 0, 0), new PointPol(0, 1, 0)), 180);
                shift(0, -b * 2, 0);
            }
            if (axis == "Z")
            {
                rotate(Tuple.Create(new PointPol(0, 0, 0), new PointPol(0, 0, 1)), 180);
                shift(0, 0, -c * 2);
            }
        }
    }
}
