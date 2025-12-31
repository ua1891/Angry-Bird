using GameFrameWork;
using LaeeqFramwork.Core;
using LaeeqFramwork.Interfaces;
using LaeeqFramwork.Systems;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace LaeeqFramwork
{
    public partial class Form1 : Form, IGameHost
    {
        private int zoomIntroFrames;
        Game game;
        PhysicsSystem physics = new PhysicsSystem();
        CollisionSystem collisions = new CollisionSystem();
        GameTime gameTime = new GameTime();
        ScoreSystem scoreSystem = new ScoreSystem();

        private int levelCooldown = 0;
        // NEW: Add the Level Manager
        LevelManager levelManager;

        public Game Gam => game;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // 1. Init Game
            game = new Game(scoreSystem);
            game.ViewPort = this.ClientSize;

            // 2. Init Level Manager
            levelManager = new LevelManager(game, scoreSystem, this);

            // 3. Load First Level
            levelManager.LoadLevel(1);
            Zoom();
           // ▶ Background Music
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
            // NEW: Decrease cooldown
            if (levelCooldown > 0)
            {
                levelCooldown--;
            }
            else
            {
                // ONLY check status if cooldown is finished
                CheckGameStatus();
            }
           Normalize_the_zooming();
            Invalidate();
        }
        private void CheckGameStatus()
        {
            if (game.AllPigsDestroyed())
            {
                Main.Stop();
                MessageBox.Show("LEVEL COMPLETE! Loading next level...");
                int nextLevel = levelManager.CurrentLevelIndex + 1;
                levelManager.LoadLevel(nextLevel);
                Zoom();
                levelCooldown = 60;

                Main.Start();
            }
            else if (!game.Bird.HasBirdsLeft() && game.Bird.Currentplayer == null)
            {
                Main.Stop();
                DialogResult res = MessageBox.Show("FAILED! Retry?", "Game Over", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    levelManager.LoadLevel(levelManager.CurrentLevelIndex);
                    levelCooldown = 60; // NEW: Reset cooldown on retry too
                    Zoom();
                    Main.Start();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Zoom()
        {
            game.camera.Zoom = 0.6f;
            zoomIntroFrames = 100;
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
    }
}