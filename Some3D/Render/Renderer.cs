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

                    bool preciseCameraRay = false;
                    if (preciseCameraRay)
                    {
                        var triWorldCenterX = (_tri[0].X + _tri[1].X + _tri[2].X) / 3f;
                        var triWorldCenterY = (_tri[0].Y + _tri[1].Y + _tri[2].Y) / 3f;
                        var triWorldCenterZ = (_tri[0].Z + _tri[1].Z + _tri[2].Z) / 3f;

                        _cameraRay.X = camera.Position.X - triWorldCenterX;
                        _cameraRay.Y = camera.Position.Y - triWorldCenterY;
                        _cameraRay.Z = camera.Position.Z - triWorldCenterZ;
                    }
                    else
                    {
                        _cameraRay.X = camera.Position.X - _tri[0].X;
                        _cameraRay.Y = camera.Position.Y - _tri[0].Y;
                        _cameraRay.Z = camera.Position.Z - _tri[0].Z;
                    }

                    // CounterClockWise Model
                    // https://stackoverflow.com/questions/22171776/order-of-vector-cross-product-for-a-ccw-triangle

                    var normal = SomeMaths.Cross(_tri[0], _tri[1], _tri[2]);
                    normal.MultiplySelf(1 / normal.Length); // normalize


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
                    // Сдвигаем точки в центр экрана
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
                    // SOLID
                    SomeDrawing.FillTriangle(screen, _tri[0], _tri[1], _tri[2], color);

                    // WIREFRAME
                    for (int i = 0; i < 3; i++)
                    {
                        SomeDrawing.Line(screen, _tri[i], _tri[(i + 1) % 3], unchecked((int)0xFF000000));
                    }
                }
            }
        }
    }
}