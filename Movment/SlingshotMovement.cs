using GameFrameWork;
using LaeeqFramwork.Interfaces;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace LaeeqFramwork.Systems
{

    public class SlingshotMovement : IMovement
    {
        public bool isDragging = false;      // True while mouse is held down
        public PointF CurrentDrag;           // Current mouse position
        public PointF Origin;            // Bird's resting position (saved once)
        public SoundPlayer Crash;
        public float PowerMultiplier = 0.5f;  // Controls launch strength
        public float MaxPullDistance = 100f;  // Maximum elastic stretch
        private bool isLaunched = false;
        private Control canvas;                // Form for mouse input

        
        AudioFileReader reader;
        public SlingshotMovement(Control canvas, PointF point)
        {
            Origin = point;
            this.canvas = canvas;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;
          
        }
      
        public void Move(GameObject obj, GameTime gameTime)
        {
            if (!(obj is Player bird))
                return;
            // Already launched → physics handles movement
            if (isLaunched)
                return;

            // this is the logic Before Launch just Move Up write Before Lunach
            if (isDragging)
            {

                float dx = Origin.X - CurrentDrag.X;
                float dy = Origin.Y - CurrentDrag.Y;
                float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                // Here I check stretch stretch 
                if (distance > MaxPullDistance)
                {
                    float scale = MaxPullDistance / distance;
                    dx *= scale;
                    dy *= scale;
                }
                bird.Position = new PointF(Origin.X + dx, Origin.Y + dy);
                bird.Velocity = PointF.Empty;
                bird.HasPhysics = false;
                isLaunched = false;
            }
            
        }
        // ================= MOUSE INPUT =================
        // Mouse pressed → start dragging
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isLaunched)
                    return;
                isDragging = true;
                CurrentDrag = e.Location;
            }
        }

        // Mouse moved → update drag position
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;
            CurrentDrag = e.Location;

        }

        // Mouse releases → stop dragging
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !isDragging)
                return;

            isDragging = false;
            LaunchBird();
        }
        public void LaunchBird()
        {
            if (!isLaunched)

            {
                if (!(canvas.FindForm() is Form form))
                    return;

                if (!(form is IGameHost host))
                    return;
                Player bird = host.Gam.Bird?.Currentplayer;

                if (bird == null)
                    return;

                float vx = Origin.X - bird.Position.X;
                float vy = Origin.Y - bird.Position.Y;
                //Extensions
                float PullDistacnce =(float)Math.Sqrt(vx * vx + vy * vy);
                float NormalizedPull = PullDistacnce / MaxPullDistance;

                //if it is greater than One which is Harmfull for Our Bird it feel like rocket so Bound to be <=1
                NormalizedPull = Math.Min(NormalizedPull, 1f);
                // here "O.6"How strong should the weakest possible shot be
                float Power = PowerMultiplier * (0.3f+ NormalizedPull * NormalizedPull);
                bird.Velocity = new PointF(
                    vx *Power,
                    vy *Power
                );

                bird.HasPhysics = true;
                bird.HasImpacted = false;
                bird.Launch();
                AudioManager.Instance.PlaySfx("Assets/Audio/Slingshot.mp3");
                AudioManager.Instance.PlaySfx("Assets/Audio/Slingshot.mp3");
                isLaunched = true;
            }
        }
        // ================== RESET FOR NEXT BIRD ==================
        /// <summary>
        /// Call this when a new bird spawns to allow dragging again.
        /// </summary>
        public void Reset()
        {
            isDragging = false;
            isLaunched = false;
        }
    }
}
