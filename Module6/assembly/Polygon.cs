using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class Polygon
    {
        public List<int> vertices;
        public List<Edge> edges;

        public Polygon(List<int> inp) {
            vertices = inp;
            edges = new List<Edge>();
            for (int i = 0; i < inp.Count; ++i)
                edges.Add(new Edge(inp[i], inp[(i + 1) % inp.Count]));
        }

        public string checkin()
        {
            string res = " Ребра: ";
            foreach (var i in edges)
                res += "(" + i.E1 + ";" + i.E2 + ")   "; 
            return res;
        }
    }

    public class Edge {
        public int E1, E2;
        public Edge(int e1, int e2) {
            E1 = e1; E2 = e2;
        }    
    }

}
