using Some3D.Utils;

namespace Some3D.Render
{
    public class Object3D
    {
        public Vector3f Position { get; private set; }

        private MatrixF _translationMatrix;
        public MatrixF TranslationMatrix => _translationMatrix;

        public void SetPosition(Vector3f position)
        {
            Position = position;
            CalculateTranslationMatrix();
        }

        private void CalculateTranslationMatrix()
        {
            _translationMatrix.MakeIdentity();
            _translationMatrix[3, 0] = Position.X;
            _translationMatrix[3, 1] = Position.Y;
            _translationMatrix[3, 2] = Position.Z;
        }

        public Vector3f Scale { get; private set; }

        private MatrixF _scaleMatrix;
        public MatrixF ScaleMatrix => _scaleMatrix;

        public void SetScale(Vector3f scale)
        {
            Scale = scale;
            CalculateScaleMatrix();
        }

        private void CalculateScaleMatrix()
        {
            _scaleMatrix.MakeIdentity();
            _scaleMatrix[0, 0] = Scale.X;
            _scaleMatrix[1, 1] = Scale.Y;
            _scaleMatrix[2, 2] = Scale.Z;
            _scaleMatrix[3, 3] = 1;
        }

        public Vector3f Rotation { get; private set; }

        private MatrixF _rotationMatrix;
        public MatrixF RotationMatrix => _rotationMatrix;

        public void SetRotation(Vector3f rotation)
        {
            Rotation = rotation;
            CalculateRotationMatrix();
        }

        private void CalculateRotationMatrix()
        {
            _rotationMatrix.MakeIdentity();
            _rotationMatrix.RotateXSelf(Rotation.X);
            _rotationMatrix.RotateYSelf(Rotation.Y);
            _rotationMatrix.RotateZSelf(Rotation.Z);
        }

        public Object3D()
        {
            Position = 0;
            Scale = 1;
            Rotation = 0;
            _translationMatrix = new MatrixF(4, 4);
            _scaleMatrix = new MatrixF(4, 4);
            _rotationMatrix = new MatrixF(4, 4);
            CalculateRotationMatrix();
            CalculateScaleMatrix();
            CalculateTranslationMatrix();
        }

        public Object3D(Vector3f position, Vector3f scale, Vector3f rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
            _translationMatrix = new MatrixF(4, 4);
            _scaleMatrix = new MatrixF(4, 4);
            _rotationMatrix = new MatrixF(4, 4);
            CalculateRotationMatrix();
            CalculateScaleMatrix();
            CalculateTranslationMatrix();
        }
    }
}