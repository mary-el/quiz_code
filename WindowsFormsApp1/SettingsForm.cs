using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SettingsForm : Form
    {
        public string dir = Directory.GetCurrentDirectory();
        public List<GameSettings> Settings { get; set; }
        public NumericUpDown[] timers;
        public SettingsForm()
        {
            InitializeComponent();
            timers = new NumericUpDown[]{numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4 };
            Settings = StartForm.game.SettingsL;
            if (Settings.Count > 0)
            {
                textBox1.Text = StartForm.game.QuizFile;
                textBox2.Text = StartForm.game.LogFile;
                textBox3.Text = StartForm.game.PicsFile;
                for (int i = 0; i < 4; i++)
                    timers[i].Value = StartForm.game.Timer[i];
                return;
            }

            Settings = new List<GameSettings>();
            Settings.Add(new GameSettings() {TourN=1, StalkersMethod=2, Length=5, Cost = new int[] {8, 6, 4, 2 }, CostsSt = new int[] {5, 3, 2}, Individual=true});
            Settings.Add(new GameSettings() {TourN=2, StalkersMethod=2, Length=5, Cost = new int[] {8, 6, 4, 2 }, CostsSt = new int[] {5, 3, 2}, Individual=true});
            Settings.Add(new GameSettings() {TourN=3, StalkersMethod=2, Length=5, Cost = new int[] {12, 9, 6, 3}, CostsSt = new int[] {5, 3, 2}, Individual=true});

        }
        private int Fields = 10;

        private void Settings_Load(object sender, EventArgs e)
        {
            dataGridView2.RowCount = 3;
            dataGridView2.ColumnCount = Fields;
            DataGridViewCheckBoxColumn bc = new DataGridViewCheckBoxColumn();
            dataGridView2.Columns.Add(bc);
            //dataGridView2.Columns[5].HeaderText = "бонус";
            dataGridView2.Columns[Fields].HeaderText = "инд";
            dataGridView2.Columns[5].HeaderText = $"ст осн";
            dataGridView2.Columns[6].HeaderText = $"ст доп";
            dataGridView2.Columns[7].HeaderText = $"ст комп";
            dataGridView2.Columns[Fields - 2].HeaderText = "длина";
            dataGridView2.Columns[Fields - 1].HeaderText = "очер (1-4)";
            for (int i=0; i<3; i++)
            {
                dataGridView2.Columns[0].HeaderText = "тур";
                dataGridView2[0, i].Value = Settings[i].TourN;
                for (int j = 0; j < 4; j++)
                {
                    dataGridView2.Columns[j+1].HeaderText = $"вопрос {j+1}";
                    dataGridView2[j + 1, i].Value = Settings[i].Cost[j];
                }
                for (int j = 0; j < 3; j++)
                {
                    dataGridView2[j + 5, i].Value = Settings[i].CostsSt[j];
                }
                dataGridView2[Fields - 2, i].Value = Settings[i].Length;
                dataGridView2[Fields - 1, i].Value = Settings[i].StalkersMethod;
                dataGridView2[Fields, i].Value = Settings[i].Individual;
                               
            }

            this.gameSettingsBindingSource2.DataSource = Settings;
            // dataGridView2.DataSource = gameSettingsBindingSource2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartForm.game.QuizFile = textBox1.Text;
            try
            {
                StartForm.quiz = Quiz.Deserialize(textBox1.Text);
                if (StartForm.quiz.Tours.Count != 3)
                    throw new Exception();
                for (int i=0;i<3; i++)
                {
                    int TourLength = Convert.ToInt32(dataGridView2[Fields - 2, i].Value);
                    if (StartForm.quiz.Tours[i].Expeditions.Count < TourLength)
                        throw new Exception();
                    for (int j = 0; j < TourLength; j++)
                        if (StartForm.quiz.Tours[i].Expeditions[j].Questions.Count != 4)
                            throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат файла квиза", "", MessageBoxButtons.OK);
                return;
            }
            Settings.Clear();
            try
            {
                StartForm.pics = Pics.Deserialize(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Неверный формат файла изображений", "", MessageBoxButtons.OK);
                return;
            }
            StartForm.game.PicsFile = textBox3.Text;
            for (int i=0; i<3; i++)
            {
                for (int j = 0; j < Fields; j++)
                    dataGridView2[j, i].Value = Convert.ToInt32(dataGridView2[j, i].Value);
                GameSettings gs = new GameSettings();
                gs.TourN = (int)dataGridView2[0, i].Value;
                gs.Cost = new int[4];
                gs.CostsSt = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    gs.Cost[j] = (int)dataGridView2[j + 1, i].Value;
                    gs.CostsSt[j] = (int)dataGridView2[j + 5, i].Value;
                }
                
                gs.Length = (int)dataGridView2[Fields - 2, i].Value;
                gs.StalkersMethod = (int)dataGridView2[Fields - 1, i].Value;
                gs.Individual = (bool)dataGridView2[Fields, i].Value;
                Settings.Add(gs);
            }
            for (int i=0;i<4;i++)
                StartForm.game.Timer[i] = (int)timers[i].Value;
            StartForm.game.HintTimer = (int)numericUpDown5.Value;
            StartForm.game.SettingsL = Settings;
            StartForm.game.Round3On = false;
            StartForm.game.Stalkers = 6;
            StartForm.game.LogFile = textBox2.Text;
            StartForm.game.Serialize();
            TeamListForm teamlistform = new TeamListForm();
            teamlistform.Show();
            this.Hide();
        }

        public static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath + "/");
            var relativeUri = referenceUri.MakeRelativeUri(fileUri);
            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', Path.DirectorySeparatorChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = MakeRelative(openFileDialog1.FileName, dir);
            
            textBox1.Text = filename;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gameSettingsBindingSource2_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == Fields)
                return;
            int newInteger;

            if (!int.TryParse(e.FormattedValue.ToString(),out newInteger) || newInteger < 0)
            {
                
                e.Cancel = true;
                //dataGridView2.Rows[e.RowIndex].ErrorText = "the value must be a non-negative integer";
            }
            else
            {
                if (e.ColumnIndex == 11 && (newInteger < 1 || newInteger > GameSettings.methods.Length))
                    e.Cancel = true;
                else 
                    dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newInteger;
            }
                

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = MakeRelative(saveFileDialog1.FileName, dir);
            textBox2.Text = filename;
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = MakeRelative(openFileDialog1.FileName, dir);
            textBox3.Text = filename;

        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
