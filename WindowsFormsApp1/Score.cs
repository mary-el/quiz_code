using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    [Serializable]
    public class Score
    {
        public int TeamN { get; set; }
        public int Place { get; set; }
        public int PrevPlace { get; set; }
        public string Team { get; set; }
        public int Start { get; set; }
        public bool Invisible { get; set; }
        
        public int Tour1
        {
            get
            { return Tours[0]; }
            set
            {
                Tours[0] = value;
            }
        }

        public int Tour1St
        { get
            { return ToursSt[0]; }
          set
            {
                ToursSt[0] = value;
            }
        }
        public int Tour2
        {
            get
            { return Tours[1]; }
            set
            {
                Tours[1] = value;
            }
        }
        public int Tour2St
        {
            get
            { return ToursSt[1]; }
            set
            {
                ToursSt[1] = value;
            }
        }
        public int Tour3St
        {
            get
            { return ToursSt[2]; }
            set
            {
                ToursSt[2] = value;
            }
        }
        public int Tour3
        {
            get
            { return Tours[2]; }
            set
            {
                Tours[2] = value;
            }
        }
        public int[] ToursSt { get; set; }
        public int[] Tours { get; set; }
        public int Sum { get; set; }
        public int LastAnswer { get; set; }
        public int OldLastAnswer { get; set; }       
           
        public Score()
        {
            ToursSt = new int[3];
            Tours = new int[3];
            LastAnswer = 100;
            OldLastAnswer = 100;
            Invisible = false;
        }
        public void RecountSum()
        {
            int sum = Start;
            for (int i=0;i <3; i++)
            {
                sum += ToursSt[i];
                sum += Tours[i];
            }

            Sum = sum;

        }


    }

    public class Results
    {
        public int Place { get; set; }
        public string team { get; set; }
        public int ScoreBeforeTour { get; set; }
        public int CurrentTeamScore { get; set; }
        public int CurrentStalkersScore { get; set; }
        public int Sum { get; set; }
        public Results() { }
        public override string ToString()
        {
            string text = $"{Place} :  {team}  (ОДТ = {ScoreBeforeTour}, ОНК = {CurrentTeamScore}, ОНС = {CurrentStalkersScore}, Сумма = {Sum})";
            return text;
        }


    }

}
