using System;
using System.Drawing;

namespace GameFrameWork
{
    public class Pig : Enemy
    {
        private int _lifeTimeFrames = 0;
        private const int SAFE_FRAMES = 60;
        public Pig()
        {
            Mass = 0.5f;
            Size = new Size(50, 50);
            Velocity = PointF.Empty;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _lifeTimeFrames++;
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.LimeGreen, Bounds);
            g.DrawEllipse(Pens.DarkGreen, Bounds);

            float noseSize = Size.Width / 3;
            g.FillEllipse(Brushes.DarkGreen,
                Position.X + Size.Width / 2 - noseSize / 2,
                Position.Y + Size.Height / 2 - noseSize / 2,
                noseSize, noseSize);

            if (_lifeTimeFrames < SAFE_FRAMES)
            {
                g.DrawEllipse(Pens.Cyan, Position.X - 2, Position.Y - 2, Size.Width + 4, Size.Height + 4);
            }

            // Eyes
            g.FillEllipse(Brushes.White, Position.X + 10, Position.Y + 10, 10, 10);
            g.FillEllipse(Brushes.White, Position.X + 30, Position.Y + 10, 10, 10);
        }

        public override void OnCollision(GameObject other)
        {
            if (_lifeTimeFrames < SAFE_FRAMES)
                return;

            float vX = this.Velocity.X - other.Velocity.X;
            float vY = this.Velocity.Y - other.Velocity.Y;
            float impactSpeed = (float)Math.Sqrt(vX * vX + vY * vY);

            float breakThreshold = 3.5f;

            if (other is Player)
            {
                IsActive = false;
                ScoreSystem?.AdScore(500, Position, other as Player);
            }
            else if (impactSpeed > breakThreshold)
            {
                IsActive = false;
                ScoreSystem?.AdScore(100, Position, other as Player); // Give points for crushing pigs with blocks
            }
        }
    }
}