using System;
using System.Linq;

namespace Some3D.Utils
{
    public class MatrixF
    {
        private int _width;
        private int _height;

        private float[] _array;

        public float this[int i, int j]
        {
            get => _array[j + i * _width];
            set => _array[j + i * _width] = value;
        }

        public static MatrixF operator *(MatrixF m1, MatrixF m2)
        {
            MatrixF m = new MatrixF(m1._width, m1._height);

        public MatrixF MakeIdentity()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    this[i, j] = 0;
                }
            }

            for (int i = 0; i < _width; i++)
            {
                this[i, i] = 1;
            }

            return this;
        }
            for (int i = 0; i < m._width; i++)
            {
                for (int j = 0; j < m._height; j++)
                {
                    for (int k = 0; k < m._width; k++)
                    {
                        m[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

        public MatrixF ScaleSelf(float scaleX, float scaleY, float scaleZ)
        {
            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = scaleX;
            m[1, 1] = scaleY;
            m[2, 2] = scaleZ;
            m[3, 3] = 1;
            return this.MultiplySelf(m);
        }

        public MatrixF RotateXSelf(float angle)
        {
            var angleRad = angle / 180 * Math.PI;
            var cos = (float)Math.Cos(angleRad);
            var sin = (float)Math.Sin(angleRad);

            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = 1;
            m[1, 1] = cos;
            m[1, 2] = -sin;
            m[2, 1] = sin;
            m[2, 2] = cos;
            m[3, 3] = 1;
            return this.MultiplySelf(m);
        }

        public MatrixF RotateYSelf(float angle)
        {
            var angleRad = angle / 180 * Math.PI;
            var cos = (float)Math.Cos(angleRad);
            var sin = (float)Math.Sin(angleRad);

            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = cos;
            m[0, 2] = sin;
            m[1, 1] = 1;
            m[2, 0] = -sin;
            m[2, 2] = cos;
            m[3, 3] = 1;
            return this.MultiplySelf(m);
        }

        public MatrixF RotateZSelf(float angle)
        {
            var angleRad = angle / 180 * Math.PI;
            var cos = (float)Math.Cos(angleRad);
            var sin = (float)Math.Sin(angleRad);

            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = cos;
            m[0, 1] = -sin;
            m[1, 0] = sin;
            m[1, 1] = cos;
            m[2, 2] = 1;
            m[3, 3] = 1;
            return this.MultiplySelf(m);
        }

        public MatrixF TranslateToSelf(Vector3f vector)
        {
            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = 1;
            m[1, 1] = 1;
            m[2, 2] = 1;
            m[3, 3] = 1;
            m[0, 3] = vector.X;
            m[1, 3] = vector.Y;
            m[2, 3] = vector.Z;
            return this.MultiplySelf(m);
        }

        public MatrixF TranslateFromSelf(Vector3f vector)
        {
            MatrixF m = new MatrixF(4, 4);
            m[0, 0] = 1;
            m[1, 1] = 1;
            m[2, 2] = 1;
            m[3, 3] = 1;
            m[0, 3] = -vector.X;
            m[1, 3] = -vector.Y;
            m[2, 3] = -vector.Z;
            return this.MultiplySelf(m);
        }

            return m;
        }

        public MatrixF(int width, int height)
        {
            _width = width;
            _height = height;
            _array = new float[width * height];
        }
    }
}