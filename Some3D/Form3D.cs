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

namespace Some3D
{
    public partial class Form3D : Form
    {
        private DirectBitmap screen;

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
            screen = new DirectBitmap(ClientSize.Width, ClientSize.Height);
            SomeDrawing.FastLineAlgorithm(screen, 0, 0, screen.Width - 1, screen.Height - 1, unchecked((int) 0xFF000000));
            Application.Idle += OnApplicationIdle;
        }

        private void OnApplicationIdle(object sender, EventArgs e)
        {
            while (NativeMethods.AppIsIdle())
            {
                Refresh();
            }
        }

        private void Form3D_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(screen, 0, 0);
        }
    }
}