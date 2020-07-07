using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
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