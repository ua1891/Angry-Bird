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

        public override void OnCollision(GameObject other)
        {
            if (_lifeTimeFrames<SAFE_FRAMES)
            {
                return;
            }
            // Instant explode on any contact
            IsActive = false;

            // Blast the other object away!
            float blastPower = 20f;

            // Reverse velocity and multiply it
            other.Velocity = new PointF(
                -other.Velocity.X * 2f + (other.Position.X < Position.X ? -blastPower : blastPower),
                -other.Velocity.Y * 2f - blastPower // Always blast up
            );
            ScoreSystem?.AdScore(50, Position, other as Player);
        }
    }
}