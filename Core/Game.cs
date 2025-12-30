using LaeeqFramwork.Core;
using LaeeqFramwork.Systems;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameFrameWork
{
    public partial class Game
    {
        private List<GameObject> objects = new List<GameObject>();
        private Camera camera = new Camera();
        public Size ViewPort;
        public List<GameObject> Objects => objects;
        public BirdManger Bird {  get; set; }
        public ScoreSystem DrawScore {  get; set; }
        public Game(ScoreSystem score) { 
                    DrawScore = score;
        
        }
        public void AddObject(GameObject obj)
        {
            objects.Add(obj);
        }

        public void Update(GameTime gameTime)
        {
            Bird?.Update();
            var player = objects.OfType<Player>().FirstOrDefault();

            foreach (var obj in objects.Where(o => o.IsActive))
                obj.Update(gameTime);

            if (player != null)  camera.Follow(player.Position, ViewPort, gameTime.DeltaTime);

        }

        public void Draw(Graphics g, GameTime gameTime)
        {
            g.Transform = camera.GetTransform(ViewPort, gameTime.DeltaTime);
            foreach (var obj in objects.Where(o => o.IsActive))
                obj.Draw(g);
            DrawScore.Draw(g);
            //now apply logic for player line draw in slinghot -->start
            var player = objects.OfType<Player>().FirstOrDefault();
            if (player != null && player.Movement is SlingshotMovement Slingshot)
            {
                if (Slingshot.isDragging)
                {
                    using (Pen P = new Pen(Color.FromArgb(80, 44, 44), 3)) {
                        g.DrawLine(P, Slingshot.Origin, Slingshot.CurrentDrag); }
                }
            }


            g.ResetTransform();
        }
        
        public void Cleanup()
        {
            objects.RemoveAll(o => !o.IsActive);
        }
        public bool AllPigsDestroyed()//For Win Descion
        {
            return !Objects.OfType<Pig>().Any(p => p.IsActive);
        }
    }
}
