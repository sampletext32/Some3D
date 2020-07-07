using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D
{
    public class Vector3f
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3f()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Duplicate(Vector3f to)
        {
            to.X = X;
            to.Y = Y;
            to.Z = Z;
        }

        public static Vector3f operator *(Vector3f vec, MatrixF m)
        {
            Vector3f v = new Vector3f();

            v.X = vec.X * m[0, 0] + vec.Y * m[1, 0] + vec.Z * m[2, 0] + m[3, 0];
            v.Y = vec.X * m[0, 1] + vec.Y * m[1, 1] + vec.Z * m[2, 1] + m[3, 1];
            v.Z = vec.X * m[0, 2] + vec.Y * m[1, 2] + vec.Z * m[2, 2] + m[3, 2];
            float w = vec.X * m[0, 3] + vec.Y * m[1, 3] + vec.Z * m[2, 3] + m[3, 3];

            if (Math.Abs(w) > 0.00000001f)
            {
                v.X /= w;
                v.Y /= w;
                v.Z /= w;
            }

            return v;
        }

        public static implicit operator Vector3f(float value)
        {
            return new Vector3f(value, value, value);
        }
    }
}