using System;

namespace Some3D.Utils
{
    public class SomeDrawing
    {
        //Done, using EFLA-E Algorithm (http://www.edepot.com/algorithm.html)
        public static void Line(DirectBitmap bitmap, int startX, int startY, int endX, int endY, int color)
        {
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

        public static void Line(DirectBitmap bitmap, float startX, float startY, float endX, float endY, int color)
        {
            Line(bitmap, (int)startX, (int)startY, (int)endX, (int)endY, color);
        }

        public static void Line(DirectBitmap bitmap, Vector3f start, Vector3f end, int color)
        {
            Line(bitmap, (int)start.X, (int)start.Y, (int)end.X, (int)end.Y, color);
        }
    }
}