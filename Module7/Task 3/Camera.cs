using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class Camera
    {
        public PointPol position;
        public double pitch, yaw, roll; //Тангаж, рысканье и крен
        //public PointPol view;

        public Camera(double x, double y, double z, double p, double ya, double r) {
            position = new PointPol(x, y, z);
            pitch = p * Math.PI / 180;
            yaw = ya * Math.PI / 180;
            roll = r * Math.PI / 180;
        }

        public double[,] translateAtPosition() {
            return new double[4, 4]{{1,0,0,-(position.X)},{0,1,0,-(position.Y)}, {0,0,1,-(position.Z)}, 
            {0,0,0,1}};     
        }

        public double[,] translateAtAngles() {
            return new double[3, 3] { 
                { Math.Cos(yaw) * Math.Cos(roll), -Math.Cos(yaw) * Math.Sin(roll), Math.Sin(yaw) },
                {Math.Sin(pitch)*Math.Sin(yaw)*Math.Cos(roll) + Math.Sin(roll)*Math.Cos(pitch), 
                                                            -Math.Sin(pitch)*Math.Sin(yaw)*Math.Sin(roll) + Math.Cos(roll)*Math.Cos(pitch),
                                                                        -Math.Sin(pitch)*Math.Cos(yaw)},
                {-Math.Cos(pitch)*Math.Sin(yaw)*Math.Cos(roll) + Math.Sin(pitch)*Math.Sin(roll), 
                    Math.Cos(pitch)*Math.Sin(yaw)*Math.Sin(roll) + Math.Sin(pitch)*Math.Cos(roll),
                                                                        Math.Cos(yaw)*Math.Cos(pitch)}
            };
        }
    }
}
