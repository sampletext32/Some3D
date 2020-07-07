using Some3D.Utils;

namespace Some3D.Render
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