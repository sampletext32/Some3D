using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Some3D.Utils
{
    public class SomeMaths
    {
        public static Vector3f Cross(Vector3f line1, Vector3f line2)
        {
            Vector3f v = new Vector3f();
            v.X = line1.Y * line2.Z - line1.Z * line2.Y;
            v.Y = line1.Z * line2.X - line1.X * line2.Z;
            v.Z = line1.X * line2.Y - line1.Y * line2.X;
            return v;
        }

        public static Vector3f Cross(Vector3f v0, Vector3f v1, Vector3f v2)
        {
            Vector3f line1 = v1 - v0;
            Vector3f line2 = v2 - v0;
            return Cross(line1, line2);
        }
    }
}