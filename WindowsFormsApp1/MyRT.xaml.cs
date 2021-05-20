using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;


namespace WindowsFormsApp1
{
    /// <summary>
    /// Interaction logic for MyRT.xaml
    /// </summary>
    public partial class MyRT : RichTextBox
    {
        public void SetFontF(FontFamily ff)
        {
        }

        public void SetQ(string q)
        {
        } 
        public void SetSize(int s)
        {
        }
        public MyRT()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = new CultureInfo("ru-RU");
            string st = @"{\rtf1\ansi\cpg1251 Это текст! }";
            st = ToCyrillic(st);
            byte[] bb = Encoding.Default.GetBytes(st);
            MemoryStream inputStream = new MemoryStream(bb);
            FlowDocument fldoc = new FlowDocument();
            TextRange tr = new TextRange(
                fldoc.ContentStart, fldoc.ContentEnd);            
            tr.Load(inputStream, DataFormats.Rtf);
            Document = fldoc;

            
        }
        public static string ToCyrillic(string cyrText)
        {
            System.Windows.Forms.RichTextBox dummy = new System.Windows.Forms.RichTextBox { Text = cyrText };

            dummy.SelectAll();
            return dummy.SelectedText;

        }


        private void RTB_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }
        public void SetRTFText(MemoryStream stream)
        {        
            
        }

        public void SetColor(Color color)
        {

        }
        public void ClearRTF()
        {

        }
    }

}
