using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class myTextBox : RichTextBox
    {
        public myTextBox()
        {
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
        
        protected override CreateParams CreateParams
        {
            get
            {
                //This makes the control's background transparent
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= 0x20;
                return CP;
            }
        }
        private void myTextBox_Load(object sender, EventArgs e)
        {

        }
    }

}
