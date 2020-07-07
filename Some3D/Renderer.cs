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
                        _tri[i].X -= camera.Position.X;
                        _tri[i].Y -= camera.Position.Y;
                        _tri[i].Z -= camera.Position.Z;

                        _tri[i] *= projectionMatrix;

                        _tri[i].X += 1.0f;
                        _tri[i].Y += 1.0f;

                        _tri[i].X *= screen.Width / 2f;
                        _tri[i].Y *= screen.Height / 2f;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        SomeDrawing.Line(screen,
                            _tri[i].X,
                            _tri[i].Y,
                            _tri[(i + 1) % 3].X,
                            _tri[(i + 1) % 3].Y, unchecked((int) 0xFF000000));
                    }
                }
            }
        }
    }
}