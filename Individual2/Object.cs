using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual2
{
    class Object
    {
        public Shape shape;
        public Material material;

        public Object(Shape s, Material m)
        {
            shape = s;
            material = m;
        }
    }

}
