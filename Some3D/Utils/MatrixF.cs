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