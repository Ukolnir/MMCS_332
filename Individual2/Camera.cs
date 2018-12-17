using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual2
{
    class Camera
    {
        public Vec position;
        public List<List<double>> rotation;

        public Camera(Vec pos)
        {
            position = pos;
            rotation = new List<List<double>>();

            for (int i = 0; i < 3; ++i)
            {
                rotation.Add(new List<double>());
                for (int j = 0; j < 3; ++j)
                {
                    rotation[i].Add((i == j) ? 1.0 : 0.0);
                }
            }
        }

        public void rotate(double angle_y)
        {
            double a = angle_y * Math.PI / 360;

            rotation[0][0] = Math.Cos(a);
            rotation[0][1] = 0;
            rotation[0][2] = -Math.Sin(a);
            rotation[1][0] = 0;
            rotation[1][1] = 1;
            rotation[1][2] = 0;
            rotation[2][0] = Math.Sin(a);
            rotation[2][1] = 0;
            rotation[2][2] = Math.Cos(a);
        }
    }
}
