using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RayCasting
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        List<Sphere> spheres = new List<Sphere>();
        List<Light> lights = new List<Light>();

        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            for (int i = 0; i < 60; i++)
            {
                float x = 10 + rnd.Next(w - 10);
                float y = 10 + rnd.Next(h - 10);
                float z = 10 + rnd.Next(200);
                float r = 10 + rnd.Next(50); ;
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                spheres.Add(new Sphere(x, y, z, r, c));
            }

            lights.Add(new Light(new Vector3(10, 500, -100), Color.Yellow));
            lights.Add(new Light(new Vector3(500, 10, -30), Color.White));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    float zmin = float.PositiveInfinity;
                    foreach (Sphere sphere in spheres)
                    {
                        (bool isIntersect, float z) = sphere.Intersect(x, y);

                        if (isIntersect)
                        {
                            if (z < zmin)
                            {
                                Vector3 p = new Vector3(x, y, z);
                                Vector3 N = sphere.Normal(p);
                                Vector3 V = new Vector3(0, 0, -1);

                                Vector3 colorP = new Vector3(0, 0, 0);

                                foreach (Light light in lights)
                                {

                                    Vector3 L = Vector3.Normalize(light.Position - p);
                                    Vector3 R = Vector3.Reflect(L, N);

                                    float cosAlpha = Vector3.Dot(N, L);
                                    if (cosAlpha < 0)
                                        cosAlpha = 0;

                                    float cosFi = (float)(Math.Pow(Vector3.Dot(V, R), 30));
                                    if (cosAlpha < 0)
                                        cosAlpha = 0;



                                    colorP = Vector3.Add(colorP,
                                        new Vector3(
                                            (float)(sphere.Color.R * light.Color.R * (cosAlpha + cosFi) / 255.0 / 255.0),
                                            (float)(sphere.Color.G * light.Color.G * (cosAlpha + cosFi) / 255.0 / 255.0),
                                            (float)(sphere.Color.B * light.Color.B * (cosAlpha + cosFi) / 255.0 / 255.0)));
                                }

                                colorP = Vector3.Clamp(colorP, new Vector3(0, 0, 0), new Vector3(1, 1, 1));

                                bmp.SetPixel(x, y, Color.FromArgb((int)(255 * colorP.X), (int)(255 * colorP.Y), (int)(255 * colorP.Z)));
                                zmin = z;
                            }
                        }
                    }
                }
            pictureBox1.Refresh();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(bmp, 0, 0);
        }
    }
}
