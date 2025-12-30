using System;
using System.Drawing;

namespace GameFrameWork
{
    public class StoneBlock :Enemy
    {
        public StoneBlock()
        {
            Mass = 4.0f; // Very heavy (Standard is 1.0f)
            IsRigidBody = false;
        }

        public override void Draw(Graphics g)
        {
            // Draw a grey stone look
            g.FillRectangle(Brushes.Gray, Bounds);
            g.DrawRectangle(Pens.Black, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);

            // Draw an "X" or detail to show it is hard
            g.DrawLine(Pens.DarkGray, Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height);
        }

        public override void OnCollision(GameObject other)
        {
            if (HasScored) return;
            float vX = Velocity.X - other.Velocity.X;
            float vY = Velocity.Y - other.Velocity.Y;
            float impactSpeed = (float)Math.Sqrt(vX * vX + vY * vY);

            if (impactSpeed < 8f) return;
            if (!(other is Player player)) return;
            HasScored = true;
            ScoreSystem?.AdScore(50, Position, player);
        }
    }
}