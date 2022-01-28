using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace RayCasting
{
    internal class Sphere
    {
        public float Xc;
        public float Yc;    
        public float Zc;

        public float R;

        public Color Color;

        public Sphere(float xc, float yc, float zc, float r, Color color)
        {
            Xc = xc;
            Yc = yc;
            Zc = zc;
            R = r;
            Color = color;
        }

        public (bool, float) Intersect(float xr, float yr)
        {
            if ((Xc - xr) * (Xc - xr) + (Yc - yr) * (Yc - yr) > R * R)
                return (false, 0);

            double zm = Zc - Math.Sqrt(R*R - (Xc-xr)*(Xc-xr) - (Yc-yr)*(Yc-yr));

            return (true, (float)zm);
        }

        public Vector3 Normal(Vector3 p)
        {
            Vector3 pc = new Vector3(Xc,Yc,Zc);
            return Vector3.Normalize(p-pc);
        }

    }
}
