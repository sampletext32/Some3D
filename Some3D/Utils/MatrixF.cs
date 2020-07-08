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

        public void Duplicate(MatrixF to)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                to._array[i] = _array[i];
            }
        }

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

        public MatrixF MultiplySelf(MatrixF m)
        {
            float[] vals = new float[_width * _height];
            for (int i = 0; i < m._width; i++)
            {
                for (int j = 0; j < m._height; j++)
                {
                    for (int k = 0; k < m._width; k++)
                    {
                        vals[j + i * _width] += _array[k + i * _width] * m._array[j + k * _width];
                    }
                }
            }

            for (int i = 0; i < _array.Length; i++)
            {
                this._array[i] = vals[i];
            }

            return this;
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

        public MatrixF InverseSelf()
        {
            float[] vals = new float[_width * _height];
            vals[0] = this[0, 0];
            vals[1] = this[1, 0];
            vals[2] = this[2, 0];
            vals[3] = 0f;
            vals[4] = this[0, 1];
            vals[5] = this[1, 1];
            vals[6] = this[2, 1];
            vals[7] = 0f;
            vals[8] = this[0, 2];
            vals[9] = this[1, 2];
            vals[10] = this[2, 2];
            vals[11] = 0f;
            vals[12] = -(this[3, 0] * vals[0] + this[3, 1] * vals[4] + this[3, 2] * vals[8]);
            vals[13] = -(this[3, 0] * vals[1] + this[3, 1] * vals[5] + this[3, 2] * vals[9]);
            vals[14] = -(this[3, 0] * vals[2] + this[3, 1] * vals[6] + this[3, 2] * vals[10]);
            vals[15] = 1.0f;

            for (int i = 0; i < _array.Length; i++)
            {
                this._array[i] = vals[i];
            }

            return this;
        }

        public MatrixF Inverse()
        {
            MatrixF m = new MatrixF(this._width, this._height);

            this.Duplicate(m);
            m.InverseSelf();

            return m;
        }

        public static MatrixF operator *(MatrixF m1, MatrixF m2)
        {
            MatrixF m = new MatrixF(m1._width, m1._height);

            m1.Duplicate(m);
            m1.MultiplySelf(m2);

            return m;
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < _height; i++)
            {
                s += string.Join(" ", _array.Skip(i * _width).Take(_width)) + "\n";
            }

            return s;
        }

        public MatrixF(int width, int height)
        {
            _width = width;
            _height = height;
            _array = new float[width * height];
        }
    }
}