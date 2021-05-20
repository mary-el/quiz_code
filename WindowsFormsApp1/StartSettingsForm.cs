using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class StartSettingsForm : Form
    {
        public StartSettingsForm()
        {
            InitializeComponent();
        }

        private void StartSettings_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numericUpDown41_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            int[] costs = new int[12];
            foreach (var num in tableLayoutPanel3.Controls.OfType<NumericUpDown>())
            {
                costs[i] = (int)num.Value;
                i += 1;
            }

            i = 0;
            int[] balances = new int[12];
            foreach (var num in tableLayoutPanel2.Controls.OfType<NumericUpDown>())
            {
                balances[i] = (int)num.Value;
                i += 1;
            }

            i = 0;
            int[] costsSt = new int[12];
            foreach (var num in tableLayoutPanel1.Controls.OfType<NumericUpDown>())
            {
                costsSt[i] = (int)num.Value;
                i += 1;
            }

            i = 0;
            int[] timer = new int[4];
            foreach (var num in tableLayoutPanel4.Controls.OfType<NumericUpDown>())
            {
                timer[i] = (int)num.Value;
                i += 1;
            }

            // StartForm.game.SetStartSettings((int)StalkersNum.Value, costs, balances, costsSt, timer, Tour3OnBox.Checked);
            TeamListForm teamlistform = new TeamListForm();
            teamlistform.Show();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
