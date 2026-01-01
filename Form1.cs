using GameFrameWork;
using LaeeqFramwork.Core;
using LaeeqFramwork.Extensions;
using LaeeqFramwork.GameAllForms;
using LaeeqFramwork.Interfaces;
using LaeeqFramwork.Systems;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static LaeeqFramwork.Core.LevelFlowManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace LaeeqFramwork
{
    public partial class Form1 : Form, IGameHost
    {
        private int zoomIntroFrames;
        private int HighestScore {  get; set; }
        Game game;
        PhysicsSystem physics = new PhysicsSystem();
        CollisionSystem collisions = new CollisionSystem();
        GameTime gameTime = new GameTime();
        ScoreSystem scoreSystem = new ScoreSystem();
        GameRepo gameRepo = new GameRepo();
        private int levelCooldown = 0;
        // NEW: Add the Level Manager
        LevelFlowManager _LevelFlowManager { get; set; }
        LevelDataManager LevelDataManager { get; set; }

        public Game Gam => game;

        public Form1(int Level,int HScore)
        {
            InitializeComponent();
            DoubleBuffered = true;
            HighestScore = HScore;
            HighScore.Text = HScore.ToString();
            game = new Game(scoreSystem);
            game.ViewPort = this.ClientSize;
            LevelDataManager = new LevelDataManager(game, scoreSystem, this);
            _LevelFlowManager = new LevelFlowManager(LevelDataManager, Gam, 4, Level);
            Zoom();
            AudioManager.Instance.PlayMusic("Assets/Audio/bg.mp3", true);
            Main.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            game.Draw(e.Graphics, gameTime);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            gameTime.Update();
            game.Update(gameTime);
            physics.Apply(game.Objects.ToList());
            collisions.Check(game.Objects.ToList());
            game.Cleanup();
            scoreSystem.Update();
            Score.Text =scoreSystem.totalScore.ToString();
            _LevelFlowManager.Update();
            // NEW: Decrease cooldown
            if (levelCooldown > 0)
            {
                levelCooldown--;
            }
            else
            {
                CheckGameStatus();
            }
            Normalize_the_zooming();
            Invalidate();
        }
        private void CheckGameStatus()
        {
            switch (_LevelFlowManager.State)
            {
                case GameState.LevelCompleted:
                    Main.Stop();
                    DialogResult Result=  MessageBox.Show("LEVEL COMPLETE!"," Are You wanna to continue Next...", MessageBoxButtons.YesNo);
                    HScore();///////////
                    if (Result==DialogResult.Yes)
                    {
                    _LevelFlowManager.NextLevel();
                    LoadImages(_LevelFlowManager.CurrentLevel);
                    Zoom();
                    levelCooldown = 60;
                    Main.Start();          
                    }
                    else
                    {
                        gameRepo.Add(_LevelFlowManager.CurrentLevel + 1, HighestScore);
                        OpenMainForm();
                    }
                     break;
                case GameState.Failed:

                    Main.Stop();
                    DialogResult res = MessageBox.Show("FAILED! Retry?", "Game Over", MessageBoxButtons.YesNo);
                    Score.Text = "";
                    if (res == DialogResult.Yes)
                    {
                        Main.Stop();
                        _LevelFlowManager.Retry();
                        levelCooldown = 60; // NEW: Reset cooldown on retry too
                        Zoom();
                        Main.Start();
                    }
                    else
                    {
                        gameRepo.Add(_LevelFlowManager.CurrentLevel, HighestScore);
                        OpenMainForm();
                    }
                    break;

                case GameState.GameCompleted:
                    Main.Stop();
                    gameRepo.Add(_LevelFlowManager.CurrentLevel, HighestScore);
                    MessageBox.Show("YOU FINISHED ALL LEVELS!");
                    OpenMainForm();
                    break;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void LoadImages(int Level)
        {
            switch (Level)
            {
                case 1:
                    BackgroundImage = Properties.Resources.Bg;
                    break;
                case 2:
                    BackgroundImage = Properties.Resources.Level2;
                    break;
                case 3:
                    BackgroundImage = Properties.Resources.Bg;
                    break;
                case 4:
                    BackgroundImage = Properties.Resources.Bg;
                    break;


            }
        }
        private void Zoom()
        {
            game.camera.Zoom = 0.6f;
            zoomIntroFrames = 60;
        }
        private void Normalize_the_zooming()
        {
            if (zoomIntroFrames > 0)
            {
                zoomIntroFrames--;

                if (zoomIntroFrames == 0)
                {
                    game.camera.Zoom = 1f;
                }
            }
        }
        private void HScore()
        {
            int core = int.Parse(Score.Text);
            if (core>HighestScore)
            {
                HighestScore = core;
                HighScore.Text= Score.Text;
                scoreSystem.totalScore = 0;
                Score.Text = "";
            }
            else
            {
                scoreSystem.totalScore = 0;
                Score.Text = "";
            }
        }
        public void OpenMainForm()
        {
            this.Close();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}