using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Triangle
    {
        public Vector3f[] Points { get; set; }

        public Vector3f this[int index]
        {
            get => Points[index];
            set => Points[index] = value;
        }

        public Triangle()
        {
            Points = new Vector3f[] {0, 0, 0};
        }

        public Triangle(Vector3f p1, Vector3f p2, Vector3f p3)
        {
            Points = new Vector3f[] {p1, p2, p3};
        }

        public void Duplicate(Triangle to)
        {
            Points[0].Duplicate(to[0]);
            Points[1].Duplicate(to[1]);
            Points[2].Duplicate(to[2]);
        }
    }
}