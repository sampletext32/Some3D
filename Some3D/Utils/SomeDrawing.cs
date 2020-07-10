using System;

namespace Some3D.Utils
{
    public class SomeDrawing
    {
        //Done, using EFLA-E Algorithm (http://www.edepot.com/algorithm.html)
        public static void Line(DirectBitmap bitmap, int startX, int startY, int endX, int endY, uint color)
        {
            if (Math.Abs(endX - startX) > bitmap.Width + bitmap.Height)
            {
                return;
            }

            if (Math.Abs(endY - startY) > bitmap.Width + bitmap.Height)
            {
                return;
            }

            bool yLonger = false;
            int shortLen = endY - startY;
            int longLen = endX - startX;
            if (Math.Abs(shortLen) > Math.Abs(longLen))
            {
                int swap = shortLen;
                shortLen = longLen;
                longLen = swap;
                yLonger = true;
            }

            int decInc;
            if (longLen == 0) decInc = 0;
            else decInc = (shortLen << 16) / longLen;

            if (yLonger)
            {
                if (longLen > 0)
                {
                    longLen += startY;
                    for (int j = 0x8000 + (startX << 16); startY <= longLen; ++startY)
                    {
                        bitmap.SetPixel(j >> 16, startY, color);
                        j += decInc;
                    }

                    return;
                }

                longLen += startY;
                for (int j = 0x8000 + (startX << 16); startY >= longLen; --startY)
                {
                    bitmap.SetPixel(j >> 16, startY, color);
                    j -= decInc;
                }

                return;
            }

            if (longLen > 0)
            {
                longLen += startX;
                for (int j = 0x8000 + (startY << 16); startX <= longLen; ++startX)
                {
                    bitmap.SetPixel(startX, j >> 16, color);
                    j += decInc;
                }

                return;
            }

            longLen += startX;
            for (int j = 0x8000 + (startY << 16); startX >= longLen; --startX)
            {
                bitmap.SetPixel(startX, j >> 16, color);
                j -= decInc;
            }
        }

        public static void HorizontalLine(DirectBitmap bitmap, int startX, int endX, int Y, uint color)
        {
            for (int i = startX; i <= endX; i++)
            {
                bitmap.SetPixel(i, Y, color);
            }
        }

        public static void Line(DirectBitmap bitmap, float startX, float startY, float endX, float endY, uint color)
        {
            Line(bitmap, (int)startX, (int)startY, (int)endX, (int)endY, color);
        }

        public static void Line(DirectBitmap bitmap, Vector3f start, Vector3f end, uint color)
        {
            Line(bitmap, (int)start.X, (int)start.Y, (int)end.X, (int)end.Y, color);
        }

        public static void FillBottomFlatTriangle(DirectBitmap bitmap, Vector3f v1, Vector3f v2, Vector3f v3,
            uint color)
        {
            float invslope1 = (v2.X - v1.X) / (v2.Y - v1.Y);
            float invslope2 = (v3.X - v1.X) / (v3.Y - v1.Y);

            float curx1 = v1.X;
            float curx2 = v1.X;

            for (float scanlineY = v1.Y; scanlineY <= v2.Y; scanlineY++)
            {
                HorizontalLine(bitmap, (int)curx1, (int)curx2, (int)scanlineY, color);
                // Line(bitmap, curx1, scanlineY, curx2, scanlineY, color);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        public static void FillTopFlatTriangle(DirectBitmap bitmap, Vector3f v1, Vector3f v2, Vector3f v3, uint color)
        {
            float invslope1 = (v3.X - v1.X) / (v3.Y - v1.Y);
            float invslope2 = (v3.X - v2.X) / (v3.Y - v2.Y);

            float curx1 = v3.X;
            float curx2 = v3.X;

            for (float scanlineY = v3.Y; scanlineY >= v1.Y; scanlineY--)
            {
                HorizontalLine(bitmap, (int)curx1, (int)curx2, (int)scanlineY, color);
                // Line(bitmap, curx1, scanlineY, curx2, scanlineY, color);
                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }


        public static void FillTriangle(DirectBitmap bitmap, Vector3f v1, Vector3f v2, Vector3f v3, uint color)
        {
            /* at first sort the three vertices by y-coordinate ascending so v1 is the topmost vertice */
            if (v1.Y > v2.Y)
            {
                Vector3f v = v1;
                v1 = v2;
                v2 = v;
            }

            if (v1.Y > v3.Y)
            {
                Vector3f v = v1;
                v1 = v3;
                v3 = v;
            }

            if (v2.Y > v3.Y)
            {
                Vector3f v = v2;
                v2 = v3;
                v3 = v;
            }

            /* here we know that v1.y <= v2.y <= v3.y */
            /* check for trivial case of bottom-flat triangle */
            if (v2.Y == v3.Y)
            {
                FillBottomFlatTriangle(bitmap, v1, v2, v3, color);
            }
            /* check for trivial case of top-flat triangle */
            else if (v1.Y == v2.Y)
            {
                FillTopFlatTriangle(bitmap, v1, v2, v3, color);
            }
            else
            {
                /* general case - split the triangle in a topflat and bottom-flat one */
                Vector3f v4 = new Vector3f(
                    (int)(v1.X + (v2.Y - v1.Y) / (v3.Y - v1.Y) * (v3.X - v1.X)), v2.Y, 0);
                FillBottomFlatTriangle(bitmap, v1, v2, v4, color);
                FillTopFlatTriangle(bitmap, v2, v4, v3, color);
            }
        }
    }
}