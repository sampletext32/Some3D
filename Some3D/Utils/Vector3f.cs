using System;

namespace Some3D.Utils
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

        public Vector3f MultiplySelf(MatrixF m)
        {
            float tX = X;
            float tY = Y;
            float tZ = Z;

            X = tX * m[0, 0] + tY * m[1, 0] + tZ * m[2, 0] + m[3, 0];
            Y = tX * m[0, 1] + tY * m[1, 1] + tZ * m[2, 1] + m[3, 1];
            Z = tX * m[0, 2] + tY * m[1, 2] + tZ * m[2, 2] + m[3, 2];

            float w = tX * m[0, 3] + tY * m[1, 3] + tZ * m[2, 3] + m[3, 3];

            if (Math.Abs(w) > 0.00000001f)
            {
                X /= w;
                Y /= w;
                Z /= w;
            }

            return this;
        }

        public Vector3f AddSelf(Vector3f v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;

            return this;
        }

        public Vector3f SubSelf(Vector3f v)
        {
            X -= v.X;
            Y -= v.Y;
            Z -= v.Z;

            return this;
        }

        public static Vector3f operator *(Vector3f self, MatrixF m)
        {
            Vector3f v = new Vector3f();
            self.Duplicate(v);

            v.MultiplySelf(m);

            return v;
        }

        public static Vector3f operator +(Vector3f self, Vector3f vec)
        {
            Vector3f v = new Vector3f();
            self.Duplicate(v);

            v.AddSelf(vec);

            return v;
        }

        public static Vector3f operator -(Vector3f self, Vector3f vec)
        {
            Vector3f v = new Vector3f();
            self.Duplicate(v);

            v.SubSelf(vec);

            return v;
        }

        public static implicit operator Vector3f(float value)
        {
            return new Vector3f(value, value, value);
        }
    }
}