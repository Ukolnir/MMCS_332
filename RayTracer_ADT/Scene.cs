using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Scene
    {
        public List<Sphere> Spheres = new List<Sphere>();
        public List<Light> Lights = new List<Light>();
    }
}
