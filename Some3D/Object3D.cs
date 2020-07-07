using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Object3D
    {
        public Vector3f Position { get; set; }
        public Vector3f Scale { get; set; }

        public Object3D()
        {
            Position = 0;
            Scale = 0;
        }

        public Object3D(Vector3f position, Vector3f scale)
        {
            Position = position;
            Scale = scale;
        }
    }
}