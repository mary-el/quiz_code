using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class TeamListForm : Form
    {
        public List<Team> Teams;
        public TeamListForm()
        {
            InitializeComponent();
            Teams = StartForm.game.Teams;
            if (Teams.Count > 0)
                return;
            Teams.Add(new Team() { Name = "", Number=1, Group=1, Plays1=false, Plays2=false, Short=""});
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Teams = StartForm.game.Teams;
            teamBindingSource.DataSource = Teams;
            bindingNavigator1.BindingSource = teamBindingSource;
            dataGridView1.DataSource = teamBindingSource;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Teams.Count < 4)
            {
                MessageBox.Show("Для игры нужны не менее 4 команд", "", MessageBoxButtons.OK);
                return;
            }
            Teams = Teams.OrderBy(u => u.Number).ToList();
            for (int i=0; i < Teams.Count; i++)
            {
                if (Teams[i].Number != i + 1)
                {
                    MessageBox.Show("Пронумеруйте команды последовательными числами", "", MessageBoxButtons.OK);
                    return;
                }
            }
            List<List<List<int>>> Delegated = new List<List<List<int>>>();
            int st1 = 0;
            int st2 = 0;

            if (StartForm.game.TourN == -1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Delegated.Add(new List<List<int>>());
                    for (int j = 0; j < 6; j++)
                    {
                        Delegated[i].Add(new List<int>());
                    }
                    foreach (Team t in Teams)
                    {
                        for (int k = 0; k < 6; k++)
                            Delegated[i][k].Add(-1);
                    }

                }
                bool all_named = true;
                for (int i=0; i<Teams.Count; i++)
                {
                    var t = Teams[i];
                    if (t.Group < 1 || t.Group > 3)
                    {
                        MessageBox.Show("Номера групп должны принимать значения от 1 до 3", "", MessageBoxButtons.OK);
                        return;
                    }
                    //Delegated[0][0][i] = t.Plays1  ? 1 : -1;
                    //Delegated[0][1][i] = t.Plays2  ? 1 : -1;
                    if ((t.Name == null) || (t.Short == null) || (t.Name.Trim() == "") || (t.Short.Trim() == ""))
                        all_named = false;
                    //st1 += t.Plays1 ? 1 : 0;
                    //st2 += t.Plays2 ? 1 : 0;
                }
                if (!all_named)
                {
                    MessageBox.Show("У всех команд должны быть полные и краткие названия", "", MessageBoxButtons.OK);
                    return;
                }
                StartForm.logs = new Logs(StartForm.game.LogFile);
                StartForm.logs.CreateFile();
                StartForm.game.StalkersDelegated = Delegated;
                StartForm.game.Teams = Teams;
                StartForm.game.Serialize();
            }
            GameForm newForm = new GameForm();
            newForm.Show();
            this.Hide();
            
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int newInteger;
            if (e.ColumnIndex == 1)
                if (!int.TryParse(e.FormattedValue.ToString(),
               out newInteger) || newInteger < 0)
                    e.Cancel = true;

            if (e.ColumnIndex == 5)
                if (!int.TryParse(e.FormattedValue.ToString(),
               out newInteger) || newInteger < 1 || newInteger > 3)
                    e.Cancel = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)
            {
            
            if (e.ColumnIndex == 0)
            {
                string team = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                dataGridView1[2, e.RowIndex].Value = team;

            }
           StartForm.game.Serialize();
            
            }

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter) || (e.KeyData == Keys.Tab))
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                int row = dataGridView1.CurrentCell.RowIndex;

                if (col < dataGridView1.ColumnCount - 1)
                {
                    col++;
                }
                else
                {
                    col = 0;
                    row++;
                }

                if ((row == dataGridView1.RowCount) && (dataGridView1[1, row].Value != null))
                    dataGridView1.Rows.Add();
                    dataGridView1.CurrentCell = dataGridView1[col, row];
                
              
                e.Handled = true;
            }
        }

        private void TeamListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog1.FileName;
            try
            {
                var formatter = new XmlSerializer(typeof(List<Team>));
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    formatter.Serialize(fs, Teams);
                }

            }
            catch
            { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            try
            {
                var formatter = new XmlSerializer(typeof(List<Team>));
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    Teams = (List<Team>)formatter.Deserialize(fs);
                    teamBindingSource.DataSource = Teams;
                    StartForm.game.Teams = Teams;
                    StartForm.game.Serialize();
                }

            }
            catch
            {
                MessageBox.Show("Неправильный формат файла");
                return;
            }

        }

        private void TeamListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
