using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Some3D.Render;
using Some3D.Utils;

namespace Some3D
{
    public partial class Form3D : Form
    {
        private DirectBitmap screen;

        private Renderer renderer;

        private World world;

        private Camera camera;

        private Mesh mesh;

        public Form3D()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            UpdateStyles();
        }

        private void Form3D_Load(object sender, EventArgs e)
        {
            renderer = new Renderer();
            world = new World();

            mesh = new Cube(5);
            // mesh = new Mesh();
            // mesh.Triangles.Add(new Triangle(new Vector3f(0, 0, 0), new Vector3f(0, -1, 0), new Vector3f(1, 0, 0)));
            // mesh.Triangles.Add(new Triangle(new Vector3f(0, 0, 0), new Vector3f(0, -1, 0), new Vector3f(-1, 0, 0)));
            // mesh.Triangles.Add(new Triangle(new Vector3f(0, 0, 0), new Vector3f(0, -1, -1), new Vector3f(0, -1, -1)));
            world.Meshes.Add(mesh);

            screen = new DirectBitmap(ClientSize.Width, ClientSize.Height);
            camera = new Camera(10000, 0, (float)screen.Height / screen.Width, 90);

            camera.Position = new Vector3f(0, 0, -2);

            Application.Idle += OnApplicationIdle;
        }

        private void OnApplicationIdle(object sender, EventArgs e)
        {
            while (NativeMethods.AppIsIdle())
            {
                screen.Clear();
                renderer.Render(world, camera, screen);
                Refresh();
            }
        }

        private void Form3D_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(screen, 0, 0);
        }

        private void Form3D_KeyDown(object sender, KeyEventArgs e)
        {
            float speed = 0.05f;
            if (e.Control)
            {
                speed /= 10f;
            }

            if (e.KeyCode == Keys.W)
            {
                camera.Position.Z += speed;
            }
            else if (e.KeyCode == Keys.S)
            {
                camera.Position.Z -= speed;
            }
            else if (e.KeyCode == Keys.A)
            {
                camera.Position.X -= speed;
            }
            else if (e.KeyCode == Keys.D)
            {
                camera.Position.X += speed;
            }
            else if (e.KeyCode == Keys.C)
            {
                camera.Position.Y += speed;
            }
            else if (e.KeyCode == Keys.Space)
            {
                camera.Position.Y -= speed;
            }
        }

        private void Form3D_Resize(object sender, EventArgs e)
        {
            screen.Dispose();
            screen = new DirectBitmap(ClientSize.Width, ClientSize.Height);
            camera.AspectRatio = (float)screen.Width / screen.Height;
        }
    }
}