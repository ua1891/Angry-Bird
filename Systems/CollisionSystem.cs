using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameFrameWork
{
    public class CollisionSystem
    {
        public void Check(List<GameObject> objects)
        {
            var collidables = objects.OfType<ICollidable>().ToList();

            // Reset grounded state
            foreach (var player in objects.OfType<Player>())
                player.Isgorunded = false;

            for (int i = 0; i < collidables.Count; i++)
            {
                for (int j = i + 1; j < collidables.Count; j++)
                {
                    var a = (GameObject)collidables[i];
                    var b = (GameObject)collidables[j];

                    if (!a.Bounds.IntersectsWith(b.Bounds))
                        continue;

                    RectangleF overlap = RectangleF.Intersect(a.Bounds, b.Bounds);
                    if (overlap.Width <= 0 || overlap.Height <= 0)
                        continue;

                    if (a.IsRigidBody && !b.IsRigidBody)
                        ResolveRigidCollision(a, b, overlap);
                    else if (b.IsRigidBody && !a.IsRigidBody)
                        ResolveRigidCollision(b, a, overlap);
                   
                        else
                        {
                            // Prevent stacked blocks from behaving like jelly
                            if (a is Enemy || b is Enemy || a is StoneBlock || b is StoneBlock)
                                ResolveRigidCollision(a, b, overlap);
                            else
                                ResolveSoftCollision(a, b, overlap);
                        
                    }

                    collidables[i].OnCollision(b);
                    collidables[j].OnCollision(a);
                }
            }
        }

        // ================= RIGID COLLISION =================
        private void ResolveRigidCollision(GameObject rigid, GameObject movable, RectangleF overlap)
        {
            // WALL
            if (overlap.Width < overlap.Height)
            {
                float dir = movable.Position.X < rigid.Position.X ? -1 : 1;
                movable.Position = new PointF(
                    movable.Position.X + dir * overlap.Width,
                    movable.Position.Y
                );

                movable.Velocity = new PointF(-movable.Velocity.X * 0.4f, movable.Velocity.Y);//Bounce increase or decres By this 0.4 when Any movable object hit Rogid
            }
            // GROUND / CEILING
            else
            {
                if (movable.Position.Y < rigid.Position.Y)
                {
                    movable.Position = new PointF(
                        movable.Position.X,
                        movable.Position.Y - overlap.Height
                    );

                    // Dynamic friction (IMPORTANT)
                    float friction = Math.Abs(movable.Velocity.X) > 2f ? 0.98f : 0.9f;//Firiction slip

                    if (Math.Abs(movable.Velocity.Y) < 1f)
                    {
                        movable.Velocity = new PointF(movable.Velocity.X * friction, 0);

                        if (movable is Player p)
                        {
                            p.Isgorunded = true;
                            p.JumCount = 0;
                        }
                    }
                    else
                    {
                        movable.Velocity = new PointF(
                            movable.Velocity.X * friction,
                            -movable.Velocity.Y * 0.3f //small Bounce Here 
                        );
                    }
                }
                else
                {
                    movable.Position = new PointF(
                        movable.Position.X,
                        movable.Position.Y + overlap.Height
                    );

                    movable.Velocity = new PointF(
                        movable.Velocity.X,
                        -movable.Velocity.Y * 0.3f
                    );
                }
            }
        }

        // ================= SOFT COLLISION =================
        private void ResolveSoftCollision(GameObject a, GameObject b, RectangleF overlap)
        {
            bool vertical = overlap.Width > overlap.Height;

            if (vertical)
            {
                float sep = overlap.Height / 2f;

                if (a.Position.Y < b.Position.Y)
                {
                    a.Position = new PointF(a.Position.X, a.Position.Y - sep);
                    b.Position = new PointF(b.Position.X, b.Position.Y + sep);
                }
                else
                {
                    a.Position = new PointF(a.Position.X, a.Position.Y + sep);
                    b.Position = new PointF(b.Position.X, b.Position.Y - sep);
                }

                // Vertical energy sharing
                float avgY = (a.Velocity.Y + b.Velocity.Y) / 2f;
                a.Velocity = new PointF(a.Velocity.X, avgY);
                b.Velocity = new PointF(b.Velocity.X, avgY);
            }
            else
            {
                // HORIZONTAL IMPULSE (KEY PART)
                float sep = overlap.Width / 2f;

                if (a.Position.X < b.Position.X)
                {
                    a.Position = new PointF(a.Position.X - sep, a.Position.Y);
                    b.Position = new PointF(b.Position.X + sep, b.Position.Y);
                }
                else
                {
                    a.Position = new PointF(a.Position.X + sep, a.Position.Y);
                    b.Position = new PointF(b.Position.X - sep, b.Position.Y);
                }

                float vA = a.Velocity.X;
                float vB = b.Velocity.X;

                float totalMass = a.Mass + b.Mass;
                if (totalMass <= 0) totalMass = 1f;

                float impulse = (vA - vB) * (a.Mass / totalMass);

                a.Velocity = new PointF(vA - impulse * 0.4f, a.Velocity.Y);
                b.Velocity = new PointF(vB + impulse * 0.9f, b.Velocity.Y);
            }
        }
    }
}
