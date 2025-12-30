using LaeeqFramwork.Systems;
using System;
using System.Drawing;

namespace GameFrameWork
{
    public class Enemy : GameObject
    {
        public IMovement Movement { get; set; }

        public Enemy() { }

        public override void Update(GameTime gameTime)
        {
            Movement?.Move(this, gameTime);
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (!IsActive) return; // ✅ FIX

            float vX = Velocity.X - other.Velocity.X;
            float vY = Velocity.Y - other.Velocity.Y;
            float ImpactSpeed = (float)Math.Sqrt(vX * vX + vY * vY);
            float RequiredImpact = 4.0f;

            if (other is Bullet)
            {
                IsActive = false;
                return;
            }
             if (ImpactSpeed >= RequiredImpact &&other is Player player)
             {
                    IsActive = false;
             }
            

            if (other is Enemy && ImpactSpeed > 5.0f)
            {
                IsActive = false;
            }
        }
    }
}
