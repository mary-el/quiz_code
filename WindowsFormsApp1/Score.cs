using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Score
    {
        public int TeamN { get; set; }
        public int Place { get; set; }
        public int RealPlace { get; set; }
        public int PrevPlace { get; set; }
        public string Team { get; set; }
        public int Start { get; set; }
        public bool Invisible { get; set; }
        
        public int Tour11
        {
            get
            { return Rounds[0]; }
            set
            {
                Rounds[0] = value;
            }
        }
        public bool St11
        {
            get
            { return MissionAccomplished[0]; }
            set
            {
                MissionAccomplished[0] = value;
            }
        }
        public int Tour12
        {
            get
            { return Rounds[1]; }
            set
            {
                Rounds[1] = value;
            }
        }

        public bool St12
        {
            get
            { return MissionAccomplished[1]; }
            set
            {
                MissionAccomplished[1] = value;
            }
        }

        public int Tour13
        {
            get
            { return Rounds[2]; }
            set
            {
                Rounds[2] = value;
            }
        }


        public bool St13
        {
            get
            { return MissionAccomplished[2]; }
            set
            {
                MissionAccomplished[2] = value;
            }
        }

        public int Tour14
        {
            get
            { return Rounds[3]; }
            set
            {
                Rounds[3] = value;
            }
        }

        public bool St14
        {
            get
            { return MissionAccomplished[3]; }
            set
            {
                MissionAccomplished[3] = value;
            }
        }
        public int Tour21
        {
            get
            { return Rounds[4]; }
            set
            {
                Rounds[4] = value;
            }
        }

        public bool St21
        {
            get
            { return MissionAccomplished[4]; }
            set
            {
                MissionAccomplished[4] = value;
            }
        }
        public int Tour22
        {
            get
            { return Rounds[5]; }
            set
            {
                Rounds[5] = value;
            }
        }

        public bool St22
        {
            get
            { return MissionAccomplished[5]; }
            set
            {
                MissionAccomplished[5] = value;
            }
        }
        public int Tour23
        {
            get
            { return Rounds[6]; }
            set
            {
                Rounds[6] = value;
            }
        }

        public bool St23
        {
            get
            { return MissionAccomplished[6]; }
            set
            {
                MissionAccomplished[6] = value;
            }
        }
        public int Tour24
        {
            get
            { return Rounds[7]; }
            set
            {
                Rounds[7] = value;
            }
        }


        public bool St24
        {
            get
            { return MissionAccomplished[7]; }
            set
            {
                MissionAccomplished[7] = value;
            }
        }

        public int Tour31
        {
            get
            { return Rounds[8]; }
            set
            {
                Rounds[8] = value;
            }
        }


        public bool St31
        {
            get
            { return MissionAccomplished[8]; }
            set
            {
                MissionAccomplished[8] = value;
            }
        }
        public int Tour32
        {
            get
            { return Rounds[9]; }
            set
            {
                Rounds[9] = value;
            }
        }

        public bool St32
        {
            get
            { return MissionAccomplished[9]; }
            set
            {
                MissionAccomplished[9] = value;
            }
        }


        public int Tour33
        {
            get
            { return Rounds[10]; }
            set
            {
                Rounds[10] = value;
            }
        }

        public bool St33
        {
            get
            { return MissionAccomplished[10]; }
            set
            {
                MissionAccomplished[10] = value;
            }
        }
        public int Tour34
        {
            get
            { return Rounds[11]; }
            set
            {
                Rounds[11] = value;
            }
        }

        public bool St34
        {
            get
            { return MissionAccomplished[11]; }
            set
            {
                MissionAccomplished[11] = value;
            }
        }
        [XmlIgnore]
        public int[] RoundsFull { get; set; }
        [XmlIgnore]
        public bool[] MissionAccomplished { get; set; }
        [XmlIgnore]
        public int[] Rounds { get; set; }
        public int Sum { get; set; }
        public int LastAnswer { get; set; }
        public int OldLastAnswer { get; set; }
        public Score()
        {
            RoundsFull = new int[12];
            Rounds = new int[12];
            MissionAccomplished = new bool[12];
            LastAnswer = 100;
            OldLastAnswer = 100;
            Invisible = false;
        }
        public void RecountSum()
        {
            int sum = Start;
            for (int i=0;i <12; i++)
            {
                RoundsFull[i] = Rounds[i];
                //if (MissionAccomplished[i] == true)
                //    RoundsFull[i] += Rounds[i];
                sum += RoundsFull[i];
            }
            Sum = sum;
            if (Start < 0)
                Invisible = true;
            else
                Invisible = false;
        }

    }

    public class Results
    {
        public int Place { get; set; }
        public string ShowPlaceStr { get; set; }
        public int ShowPlace { get; set; }
        public string team { get; set; }
        public int ScoreBeforeTour { get; set; }
        public string ScoreBeforeTourStr { get; set; }
        public int CurrentTeamScore { get; set; }
        public int CurrentStalkersScore { get; set; }

        public int Round1
        {
            get
            { return Rounds[0]; }
            set
            {
                Rounds[0] = value;
            }
        }
        public int Round2
        {
            get
            { return Rounds[1]; }
            set
            {
                Rounds[1] = value;
            }
        }

        public int Round3
        {
            get
            { return Rounds[2]; }
            set
            {
                Rounds[2] = value;
            }
        }

        public int Round4
        {
            get
            { return Rounds[3]; }
            set
            {
                Rounds[3] = value;
            }
        }
        public bool[] MissionAccomplished { get; set; }
        public int[] Rounds { get; set; }
        public int Sum { get; set; }
        public Results() {
            Rounds = new int[4];
            MissionAccomplished = new bool[4];
        }
        public override string ToString()
        {
            string text = $"{Place} :  {team}  (ОДТ = {ScoreBeforeTour}, ОНК = {CurrentTeamScore}, ОНС = {CurrentStalkersScore}, Сумма = {Sum})";
            return text;
        }


    }

}
