using System.Diagnostics;

namespace GameFrameWork
{
    public class GameTime
    {
        private Stopwatch stopwatch = new Stopwatch();
        private long lastTime;

        public float DeltaTime { get; private set; }

        public GameTime()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;
        }

        public void Update()
        {
            long current = stopwatch.ElapsedMilliseconds;
            DeltaTime = (current - lastTime) / 1000f;
            lastTime = current;
        }
    }
}
