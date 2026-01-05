using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameFrameWork
{
    public class PhysicsSystem
    {
        // Global gravity value (can be positive or negative)
        public float Gravity { get; set; } = 0.8f;

        // Apply physics to objects that have physics enabled
        public void Apply(List<GameObject> objects)
        {
            foreach (var obj in objects.OfType<IPhysicsObject>().Where(o => o.HasPhysics))
            {
                if (obj is IMovable movable)
                {

                    float appliedGravity = obj.CustomGravity ?? Gravity;        
                        movable.Velocity = new PointF(
                            movable.Velocity.X, // Horizontal velocity remains unchanged
                            movable.Velocity.Y + appliedGravity // Vertical velocity changes based on gravity
                        );

                        if (obj is GameObject gameObject)
                        {
                            gameObject.Position = new PointF(
                                gameObject.Position.X + movable.Velocity.X,
                                gameObject.Position.Y + movable.Velocity.Y
                            );
                        }
                }
            }
        }
    }
}