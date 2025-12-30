using EZInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace GameFrameWork
{
    public class KeyboardMovement : IMovement
    {
        public float Speed { get; set; } = 5f;
        private PointF LastPosition;

        public void Move(GameObject obj, GameTime gameTime)
        {
            LastPosition = obj.Position;
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
                obj.Position = new PointF(obj.Position.X - Speed, obj.Position.Y);

            if (Keyboard.IsKeyPressed(Key.RightArrow))
                obj.Position = new PointF(obj.Position.X + Speed, obj.Position.Y);

            if (Keyboard.IsKeyPressed(Key.UpArrow))
                obj.Position = new PointF(obj.Position.X, obj.Position.Y - Speed);

            if (Keyboard.IsKeyPressed(Key.DownArrow))
                obj.Position = new PointF(obj.Position.X, obj.Position.Y + Speed);
            if (Keyboard.IsKeyPressed(Key.Space))
            {
                if (obj is Player player && player.JumCount < player.JumpMax)
                {


                    //      obj.Position = new PointF(obj.Position.X, -10);
                    player.Velocity = new PointF(player.Velocity.X, -10f);
                    player.JumCount++;
                    player.Isgorunded = false;
                    player.HasPhysics = true;

                }
            }
            if (LastPosition != obj.Position)
                Console.WriteLine($"Player Pos: {obj.Position}");
        }
    }

}
