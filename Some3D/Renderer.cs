using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Renderer
    {
        public void Render(World world, Camera camera, DirectBitmap screen)
        {
            var projectionMatrix = camera.ProjectionMatrix;
            foreach (var mesh in world.Meshes)
            {
                foreach (var triangle in mesh.Triangles)
                {
                    Triangle tri = triangle.Duplicate();

                    for (int i = 0; i < 3; i++)
                    {
                        tri[i].X -= camera.Position.X;
                        tri[i].Y -= camera.Position.Y;
                        tri[i].Z -= camera.Position.Z;

                        tri[i] *= projectionMatrix;

                        tri[i].X += 1.0f;
                        tri[i].Y += 1.0f;

                        tri[i].X *= screen.Width / 2f;
                        tri[i].Y *= screen.Height / 2f;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        SomeDrawing.Line(screen,
                            tri[i].X,
                            tri[i].Y,
                            tri[(i + 1) % 3].X,
                            tri[(i + 1) % 3].Y, unchecked((int) 0xFF000000));
                    }
                }
            }
        }
    }
}