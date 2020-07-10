using Some3D.Utils;

namespace Some3D.Render
{
    public class Cube : Mesh
    {
        public Cube(float size)
        {
            float sizeHalf = size / 2f;
            // BACK
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, -sizeHalf, -sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, -sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, -sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, -sizeHalf, -sizeHalf)));

            // LEFT 
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(sizeHalf, -sizeHalf, sizeHalf)));

            // FRONT 
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, -sizeHalf, sizeHalf)));

            // RIGHT
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, -sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(-sizeHalf, -sizeHalf, -sizeHalf)));

            // TOP  
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(-sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(-sizeHalf, sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, sizeHalf),
                new Vector3f(sizeHalf, sizeHalf, -sizeHalf)));

            // BOTTOM
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, -sizeHalf, -sizeHalf)));
            Triangles.Add(new Triangle(
                new Vector3f(sizeHalf, -sizeHalf, sizeHalf),
                new Vector3f(-sizeHalf, -sizeHalf, -sizeHalf),
                new Vector3f(sizeHalf, -sizeHalf, -sizeHalf)));
        }
    }
}