using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace RayCasting
{
    internal class Light
    {
        public Vector3 Position;

        public Color Color;

        public Light(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
