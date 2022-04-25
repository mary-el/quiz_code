using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    
    [Serializable]
    public class Logs
    {
        Game game = StartForm.game;
        public string FileName { get; set; }
        public Logs() { }
        public Logs(string file) { FileName = file; }

        public void CreateFile()
        {
            try
            {
                if (FileName != "")
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName))
                    {
                        string LogText = $"Игра " + System.DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
                        LogText += "\n Команды:\n";
                        foreach (Team t in StartForm.game.Teams)
                            LogText += t.ToString();
                        LogText += "Настройки:\n";
                        foreach (GameSettings t in game.SettingsL)
                            LogText += t.ToString();

                        file.WriteLine(LogText);
                    }
            }
            catch { }

        }
        public void AddTour(int i)
        {
            try
            {
                if (FileName != "")
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
                    {
                        file.WriteLine($"{System.DateTime.Now.ToString("dd.MM.yy HH:mm:ss")}\nТур №{i}\n");
                    }
            }
            catch { }

        }
        public void AddExp(List<int> PlayingStalkers, int[] current_scores, int expNum)
        {
            try
            {
                if (FileName != "")
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
                    {
                        file.WriteLine($"{System.DateTime.Now.ToString("dd.MM.yy HH:mm:ss")}\nЭкспедиция №{expNum}");
                        for (int i = 0; i < game.Stalkers; i++)
                        {
                            file.WriteLine($"{game.Teams[PlayingStalkers[i]].Short} +{current_scores[i]}");
                        }
                        file.WriteLine();
                    }
            }
            catch { }

        }
        public void AddResults(List<Results> results)
        {

                if (FileName != "")
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
                    {
                        file.WriteLine($"Результаты: ");
                        for (int i=results.Count -1; i>=0; i--)
                        {
                            file.WriteLine($"{results[i]}");
                        }
                    file.WriteLine();
                    }
        }
        
    }
    [Serializable]
    public class QuestionLog
    {
        public int TourN { get; set; }
        public int ExpeditionN { get; set; }
        public int QuestionN { get; set; }
        public int TeamAnswers { get; set; }
        public bool RightAnswer { get; set; }
        public int TeamGets { get; set; }
        public QuestionLog() { }
    }
}
