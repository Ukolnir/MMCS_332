using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class ViewPort
    {
        public float Vw, Vh;
        public float Distance;
        public float Width, Height;

        public ViewPort(float vw, float vh, float d, float w, float h){
            Vw = vw;
            Vh = vh;
            Distance = d;
            Width = w;
            Height = h;
        }

        public Vector3 PictureToViewPort(int x, int y) {
            return new Vector3(x * Vw / Width, y * Vh / Height, Distance);
        }
    }
}
