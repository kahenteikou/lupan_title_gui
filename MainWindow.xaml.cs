using Microsoft.Diagnostics.Runtime.ICorDebug;
using w32=Microsoft.Win32;
using wform = System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.WindowsAPICodePack.Dialogs.Controls;
using Microsoft.WindowsAPICodePack;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using sysdw = System.Drawing;
using System.CodeDom.Compiler;

namespace lupinmail_CS
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private struct LogTMSTRUCT
        {
            public DateTime dtm;
            public string logdata;
            public LogWindow lwwin;
        }
        //private LogWindow logwin = new LogWindow();
        private static LogWindow logwin;
        // sprintf を使う為にC++ランタイムをインクルード
        #region swprintf

        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, int i2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, double d2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, String s2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, int i2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, double d2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, String s2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, int i2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, double d2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, String s2);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, int i2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, int i2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, int i2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, double d2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, double d2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, double d2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, String s2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, String s2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, int i1, String s2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, int i2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, int i2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, int i2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, double d2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, double d2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, double d2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, String s2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, String s2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, double d1, String s2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, int i2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, int i2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, int i2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, double d2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, double d2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, double d2, String s3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, String s2, int i3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, String s2, double d3);
        [DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int swprintf(StringBuilder buffer, String format, String s1, String s2, String s3);

        #endregion //ここまで
        
        private FontFamily CurrentFont { get; set; }
        public MainWindow(LogWindow lwwin2)
        {
            InitializeComponent();
            this.TaskbarItemInfo = new System.Windows.Shell.TaskbarItemInfo();
            logwin = lwwin2;

        }
        public IntPtr Handle
        {
            get
            {
                var helper = new System.Windows.Interop.WindowInteropHelper(this);
                return helper.Handle;
            }
        }
        private async void Create_button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement1.Close();
            int x_size = int.Parse(X_TEXTBOX.Text);
            int y_size = int.Parse(Y_TEXTBOX.Text);

            if(x_size > 10000)
            {
                System.Windows.Forms.MessageBox.Show("横の解像度が範囲外です。\r\n解像度は10000x10000以下を指定してください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            if(y_size > 10000)
            {
                System.Windows.Forms.MessageBox.Show("縦の解像度が範囲外です。\r\n解像度は10000x10000以下を指定してください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            string lupin_text_data = LUPIN_TEXT.Text; //テキストデータ
            if (lupin_text_data.Length < 1)
            {
                System.Windows.Forms.MessageBox.Show("文字を入力してください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            if(lupin_text_data.Length > 60)
            {
                System.Windows.Forms.MessageBox.Show("60文字以内にしてください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            lupin_text_data = lupin_text_data.Replace(" ", "　"); //半角スペースを全角スペースに変換(コマンドラインオプションでエラー起こさないように)


            string executablepath = "lupinavi2";
            string cmdline = "-o temp.avi";
            cmdline += " -m ";
            cmdline += "\"";
            cmdline += lupin_text_data;
            cmdline += "\"";
            StringBuilder stb = new StringBuilder(lupin_text_data);
            swprintf(stb, " -s %dx%d", x_size, y_size);
            cmdline += stb;
            Process proc = new Process();
            proc.StartInfo.FileName = executablepath;
            proc.StartInfo.Arguments = cmdline;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            proc.StartInfo.CreateNoWindow = true;
            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
            Save_button.IsEnabled = false;
            Play_Button.IsEnabled = false;
            Create_button.IsEnabled = false;
            string dataretu="HOMO";
            await Task.Run(() =>
            {
                proc.Start();
                this.Dispatcher.Invoke((Action)(() =>
                {

                    logwin.AddLog(DateTime.Now, "exec: " + executablepath + " " + cmdline);
                }));
                dataretu = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
            });
            proc.Close();
            logwin.AddLog(DateTime.Now,dataretu);
            MediaElement1.Source = new Uri("temp.avi", UriKind.RelativeOrAbsolute);
            Save_button.IsEnabled = true;
            Play_Button.IsEnabled = true;

            Create_button.IsEnabled = true;
            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.None;


        }

        private void Window_Initialized(object sender, EventArgs e)
        {


            MediaElement1.LoadedBehavior = MediaState.Manual;
            System.Windows.Forms.Application.EnableVisualStyles();//Messagebox用
            if(!System.IO.File.Exists(System.Environment.CurrentDirectory + "\\lupinavi2.exe"))
            {
                System.Windows.Forms.MessageBox.Show("lupinavi2.exeが見つかりませんでした。\r\nファイルの作成に必要ですので\r\n消さないでください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                System.Diagnostics.Process hProcess = System.Diagnostics.Process.GetCurrentProcess();
                hProcess.Kill();
            }
            if(!System.IO.Directory.Exists(System.Environment.CurrentDirectory + "\\Fonts"))
                System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\Fonts");
            if (!System.IO.File.Exists(System.Environment.CurrentDirectory +"\\ffmpeg.exe"))
            {
                System.Windows.Forms.MessageBox.Show("ffmpeg.exeが見つかりませんでした。\r\nファイルの保存に必要ですので\r\n消さないでください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                System.Diagnostics.Process hProcess = System.Diagnostics.Process.GetCurrentProcess();
                hProcess.Kill();
            }
            string Win_FONT_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            Win_FONT_PATH = Win_FONT_PATH + "\\Fonts\\";
            string strCurDir = System.Environment.CurrentDirectory;
            //MessageBox.Show(strCurDir);
            FontNameBox.Text = "ＭＳ 明朝";
            System.IO.File.Copy(Win_FONT_PATH + "msmincho.ttc", strCurDir + "\\font.ttc",true);
            byte[] datadest = new byte[9];
            datadest[8] = 0;
            string setpath = "font.ttc";
            byte[] data = System.Text.Encoding.ASCII.GetBytes(setpath);
            data.CopyTo(datadest, 0);
            string fileName = "fontpath";
            using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
            {
                writer.Write(datadest);
            }



        }

        private void X_TEXTBOX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void X_TEXTBOX_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void Y_TEXTBOX_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void Y_TEXTBOX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MediaElement1.Close();

            System.IO.FileInfo fi = new System.IO.FileInfo(System.Environment.CurrentDirectory + "\\temp.avi");
            fi.Delete();
            logwin.MainWin_Close();
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MediaElement1.Stop();
            }
            catch (Exception er)
            {
                //しょりしない 
                logwin.AddLog(DateTime.Now, "Error: " + er.ToString());
            }
            MediaElement1.Play();
        }

        private async void Save_button_Click(object sender, RoutedEventArgs e)
        {
            /*
            string ffmpegoption;
            var dialog = new CommonSaveFileDialog();
            // ファイルの種類を設定

            CommonFileDialogTextBox commonFileDialogText = new CommonFileDialogTextBox();
            CommonFileDialogLabel commonFileDialogLabel = new CommonFileDialogLabel();
            commonFileDialogLabel.Text = ("FFMPEGのオプション");
            dialog.Controls.Add(commonFileDialogLabel);
            dialog.Controls.Add(commonFileDialogText);
            dialog.Filters.Add(new CommonFileDialogFilter("MP4 Video ファイル", "*.mp4"));
            dialog.Filters.Add(new CommonFileDialogFilter("Webm Video ファイル", "*.webm"));
            dialog.Filters.Add(new CommonFileDialogFilter("すべてのファイル", "*"));
            dialog.AlwaysAppendDefaultExtension = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                
                logwin.AddLog(DateTime.Now, "File save : " + dialog.FileName);
                ffmpegoption = commonFileDialogText.Text;
                if (commonFileDialogText.Text == "")
                {
                    if (System.IO.Path.GetExtension(dialog.FileName) == ".mp4")
                    {
                        ffmpegoption = "-vcodec libx264";
                    }
                    if (System.IO.Path.GetExtension(dialog.FileName) == ".webm")
                    {
                        ffmpegoption = "-vcodec libvpx-vp9";
                    }
                }
                string cmdline = "-y -i temp.avi ";
                cmdline += ffmpegoption;
                cmdline += " \"";
                cmdline += dialog.FileName;
                cmdline += "\"";
                Process proc = new Process();
                proc.StartInfo.FileName = "ffmpeg.exe";
                proc.StartInfo.Arguments = cmdline;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardInput = false;
                //ウィンドウを表示しないようにする
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
                wform.MessageBox.Show("エンコードに成功!", "Success");

            }*/
            MovieSaveDialog msdialog = new MovieSaveDialog(logwin);
            msdialog.Owner = this;
            msdialog.FormCloseEvent += MSFM_CLosed;
            var topElm = ((UIElement)VisualTreeHelper.GetChild(this, 0));
            topElm.IsEnabled = false;
            msdialog.Show();

        }
        private void MSFM_CLosed(MovieSaveDialog sender,EventArgs e)
        {
            var topElm = ((UIElement)VisualTreeHelper.GetChild(this, 0));
            topElm.IsEnabled = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            
            CurrentFont = new FontFamily("MS Mincho");
            FontNameBox.FontFamily = CurrentFont;
            FontNameBox.Text = CurrentFont.ToString();
        }

        private void FontChangeButton_Click(object sender, RoutedEventArgs e)
        {
            FontChangeWindow fontChangeWindow = new FontChangeWindow(CurrentFont);
            fontChangeWindow.Owner = this;
            fontChangeWindow.FormCloseEvent += new FontChangeWindow.FormCloseHandler(FontWindows_Close);
            fontChangeWindow.ShowDialog();
        }
        Action<Object> LogUpdate = (o) =>
        {
            LogTMSTRUCT lstr = (LogTMSTRUCT)o;
            lstr.lwwin.AddLog(lstr.dtm, lstr.logdata);
            
        };
        private void FontWindows_Close(FontChangeWindow sender,EventArgs e)
        {
            if (sender.Isfontset) //フォントが選択されているとき

            {
                //Debug.Print(sender.SelectFontF.ToString());
                logwin.AddLog(DateTime.Now, "Font Selected:" + sender.SelectFontF.ToString());
                CurrentFont = new FontFamily(new Uri("file:///" + sender.SelectFontF.Key,UriKind.Absolute),sender.SelectFontF.Value);
                FontNameBox.FontFamily = CurrentFont;
                FontNameBox.Text = CurrentFont.ToString();
                
                logwin.AddLog(DateTime.Now, "Font path: " + sender.SelectFontF.Key);
                FNTCopyAsync(sender.SelectFontF.Key, logwin);


            }
        }
        async private void FNTCopyAsync(string fntpath,LogWindow lwa)
        {
            await Task.Run(() =>
            {
                byte[] datadest=new byte[9];
                datadest[8] = 0;
                string setpath="font.ttc";
                string strCurDir = System.Environment.CurrentDirectory;
                if(System.IO.Path.GetExtension(fntpath).ToLower() == ".ttc")
                {
                    setpath = "font.ttc";
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(setpath);
                    data.CopyTo(datadest, 0);

                }
                else if (System.IO.Path.GetExtension(fntpath).ToLower() == ".ttf")
                {
                    setpath = "font.ttf";
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(setpath);
                    data.CopyTo(datadest, 0);

                }
                else {

                }
                string fileName = "fontpath";
                using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
                {

                    writer.Write(datadest);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        lwa.AddLog(DateTime.Now, "Written : fontpath" );
                    }));
                }

                string deststr = strCurDir + "\\" + setpath;
                this.Dispatcher.Invoke((Action)(() =>
                {
                    lwa.AddLog(DateTime.Now, "Copying : " + fntpath + " -> " + deststr);
                }));
                File.Copy(fntpath, deststr, true);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    lwa.AddLog(DateTime.Now, "Copying Successful!!");
                }));
            });
        }


    }
}
