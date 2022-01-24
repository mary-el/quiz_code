namespace WindowsFormsApp1
{
    partial class ResultsByTour
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            this.myDataGridView1 = new WindowsFormsApp1.MyDataGridView();
            this.Team = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Round2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Round3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Round4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tour3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // myDataGridView1
            // 
            this.myDataGridView1.AllowUserToAddRows = false;
            this.myDataGridView1.AllowUserToDeleteRows = false;
            this.myDataGridView1.AllowUserToResizeColumns = false;
            this.myDataGridView1.AllowUserToResizeRows = false;
            this.myDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myDataGridView1.CausesValidation = false;
            this.myDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.myDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.myDataGridView1.ColumnHeadersHeight = 60;
            this.myDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.myDataGridView1.ColumnHeadersVisible = false;
            this.myDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Team,
            this.Round2,
            this.Round3,
            this.Round4,
            this.Tour3,
            this.Sum,
            this.Place});
            this.myDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.myDataGridView1.Location = new System.Drawing.Point(2, 131);
            this.myDataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.myDataGridView1.MultiSelect = false;
            this.myDataGridView1.Name = "myDataGridView1";
            this.myDataGridView1.ReadOnly = true;
            this.myDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.myDataGridView1.RowHeadersVisible = false;
            this.myDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.myDataGridView1.RowTemplate.Height = 35;
            this.myDataGridView1.RowTemplate.ReadOnly = true;
            this.myDataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.myDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.myDataGridView1.Size = new System.Drawing.Size(894, 514);
            this.myDataGridView1.TabIndex = 1;
            this.myDataGridView1.SelectionChanged += new System.EventHandler(this.myDataGridView1_SelectionChanged);
            // 
            // Team
            // 
            this.Team.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Team.DefaultCellStyle = dataGridViewCellStyle29;
            this.Team.FillWeight = 500F;
            this.Team.HeaderText = "Name";
            this.Team.Name = "Team";
            this.Team.ReadOnly = true;
            // 
            // Round2
            // 
            this.Round2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Round2.DataPropertyName = "Start";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Round2.DefaultCellStyle = dataGridViewCellStyle30;
            this.Round2.HeaderText = "Start";
            this.Round2.Name = "Round2";
            this.Round2.ReadOnly = true;
            this.Round2.Width = 5;
            // 
            // Round3
            // 
            this.Round3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Round3.DataPropertyName = "Tour1";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Round3.DefaultCellStyle = dataGridViewCellStyle31;
            this.Round3.HeaderText = "Tour1";
            this.Round3.Name = "Round3";
            this.Round3.ReadOnly = true;
            // 
            // Round4
            // 
            this.Round4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Round4.DataPropertyName = "Tour2";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Round4.DefaultCellStyle = dataGridViewCellStyle32;
            this.Round4.HeaderText = "Tour2";
            this.Round4.Name = "Round4";
            this.Round4.ReadOnly = true;
            // 
            // Tour3
            // 
            this.Tour3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Tour3.DefaultCellStyle = dataGridViewCellStyle33;
            this.Tour3.HeaderText = "Tour3";
            this.Tour3.Name = "Tour3";
            this.Tour3.ReadOnly = true;
            // 
            // Sum
            // 
            this.Sum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Sum.DefaultCellStyle = dataGridViewCellStyle34;
            this.Sum.HeaderText = "Sum";
            this.Sum.Name = "Sum";
            this.Sum.ReadOnly = true;
            // 
            // Place
            // 
            this.Place.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Place.DefaultCellStyle = dataGridViewCellStyle35;
            this.Place.HeaderText = "Place";
            this.Place.Name = "Place";
            this.Place.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.myDataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox8, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1057, 647);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox8.Location = new System.Drawing.Point(898, 0);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(159, 129);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 6;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(892, 129);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResultsByTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 647);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ResultsByTour";
            this.Load += new System.EventHandler(this.ResultsByTour_Load);
            this.ClientSizeChanged += new System.EventHandler(this.ResultsByTour_ClientSizeChanged);
            this.SizeChanged += new System.EventHandler(this.ResultsByTour_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyDataGridView myDataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Team;
        private System.Windows.Forms.DataGridViewTextBoxColumn Round2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Round3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Round4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tour3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Place;
        private System.Windows.Forms.Label label1;
    }
}