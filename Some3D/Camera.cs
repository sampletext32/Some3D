using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Camera : Object3D
    {
        public float ZFar { get; set; }
        public float ZNear { get; set; }
        public float AspectRatio { get; set; }
        public float FOV { get; set; }

        public MatrixF ProjectionMatrix
        {
            get
            {
                var matrixF = new MatrixF(4, 4);

                float inverseFOV = (float) (1 / Math.Tan(FOV / 2 * Math.PI / 180f));
                float clippingTrapezeRelation = ZFar / (ZFar - ZNear);

                matrixF[0, 0] = AspectRatio * inverseFOV;
                matrixF[1, 1] = inverseFOV;
                matrixF[2, 2] = clippingTrapezeRelation;
                matrixF[3, 2] = -clippingTrapezeRelation * ZNear;
                matrixF[2, 3] = 1;

                return matrixF;
            }
        }

        public Camera()
        {
        }

        public Camera(float zFar, float zNear, float aspectRatio, float fov)
        {
            ZFar = zFar;
            ZNear = zNear;
            AspectRatio = aspectRatio;
            FOV = fov;
        }
    }
}