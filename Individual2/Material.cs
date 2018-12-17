using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Individual2
{
    class Material
    {
        //дифузное отражение
        public Color Diffuse;
        //на сколько блистящий, отражение, прозрачность
        public int Specular; public double Reflective, Refraction;

        public Material(Color diffuse, int specular = -1, double reflective = 0, double refraction = 0)
        {
            Diffuse = diffuse;
            Specular = specular;
            Reflective = reflective;
            Refraction = refraction;
        }

    }
}
