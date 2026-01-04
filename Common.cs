using LaeeqFramwork.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaeeqFramwork
{
    public class Common
    {
        public static void PlaySound(string Path)
        {
            AudioManager.Instance.Dispose();
            AudioManager.Instance.PlayMusic(Path, true);
        }
        public static void SoundOnLevel(int level)
        {
            switch (level)
            {
                case 1:
                    PlaySound("Assets/Audio/Level1.mp3");
                    break;
                case 2:
                    PlaySound("Assets/Audio/Level2.mp3");
                    break;
                case 3:
                    PlaySound("Assets/Audio/Level3.mp3");
                    break;
                case 4:
                    PlaySound("Assets/Audio/Level4.mp3");
                    break;
            }
        }
    }
}
