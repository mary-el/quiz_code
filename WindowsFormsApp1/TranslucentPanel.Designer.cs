using System;

namespace WindowsFormsApp1
{
    partial class TranslucentPanel
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

        public EventHandler Load { get; private set; }


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TranslucentPanel
            // 
            this.Name = "TranslucentPanel";
            this.Load += new System.EventHandler(this.TranslucentPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
