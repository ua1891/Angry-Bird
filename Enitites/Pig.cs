using System;
using System.Drawing;

namespace GameFrameWork
{
    public class Pig : Enemy
    {
        // 1. Add a timer to track how long the pig has been alive
        private int _lifeTimeFrames = 0;
        private const int SAFE_FRAMES = 60;

        public Pig()
        {
            Mass = 0.5f;
            Size = new Size(50, 50);
            Velocity = PointF.Empty;
        }

        // 2. We MUST override Update to count the frames
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

            // Visual Debug: Draw a shield if safe
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
            // 3. SAFETY CHECK: If the level just started, ignore collisions!
            if (_lifeTimeFrames < SAFE_FRAMES)
                return;

            float vX = this.Velocity.X - other.Velocity.X;
            float vY = this.Velocity.Y - other.Velocity.Y;
            float impactSpeed = (float)Math.Sqrt(vX * vX + vY * vY);

            // 4. I also increased the threshold slightly (from 3.0 to 4.5) to prevent accidental deaths from falling short distances
            float breakThreshold = 4.5f;

            if (other is Player)
            {
                IsActive = false;
                ScoreSystem?.AdScore(500, Position, other as Player);
            }
            else if (impactSpeed > breakThreshold)
            {
                IsActive = false;
                ScoreSystem?.AdScore(100, Position, other as Player); // Give points for crushing pigs with blocks!
            }
        }
    }
}