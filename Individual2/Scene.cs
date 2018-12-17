using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Individual2
{
    class Scene
    {
        public List<Object> objects;
        public List<Light> lights;

        public Scene(List<Object> objs, List<Light> l)
        {
            objects = objs;
            lights = l;
        }
    }
}
