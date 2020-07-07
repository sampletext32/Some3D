using System;
using Some3D.Utils;

namespace Some3D.Render
{
    public class Camera : Object3D
    {
        public float ZFar { get; set; }
        public float ZNear { get; set; }
        public float AspectRatio { get; set; }
        public float FOV { get; set; }

        public MatrixF ProjectionMatrix { get; private set; }

        public Camera()
        {
        }

        public Camera(float zFar, float zNear, float aspectRatio, float fov)
        {
            ZFar = zFar;
            ZNear = zNear;
            AspectRatio = aspectRatio;
            FOV = fov;

            ProjectionMatrix = new MatrixF(4, 4);
            MakeProjectionMatrix();
        }

        private void MakeProjectionMatrix()
        {
            float inverseFOV = (float) (1 / Math.Tan(FOV / 2 * Math.PI / 180f));
            float clippingTrapezeRelation = ZFar / (ZFar - ZNear);

            ProjectionMatrix[0, 0] = AspectRatio * inverseFOV;
            ProjectionMatrix[1, 1] = inverseFOV;
            ProjectionMatrix[2, 2] = clippingTrapezeRelation;
            ProjectionMatrix[3, 2] = -clippingTrapezeRelation * ZNear;
            ProjectionMatrix[2, 3] = 1;
        }
    }
}