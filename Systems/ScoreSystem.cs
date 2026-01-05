using GameFrameWork;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LaeeqFramwork.Systems
{
    public class ScoreSystem
    {

       public  int totalScore {  get; set; }
        public List<ScorePopup> ScorePopups= new List<ScorePopup>();
        public ScoreSystem() { }
        public void AdScore(int Point, PointF Position,Player player)
        {
            if (player == null) return;
            if (!player.IsScoringActive) return;
            totalScore += Point;
            ScorePopups.Add(new ScorePopup
            {
                Position = Position,
                LifeTime = 30,
                Points = Point

            });
        }
        public void Update()
        {
             
            for (int i = ScorePopups.Count - 1; i >= 0; i--)
            {
                ScorePopups[i].Update();
                if (ScorePopups[i].LifeTime <= 0)
                    ScorePopups.RemoveAt(i);
            }
        }
        
        public void Draw(Graphics g)
        {
            foreach (var popup in ScorePopups)
                popup.Draw(g);
        }
    }
    // Same popup class as before
    public class ScorePopup
    {
        public PointF Position;
        public int Points;
        public int LifeTime;

        public void Update()
        {
            Position = new PointF(Position.X, Position.Y - 1f); // move up
            LifeTime--;
        }
        public void Draw(Graphics g)
        {
            g.DrawString("+" + Points, new Font("Arial", 12), Brushes.Black, Position);
        }
    }
}

