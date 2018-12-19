using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracer
{
    public class ColorT
    {
        public double R, G, B;

        public ColorT(double r = 0, double g = 0, double b = 0) {
            R = r;
            G = g;
            B = b;
        }

        public static ColorT operator +(ColorT left, ColorT right)
        {
            var r = left.R + right.R;
            var g = left.G + right.G;
            var b = left.B + right.B;

            return new ColorT(r, g, b);
        }

        public static ColorT operator -(ColorT left, ColorT right)
        {
            var r = left.R - right.R;
            var g = left.G - right.G;
            var b = left.B - right.B;

            return new ColorT(r, g, b);
        }

        public static ColorT operator *(ColorT left, ColorT right)
        {
            var r = left.R * right.R;
            var g = left.G * right.G;
            var b = left.B * right.B;

            return new ColorT(r, g, b);
        }

        public static ColorT operator /(ColorT left, ColorT right)
        {
            var r = left.R / right.R;
            var g = left.G / right.G;
            var b = left.B / right.B;

            return new ColorT(r, g, b);
        }

        public static ColorT operator *(ColorT color, double val)
        {
            var r = color.R * val;
            var g = color.G * val;
            var b = color.B * val;

            return new ColorT(r, g, b);
        }

        public static ColorT operator /(ColorT color, double val)
        {
            var r = color.R / val;
            var g = color.G / val;
            var b = color.B / val;

            return new ColorT(r, g, b);
        }

        public Color Trunc() { // Стоит переходить на [1.0, 0.0]?
            int r = Convert.ToInt32(Math.Max(System.Math.Min(255, R), 0));
            int g = Convert.ToInt32(Math.Max(System.Math.Min(255, G), 0));
            int b = Convert.ToInt32(Math.Max(System.Math.Min(255, B), 0));
            return Color.FromArgb(r, g, b);
        } 
    }
}
