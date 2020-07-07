using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Some3D.Utils
{
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap;
        public int[] Bits;
        public bool Disposed;
        public int Height;
        public int Width;
        protected GCHandle BitsHandle;

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb,
                BitsHandle.AddrOfPinnedObject());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, int color)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                Bits[x + y * Width] = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetPixel(int x, int y)
        {
            return Bits[x + y * Width];
        }

        public void Clear()
        {
            int c = 0xFFFFFF; // 0xFFFFFFFF
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, c);
                }
            }
        }

        public void CopyTo(DirectBitmap b)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, b.GetPixel(x, y));
                }
            }
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
            GC.SuppressFinalize(this);
        }

        public static implicit operator Bitmap(DirectBitmap directBitmap)
        {
            return directBitmap.Bitmap;
        }
    }
}