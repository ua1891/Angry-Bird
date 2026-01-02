using GameFrameWork;
using LaeeqFramwork.Core;
using LaeeqFramwork.Systems;
using System.Drawing;
using System.Windows.Forms;

namespace LaeeqFramwork
{
    public class LevelDataManager
    {
        private Game _game;
        private ScoreSystem _scoreSystem;
        private Control _canvas;

        public int CurrentLevelIndex { get; private set; } = 1;

        // ===== WORLD CONSTANTS =====
        private const float GROUND_Y = 560f; // The floor Y coordinate
        private const float BLOCK_W = 40f;   // Standard Block Width
        private const float BLOCK_H = 40f;   // Standard Block Height
        private const float PLANK_W = 140f;  // Long Plank Width
        private const float PLANK_H = 20f;   // Long Plank Height (Thinner!)

        public LevelDataManager(Game game, ScoreSystem scoreSystem, Control canvas)
        {
            _game = game;
            _scoreSystem = scoreSystem;
            _canvas = canvas;
        }

        public void LoadLevel(int levelNumber)
        {
            CurrentLevelIndex = levelNumber;

            // 1. CLEAR WORLD
            _game.Objects.Clear();

            // 3. SETUP BASICS (Ground & Birds)
            SetupCommonWorld();

            // 4. LOAD LEVEL ARCHITECTURE
            switch (levelNumber)
            {
                case 1: SetupLevel1(); break;
                case 2: SetupLevel2(); break; // Updated with Stone Base
                case 3: SetupLevel3(); break;
                case 4: SetupLevel4(); break;
                default:
                    MessageBox.Show("CONGRATULATIONS! YOU BEAT THE GAME!");
                    Application.Exit();
                    break;
            }
        }

        // ================= COMMON WORLD =================
        private void SetupCommonWorld()
        {
            // --- GROUND ---
            _game.AddObject(new GameObject
            {
                Position = new PointF(0, GROUND_Y),
                Size = new Size(1600, 40),
                Sprite = Properties.Resources.Wood,
                IsRigidBody = true, // Solid ground
                HasPhysics = false
            });

            // --- BIRDS ---
            BirdManger birdManager = new BirdManger(_game, new PointF(160, 460));
            _game.Bird = birdManager;

            // Give player 4 birds per level
            for (int i = 0; i < 4; i++)
            {
                birdManager.EnqueueBird(new Player
                {
                    Size = new Size(50, 50),
                    Mass = 2f,
                    Sprite = Properties.Resources.Main,
                    Movement = new SlingshotMovement(_canvas, new PointF(160, 460)),
                    ScoreSystem = _scoreSystem
                });
            }

            birdManager.Start();
        }

        // ================= LEVEL 1: THE WOODEN TOWER =================
        private void SetupLevel1()
        {
            float cx = 800;             // Center X of the tower
            float currentY = GROUND_Y;  // Start exactly at the ground (500)

            // --- TIER 1: The Feet (Height 40) ---
            AddBlock(cx - 50, currentY);
            AddBlock(cx + 10, currentY);
            currentY -= BLOCK_H; // Move cursor up to 460

            AddLongBlock(cx - 70, currentY);
            currentY -= PLANK_H; // Move cursor up to 440

            // --- TIER 3: Lower Room (Height 40) ---
            AddBlock(cx - 50, currentY);
            AddPig(cx - 20, currentY);   // Pig trapped inside
            AddBlock(cx + 10, currentY);
            currentY -= BLOCK_H; // Move cursor up to 400

            // --- TIER 4: Middle Plank (Height 20) ---
            AddLongBlock(cx - 70, currentY);
            currentY -= PLANK_H; // Move cursor up to 380

            // --- TIER 5: Upper Room (Double Height = 80) ---
            AddBlock(cx - 50, currentY);
            AddPig(cx - 20, currentY);   // Pig inside
            AddBlock(cx + 10, currentY);
            currentY -= BLOCK_H; // Move cursor up to 340

            // Stack Top (Sits on 340 -> Top 300)
            AddBlock(cx - 50, currentY);
            AddBlock(cx + 10, currentY);
            currentY -= BLOCK_H; // Move cursor up to 300

            // --- TIER 6: Roof Plank (Height 20) ---
            // Sits on 300. Top becomes 280.
            AddLongBlock(cx - 70, currentY);
            currentY -= PLANK_H; // Move cursor up to 280

            // --- TIER 7: Top Hat (Height 40) ---
            AddBlock(cx - 20, currentY);
        }

        // ================= LEVEL 2: THE REPLICATED STRUCTURE =================
        private void SetupLevel2()
        {
            float cx = 800;             // Center X of the structure
            float currentY = GROUND_Y;  // Start building from the ground
            float halfBlock = BLOCK_W / 2; // 20
            float halfPlank = PLANK_W / 2; // 70

            // --- TIER 1: Base Pillars (2x STONE Blocks) ---
            // CHANGED: Using AddStone instead of AddBlock for the bottom supports
            AddStone(cx - 50 - halfBlock, currentY); // Left Stone Pillar at X=730
            AddStone(cx + 50 - halfBlock, currentY); // Right Stone Pillar at X=830
            currentY -= BLOCK_H; // Move up by one block height

            // --- TIER 2: Lower Pigs (2x Pigs) ---
            // Sitting directly on top of the stone pillars.
            AddPig(cx - 50 - halfBlock, currentY);
            AddPig(cx + 50 - halfBlock, currentY);
            currentY -= BLOCK_H;

            // --- TIER 3: First Plank (1x Long Wood Plank) ---
            AddLongBlock(cx - halfPlank, currentY);
            currentY -= PLANK_H;

            // --- TIER 4: Middle Section (Block - Pig - Block) ---
            AddBlock(cx - 50 - halfBlock, currentY); // Left Block
            AddPig(cx - halfBlock, currentY);        // Center Pig
            AddBlock(cx + 50 - halfBlock, currentY); // Right Block
            currentY -= BLOCK_H;

            // --- TIER 5: Second Plank (1x Long Wood Plank) ---
            AddLongBlock(cx - halfPlank, currentY);
            currentY -= PLANK_H;

            // --- TIER 6: Top Block (1x Wood Block) ---
            AddBlock(cx - halfBlock, currentY);
        }

        // ================= LEVEL 3: THE STAIRCASE =================
        private void SetupLevel3()
        {
            float cx = 750;
            float y = GROUND_Y;

            // 1. The Stone Wall (Backstop - 4 Blocks High)
            for (int i = 0; i < 4; i++)
            {
                AddStone(cx + 160, y - (i * BLOCK_H));
            }

            // 2. Column 1 (Tallest Wood - 3 Blocks High)
            AddBlock(cx + 80, y);
            AddBlock(cx + 80, y - BLOCK_H);
            AddBlock(cx + 80, y - BLOCK_H * 2);
            AddPig(cx + 80, y - BLOCK_H * 3); // Pig on top

            // 3. Column 2 (Medium Wood - 2 Blocks High)
            AddBlock(cx, y);
            AddBlock(cx, y - BLOCK_H);
            AddPig(cx, y - BLOCK_H * 2); // Pig on top

            // 4. Column 3 (Short Wood - 1 Block High)
            AddBlock(cx - 80, y);
            AddPig(cx - 80, y - BLOCK_H); // Pig on top
        }

        // ================= LEVEL 4: THE PYRAMID SKYSCRAPER =================
        private void SetupLevel4()
        {
            float x = 750;
            float currentY = GROUND_Y;

            // --- ROW 1: Stone Base (Height 40) ---
            AddStone(x, currentY);
            AddStone(x + 40, currentY);
            AddStone(x + 80, currentY);
            currentY -= BLOCK_H;

            // --- ROW 2: Supports (Height 40) ---
            AddBlock(x, currentY);
            AddStone(x + 40, currentY); // Solid center for TNT
            AddBlock(x + 60, currentY);
            currentY -= BLOCK_H;

            // --- ROW 3: TNT Core (Height 40) ---
            AddStone(x, currentY);
            AddTNT(x + 40, currentY);   // <--- THE WEAK SPOT
            AddStone(x + 80, currentY);
            currentY -= BLOCK_H;

            // --- ROW 4: Roof Supports (Height 40) ---
            AddBlock(x, currentY);
            AddBlock(x + 80, currentY);
            currentY -= BLOCK_H;

            // --- ROW 5: Roof Plank (Height 20) ---
            AddLongBlock(x - 20, currentY);
            currentY -= PLANK_H;

            // --- ROW 6: King Pig ---
            AddPig(x + 40, currentY);

            // --- Distant Tower ---
            AddStone(x + 250, GROUND_Y);
            AddPig(x + 250, GROUND_Y - BLOCK_H);
        }

        // ================= HELPER METHODS =================
        // These methods automatically calculate the Top Y position based on height.
        // We pass the BOTTOM Y (where it sits), and it subtracts height to find Top-Left.

        private void AddBlock(float x, float bottomY)
        {
            _game.AddObject(new Enemy
            {
                Position = new PointF(x, bottomY - BLOCK_H),
                Size = new Size((int)BLOCK_W, (int)BLOCK_H),
                Mass = 1f,
                HasPhysics = true,
                Sprite = Properties.Resources.Box,
                ScoreSystem = _scoreSystem
            });
        }

        private void AddStone(float x, float bottomY)
        {
            _game.AddObject(new StoneBlock
            {
                Position = new PointF(x, bottomY - BLOCK_H),
                Size = new Size((int)BLOCK_W, (int)BLOCK_H),
                HasPhysics = true,
                ScoreSystem = _scoreSystem
            });
        }

        private void AddPig(float x, float bottomY)
        {
            _game.AddObject(new Pig
            {
                Position = new PointF(x, bottomY - BLOCK_H),
                Size = new Size((int)BLOCK_W, (int)BLOCK_H),
                HasPhysics = true,
                ScoreSystem = _scoreSystem
            });
        }

        private void AddTNT(float x, float bottomY)
        {
            _game.AddObject(new TNT
            {
                Position = new PointF(x, bottomY - BLOCK_H),
                Size = new Size((int)BLOCK_W, (int)BLOCK_H),
                HasPhysics = true,
                ScoreSystem = _scoreSystem
            });
        }

        // NOTE: Uses PLANK_H (20) instead of BLOCK_H (40)
        private void AddLongBlock(float x, float bottomY)
        {
            _game.AddObject(new Enemy
            {
                Position = new PointF(x, bottomY - PLANK_H),
                Size = new Size((int)PLANK_W, (int)PLANK_H),
                Mass = 1.5f,
                HasPhysics = true,
                Sprite = Properties.Resources.Box, // Or a Plank sprite if available
                ScoreSystem = _scoreSystem
            });
        }
    }
}