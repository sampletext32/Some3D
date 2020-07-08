using Some3D.Utils;

namespace Some3D.Render
{
    public class Object3D
    {
        public Vector3f Position { get; private set; }
        public Vector3f Scale { get; private set; }
        public Vector3f Rotation { get; private set; }

        public Object3D()
        {
            Position = 0;
            Scale = 0;
            Rotation = 0;
        }

        public Object3D(Vector3f position, Vector3f scale, Vector3f rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
    }
}