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
            if (!(other is Player player)) return;

            float speed = (float)Math.Sqrt(
                player.Velocity.X * player.Velocity.X +
                player.Velocity.Y * player.Velocity.Y
            );

            if (speed < 8f) return;

            HasScored = true;
            IsActive = false;
            ScoreSystem?.AdScore(50, Position, player);
        }
    }
}