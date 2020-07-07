using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Mesh : Object3D
    {
        public List<Triangle> Triangles { get; set; }

        public Mesh()
        {
            Triangles = new List<Triangle>();
        }
    }
}