using System;
using System.Drawing;

namespace GameFrameWork
{
    public class TNT : Enemy
    {
        private int _lifeTimeFrames = 0;
        private const int SAFE_FRAMES = 60;

        public TNT()
        {
            Mass = 0.5f; // Light
        }
        public override void Update(GameTime gameTime)
        {
                base.Update(gameTime);
            _lifeTimeFrames++;
        }

        public override void Draw(Graphics g)
        {

            g.FillRectangle(Brushes.Red, Bounds);
            g.DrawString("TNT", new Font("Arial", 8, FontStyle.Bold), Brushes.Yellow, Position.X + 2, Position.Y + 10);
            if (_lifeTimeFrames < SAFE_FRAMES)
            {
                g.DrawEllipse(Pens.Cyan, Position.X - 2, Position.Y - 2, Size.Width + 4, Size.Height + 4);
            }

        }

        public override void OnCollision(GameObject player)
        {
            if (_lifeTimeFrames < SAFE_FRAMES) return;

            float speed = (float)Math.Sqrt(
                player.Velocity.X * player.Velocity.X +
                player.Velocity.Y * player.Velocity.Y
            );

            if (speed < 6f) return; // 🔥 Impact threshold

            IsActive = false;

            float blastPower = 20f;

            player.Velocity = new PointF(
                -player.Velocity.X * 2f,
                -blastPower
            );
            if (player is Player p)
            {
                
            ScoreSystem?.AdScore(50, Position, p);
            }
        }

    }
}