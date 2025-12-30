using LaeeqFramwork.Systems;
using System;
using System.Drawing;

namespace GameFrameWork
{
    public class Player : GameObject
    {
        public bool IsScoringActive { get; set; }
        public IMovement Movement { get; set; }

        public float JumCount { get; set; }
        public float JumpMax = 2f;
        public bool HasImpacted = false;
        public bool Isgorunded { get; set; } = false;

        public int Health { get; set; } = 100;
        public int Score { get; set; } = 0;

        public float MaxTime = 4.5f;
        public float CountTime { get; set; }

        public override void Update(GameTime gameTime)
        {
            Movement?.Move(this, gameTime);

            if (IsScoringActive)
            {
                CountTime += gameTime.DeltaTime;

                float speed = (float)Math.Sqrt(
                    Velocity.X * Velocity.X +
                    Velocity.Y * Velocity.Y
                );

                if (CountTime >= MaxTime)
                    FinishFlight();
                else if (Position.Y > 900 || Position.X < -200 || Position.X > 1600)
                    FinishFlight();
                else if (HasImpacted && speed < 0.8f)
                    FinishFlight();
            }

            base.Update(gameTime);
        }

        public void FinishFlight()
        {
            Velocity = PointF.Empty;
            IsActive = false;
        }

        public void Launch()
        {
            if (IsScoringActive) return;
            IsScoringActive = true;
            CountTime = 0;
        }
        public override void OnCollision(GameObject other)
        {
            if (!IsScoringActive) return;

            if (other is Enemy enemy)
            {

                Health -= 10;

                if (!HasImpacted)
                {
                    Velocity = new PointF(Velocity.X * 0.2f, Velocity.Y * 0.2f);
                    HasImpacted = true;
                }
                if (!(enemy.HasScored))
                {
                ScoreSystem.AdScore(20, enemy.Position, this);
                enemy.HasScored = true; // ✅ PREVENT REPEAT          
                }
            }
            else if (other is PowerUp power)
            {
                if (power.HasScored) return;

                Health += 20;
                ScoreSystem.AdScore(10, power.Position, this);
                power.HasScored = true;
            }
        }

        public void ResetForLaunch()
        {
            HasImpacted = false;
            IsScoringActive = false;
            CountTime = 0f;
            Velocity = PointF.Empty;
        }
    }
}
