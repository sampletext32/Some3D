using Some3D.Utils;

namespace Some3D.Render
{
    public class Cube : Mesh
    {
        public Cube(float size)
        {
            // SOUTH
            Triangles.Add(new Triangle(new Vector3f(0, 0, 0), new Vector3f(0, size, 0), new Vector3f(size, size, 0)));
            Triangles.Add(new Triangle(new Vector3f(0, 0, 0), new Vector3f(size, size, 0), new Vector3f(size, 0, 0)));

            // EAST 
            Triangles.Add(new Triangle(new Vector3f(size, 0, 0), new Vector3f(size, size, 0), new Vector3f(size, size, size)));
            Triangles.Add(new Triangle(new Vector3f(size, 0, 0), new Vector3f(size, size, size), new Vector3f(size, 0, size)));

            // NORTH 
            Triangles.Add(new Triangle(new Vector3f(size, 0, size), new Vector3f(size, size, size), new Vector3f(0, size, size)));
            Triangles.Add(new Triangle(new Vector3f(size, 0, size), new Vector3f(0, size, size), new Vector3f(0, 0, size)));

            // WEST
            Triangles.Add(new Triangle(new Vector3f(0, 0, size), new Vector3f(0, size, size), new Vector3f(0, size, 0)));
            Triangles.Add(new Triangle(new Vector3f(0, 0, size), new Vector3f(0, size, 0), new Vector3f(0, 0, 0)));

            // TOP  
            Triangles.Add(new Triangle(new Vector3f(0, size, 0), new Vector3f(0, size, size), new Vector3f(size, size, size)));
            Triangles.Add(new Triangle(new Vector3f(0, size, 0), new Vector3f(size, size, size), new Vector3f(size, size, 0)));

            // BOTTOM
            Triangles.Add(new Triangle(new Vector3f(size, 0, size), new Vector3f(0, 0, size), new Vector3f(0, 0, 0)));
            Triangles.Add(new Triangle(new Vector3f(size, 0, size), new Vector3f(0, 0, 0), new Vector3f(size, 0, 0)));
        }
    }
}