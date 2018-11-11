using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class Cam_vector
    {
        public double X, Y, Z;

        public Cam_vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Cam_vector Normalized()
        {
            return this / Len();
        }

        public double Len()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Cam_vector operator +(Cam_vector a, Cam_vector b)
        {
            double x = a.X + b.X;
            double y = a.Y + b.Y;
            double z = a.Z + b.Z;
            return new Cam_vector(x, y, z);
        }

        public static Cam_vector operator -(Cam_vector a, Cam_vector b)
        {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            double z = a.Z - b.Z;
            return new Cam_vector(x, y, z);
        }

        public static Cam_vector operator -(Cam_vector vec)
        {
            double x, y, z;
            x = -vec.X;
            y = -vec.Y;
            z = -vec.Z;
            return new Cam_vector(x, y, z);
        }

        public static Cam_vector operator *(Cam_vector v, double mul)
        {
            return new Cam_vector(v.X * mul, v.Y * mul, v.Z * mul);
        }

        public static Cam_vector operator /(Cam_vector v, double mul)
        {
            return new Cam_vector(v.X / mul, v.Y / mul, v.Z / mul);
        }

        public static double operator *(Cam_vector a, Cam_vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Cam_vector operator ^(Cam_vector a, Cam_vector b)
        {
            double x, y, z;

            x = a.Y * b.Z - a.Z * b.Y;
            y = a.X * b.Z - a.Z * b.X;
            z = a.X * b.Y - a.Y * b.X;

            return new Cam_vector(x, y, z);
        }
    }
}
