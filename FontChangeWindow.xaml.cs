using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using sysio = System.IO;
using sysenv = System.Environment;

namespace lupinmail_CS
{
    /// <summary>
    /// FontChangeWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FontChangeWindow : Window
    {
        private FontFamily fmamily;
        public KeyValuePairstr SelectFontF { get; set; }
        public bool Isfontset { get; set; }
        private List<KeyValuePairstr> fontpathnamelist;
        public delegate void FormCloseHandler(FontChangeWindow sender, EventArgs e);
        public event FormCloseHandler FormCloseEvent;
        public FontChangeWindow(FontFamily fontFamily)
        {

            fontpathnamelist = new List<KeyValuePairstr>();
            fmamily = fontFamily;
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FontLoading floading = new FontLoading(fmamily);
            floading.Owner = this;
            floading.FormCloseEvent += new FontLoading.FormCloseHandler(FontLoading_Closed);
            floading.ShowDialog();
            //FontListBox.ItemsSource = fontpathnamelist;
            //FontListBox.ItemsSource = fontlistkun();
        }
        private void FontLoading_Closed(FontLoading sender,EventArgs e)
        {
            FontListBox.ItemsSource = sender.FontPathNamelist;
            FontListBox.SelectedIndex = 0;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {

            Isfontset = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            FormCloseEvent(this, null);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            
            SelectFontF = (KeyValuePairstr)FontListBox.Items[FontListBox.SelectedIndex];
            Isfontset = true;
            Close();
        }

        private void OKButton_Initialized(object sender, EventArgs e)
        {
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



    }


    
}
