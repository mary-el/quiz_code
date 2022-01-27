using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ResultsByTour : Form
    {
        static public List<Score> scores;
        public ResultsByTour()
        {
            InitializeComponent();
        }

        private void resultsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ResultsByTour_Load(object sender, EventArgs e)
        {
            myDataGridView1.ClearSelection();
            scores = scores.OrderBy(u => u.RealPlace).ToList();
            Pics pics = StartForm.pics;
            PrivateFontCollection fontCollection = new PrivateFontCollection();
            FontFamily familyQ;
            FontFamily familyTeams;
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

            try
            {
                tableLayoutPanel1.BackgroundImage = Image.FromFile(pics.BackgroundExp);
            }
            catch { }

            try
            {
                Image logo = Image.FromFile(pics.Logo);
                pictureBox8.Image = logo;
            }
            catch
            { }
            Font fontExpName = new Font(familyTeams, pics.FontSizeExp);
            Font fontTeams = new Font(familyTeams, pics.FontSizeTeams);
            Font fontSelectedTeams = new Font(familyTeams, pics.FontSizeSelectedTeams);
            Font fontTeamsGame = new Font(familyTeams, pics.FontSizeTeamsGame);
            Font fontExpN = new Font(familyTeams, pics.FontSizeExpN);
            Color ColorQ = ColorTranslator.FromHtml(pics.FontColorQ);
            Color ColorResults = ColorTranslator.FromHtml(pics.FontColorResults);
            Color ColorRunners = ColorTranslator.FromHtml(pics.FontColorRunners);
            Color TeamsColor = ColorTranslator.FromHtml(pics.FontColorTeams);
            Color TeamsSelectedColor = ColorTranslator.FromHtml(pics.FontColorSelectedTeams);
            Color ColorHelp = ColorTranslator.FromHtml(pics.FontColorHelp);

            myDataGridView1.ColumnHeadersDefaultCellStyle.Font = fontTeams;
            myDataGridView1.RowTemplate.DefaultCellStyle.Font = fontTeams;
            myDataGridView1.RowHeadersDefaultCellStyle.Font = fontTeams;
            myDataGridView1.Font = fontSelectedTeams;
            myDataGridView1.ForeColor = ColorResults;
            myDataGridView1.Columns[5].DefaultCellStyle.ForeColor = TeamsSelectedColor;
            myDataGridView1.Columns[6].DefaultCellStyle.ForeColor = ColorHelp;
            int VisibleTeams = scores.FindAll(u => u.Invisible == false).Count();

            myDataGridView1.RowTemplate.Height = ((myDataGridView1.Height - myDataGridView1.ColumnHeadersHeight) / VisibleTeams);
            label1.Font = fontExpName;
            label1.ForeColor = ColorResults;
            label1.Text = pics.ResultsName;
            int j = 0;
            foreach (Score score in scores)
            {
                string start = "";
                string place = "";
                if (score.Start < 0)
                    continue;
                if (score.Start > 0)
                    start = score.Start.ToString();
                if (score.Place > 0)
                    place = score.Place.ToString();
                int tour1 = 0;
                int tour2 = 0;
                int tour3 = 0;
                for (int i=0; i<4; i++)
                {
                    tour1 += score.Rounds[i];
                    tour2 += score.Rounds[4 + i];
                    tour3 += score.Rounds[8 + i];
                }
                myDataGridView1.Rows.Add(score.Team, start, tour1.ToString(), tour2, tour3, score.Sum, place);
                if (score.Place == 1)
                    myDataGridView1.Rows[j].Cells[0].Style.ForeColor = TeamsSelectedColor;
                else
                if (score.Place <= 3)
                    myDataGridView1.Rows[j].Cells[0].Style.ForeColor = ColorHelp;
                if (score.Place == -1)
                    myDataGridView1.Rows[j].Cells[0].Style.ForeColor = ColorRunners;
                j += 1;
            }
        }

        private void myDataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            myDataGridView1.ClearSelection();

        }

        private void ResultsByTour_SizeChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form form = this.FindForm();
            Bitmap BM = new Bitmap(form.Width, form.Height);
            form.DrawToBitmap(BM, form.ClientRectangle);
            try
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp";
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    BM.Save(SFD.FileName);
                }
            }
            catch { }
        }

        private void ResultsByTour_ClientSizeChanged(object sender, EventArgs e)
        {

            int VisibleTeams = scores.FindAll(u => u.Invisible == false).Count();
            int h = ((myDataGridView1.Height - myDataGridView1.ColumnHeadersHeight) / VisibleTeams);
            foreach (DataGridViewRow row in myDataGridView1.Rows)
            {
                row.Height = h;
            }
        }
    }
}
