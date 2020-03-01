using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using sysio = System.IO;
using sysenv = System.Environment;
namespace lupinmail_CS
{
    /// <summary>
    /// FontLoading.xaml の相互作用ロジック
    /// </summary>
    public partial class FontLoading : Window
    {

        private FontFamily ffont;
        public List<KeyValuePairstr> FontPathNamelist { get; set; }
        public delegate void FormCloseHandler(FontLoading sender, EventArgs e);
        public event FormCloseHandler FormCloseEvent;
        public FontLoading(FontFamily currentfont)
        {
            InitializeComponent();
            FontPathNamelist = new List<KeyValuePairstr>();
            MouseLeftButtonDown += (o, e) => DragMove();
            this.TaskbarItemInfo = new System.Windows.Shell.TaskbarItemInfo();
            this.TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
            ffont = currentfont;
        }
        private async void syori(FontFamily cfont)
        {
            await Task.Run(() =>
            {
                List<string> fontpathlist1 = fontlistkun();
                foreach (string pathstr in fontpathlist1)
                {
                    System.Drawing.Text.PrivateFontCollection pfc =
        new System.Drawing.Text.PrivateFontCollection();
                    pfc.AddFontFile(pathstr);
                    string fntname = pfc.Families[0].Name;
                    KeyValuePairstr newobj = new KeyValuePairstr(pathstr, fntname);
                    FontPathNamelist.Add(newobj);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        logText.Content = fntname + " : " + pathstr;
                    }));
                    //pfc.Families[0].Name;
                }
            });
            this.TaskbarItemInfo.ProgressState
= System.Windows.Shell.TaskbarItemProgressState.None;
            sintyokubar.IsIndeterminate = false;
            this.Close();
        }
        private List<string> fontlistkun()
        {
            //this.FontList = Fonts.SystemFontFamilies;

            string[] ttcfilesctr = sysio.Directory.GetFiles(sysenv.CurrentDirectory + "\\Fonts", "*.ttc");
            string[] ttffilesctr = sysio.Directory.GetFiles(sysenv.CurrentDirectory + "\\Fonts", "*.ttf");
            string[] ttcfilessys = sysio.Directory.GetFiles(sysenv.GetFolderPath(sysenv.SpecialFolder.Windows) + "\\Fonts", "*.ttc");
            string[] ttffilessys = sysio.Directory.GetFiles(sysenv.GetFolderPath(sysenv.SpecialFolder.Windows) + "\\Fonts", "*.ttf");





            /*
            string[] sysfontfiles = new string[otffilessys.Length + ttffilessys.Length];
            Array.Copy(otffilessys, sysfontfiles, otffilessys.Length);
            // Array.Copy(otcfilessys, 0,sysfontfiles, otffilessys.Length, otcfilessys.Length);
            Array.Copy(ttffilessys, 0, sysfontfiles, otffilessys.Length, ttffilessys.Length);
            //Array.Copy(ttcfilessys, 0, sysfontfiles, ttffilessys.Length, ttcfilessys.Length);
            string[] ctrfontfiles = new string[otffilesctr.Length + ttffilesctr.Length];
            Array.Copy(otffilesctr, ctrfontfiles, otffilesctr.Length);
            //Array.Copy(otcfilesctr, 0, ctrfontfiles, otffilesctr.Length, otcfilesctr.Length);
            Array.Copy(ttffilesctr, 0, ctrfontfiles, otffilesctr.Length, ttffilesctr.Length);
            // Array.Copy(ttcfilesctr, 0, ctrfontfiles, ttffilesctr.Length, ttcfilesctr.Length);
            string[] allfontfilespath = new string[sysfontfiles.Length + ctrfontfiles.Length];
            Array.Copy(ctrfontfiles, allfontfilespath, ctrfontfiles.Length);
            Array.Copy(sysfontfiles, 0, allfontfilespath, ctrfontfiles.Length, sysfontfiles.Length);
            List<string> returnlist = new List<string>();
            returnlist.AddRange(allfontfilespath);
            return returnlist;
            */
            List<string> allpathstrlist = new List<string>();
            allpathstrlist.AddRange(ttcfilesctr);
            allpathstrlist.AddRange(ttcfilessys);
            allpathstrlist.AddRange(ttffilesctr);
            allpathstrlist.AddRange(ttffilessys);
            return allpathstrlist;

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            this.TaskbarItemInfo.ProgressState
  = System.Windows.Shell.TaskbarItemProgressState.None;
            sintyokubar.IsIndeterminate = false;

            FormCloseEvent(this, null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            syori(ffont);
        }
    }
}
