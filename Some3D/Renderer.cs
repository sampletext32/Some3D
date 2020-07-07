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

                    SomeDrawing.Line(screen,
                        tri[0].X,
                        tri[0].Y,
                        tri[1].X,
                        tri[1].Y, unchecked((int) 0xFF000000));
                    SomeDrawing.Line(screen,
                        tri[1].X,
                        tri[1].Y,
                        tri[2].X,
                        tri[2].Y, unchecked((int) 0xFF000000));
                    SomeDrawing.Line(screen,
                        tri[2].X,
                        tri[2].Y,
                        tri[0].X,
                        tri[0].Y, unchecked((int) 0xFF000000));
                }
            }
        }
    }
}