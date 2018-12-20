using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            var z = a.Z + b.Z;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            var z = a.Z - b.Z;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator -(Vector3 vec)
        {
            var x = -vec.X;
            var y = -vec.Y;
            var z = -vec.Z;
            return new Vector3(x, y, z);
        }

        public static Vector3 operator *(Vector3 v, float mul)
        {
            return new Vector3(v.X * mul, v.Y * mul, v.Z * mul);
        }

        public static Vector3 operator /(Vector3 v, float mul)
        {
            return new Vector3(v.X / mul, v.Y / mul, v.Z / mul);
        }

        public static double operator *(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double dot(Vector3 v)
        {
            return (X * v.X) + (Y * v.Y) + (Z * v.Z);
        }

        public static Vector3 operator *(List<List<double>> rotation, Vector3 v)
        {
            float x = 0, y = 0, z = 0;

            for (int i = 0; i < 3; ++i){
                x += (float)rotation[0][i] * v.X;
                y += (float)rotation[1][i] * v.Y;
                z += (float)rotation[2][i] * v.Z;
            }

            return new Vector3(x, y, z);
        }
    }
}
