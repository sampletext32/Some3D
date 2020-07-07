using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
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

                    for (int i = 0; i < 3; i++)
                    {
                        SomeDrawing.Line(screen, _tri[i], _tri[(i + 1) % 3], unchecked((int) 0xFF000000));
                    }
                }
            }
        }
    }
}