﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }
        public void CreateQuizTemplate()
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "xml";
            if (sd.ShowDialog() == DialogResult.Cancel)
                return;
            string fileName = sd.FileName;
            
            Quiz quiz = new Quiz();
            quiz.Tours = new List<Tour>();
            for (int i=1; i<=3; i++)
            {
                Tour tour_ = new Tour();
                tour_.Expeditions = new List<Expedition>();
                for (int j=1; j<=6; j++)
                {
                    Expedition exped = new Expedition();
                    exped.Questions = new List<Question>();
                    exped.Name = $"Tour {i} Expedition {j}";
                    exped.HelpType = $"Help {j}";
                    for (int k=1; k<=4; k++)
                    {
                        Question q = new Question($"Expedition {j} Question {k}", $"Prompt {k}", k.ToString(), $"Hint to question {k} - 1\nHint to question {k} - 2", $"Form of question {k}");
                        q.AudioA = "";
                        q.AudioQ = "";
                        q.PictureA = "";
                        q.PictureQ = "";
                        q.Video = "";
                        q.Answer = $"Answer {k}";
                        exped.Questions.Add(q);
                    }
                    tour_.Expeditions.Add(exped);
                }
                quiz.Tours.Add(tour_);
            }
            quiz.Serialize(fileName);
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }
        static public Game game;
        static public Quiz quiz;
        static public Logs logs;
        static public Pics pics;
        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = SettingsForm.MakeRelative(saveFileDialog1.FileName, Directory.GetCurrentDirectory());
            game = new Game(filename);
            game.TourN = -1;
            SettingsForm startsettingsform = new SettingsForm();
            startsettingsform.Show();

        }

        public bool OpenPics()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return false;

            string filename = openFileDialog1.FileName;
            try
            {
                pics = Pics.Deserialize(filename);
            }
            catch
            {
                MessageBox.Show("Неправильный формат файла картинок");
                return false;
            }
            return true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            try
            {
                game = Game.Deserialize(filename);
            }
            catch
            {
                MessageBox.Show("Неправильный формат файла игры");
                return;
            }

            try
            {
                pics = Pics.Deserialize(game.PicsFile);
            }
            catch
            {
                MessageBox.Show("Неправильный формат файла картинок");
                return;
            }

            logs = new Logs(game.LogFile);
            if (game.TourN == -1)
            {
                SettingsForm startsettingsform = new SettingsForm();
                startsettingsform.Show();
                game.ScoresFinal = new List<Score>();
                game.ScoresTable = new List<int[]>();
                game.StalkersDelegated = new List<List<List<int>>>();
            }
            else
            {
                if (game.TourN < 3)
                    for (int i = game.ExpeditionN + 1; i < 6; i++)
                        for (int j = 0; j < game.Teams.Count; j++)
                            game.StalkersDelegated[game.TourN][i][j] = -1;
                GameForm gameForm = new GameForm();
                gameForm.Show();
            }
            game.FileName = filename;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateQuizTemplate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!OpenPics())
                return;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                var formatter = new XmlSerializer(typeof(List<Score>));
                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    List<Score> ScoreList = (List<Score>)formatter.Deserialize(fs);

                    ResultsByTour.scores = ScoreList;
                    ResultsByTour startsettingsform = new ResultsByTour();
                    startsettingsform.Show();

                }
            }
            catch
            {
                MessageBox.Show("Неправильный формат файла");
                return;
            }

        }
    }
}
