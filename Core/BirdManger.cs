using GameFrameWork;
using LaeeqFramwork.Systems;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaeeqFramwork.Core
{
    public  class BirdManger
    {
        private readonly Queue<Player> BirdQue=new Queue<Player>();
        private readonly Game game;
        public Player Currentplayer;
        private  PointF StartPosition;

        public BirdManger(Game game,PointF point)
        {
            this.game=game;
            this.StartPosition=point;
        }
        // Add birds before game starts
        public void EnqueueBird(Player bird)
        {
            bird.IsScoringActive = false;
            BirdQue.Enqueue(bird);
        }
        public void ShowNextBird()
        {
            if (Currentplayer!=null)
            {
                Currentplayer.IsActive = false;

            }
            if (BirdQue.Count == 0)
            {
                Currentplayer = null;
                return;
            }
            Currentplayer = BirdQue.Dequeue();
            Currentplayer.IsActive = true;
            Currentplayer.Position= StartPosition;
            Currentplayer.ResetForLaunch();

            game.AddObject(Currentplayer);
            if (Currentplayer.Movement is SlingshotMovement slingshot)//for Dragging Next Bird
                slingshot.Reset();
        }
        public void Start()
        {
            ShowNextBird();
        }
        public void Update()
        {
            if (Currentplayer == null) return;

            // Bird has finished its flight
            if (!(Currentplayer.IsActive))
            {
                ShowNextBird();
            }
        }
        public bool HasBirdsLeft()//In case of Checking is we loss the the game or Not
        {
            return Currentplayer != null || BirdQue.Count > 0;
        }
    }
}
