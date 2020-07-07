using System;
using Some3D.Utils;

namespace Some3D.Render
{
    public class Camera : Object3D
    {
        private float _zFar;
        private float _zNear;
        private float _aspectRatio;
        private float _fov;

        public float ZFar
        {
            get { return _zFar; }
            set
            {
                _zFar = ZFar;
                MakeProjectionMatrix();
            }
        }

        public float ZNear
        {
            get => _zNear;
            set
            {
                _zNear = value; 
                MakeProjectionMatrix();
            }
        }

        public float AspectRatio
        {
            get => _aspectRatio;
            set
            {
                _aspectRatio = value;
                MakeProjectionMatrix();
            }
        }

        public float FOV
        {
            get => _fov;
            set
            {
                _fov = value;
                MakeProjectionMatrix();
            }
        }

        public MatrixF ProjectionMatrix { get; }

        public Camera()
        {
        }

        public Camera(float zFar, float zNear, float aspectRatio, float fov)
        {
            _zFar = zFar;
            _zNear = zNear;
            _aspectRatio = aspectRatio;
            _fov = fov;
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