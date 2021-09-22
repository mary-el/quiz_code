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
    public class Quiz
    {
        public List<Tour> Tours { get; set; }
        public Quiz() { }

        public void Serialize(string name)
        {

            var formatter = new XmlSerializer(typeof(Quiz));
            using (FileStream fs = new FileStream(name, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }

        }

        public static Quiz Deserialize(string rel_file)
        {
            string file = Path.GetFullPath(rel_file);
            var formatter = new XmlSerializer(typeof(Quiz));

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                return (Quiz)formatter.Deserialize(fs);
            }
        }
    }

    [Serializable]
    public class Pics
    {

        public string MusicPict { get; set; }
        public string VideoPict { get; set; }
        public string Logo { get; set; }
        public string BackgroundQ { get; set; }
        public string BackgroundSt { get; set; }
        public string BackgroundAn { get; set; }
        public string BackgroundExp { get; set; }
        public string BackgroundRes { get; set; }
        public string BackgroundRepeat { get; set; }
        public string YesGoldPict { get; set; }
        public string YesSilverPict { get; set; }
        public string YesBronzePict { get; set; }
        public List<string> NoPics { get; set; }
        public List<string> YesPics { get; set; }
        public List<string> MissionAccomplished { get; set; }
        public List<string> WelcomePicsStart { get; set; }
        public List<string> WelcomePicsMid { get; set; }
        public List<string> WelcomePicsEnd { get; set; }
        public string FontTeams { get; set; }
        public string FontQ { get; set; }
        public int FontSizeTeams { get; set; } = 20;
        public int FontSizeHints { get; set; } = 20;
        public int FontSizeQ { get; set; } = 25;
        public int FontSizeSelectedTeams { get; set; } = 30;
        public int FontSizeExp { get; set; } = 40;
        public int FontSizeExpN { get; set; } = 50;
        public int FontSizeTeamsGame { get; set; } = 15;
        public string FontColorTeams { get; set; } = "0xe6e6e6ff";
        public string FontColorSelectedTeams { get; set; } = "0x9ec82ff";
        public string FontColorExp { get; set; } = "0xe6e6e6ff";
        public string FontColorCost { get; set; } = "0xff00ff";
        public string FontColorQ { get; set; } = "0xffffff";
        public string BackColorQ { get; set; } = "0x000000";
        public string FontColorHelp { get; set; } = "0x00ffff";
        public string FontColorMission { get; set; } = "0xff0000";
        public Pics() { }

        public void Serialize(string name)
        {

            var formatter = new XmlSerializer(typeof(Pics));
            using (FileStream fs = new FileStream(name, FileMode.Create))
            {
                formatter.Serialize(fs, this);
            }

        }

        public static Pics Deserialize(string file)
        {
            var formatter = new XmlSerializer(typeof(Pics));

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                return (Pics)formatter.Deserialize(fs);
            }
        }
    }
}
