using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class MyDataGridView : DataGridView
    {
       
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);
            if (this.Parent.BackgroundImage != null)
            {
                Rectangle rectSource = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
                Rectangle rectDest = new Rectangle(0, 0, rectSource.Width, rectSource.Height);

                Bitmap b = new Bitmap(Parent.ClientRectangle.Width, Parent.ClientRectangle.Height);
                Graphics.FromImage(b).DrawImage(this.Parent.BackgroundImage, Parent.ClientRectangle);
                
                graphics.DrawImage(b, rectDest, rectSource, GraphicsUnit.Pixel);
                SetCellsTransparent();
            }
        }


        public void SetCellsTransparent()
        {
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            this.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;

            this.DefaultCellStyle.SelectionBackColor = this.DefaultCellStyle.BackColor;
            this.DefaultCellStyle.SelectionForeColor = this.DefaultCellStyle.ForeColor;

            this.DefaultCellStyle.BackColor = Color.Transparent;
            /*foreach (DataGridViewColumn col in this.Columns)
            {
                col.DefaultCellStyle.BackColor = Color.Transparent;
                col.DefaultCellStyle.SelectionBackColor = Color.Transparent;

            }*/
        }
    }
}
