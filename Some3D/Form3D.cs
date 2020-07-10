using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        AutoResetEvent renderResetEvent = new AutoResetEvent(false);

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
            camera = new Camera(100, 5, (float)screen.Height / screen.Width, 90);

            camera.SetPosition(new Vector3f(0, 0, -2));

            if (false)
            {
                screen.Clear();
                renderer.Render(world, camera, screen);
                Refresh();
            }
            else
            {
                Application.Idle += OnApplicationIdle;
                renderResetEvent.Set();
            }
        }

        private async void OnApplicationIdle(object sender, EventArgs e)
        {
            while (NativeMethods.AppIsIdle())
            {
                await Task.Run(()=>renderResetEvent.WaitOne());
                screen.Clear();
                renderer.Render(world, camera, screen);
                Refresh();
                renderResetEvent.Set();
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
                camera.SetPosition(camera.Position.AddSelf(new Vector3f(0, 0, speed)));
            }
            else if (e.KeyCode == Keys.S)
            {
                camera.SetPosition(camera.Position.SubSelf(new Vector3f(0, 0, speed)));
            }
            else if (e.KeyCode == Keys.A)
            {
                camera.SetPosition(camera.Position.SubSelf(new Vector3f(speed, 0, 0)));
            }
            else if (e.KeyCode == Keys.D)
            {
                camera.SetPosition(camera.Position.AddSelf(new Vector3f(speed, 0, 0)));
            }
            else if (e.KeyCode == Keys.C)
            {
                camera.SetPosition(camera.Position.AddSelf(new Vector3f(0, speed, 0)));
            }
            else if (e.KeyCode == Keys.Space)
            {
                camera.SetPosition(camera.Position.SubSelf(new Vector3f(0, speed, 0)));
            }

            renderResetEvent.Set();
        }

        private void Form3D_Resize(object sender, EventArgs e)
        {
            screen.Dispose();
            screen = new DirectBitmap(ClientSize.Width, ClientSize.Height);
            camera.AspectRatio = (float)screen.Width / screen.Height;
            renderResetEvent.Set();
        }
    }
}