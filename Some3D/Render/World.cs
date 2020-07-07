using System.Collections.Generic;

namespace Some3D.Render
{
    public class World
    {
        public List<Mesh> Meshes { get; set; }

        public World()
        {
            Meshes = new List<Mesh>();
        }
    }
}