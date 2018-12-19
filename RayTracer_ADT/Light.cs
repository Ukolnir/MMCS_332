using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Light{

        public string typeSource;
        public double intensity;
        public Vector3? position, direction;

        public Light(string t, double intens, Vector3? pos = null, Vector3? dir = null)
        {
            typeSource = t;
            intensity = intens;
            position = pos;
            direction = dir;
        }
    }
}
