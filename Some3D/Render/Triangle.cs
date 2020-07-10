using Some3D.Utils;

namespace Some3D.Render
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

        public override string ToString()
        {
            return $"{nameof(Points)}: {string.Join<Vector3f>(",", Points)}";
        }

        public void Duplicate(Triangle to)
        {
            Points[0].Duplicate(to[0]);
            Points[1].Duplicate(to[1]);
            Points[2].Duplicate(to[2]);
        }
    }
}