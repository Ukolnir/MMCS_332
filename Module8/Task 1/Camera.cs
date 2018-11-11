using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class Camera
    {
       public Cam_vector position;
       public double phi, psi;
       public PointPol view;

        public Camera(double x, double y, double z, PointPol v) {
            position = new Cam_vector(x, y, z);
            view = v;
            Cam_vector X = new Cam_vector(1, 0, 0);

            double cos = Math.PI*(position.X * X.X + position.Y * X.Y + position.Z + X.Z) / (position.Len() * X.Len()*180);
            psi = Math.Acos(cos);

            Cam_vector Y = new Cam_vector(0, 1, 0);
            cos = Math.PI * (position.X * Y.X + position.Y * Y.Y + position.Z + Y.Z) / (position.Len() * Y.Len() * 180);
            phi = Math.Acos(cos);
        }
    }
}
