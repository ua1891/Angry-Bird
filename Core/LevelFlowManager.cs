using GameFrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaeeqFramwork.Core
{
    public class LevelFlowManager
    {
        private readonly LevelDataManager _LevelDataManager;
        private readonly Game _Game;
        public int MaxLevel { get; set; }
        public int CurrentLevel { get; set; }
        public GameState State { get; set; } = GameState.Playing;
        public LevelFlowManager(LevelDataManager levelDataManager, Game game, int MaxLevel, int CurrentLevel)
        {
            _LevelDataManager = levelDataManager;
            _Game = game;
            this.MaxLevel = MaxLevel;
            this.CurrentLevel = CurrentLevel;
            _LevelDataManager.LoadLevel(CurrentLevel);
            Common.SoundOnLevel(CurrentLevel);
        }
       
        //Here I check is Game Playing ,player fail,Player Win logic Ok
        public void Update()
        {
            if (State != GameState.Playing) return;
            if (_Game.AllPigsDestroyed()) State = GameState.LevelCompleted;
            else if (!_Game.Bird.HasBirdsLeft() && _Game.Bird.Currentplayer == null) State = GameState.Failed;      
        }
        public void NextLevel()
        {
            if (CurrentLevel >= MaxLevel)//Levl being played 
            {
                State = GameState.GameCompleted;
                return;
            }
                 CurrentLevel++;
               _LevelDataManager.LoadLevel(CurrentLevel);
                State = GameState.Playing;
            
        }
        public void Retry()
        {
           _LevelDataManager.LoadLevel(CurrentLevel);
            State = GameState.Playing;
        }
    }
}
