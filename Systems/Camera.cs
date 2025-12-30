using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LaeeqFramwork.Systems
{
    public class Camera
    {
        public PointF Position { get; private set; }
        public float Zoom { get; set; } = 1f;
        public float LerpFactor { get; set; } = 8f;

        private float shakeTime;
        private float shakeIntensity;
        private readonly Random rand = new Random();

        public void Follow(PointF target, Size viewport, float deltaTime)
        {
            float desiredX = target.X - (viewport.Width / 2f) / Zoom;
            float desiredY = target.Y - (viewport.Height / 2f) / Zoom;
            float t = 1f - (float)Math.Exp(-LerpFactor * deltaTime);
            Position = new PointF(
                Position.X + (desiredX - Position.X) * t,
                Position.Y + (desiredY - Position.Y) * t
            );
        }

        public void Shake(float intensity, float duration)
        {
            shakeIntensity = intensity;
            shakeTime = duration;
        }

        public Matrix GetTransform(Size viewport, float deltaTime)
        {
            Matrix m = new Matrix();

            float offsetX = 0f, offsetY = 0f;
            if (shakeTime > 0f)
            {
                offsetX = (float)(rand.NextDouble() * 2 - 1) * shakeIntensity;
                offsetY = (float)(rand.NextDouble() * 2 - 1) * shakeIntensity;
                shakeTime -= deltaTime;
            }

            m.Translate(-Position.X + offsetX, -Position.Y + offsetY);
            m.Scale(Zoom, Zoom, MatrixOrder.Append);

            return m;
        }
        public PointF ScreenToWorld(PointF screen, Size viewport, float dt)
        {
            var matrix = GetTransform(viewport, dt);
            matrix.Invert();
            PointF[] pts = { screen };
            matrix.TransformPoints(pts);
            return pts[0];
        }

    }
}
