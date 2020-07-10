using System;
using Some3D.Utils;

namespace Some3D.Render
{
    public class Renderer
    {
        private Vector3f _cameraRay = new Vector3f();
        private Triangle _tri = new Triangle();
        private MatrixF modelMatrix = new MatrixF(4, 4);

        public void Render(World world, Camera camera, DirectBitmap screen)
        {
            Vector3f light = new Vector3f(0, 1, 0);
            light.MultiplySelf(1 / light.Length);

            // Матрица вида = матрице перемещения камеры
            var viewMatrix = camera.TranslationMatrix.Inverse();

            // матрица проекции
            var projectionMatrix = camera.ProjectionMatrix;

            foreach (var mesh in world.Meshes)
            {
                foreach (var triangle in mesh.Triangles)
                {
                    triangle.Duplicate(_tri);

                    var normal = SomeMaths.Cross(_tri[0], _tri[1], _tri[2]);
                    normal.MultiplySelf(1 / normal.Length); // normalize

                    _cameraRay = _tri[0] - camera.Position;

                    // normalization is normally before this check, just 
                    if (normal.Dot(_cameraRay) < 0)
                    {
                        // clip invisible triangles
                        continue;
                    }

                    // считаем матрицу модели

                    // !!! IMPORTANT http://opengl-tutorial.blogspot.com/p/3.html
                    // Сначала нужно изменить размер, потом прокрутить и лишь потом сдвинуть.
                    modelMatrix.MakeIdentity();
                    modelMatrix.MultiplySelf(mesh.ScaleMatrix);
                    modelMatrix.MultiplySelf(mesh.RotationMatrix);
                    modelMatrix.MultiplySelf(mesh.TranslationMatrix);



                    // проецируем точки треугольника по правилу MVP (model view project)
                    for (int i = 0; i < 3; i++)
                    {
                        _tri[i].MultiplySelf(modelMatrix);
                        _tri[i].MultiplySelf(viewMatrix);
                        _tri[i].MultiplySelf(projectionMatrix);
                    }

                    // Подгоняем точки под экран
                    for (int i = 0; i < 3; i++)
                    {
                        _tri[i].X += 1.0f;
                        _tri[i].Y += 1.0f;
                        _tri[i].X *= screen.Width / 2f;
                        _tri[i].Y *= screen.Height / 2f;
                    }

                    float lightAlignment = light.Dot(normal);

                    int luminance = (int)(Math.Max(0.3f, Math.Min(lightAlignment, 0.95f)) * 0xFF) & 0xFF;
                    int color = 0xFF << 24 | luminance << 16 | luminance << 8 | luminance;
                    // int color = 0xFF << 24 | random.Next(0, 255) << 16 | random.Next(0, 255) << 8 | random.Next(0, 255);
                    SomeDrawing.FillTriangle(screen, _tri[0], _tri[1], _tri[2], color);

                    for (int i = 0; i < 3; i++)
                    {
                        SomeDrawing.Line(screen, _tri[i], _tri[(i + 1) % 3], unchecked((int)0xFF000000));
                    }
                }
            }
        }
    }
}