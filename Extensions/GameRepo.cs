using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaeeqFramwork.Extensions
{
    public class GameRepo
    {
        private readonly string _path = "Game.txt";
        public void Add(int level,int HighScore)
        {
            using(StreamWriter Write=new StreamWriter(_path, false))
            {
                Write.WriteLine(level + "," + HighScore);
            }
        }
        public (int HScore,int Level) ReadData()
        {
            string Data="";
            using(StreamReader Read=new StreamReader(_path)) { Data = Read.ReadToEnd();}
            string[] Parts=Data.Split(',');
            int Level=int.Parse(Parts[0]);
            int score=int.Parse(Parts[1]);
            return (score, Level);
        }
    }
}
