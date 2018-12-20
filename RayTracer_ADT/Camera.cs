using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Camera
    {
        public Vector3 Position;
        public List<List<double>> Rotation;
        public Camera(Vector3 pos){
            Position = pos;
            Rotation = new List<List<double>>();

            for (int i = 0; i < 3; ++i){
                Rotation.Add(new List<double>());
                for (int j = 0; j < 3; ++j)
                    Rotation[i].Add((i == j) ? 1.0f : 0.0f);
            }
        }

        public void rotate(double ay)
        {
            double a = ay * Math.PI / 360;

            Rotation[0][0] = Math.Cos(a);
            Rotation[0][1] = 0;
            Rotation[0][2] = Math.Sin(a);
            Rotation[1][0] = 0;
            Rotation[1][1] = 1;
            Rotation[1][2] = 0;
            Rotation[2][0] = -Math.Sin(a);
            Rotation[2][1] = 0;
            Rotation[2][2] = Math.Cos(a);
        }
    }

}
