using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Game
    {
        public string FileName { get; set; }
        public List<Team> Teams { get; set;  }
        public int Stalkers { get; set; }
        public int[] Timer { get; set; }
        public int HintTimer { get; set; }
        public string PicsFile { get; set; }
        public List<GameSettings> SettingsL { get; set; }
        public bool Round3On { get; set; }
        public string QuizFile { get; set; }
        public int TourN { get; set; }
        public int ExpeditionN { get; set; }
        public int RealExpN { get; set; }
        public int QuestionN { get; set; }
        public List<List<List<int>>> StalkersDelegated { get; set; }
        public List<int[]> ScoresTable { get; set; }
        public List<Score> ScoresFinal { get; set; }
        public string LogFile { get; set; }
        public int BestStalkerTeam { get; set; }
        public int BestStalkerTour { get; set; }
        public int BestStalkerExp { get; set; }
        public int BestStalkerQ { get; set; }
        public int BestStalkerValue { get; set; }
        public string BestStalkerAnswer { get; set; }
        public Game() { }
        public Game(string name) {
            FileName = name;
            Teams = new List<Team>();
            SettingsL = new List<GameSettings>();
            TourN = 0;
            ExpeditionN = 0;
            RealExpN = 0;
            QuestionN = 0;
            StalkersDelegated = new List<List<List<int>>>();
            ScoresFinal = new List<Score>();
            ScoresTable = new List<int[]>();
            Timer = new int[4];
            BestStalkerTeam = -1;
            BestStalkerTour = -1;
            BestStalkerExp = -1;
            BestStalkerQ = -1;
            BestStalkerValue = -1;
            BestStalkerAnswer = "";

        }
        public void Serialize()
            {

            var formatter = new XmlSerializer(typeof(Game));
            using (FileStream fs = new FileStream(this.FileName, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }
                               
        }
        public static Game Deserialize(string file)
        {

            var formatter = new XmlSerializer(typeof(Game));

            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                return (Game)formatter.Deserialize(fs);
            }
        }

    }
    

}
