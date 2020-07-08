using Some3D.Utils;

namespace Some3D.Render
{
    public class Renderer
    {
        private Triangle _tri = new Triangle();

        public void Render(World world, Camera camera, DirectBitmap screen)
        {
            var projectionMatrix = camera.ProjectionMatrix;
            foreach (var mesh in world.Meshes)
            {
                foreach (var triangle in mesh.Triangles)
                {
                    triangle.Duplicate(_tri);

                    for (int i = 0; i < 3; i++)
                    {
                        // position is directly relative to mesh position
                        // position is inverse relative to camera position

                        _tri[i].AddSelf(mesh.Position).SubSelf(camera.Position);

                        _tri[i].MultiplySelf(projectionMatrix);

                        _tri[i].X += 1.0f;
                        _tri[i].Y += 1.0f;

                        _tri[i].X *= screen.Width / 2f;
                        _tri[i].Y *= screen.Height / 2f;
                    }

                    SomeDrawing.FillTriangle(screen, _tri[0], _tri[1], _tri[2], unchecked((int)0xFF000000));
                    
                    // for (int i = 0; i < 3; i++)
                    // {
                    //     SomeDrawing.Line(screen, _tri[i], _tri[(i + 1) % 3], unchecked((int)0xFF000000));
                    // }
                }
            }
        }
    }
}