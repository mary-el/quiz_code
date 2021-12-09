using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Windows.Forms.Integration;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class GameForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public GameForm()
        {

            InitializeComponent();
            //listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            //listBox1.DrawItem += ListBox1_DrawItem;
            /*
            listBox2.DrawMode = DrawMode.OwnerDrawFixed;
            listBox2.DrawItem += ListBox2_DrawItem;

            listBox3.DrawMode = DrawMode.OwnerDrawFixed;
            listBox3.DrawItem += ListBox3_DrawItem;
            */
        }

        /*
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, Items[e.Index].ToString(), e.Font,
                e.Bounds, e.ForeColor, e.BackColor, TextFormatFlags.HorizontalCenter);
        }
        */

        public Quiz quiz;
        public Game game;
        public Pics pics;
        int current_tour;
        int current_exp;
        int real_current_exp;
        int current_q;
        public int TimerT;
        public int TimerLeft;
        public int[] costsTable;
        public int bonus;
        public int[,] ArrayA;
        public List<Score> ScoreList;
        public int[] PlayingStalkers;
        public int PlayingStalkerNum;
        public int[] answered;
        int answered_count;
        public int[] current_scores;
        public int[] current_places;
        public List<int[]> score_num;
        public List<Score> AllScores;
        public List<Results> results;
        public List<Results> results_showed;
        public bool IndTour;
        public int WelcomN = 0;
        public int NoN = 0;
        public int currentYesPict = 0;
        public string[] answ_pics;
        public int media_state = 0;
        public string url = "";
        public int ExpInTour = 6;
        public int StalkerOrderMethod = 0;
        bool ResultsChanged = false;
        public int BestStalkerTeam = -1;
        public int BestStalkerValue = -1;
        public int BestStalkerQ = -1;
        public string BestStalkerAnswer = "";
        public void SetPictQ(Image pict)
        {
            if (pict is null)
            {
                pictureBox1.Visible = false;
                pictureBox1.Image = null;
                pictureBox1.Visible = false;
                qTextBox11.Margin = new Padding(50);
                qTextBox11.Width = flowLayoutPanel2.Width - 2 * qTextBox11.Margin.Right;
                return;
            }

            qTextBox11.Margin = new Padding(15);
            double x = (float)pict.Width / pict.Height;
            double y = (float)flowLayoutPanel1.Width / flowLayoutPanel1.Height;
            if (x < 0.6 * y)
            {
                pictureBox1.Height = (int)(flowLayoutPanel2.Height);
                pictureBox1.Width = (int)(pictureBox1.Height * x);
                qTextBox11.Width = flowLayoutPanel2.Width - pictureBox1.Width - 2*pictureBox1.Margin.Horizontal - 2*qTextBox11.Margin.Horizontal - 10;
            }
            else
            if (x < 4)
            {
                pictureBox1.Width = (int)(flowLayoutPanel2.Width * 0.6);
                pictureBox1.Height = (int)(pictureBox1.Width / x);
                qTextBox11.Width = flowLayoutPanel2.Width - pictureBox1.Width - 2 * pictureBox1.Margin.Horizontal - 2 * qTextBox11.Margin.Horizontal - 10;
            }
            else
            {
                pictureBox1.Width = flowLayoutPanel2.Width;
                pictureBox1.Height = (int)(pictureBox1.Width / x);
                qTextBox11.Width = flowLayoutPanel2.Width;

            }
            pictureBox1.Image = pict;
        }
        
        public void NextQuestion(int step)
        {
            current_q += step;
            media_state = 0;
            url = "";
            WMPq.Ctlcontrols.stop();
            if ((current_exp == game.ExpeditionN) && (current_q > game.QuestionN))
            {
                game.QuestionN = current_q;
            }
            if (current_q == -1)
            {
                qTextBox11.Clear();
                qTextBox11.Visible = false;
                pictureBox1.Image = null;
                label16.Text = "";
                label6.Text = "";
                return;
            }
            Question q = quiz.Tours[current_tour].Expeditions[current_exp].Questions[current_q];
            label16.Text = "+" + game.SettingsL[current_tour].Cost[current_q].ToString();
            TimerT = game.Timer[current_q];
            TimerLeft = TimerT;
            label6.Text = TimerT.ToString() + "''";

            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            qTextBox11.Text = "";
            
            qTextBox11.Rtf = @"{\rtf1\ansi\ansicpg1251 " + q.Text + "}";
            
            //myRT1.SetQ(q.Text);
            string rtf = @"{\rtf1\ansi\ansicpg1251 " + q.Text + "}";
            byte[] bytes = Encoding.Default.GetBytes(rtf);
            MemoryStream stream = new MemoryStream(bytes);

            //myRT1.SetRTFText(stream);
            qTextBox11.Visible = true;
            pictureBox1.Visible = true;

            try
            {
                if (!string.IsNullOrEmpty(q.PictureQ))
                {
                    Image pict = Image.FromFile(q.PictureQ);
                    SetPictQ(pict);
                }
                else
                if (!string.IsNullOrEmpty(q.AudioQ))
                {
                    if (!File.Exists(q.AudioQ))
                        throw new Exception();
                    pictureBox1.Image = Image.FromFile(pics.MusicPict);
                    qTextBox11.Visible = false;
                    pictureBox1.Size = flowLayoutPanel2.Size;
                    media_state = 1;
                    url = q.AudioQ;
                }
                else
                    if (!string.IsNullOrEmpty(q.Video))
                {
                    if (!File.Exists(q.Video))
                        throw new Exception();
                    media_state = 4;
                    pictureBox1.Image = Image.FromFile(pics.VideoPict);
                    qTextBox11.Visible = false;
                    pictureBox1.Size = flowLayoutPanel2.Size;
                    url = q.Video;
                }
                else
                {
                    SetPictQ(null);
                }
            }
            catch
            {
                ShowMessage("Невозможно открыть медиафайл");
                pictureBox1.Image = null;
            }
            int textWidth = (int)(TextRenderer.MeasureText(qTextBox11.Text, qTextBox11.Font).Width * 1.2);
            int textHeight = TextRenderer.MeasureText(qTextBox11.Text,qTextBox11.Font).Height;
            // Pad the text and resize the control.
            int lines = textWidth / qTextBox11.Width;
            if (textWidth % qTextBox11.Width != 0)
                lines++;
            //qTextBox1.ClientSize = new Size(qTextBox1.ClientSize.Width, Math.Min(textHeight * lines + qTextBox1.Margin.Top, flowLayoutPanel2.Height - qTextBox1.Margin.Top));
            qTextBox11.Height = Math.Min(textHeight * lines + qTextBox11.Margin.Top, flowLayoutPanel2.Height - qTextBox11.Margin.Top);
            
        }
        public Label[] labels;
        public Label[] helps;
        public Label[] costs;
        public Label[] hints_labels;
        public Label[] forms_labels;
        public PictureBox[] hints_pics;
        public Button[] buttons;
        public List<Button> teamb;
        public Logs logs;
        public Dictionary<int, Team> intToTeam;
        public bool enabled_game;
        public void SetButtonsColours()
        {

            int next_exp = current_exp + 1;
            int next_tour = current_tour + (next_exp / ExpInTour);
            next_exp %= ExpInTour;
            int num = 0;
            for (int i = 0; i < teamb.Count; i++)
            {
                if (game.StalkersDelegated[next_tour][next_exp][i] > 0)
                {
                    teamb[i].ForeColor = TeamsSelectedColor;
                    teamb[i].Font = fontSelectedTeams;
                    num++;
                }
                else
                {
                    teamb[i].ForeColor = TeamsColor;
                    teamb[i].Font = fontTeams;
                }
            }
            if ((num == game.Stalkers) || (game.Round3On && next_tour == 2 && next_exp <= 1))
                enabled_game = true;
            else
                enabled_game = false;

        }

        public void ResetStalkers()
        {
            PlayingStalkers = new int[game.Stalkers];
            for (int i = 0; i < game.Teams.Count; i++)
            {
                int num = game.StalkersDelegated[current_tour][current_exp][i];
                if (num > 0)
                {
                    listBox1.Items[num - 1] = game.Teams[i].Name;
                    PlayingStalkers[num - 1] = i;
                    if ((!game.Round3On) || current_tour != 2 || (current_exp > 1))
                        listBox2.Items[num - 1] = game.Teams[i].Name.ToUpper();
                    else
                        listBox2.Items[num - 1] = "";
                    if (current_exp < game.ExpeditionN)
                        listBox3.Items[num - 1] = score_num[current_exp][num - 1];

                }

            }
        }
        public void NextExpedition(int step)
        {
            StalkerOrderMethod = game.SettingsL[current_tour].StalkersMethod;
            label6.Text = "";
            label16.Text = "";
            timer1.Enabled = false;
            current_exp += step;
            if (step <= 1)
                real_current_exp += step;
            current_q = -1;
            NextQuestion(0);
            for (int i=0; i<game.Teams.Count; i++)
            {
                int q = game.StalkersDelegated[game.TourN][game.ExpeditionN][i];
                if (q > 0)
                    ArrayA[i, q - 1]++;
            }

            if (BestStalkerValue > game.BestStalkerValue)
            {
                game.BestStalkerValue = BestStalkerValue;
                game.BestStalkerExp = current_exp;
                game.BestStalkerQ = BestStalkerQ;
                game.BestStalkerTeam = BestStalkerTeam;
                game.BestStalkerTour = current_tour;
                game.BestStalkerAnswer = BestStalkerAnswer;
            }
            BestStalkerTeam = -1;
            BestStalkerValue = -1;
            BestStalkerQ = -1;
            BestStalkerAnswer = "";

            if (current_tour == 3)
            {
                game.Serialize();
                return;
            }
            if (current_exp > game.ExpeditionN || game.ExpeditionN == 0)
            {

                if (current_exp == ExpInTour)
                {
                    if (current_tour == 2)
                    {
                        logs.AddExp(PlayingStalkers, current_scores, real_current_exp);
                        tabControl1.SelectedTab = tabPage6;
                        NextTour(1);
                        return;
                    }
                    NextTour(1);
                    tabControl1.SelectedTab = tabPage6;
                }
                game.ExpeditionN = current_exp;
                game.RealExpN = real_current_exp;
                if (!(game.Round3On) || current_exp > 1 || current_tour != 2) 
                    GetStalkers();

                if (game.ExpeditionN == 1)
                {
                    logs.AddTour(game.TourN + 1);
                }
                if (real_current_exp > 0)
                {
                    score_num.Add((int[])current_scores.Clone());
                    if (current_exp != 0)
                        tabControl1.SelectedTab = tabPage1;
                    logs.AddExp(PlayingStalkers, current_scores, real_current_exp);
                }
            }

            ClearAnswers();
            ResetStalkers();
            listBox2.selected_n = 0;
            answered_count = 0;
            Expedition Exp = quiz.Tours[current_tour].Expeditions[current_exp];

            string ExpName = Exp.Name;
            int expN = real_current_exp + 1;
            foreach (var lab in labels)
                lab.Text = ExpName;
            labels[1].Text = $"Раунд {expN}: {ExpName}";
            labels[2].Text = $"Раунд {expN}: {ExpName}";
            label14.Text = $"Раунд {expN}";
            int next_exp = current_exp + 1;
            int next_tour = current_tour + (next_exp / ExpInTour);
            next_exp %= ExpInTour;
            if (next_tour == 3)
            {

                label5.Text = "";
                foreach (Button but in teamb)
                    but.BackColor = Color.Transparent;
            }
            else
            {
                label5.Text = quiz.Tours[next_tour].Expeditions[next_exp].Name;
                // label15.Text = $"Раунд {(current_exp + current_tour * 6 + 2)}";

                label15.Text = $"Раунд {real_current_exp + 2}";
                SetButtonsColours();
            }
            if (game.Round3On)
            {
                if (next_tour == 2 && next_exp <= 1)
                {
                    label5.Text = "Сталкеры не играют этот и следующий раунды";
                    SetButtonsColours();
                }
                if (current_exp <= 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        buttons[i].Text = "";
                        helps[i].Text = "";
                    }

                }
            }
            for (int i = 0; i < 4; i++)
            {
                buttons[i].Text = Exp.Questions[i].Prompt;
                buttons[i].Font = fontTeams;
                buttons[i].ForeColor = ColorQ;
                if (IndTour)
                {
                    if (answered[i] == 0)
                        helps[i].Text = Exp.Questions[i].Help;
                    else helps[i].Text = "";
                    label7.Text = Exp.HelpType;
                }
                else
                {
                    helps[i].Text = "";
                    label7.Text = "";
                }

                costsTable[i] = game.SettingsL[current_tour].CostsSt[i] * (Math.Max(PlayingStalkerNum, 0) + 2);
            }

            foreach (Score s in ScoreList)
            {
                s.RecountSum();
                s.PrevPlace = s.Place;
                s.OldLastAnswer = s.LastAnswer;
            }
            CalculatePlaces();
            game.Serialize();
        //  dataGridView1.Sort(dataGridView1.Columns[8], ListSortDirection.Descending);

    }
        public void NextTour(int step)
        {
            current_tour += step;
            WelcomePics = new List<Image>();
            if (current_tour == 0)
                foreach (String s in pics.WelcomePicsStart)
                    try
                    {
                        WelcomePics.Add(Image.FromFile(s));
                    }
                    catch { }
            else
                foreach (String s in pics.WelcomePicsMid)
                    try
                    {
                        WelcomePics.Add(Image.FromFile(s));
                    }
                    catch { }           
            
            if (current_tour > game.TourN)
            {
                current_exp = 0;
                game.ExpeditionN = current_exp;
                game.RealExpN = real_current_exp;
                game.TourN = current_tour;
            }
            if (current_tour == 3)
            {
                WelcomePics = new List<Image>();
                foreach (String s in pics.WelcomePicsEnd)
                    try
                    {
                        WelcomePics.Add(Image.FromFile(s));
                    }
                    catch { }
                tabControl1.TabPages[3].Enabled = false;
                tabControl1.TabPages[4].Enabled = false;
                tabControl1.TabPages[5].Enabled = false;
                tabControl1.TabPages[6].Enabled = false;
                return;
            }
            bonus = game.SettingsL[current_tour].Bonus;
            costsTable = new int[4];
            ExpInTour = game.SettingsL[current_tour].Length;
            StalkerOrderMethod = game.SettingsL[current_tour].StalkersMethod;
            for (int i = 0; i < 4; i++)
                costsTable[i] = game.SettingsL[current_tour].CostsSt[i] * 2;
            IndTour = game.SettingsL[current_tour].Individual;
            
        }

        private void buttonT_Click(object sender, System.EventArgs e)
        {

            int next_exp = current_exp + 1;
            int next_tour = current_tour + (next_exp / ExpInTour);
            next_exp %= ExpInTour;
            Button but = sender as Button;
            if ((next_tour != 3) && (!game.Round3On || current_tour != 2 || current_exp != 0))
            {
                game.StalkersDelegated[next_tour][next_exp][(int)but.Tag] *= -1;
                SetButtonsColours();
            }
        }

        public List<int> BlackBox(List<int> Sd)
        {
            int[,] arr = new int[game.Teams.Count, 2];
            int[] plays = new int[game.Teams.Count];
            for (int i = 0; i < game.Teams.Count; i++)
                if (Sd[i] > 0)
                {
                    plays[i] = 1;
                    for (int j = 0; j < game.Stalkers; j++)
                    {
                        if (ArrayA[i, j] == 2)
                            arr[i, 0]++;
                        if (ArrayA[i, j] == 1)
                            arr[i, 1]++;

                    }
                    if (arr[i, 0] + arr[i, 1] >= game.Stalkers)
                        for (int j = 0; j < game.Stalkers; j++)
                            ArrayA[i, j]--;
                }
            List<int> StalkersI = new List<int>();
            List<Team> StalkersCurrent = new List<Team>(game.Stalkers);
            List<int> StalkersQ = new List<int>(game.Stalkers);
            StalkersI.Clear();
            for (int i = 0; i < game.Stalkers; i++)
                StalkersI.Add(-1);

            while (Enumerable.Sum(plays) > 0)
            {
                int max2 = 0;
                int max1 = 0;
                StalkersQ.Clear();
                for (int i = 0; i < game.Teams.Count; i++)
                {
                    if (plays[i] == 1)
                    {
                        if (arr[i, 0] == max2 && (arr[i, 1] == max1))
                            StalkersQ.Add(i);
                        else
                        if (arr[i, 0] > max2)
                        {
                            max2 = arr[i, 0];
                            max1 = arr[i, 1];
                            StalkersQ.Clear();
                            StalkersQ.Add(i);
                        }
                        else
                        if (arr[i, 0] == max2 && arr[i, 1] > max1)
                        {
                            max1 = arr[i, 1];
                            StalkersQ.Clear();
                            StalkersQ.Add(i);

                        }
                    }
                }
                Random rand = new Random();

                foreach (int k in StalkersQ)
                {
                    List<int> ArrZ = new List<int>();
                    int min_ = 100;
                    for (int i = 0; i < game.Stalkers; i++)
                    {
                        if (StalkersI[i] == -1)
                        {
                            if (ArrayA[k, i] < min_)
                            {
                                ArrZ.Clear();
                                min_ = ArrayA[k, i];
                            }
                            if (ArrayA[k, i] == min_)
                                ArrZ.Add(i);
                        }
                    }
                    int ind = ArrZ[rand.Next(ArrZ.Count)];
                    StalkersI[ind] = k;
                    plays[k] = 0;

                }
            }
            return StalkersI;
        }

        public List<int> ByPlace(List<int> Sd, int Method)
        {
            List<int> StalkersI = new List<int>();
            for (int i = 0; i < game.Teams.Count; i++)
                if (Sd[i] > 0)
                    StalkersI.Add(i);
            if (Method == 2)
                StalkersI = StalkersI.OrderByDescending(u => ScoreList[u].Place).ToList();
            else
                if (Method == 3)
                    StalkersI = StalkersI.OrderBy(u => ScoreList[u].Place).ToList();
            else
            {
                StalkersI = StalkersI.OrderBy(u => game.Teams[u].Group).ThenByDescending(u => ScoreList[u].Place).ToList();
            }
            return StalkersI;
        }
        

        public void GetStalkers()
        {
            List<int> Sd = game.StalkersDelegated[game.TourN][game.ExpeditionN];
            if (Sd.Max() > 1)
                return;
            List<int> StalkersI;
            if (StalkerOrderMethod == 1)
                StalkersI = BlackBox(Sd);
            else
                StalkersI = ByPlace(Sd, StalkerOrderMethod);

            for (int i = 0; i < StalkersI.Count; i++)
            {
                // ArrayA[StalkersI[i], i]++;
                game.StalkersDelegated[game.TourN][game.ExpeditionN][StalkersI[i]] = i;
            }
            for (int i = 0; i < game.Teams.Count; i++)
                game.StalkersDelegated[game.TourN][game.ExpeditionN][i] += 1;           
        }

        public void CalculatePlaces()
        {
            ScoreList = ScoreList.OrderBy(u => u.Invisible).ThenByDescending(u => u.Sum).ThenBy(u => u.TeamN).ToList();
            for (int i = 0; i < ScoreList.Count; i++)
            {
                ScoreList[i].Place = i + 1;
            }
            ScoreList = ScoreList.OrderBy(u => u.TeamN).ToList();
            dataGridView1.Refresh();
        }

        PrivateFontCollection fontCollection = new PrivateFontCollection();
        Font fontExpName;
        Font fontTeams;
        Font fontSelectedTeams;
        Font fontTeamsGame;
        Font fontExpN;
        Font fontQ;
        Font fontCost;
        Font fontHints;
        Color TeamsColor;
        Color TeamsSelectedColor;
        Color ExpColor;
        Color CostColor;
        Color ColorQ;
        Color BackColorQ;
        Color ColorHelp;
        

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void SetButtonsSizes()
        {
            int TeamN = game.Teams.Count;
            int teamb_w = flowLayoutPanel1.Width / Math.Min(4, TeamN) - 10;
            int teamb_h = flowLayoutPanel1.Height / (int)Math.Ceiling((double)TeamN / 4) - 10;
            foreach (Button but in teamb)
            {

                but.Width = teamb_w;
                but.Height = teamb_h;
            }

        }

        private void CreateTeamsButton()
        {
            int TeamN = game.Teams.Count;
            int teamb_w = flowLayoutPanel1.Width / Math.Min(4, TeamN) - 10;
            int teamb_h = flowLayoutPanel1.Height / (int)Math.Ceiling((double)TeamN / 4) - 10;
            flowLayoutPanel1.Controls.Clear();
            teamb.Clear();

            for (int i = 0; i < TeamN; i++)
            {
                if (ScoreList.Count == i)
                {
                    Score sc = new Score();
                    sc.TeamN = i;
                    sc.Team = game.Teams[i].Name;
                    ScoreList.Add(sc);
                }
                Button but = new Button();
                but.Tag = i;
                flowLayoutPanel1.Controls.Add(but);
                but.Width = teamb_w;
                but.Height = teamb_h;
                but.Click += new EventHandler(buttonT_Click);
                but.Font = fontTeamsGame;
                teamb.Add(but);
                Team team = game.Teams[i];
                but.Text = team.Short.ToUpper();
                but.FlatStyle = FlatStyle.Flat;
                but.FlatAppearance.BorderSize = 0;
                but.FlatAppearance.MouseOverBackColor = Color.Transparent;
                but.FlatAppearance.MouseDownBackColor = Color.Transparent;
                but.TabStop = false;
                but.BackColor = Color.Transparent;
                but.ForeColor = TeamsColor;
                if (ScoreList[i].Invisible == true)
                    but.Visible = false;
            }
        }
        private void GameForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            //CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            //CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
            this.DoubleBuffered = true;
            this.AllowTransparency = true;
            this.Opacity = 100;
            listBox3.Width = (int)(listBox3.Parent.Width * 0.2);
            tabControl1.ItemSize = new Size(0, 1);
            WMPq.BringToFront();
            game = StartForm.game;
            pics = StartForm.pics;
            FontFamily familyTeams;
            FontFamily familyQ;
            try
            {
                fontCollection.AddFontFile(pics.FontTeams);
                fontCollection.AddFontFile(pics.FontQ);
                familyTeams = fontCollection.Families[1];
                familyQ = fontCollection.Families[0];
            }
            catch
            {
                familyTeams = new FontFamily("Times New Roman");
                familyQ = new FontFamily("Times New Roman");
            }
            fontExpName = new Font(familyTeams, pics.FontSizeExp);
            fontTeams = new Font(familyTeams, pics.FontSizeTeams);
            fontSelectedTeams = new Font(familyTeams, pics.FontSizeSelectedTeams);
            fontTeamsGame = new Font(familyTeams, pics.FontSizeTeamsGame);
            fontExpN = new Font(familyTeams, pics.FontSizeExpN);
            fontQ = new Font(familyQ, pics.FontSizeQ);
            fontCost = new Font(familyTeams, pics.FontSizeExp);
            fontHints = new Font(familyQ, pics.FontSizeHints);
            
            TeamsColor = ColorTranslator.FromHtml(pics.FontColorTeams);
            TeamsSelectedColor = ColorTranslator.FromHtml(pics.FontColorSelectedTeams);
            ExpColor = ColorTranslator.FromHtml(pics.FontColorExp);
            CostColor = ColorTranslator.FromHtml(pics.FontColorCost);
            ColorQ = ColorTranslator.FromHtml(pics.FontColorQ);
            BackColorQ = ColorTranslator.FromHtml(pics.BackColorQ);
            ColorHelp = ColorTranslator.FromHtml(pics.FontColorHelp);
            

            qTextBox11.Font = fontQ;
            qTextBox11.BackColor = BackColorQ;
            qTextBox11.ForeColor = ColorQ;
            
            label29.Font = fontCost;
            label29.ForeColor = CostColor;
            label16.Font = fontCost;
            label16.ForeColor = CostColor;
            label6.Font = fontCost;
            label6.ForeColor = CostColor;

            score_num = game.ScoresTable;
            ScoreList = game.ScoresFinal;
            current_tour = game.TourN;
            current_exp = game.ExpeditionN;
            real_current_exp = game.RealExpN;
            current_q = game.QuestionN;

            YesPlace.BringToFront();
            quiz = Quiz.Deserialize(game.QuizFile);
            panel1.BringToFront();
            panel1.Visible = false;
            WMPq.BringToFront();
            WMPa.Visible = false;
            WMPq.Visible = false;
            //pictureBox8.BringToFront();
            //pictureBox8.Image = Image.FromFile(pics.MusicPict);
            //pictureBox8.Visible = false;

            ArrayA = new int[game.Teams.Count, game.Stalkers];
            logs = StartForm.logs;
            current_exp = game.ExpeditionN;
            if ((game.TourN >= 0) && (game.TourN!=3))
            {
                for (int k = 0; k <= game.TourN; k++)
                    for (int i = 0; i <= game.ExpeditionN; i++)
                        for (int j = 0; j < game.Teams.Count; j++)
                        {
                            int place = game.StalkersDelegated[k][i][j];
                            if (place > 0)
                                ArrayA[j, place - 1]++;
                        }

            }
            if (game.TourN == -1)
                game.TourN = 0;
            current_tour = game.TourN;
            teamb = new List<Button>();
            labels = new Label[] { label1, label2, label3 };
            hints_labels = new Label[] { label10, label13, label25, label27 };
            forms_labels = new Label[] { label12, label24, label26, label28 };
            hints_pics = new PictureBox[] { pictureBox9, pictureBox11, pictureBox12, pictureBox13 };


            for (int i = 0; i < 4; i++)
            {
                hints_labels[i].Font = fontHints;
                hints_labels[i].ForeColor = ColorQ;
                forms_labels[i].Font = fontHints;
                forms_labels[i].ForeColor = CostColor;
            }

            buttons = new Button[] { button1, button2, button3, button4 };
            costs = new Label[] { label4, label17, label18, label19 };
            foreach (Label c in costs)
                c.Click += new EventHandler(CostLabelClick);
            helps = new Label[] { label20, label21, label22, label23 };
            costsTable = new int[4];
            results = new List<Results>();
            results_showed = new List<Results>();
            answered = new int[game.Stalkers];
            current_scores = new int[game.Stalkers];
            answ_pics = new string[] { pics.YesGoldPict, pics.YesSilverPict, pics.YesBronzePict };
            ScoreList = game.ScoresFinal;
            int TeamN = game.Teams.Count;
            CreateTeamsButton();

            for (int i = 0; i < game.Stalkers; i++)
            {
                listBox1.Items.Add("");
                listBox2.Items.Add("");
                listBox3.Items.Add("");
            }
            GetResults();
            current_q = -1;
            NextTour(0);
            if (game.TourN < 3)
            {
                NextExpedition(0);
                NextQuestion(0);
            }
            scoreBindingSource.DataSource = ScoreList;
            dataGridView1.DataSource = scoreBindingSource;
            resultsBindingSource.DataSource = results_showed;
            myDataGridView1.DataSource = resultsBindingSource;
            WelcomN = 0;
            try
            {
                pictureBox2.Image = WelcomePics[WelcomN];
            }
            catch
            {

            }

            for (int i = 0; i < 4; i++)
            {
                buttons[i].Click += new EventHandler(AnswerQ);
                buttons[i].Tag = i;
            }
            if (!string.IsNullOrEmpty(pics.Logo))
            {
                try
                {
                    Image logo = Image.FromFile(pics.Logo);
                    pictureBox3.Image = logo;
                    pictureBox4.Image = logo;
                    pictureBox5.Image = logo;
                    pictureBox6.Image = logo;
                    pictureBox8.Image = logo;
                    this.Icon = MakeIcon(logo, 50, true);
                }
                catch
                { }
            }
            try
            {
                tableLayoutPanel1.BackgroundImage = Image.FromFile(pics.BackgroundExp);
                tableLayoutPanel2.BackgroundImage = Image.FromFile(pics.BackgroundQ);                
                //tabPage2.BackgroundImage = Image.FromFile(pics.BackgroundQ);
                tableLayoutPanel4.BackgroundImage = Image.FromFile(pics.BackgroundAn);
                tableLayoutPanel5.BackgroundImage = Image.FromFile(pics.BackgroundSt);
                panel1.BackgroundImage = Image.FromFile(pics.BackgroundSt);
                tableLayoutPanel12.BackgroundImage = Image.FromFile(pics.BackgroundRes);
                tableLayoutPanel13.BackgroundImage = Image.FromFile(pics.BackgroundRepeat);
                tableLayoutPanel13.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { }
            YesPlace.Visible = false;
                SetDoubleBuffered(tableLayoutPanel2);
                SetDoubleBuffered(tableLayoutPanel1);
                SetDoubleBuffered(tableLayoutPanel19);
                SetDoubleBuffered(tableLayoutPanel1);
                SetDoubleBuffered(tableLayoutPanel20);
                SetDoubleBuffered(panel1);
                SetDoubleBuffered(tableLayoutPanel3);
                SetDoubleBuffered(tableLayoutPanel17);
                SetDoubleBuffered(tableLayoutPanel8);
                SetDoubleBuffered(tableLayoutPanel4);
                SetDoubleBuffered(tableLayoutPanel5);
                SetDoubleBuffered(tableLayoutPanel6);
                SetDoubleBuffered(tableLayoutPanel7);
                SetDoubleBuffered(tableLayoutPanel8);
                SetDoubleBuffered(tableLayoutPanel9);
                SetDoubleBuffered(tableLayoutPanel10);
                SetDoubleBuffered(tableLayoutPanel11);
                SetDoubleBuffered(tableLayoutPanel12);
                SetDoubleBuffered(tableLayoutPanel16);
                SetDoubleBuffered(YesPlace);
                SetDoubleBuffered(tableLayoutPanel13);
                SetDoubleBuffered(tableLayoutPanel15);
                SetDoubleBuffered(flowLayoutPanel1);
                SetDoubleBuffered(flowLayoutPanel2);
                SetDoubleBuffered(flowLayoutPanel3);
                SetDoubleBuffered(flowLayoutPanel4);
                SetDoubleBuffered(flowLayoutPanel5);
                SetDoubleBuffered(flowLayoutPanel6);
                SetDoubleBuffered(flowLayoutPanel7);
                SetDoubleBuffered(tableLayoutPanel18);
                SetDoubleBuffered(myDataGridView1);
                SetDoubleBuffered(label5);
                SetDoubleBuffered(label1);
                SetDoubleBuffered(label14);


            foreach (Score s in ScoreList)
                s.RecountSum();
            CalculatePlaces();

            foreach (Score s in ScoreList)
            {
                s.PrevPlace = s.Place;
                s.OldLastAnswer = s.LastAnswer;
            }
            foreach (Label lab in labels)
            {
                lab.Font = fontExpName;
                lab.ForeColor = ExpColor;
            }
            label5.Font = fontExpName;
            label5.ForeColor = ExpColor;
            label14.Font = fontExpN;
            label14.ForeColor = ExpColor;
            label15.Font = fontExpN;
            label15.ForeColor = ExpColor;

            myDataGridView1.ColumnHeadersDefaultCellStyle.Font = fontTeams;
            myDataGridView1.RowTemplate.DefaultCellStyle.Font = fontTeams;
            myDataGridView1.RowHeadersDefaultCellStyle.Font = fontTeams;
            myDataGridView1.Font = fontSelectedTeams;
            myDataGridView1.ForeColor = ColorQ;
            myDataGridView1.Columns[5].DefaultCellStyle.ForeColor = TeamsSelectedColor;
            myDataGridView1.Columns[6].DefaultCellStyle.ForeColor = ColorHelp;
            // myDataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            myListBox[] lbs = new myListBox[] {listBox1, listBox2, listBox3 };
            listBox1.Font = fontSelectedTeams;
            listBox2.Font = fontTeamsGame;
            listBox3.Font = fontTeamsGame;
            foreach (myListBox lb in lbs)
            {
                lb.DrawMode = DrawMode.OwnerDrawVariable;
                lb.ItemHeight = (int)(lb.Font.Height * 1.5);
                lb.ForeColor = TeamsColor;
            }
            foreach (Label lab in costs)
            {
                lab.Font = fontCost;
                lab.ForeColor = CostColor;
            }

            foreach (Label lab in helps)
            {
                lab.Font = fontCost;
                lab.ForeColor = ColorHelp;
            }
            listBox2.SelectedColor = TeamsSelectedColor;
            listBox2.SelectedFont = fontSelectedTeams;
            listBox2.selected_n = 0;
            label7.Font = fontSelectedTeams;
            label7.ForeColor = ColorHelp;
            YesPlace.BringToFront();

            label8.Font = fontSelectedTeams;
            label8.ForeColor = TeamsSelectedColor;
            label9.Font = fontSelectedTeams;
            label9.ForeColor = TeamsColor;
            label11.Font = fontSelectedTeams;
            label11.ForeColor = TeamsColor;
            myDataGridView1.RowTemplate.Height = (int)((myDataGridView1.Height - myDataGridView1.ColumnHeadersHeight) / 12);
            
        }
        private void AddScore(int num, int cost)
        {
            current_scores[num] += cost;
            ScoreList[PlayingStalkers[num]].ToursSt[game.TourN] += cost;
            listBox3.Items[num] = current_scores[num].ToString();
            ScoreList[PlayingStalkers[num]].RecountSum();
        }

        public void ClearCosts()
        {
            PlayingStalkerNum = -1;
            listBox2.selected_n = -1;
            listBox2.Refresh();
            for (int i = 0; i < 4; i++)
            {
                costs[i].Text = "";
                helps[i].Text = "";
            }
        }
        private Icon MakeIcon(Image img, int size, bool keepAspectRatio)
        {
            Bitmap square = new Bitmap(size, size); // create new bitmap
            Graphics g = Graphics.FromImage(square); // allow drawing to it

            int x, y, w, h; // dimensions for new image

            if (!keepAspectRatio || img.Height == img.Width)
            {
                // just fill the square
                x = y = 0; // set x and y to 0
                w = h = size; // set width and height to size
            }
            else
            {
                // work out the aspect ratio
                float r = (float)img.Width / (float)img.Height;

                // set dimensions accordingly to fit inside size^2 square
                if (r > 1)
                { // w is bigger, so divide h by r
                    w = size;
                    h = (int)((float)size / r);
                    x = 0; y = (size - h) / 2; // center the image
                }
                else
                { // h is bigger, so multiply w by r
                    w = (int)((float)size * r);
                    h = size;
                    y = 0; x = (size - w) / 2; // center the image
                }
            }
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, x, y, w, h);
            g.Flush();
            return Icon.FromHandle(square.GetHicon());
        }


        private void SetPlayer(int newPlayer)
        {
            PlayingStalkerNum = newPlayer;
            listBox2.selected_n = PlayingStalkerNum;
            if ((answered_count == 4) && (IndTour))
            {
                if (PlayingStalkerNum < game.Stalkers)
                    AddScore(PlayingStalkerNum, bonus);
                ClearCosts();
                return;
            }
            if (PlayingStalkerNum == game.Stalkers)
            {
                ClearCosts();
                return;
            }
            if (IndTour)
            {
                for (int i = 0; i < 4; i++)
                {
                    costsTable[i] = game.SettingsL[current_tour].CostsSt[i] * (Math.Max(PlayingStalkerNum, 0) + 2);
                    if (answered[i] == 0) 
                        costs[i].Text = "+" + (costsTable[i]).ToString();
                    else
                    {
                        costs[i].Text = "";
                        helps[i].Text = "";
                    }
                }

            }
            listBox2.Refresh();
        }

        public void ShowMessage(String text)
        {
            MyMessageBox myMessageBoxInstance = new MyMessageBox();
            DialogResult result = myMessageBoxInstance.Show(text, ColorQ, fontQ, tableLayoutPanel5.BackgroundImage);
            
        }
        private void AnswerQ(object sender, System.EventArgs e)
        {
            Button but = sender as Button;
            int tag = (int)but.Tag;
            if (answered[tag] == 2)
                return;
            Question q = quiz.Tours[current_tour].Expeditions[current_exp].Questions[tag];
            if (answered[tag] == 1)
            {
                try
                {
                    if (!string.IsNullOrEmpty(q.PictureA))
                    {
                        pictureBox10.Image = Image.FromFile(q.PictureA);
                        panel1.Visible = true;
                    }
                    else
                        if (!string.IsNullOrEmpty(q.AudioA))
                    {
                        if (!File.Exists(q.AudioA))
                            throw new Exception();
                        WMPa.URL = q.AudioA;
                        pictureBox10.Image = Image.FromFile(pics.MusicPict);
                        panel1.Visible = true;
                    }
                }
                catch
                {
                    ShowMessage("Невозможно открыть медиафайл");
                }
                answered[tag] = 2;
                but.Text = q.Answer;
                SetAnswColors();
                return;
            }
            answered[tag] = 1;
            but.Text = "";
            answered_count += 1;
            
            SetAnswColors();
            if ((PlayingStalkerNum >= 0) && (!game.Round3On || current_tour != 2 || current_exp > 1) && current_exp == game.ExpeditionN)
            {
                int team = PlayingStalkers[PlayingStalkerNum];
                ScoreList[team].LastAnswer = 1;
                for (int i = 0; i < game.Teams.Count; i++)
                {
                    if ((i != team) && (ScoreList[i].LastAnswer != 0))
                        ScoreList[i].LastAnswer++;
                    // System.Diagnostics.Debug.Write(i.ToString() + " ");
                    // System.Diagnostics.Debug.WriteLine(ScoreList[i].LastAnswer.ToString());
                }
                int cost = costsTable[tag];
                if (IndTour)
                {
                    AddScore(PlayingStalkerNum, cost);
                    if (cost > BestStalkerValue)
                    {
                        BestStalkerValue = cost;
                        BestStalkerQ = tag;
                        BestStalkerTeam = team;
                        BestStalkerAnswer = q.Answer;
                    }
                }
                else
                    for (int i = 0; i < game.Stalkers; i++)
                        AddScore(i, cost);
                CalculatePlaces();
                try
                {
                    int newPlace = ScoreList[team].Place;
                    int oldPlace = ScoreList[team].PrevPlace;
                    if (!IndTour)
                    {
                        for (int i = 0; i < pics.YesPics.Count; i++)
                        {
                            try
                            {
                                pictureBox10.Image = Image.FromFile(pics.YesPics[currentYesPict]);
                                break;
                            }
                            catch
                            {
                            }
                            currentYesPict = (currentYesPict + 1) % pics.YesPics.Count;
                        }
                        currentYesPict = (currentYesPict + 1) % pics.YesPics.Count;
                        panel1.Visible = true;
                    }
                    else
                    {
                        if (newPlace <= 3)
                            YesPlace.BackgroundImage = Image.FromFile(answ_pics[newPlace - 1]);
                        else
                        {

                            for (int i = 0; i < pics.YesPics.Count; i++)
                            {
                                try
                                {
                                    YesPlace.BackgroundImage = Image.FromFile(pics.YesPics[currentYesPict]);
                                    currentYesPict = (currentYesPict + 1) % pics.YesPics.Count;
                                    break;
                                }
                                catch
                                { }
                            currentYesPict = (currentYesPict + 1) % pics.YesPics.Count;
                            }
                        }
                            label11.Text = "Место: " + oldPlace.ToString() + " -> " + newPlace.ToString();
                            label8.Text = game.Teams[team].Name;
                            label9.Text = $"Очки: {ScoreList[team].Sum - cost} + {cost} = {ScoreList[team].Sum}";
                            YesPlace.Visible = true;
                        
                    }
                }
                catch
                {

                }
                SetPlayer(PlayingStalkerNum + 1);

                foreach (Score s in ScoreList)
                {
                    s.PrevPlace = s.Place;

                }

            }

        }
        private void GetResults()
        {
            results.Clear();
            results_showed.Clear();
            resultsBindingSource.Clear();
            int LastTour = Math.Max((current_exp > 0 ? game.TourN : game.TourN - 1), 0);
            ScoreList = ScoreList.OrderBy(u => u.Invisible).ThenByDescending(u => u.Sum).ThenBy(u => u.TeamN).ToList();
            int VisibleTeams = ScoreList.FindAll(u => u.Invisible == false).Count();
            for (int i = 0; i < VisibleTeams; i++)
            {
                Results res = new Results();
                res.Place = i + 1; // ScoreList[i].Place;
                res.team = ScoreList[i].Team;
                res.ScoreBeforeTour = ScoreList[i].Start;
                for (int j = 0; j < LastTour; j++)
                    res.ScoreBeforeTour += ScoreList[i].Tours[j] + ScoreList[i].ToursSt[j];
                res.CurrentTeamScore = ScoreList[i].Tours[LastTour];
                res.CurrentStalkersScore = ScoreList[i].ToursSt[LastTour];
                res.Sum = res.CurrentStalkersScore + res.CurrentTeamScore + res.ScoreBeforeTour;
                results.Add(res);
            }
            results = results.OrderByDescending(u => u.Place).ToList();
            ScoreList = ScoreList.OrderBy(u => u.TeamN).ToList();
            dataGridView1.Refresh();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (media_state == 5)
            {
                WMPq.Ctlcontrols.stop();
                WMPq.Visible = false;
                SetPictQ(pictureBox1.Image);
                qTextBox11.Visible = true;
                e.Cancel = true;
                return;
            }
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NextQuestion(-1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NextExpedition(-1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //if (System.Windows.Forms.SystemInformation.TerminalServerSession)
             //   return;
            //System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //aProp.SetValue(c, true, null);
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty).SetValue(c, true, null);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerLeft -= 1;
            tableLayoutPanel2.SuspendLayout();
            label6.Text = TimerLeft.ToString() + "''";
            if (TimerLeft == 0)
            {
                timer1.Enabled = false;
                label6.Text = "";
                if (current_q == 3)
                    tabControl1.SelectedIndex += 1;
            }
            tableLayoutPanel2.ResumeLayout();
        }
        public bool timerOn;
        public void SetAnswColors()
        {
            for (int i = 0; i < 4; i++)
            {
                Button but = buttons[i];
                but.BackgroundImage = answered[i] <= 1 ? Properties.Resources.plashka_question : Properties.Resources.plashka_answer;
                
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void ClearAnswers()
        {
            answered_count = 0;
            PlayingStalkerNum = 0;
            listBox2.selected_n = 0;
            Expedition Exp = quiz.Tours[current_tour].Expeditions[current_exp];

            for (int j = 0; j < 4; j++)
            {
                answered[j] = 0;
                costsTable[j] = game.SettingsL[current_tour].CostsSt[j] * 2;
                costs[j].Text = "+" + costsTable[j].ToString();
                /* else
                {
                    costs[j].Text = "";
                    helps[j].Text = "";
                } */
                buttons[j].Text = Exp.Questions[j].Prompt;
                if (IndTour)
                {
                    helps[j].Text = Exp.Questions[j].Help;
                }

            }
            for (int i = 0; i < game.Stalkers; i++)
            {
                listBox3.Items[i] = "";
                current_scores[i] = 0;
                ScoreList[i].RecountSum();
            }

            SetAnswColors();

        }
        public void CancelAnsw(bool TeamMistake)
        {
            for (int i = 0; i < game.Stalkers; i++)
                ScoreList[PlayingStalkers[i]].ToursSt[current_tour] -= current_scores[i];
             
            if (!TeamMistake)
                ClearAnswers();

            for (int i = 0; i < game.Stalkers; i++)
            {
                listBox3.Items[i] = "";
                current_scores[i] = 0;
                ScoreList[i].RecountSum();
            }

            foreach (Score s in ScoreList)
            {
                s.RecountSum();
                s.LastAnswer = s.OldLastAnswer;
            }
            CalculatePlaces();
            PlayingStalkerNum = 0;
            SetPlayer(PlayingStalkerNum);
            BestStalkerAnswer = "";
            BestStalkerQ = -1;
            BestStalkerTeam = -1;
            BestStalkerValue = -1;
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_Validating(object sender, CancelEventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    int val = (int)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (val < 0)
                    {
                        ScoreList[e.RowIndex].Invisible = true;
                        //CreateTeamsButton();
                        flowLayoutPanel1.Controls[e.RowIndex].Visible = false;
                    }
                    else
                    {
                        ScoreList[e.RowIndex].Invisible = false;
                        flowLayoutPanel1.Controls[e.RowIndex].Visible = true;

                    }

                }
                foreach (Score s in ScoreList)
                {
                    s.RecountSum();
                }
                CalculatePlaces();
                foreach (Score s in ScoreList)
                {
                    s.PrevPlace = s.Place;
                }

                game.Serialize();
            } catch
            {
                
            }
            ResultsChanged = true;
        }
        List<Image> WelcomePics;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (WelcomePics.Count > 0)
            {
                WelcomN++;
                WelcomN = WelcomN % WelcomePics.Count;
                pictureBox2.Image = WelcomePics[WelcomN];
            }
        }

        public bool playerOn;
        private void button12_Click(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedIndex == 1) && (current_q == -1) && (ResultsChanged))
            {
                GetResults();
                logs.AddResults(results);
                ResultsChanged = false;
            }
            GetResults();
            timer2.Enabled = false;
            if (tabControl1.SelectedTab == tabPage4)
            {
                pictureBox9.Size = flowLayoutPanel4.Size;
                pictureBox11.Size = flowLayoutPanel5.Size;
                pictureBox12.Size = flowLayoutPanel6.Size;
                pictureBox13.Size = flowLayoutPanel7.Size;
                for (int i = 0; i < 4; i++)
                {
                    Question q = quiz.Tours[current_tour].Expeditions[current_exp].Questions[i];
                    if (!string.IsNullOrEmpty(q.PictureQ))
                    {
                        try
                        {
                            Image pict = Image.FromFile(q.PictureQ);
                            hints_pics[i].Image = pict;
                            hints_pics[i].Visible = true;
                            hints_labels[i].Text = "";
                            forms_labels[i].Text = "";
                        }
                        catch
                        { }
                    }
                    else
                    {
                        hints_pics[i].Visible = false;
                        hints_labels[i].Text = q.Hint;
                        forms_labels[i].Text = q.HintForm;
                    }
                }
                TimerLeft = game.HintTimer;
                label29.Text = game.HintTimer.ToString();
                timer2.Enabled = true;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
        }
        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Click(object sender, EventArgs e)
        {
            
        }

        private void myDataGridView1_Click(object sender, EventArgs e)
        {
            if (resultsBindingSource.Count < results.Count)
            {                
                resultsBindingSource.Insert(0, results[resultsBindingSource.Count]);
                if (results[resultsBindingSource.Count - 1].Place < 4)
                    dataGridView1.Rows[0].Cells[0].Style.ForeColor = ColorHelp;
                myDataGridView1.Refresh();
            }
        }

        private void myDataGridView1_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void YesPlace_Paint(object sender, PaintEventArgs e)
        {

        }

        private void YesPlace_DoubleClick(object sender, EventArgs e)
        {

        }

        private void YesPlace_Click(object sender, EventArgs e)
        {
            YesPlace.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void myDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {

        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                if (tabControl1.SelectedIndex < tabControl1.TabPages.Count)
                    tabControl1.SelectedIndex += 1;
                if (current_tour == 2 && current_exp == ExpInTour && tabControl1.SelectedIndex == 5)
                    tabControl1.SelectedIndex += 1;                    

            }

            if (e.KeyCode == Keys.PageUp)
            {
                if (tabControl1.SelectedIndex > 0)
                    tabControl1.SelectedIndex -= 1;
            }

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

            switch (e.newState)
            {
                case 1:
                    WMPa.Ctlcontrols.stop();
                    WMPa.Visible = false;
                    break;
                case 8:    // MediaEnded
                    WMPa.Visible = false;
                    pictureBox1.Visible = false;
                    break;
                case 0:
                    ShowMessage("Невозможно открыть медиафайл");
                    WMPa.Visible = false;
                    pictureBox1.Visible = false;
                    break;

            }
        }

        private void WMPq_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState)
            {
                case 1:    
                    WMPq.Ctlcontrols.stop();
                    WMPq.Visible = false;
                    SetPictQ(null);
                    qTextBox11.Visible = true;
                    if (media_state == 2)
                        media_state = 3;
                    else
                        media_state = 6;
                    break;
                case 8:    // MediaEnded
                    WMPq.Visible = false;
                    WMPq.Ctlcontrols.stop();
                    SetPictQ(null);
                    qTextBox11.Visible = true;

                    if (media_state == 2)
                        media_state = 3;
                    else
                        media_state = 6;
                    break;
                    //pictureBox8.Visible = false;
                    break;
                case 0:
                    ShowMessage("Невозможно открыть медиафайл");
                    WMPq.Visible = false;
                    //pictureBox8.Visible = false;
                    break;
            }
        }

        private void qTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void qTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex -= 1;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex += 1;
        }

        private void label15_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex -= 1;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (enabled_game)
                tabControl1.SelectedIndex += 1;
            else
                ShowMessage($"Выберите команды ({game.Stalkers}) для следующего раунда");
        }

        private void button5_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (current_q <3)
                NextQuestion(1);
            else
                tabControl1.SelectedIndex += 1;

        }

        private void myListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void qTextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }
        public static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }
        private void qTextBox1_TextChanged_3(object sender, EventArgs e)
        {

        }

        private void scoreBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void myDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        { /*
            if (resultsBindingSource.Count < results.Count)
            {
                resultsBindingSource.Insert(0, results[resultsBindingSource.Count]);

                if (results[resultsBindingSource.Count - 1].Place < 4)
                    dataGridView1.Rows[0].Cells[0].Style.ForeColor = ColorHelp;
                myDataGridView1.Refresh();
                timer3.Enabled = true;
            }
            */

        }

        private void myDataGridView1_Click_2(object sender, EventArgs e)
        {

            if (timer3.Enabled == true)
                return;
            if (resultsBindingSource.Count < results.Count)
            {
                resultsBindingSource.Insert(0, results[resultsBindingSource.Count]);
                int place = results[resultsBindingSource.Count - 1].Place;
                if (place < 4)
                {
                    myDataGridView1.Rows[0].Cells[0].Style.ForeColor = ColorHelp;
                    if (place == 1)
                        myDataGridView1.Rows[0].Cells[0].Style.ForeColor = CostColor;
                }
                else
                    myDataGridView1.Rows[0].Cells[0].Style.ForeColor = TeamsColor;

                myDataGridView1.Refresh();
                timer3.Enabled = true;
            }

        }

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            timer1.Enabled ^= true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (current_q > 0)
                NextQuestion(-1);
            else
                tabControl1.SelectedIndex -= 1;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (media_state == 1 || media_state == 3)
            {
                WMPq.URL = url;
                media_state = 2;                
            }
            else 
                if (media_state == 2 || media_state == 5)
            {
                WMPq.Ctlcontrols.stop();
                WMPq.Visible = false;
                SetPictQ(pictureBox1.Image);
                qTextBox11.Visible = true;

            }
            else 
                if (media_state == 4 || media_state == 6)
            {
                WMPq.URL = url;
                WMPq.Visible = true;
                WMPq.Dock = DockStyle.Fill;
                WMPq.Size = this.Size;
                WMPq.stretchToFit = true;
                media_state = 5;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }
        private void CostLabelClick(object sender, System.EventArgs e)
        {
            WrongAnswer();
        }

        public void WrongAnswer()
        {
            if (PlayingStalkerNum >= 0)
            {
                SetPlayer(PlayingStalkerNum + 1);
                    if (pics.NoPics != null)
                    {
                        for (int i=0; i<pics.NoPics.Count; i++)
                        {
                            try
                            {
                                pictureBox10.Image = Image.FromFile(pics.NoPics[NoN]);
                            break;
                            }
                            catch
                            { }
                        NoN = (NoN + 1) % pics.NoPics.Count;
                        }
                        NoN = (NoN + 1) % pics.NoPics.Count;
                        panel1.Visible = true;
                    }

                if (!IndTour)
                {
                    CancelAnsw(true);
                    SetPlayer(-1);
                    return;
                }
                if (IndTour && PlayingStalkerNum >= 0)
                {
                    listBox3.Items[PlayingStalkerNum] = "";

                }
            }
        }

        private void tableLayoutPanel20_Click(object sender, EventArgs e)
        {
            Point p = tableLayoutPanel20.PointToClient(Cursor.Position);
            int[] widths = tableLayoutPanel20.GetColumnWidths();
            if (p.X < widths[0] + widths[1] + widths[2])
            {
                WrongAnswer();
            }

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {

            WMPa.Ctlcontrols.stop();
            panel1.Visible = false;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = TeamsSelectedColor;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = TeamsSelectedColor;

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = ColorQ;

        }

        private void YesPlace1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void YesPlace_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void YesPlace_Paint_2(object sender, PaintEventArgs e)
        {

        }
        

        private void YesPlace_Click_1(object sender, EventArgs e)
        {
                YesPlace.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex -= 1;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex += 1;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (enabled_game)
                NextExpedition(1);
            else
                ShowMessage($"Выберите команды ({game.Stalkers}) для следующего раунда");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

        }

        private void button12_Click_2(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void GameForm_ClientSizeChanged(object sender, EventArgs e)
        {
            WMPq.Dock = DockStyle.Fill;
            //WMPq.Size = this.Size;
        }

        private void GameForm_SizeChanged(object sender, EventArgs e)
        {
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {

            if (game != null)
                SetButtonsSizes();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox3_Click(object sender, EventArgs e)
        {
            if (!IndTour)
                SetPlayer(-1);
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            CancelAnsw(false);
        }

        private void StopExp()
        {
            if (current_tour < 2)
                for (int j = 0; j < game.Teams.Count; j++)
                    game.StalkersDelegated[current_tour + 1][0][j] = game.StalkersDelegated[current_tour][current_exp][j];
            game.SettingsL[current_tour].Length = current_exp;
            NextExpedition(ExpInTour - current_exp);              
        }

        private void GameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            StopExp();
        }
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
                StopExp();
            if ((e.KeyCode == Keys.N) && (tabControl1.SelectedTab == tabPage8))
            {
                WrongAnswer();
                return;
            }
            int Num = e.KeyCode - Keys.D0;
            if (((tabControl1.SelectedTab == tabPage8) || (tabControl1.SelectedTab == tabPage1)) 
                && Num >= 1 && Num <= 4)
            {
                ShowMessage($"{GameSettings.methods[Num - 1]}");
                StalkerOrderMethod = Num;
                CancelAnsw(false);
                for (int i = 0; i < game.Teams.Count; i++)
                {
                    int k = game.StalkersDelegated[game.TourN][game.ExpeditionN][i];
                    if (k == 0)
                        game.StalkersDelegated[game.TourN][game.ExpeditionN][i] = -1;
                    else
                        game.StalkersDelegated[game.TourN][game.ExpeditionN][i] = 1;
                } 
                GetStalkers();
                ResetStalkers();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowCount == 0 || e.RowCount == game.Teams.Count)
            {
                return;
            }

            for (int i = 0; i < e.RowCount; i++)
            {
                int index = e.RowIndex + i;  //get row index
                int ind = ScoreList[index].TeamN;
                ScoreList[ind].Invisible = true;

            }
            CalculatePlaces();
            CreateTeamsButton();

            int next_exp = current_exp + 1;
            int next_tour = current_tour + (next_exp / ExpInTour);
            next_exp %= ExpInTour;
            if ((next_tour != 3) && (!game.Round3On || current_tour != 2 || current_exp != 0))
            {
                for (int i = 0; i < game.Teams.Count; i++)
                    game.StalkersDelegated[next_tour][next_exp][i] = -1;
            }
            SetButtonsColours();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TimerLeft -= 1;
            if (TimerLeft == 0)
            {
                timer2.Enabled = false;
                tabControl1.SelectedIndex += 1;
            }
            label29.Text = TimerLeft.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            if (game.BestStalkerValue > 0)
            {
                label30.Text = "Лучший сталкер: " + game.Teams[game.BestStalkerTeam].Name + " " + game.BestStalkerValue.ToString() + " (" + game.BestStalkerAnswer + ")";
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
    }
}
