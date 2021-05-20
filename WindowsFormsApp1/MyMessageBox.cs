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
    public partial class MyMessageBox : Form
    {
        public MyMessageBox()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {

        }
        public DialogResult Show(string text, Color foreColour, Font font, Image backg)
        {
            label1.Text = text;
            label1.ForeColor = foreColour;
            label1.Font = font;
            tableLayoutPanel1.BackgroundImage = backg;
            return this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
