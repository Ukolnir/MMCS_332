using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Individual2
{
    public class Light
    {
        public string type;
        public double intensive;
        public Vec? position, direction;

        public Light(string t, double intens, Vec? pos = null, Vec? dir = null)
        {
            type = t;
            intensive = intens;
            position = pos;
            direction = dir;
        }
    }
}
