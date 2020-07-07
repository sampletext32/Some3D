using System.Collections.Generic;

namespace Some3D.Render
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