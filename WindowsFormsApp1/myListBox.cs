using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class myListBox : ListBox
    {
        public Font SelectedFont;
        public Color SelectedColor;
        public int selected_n;
        public myListBox(): base()
        {                        
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, false);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.FromArgb(0, Color.Transparent);
            this.DrawItem += ListBox_DrawItem;
            selected_n = -1;
        }
        
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            IntPtr hdc = pevent.Graphics.GetHdc();
            Rectangle rect = this.ClientRectangle;
            NativeMethods.DrawThemeParentBackground(this.Handle, hdc, ref rect);
            pevent.Graphics.ReleaseHdc(hdc);
        }


        internal static class NativeMethods
        {
            [DllImport("uxtheme", ExactSpelling = true)]
            public extern static Int32 DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, ref Rectangle pRect);
        }
        

        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, this.Items[e.Index].ToString(), e.Font,
                e.Bounds, e.ForeColor, e.BackColor, TextFormatFlags.HorizontalCenter);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float x = this.ClientRectangle.X;
            float y = this.ClientRectangle.Y;

            SolidBrush sel_bg_brush = new SolidBrush(Color.Cyan); // Цвет выделения
            SolidBrush fore_brush = new SolidBrush(this.ForeColor); // Цвет букв

            int cnt = this.Height / this.ItemHeight;
            
            for (int k = 0; k < cnt; k++)
            {
                int idx = k + this.TopIndex;
                if (idx >= this.Items.Count)
                    break;

                bool selected = this.selected_n == idx;
                Font font;
                if (selected)
                {
                    fore_brush.Color = this.SelectedColor;
                    font = this.SelectedFont;

                }
                else
                {
                    fore_brush.Color = this.ForeColor;
                    font = this.Font;
                }
                string val = this.GetItemText(this.Items[idx]);
                float w_text = e.Graphics.MeasureString(val,font).Width;
                float x_text = (this.Width - w_text) / 2;
                e.Graphics.DrawString(val, font, fore_brush, new PointF(x_text, y));
                y += this.ItemHeight;
            }

            sel_bg_brush.Dispose();
            fore_brush.Dispose();
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate();
        }
        
    }


}
