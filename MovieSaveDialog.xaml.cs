using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using msw32=Microsoft.Win32;

namespace lupinmail_CS
{
    /// <summary>
    /// MovieSaveDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class MovieSaveDialog : Window
    {
        public delegate void FormCloseHandler(MovieSaveDialog sender, EventArgs e);
        public event FormCloseHandler FormCloseEvent;
        private LogWindow lgwinobj;
        private bool EncodeFlag=false;
        private Process proc;
        private Process ffprobec;
        private long allframe;
        private CancellationTokenSource ffctoken;
        public MovieSaveDialog(LogWindow lgwinkun)
        {
            InitializeComponent();
            lgwinobj = lgwinkun;

        }

        private async void Save_button_Click(object sender, RoutedEventArgs e)
        {
            if (PathTextBox.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("エクスポートするファイルを指定してください。", "エラー", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            Save_button.IsEnabled = false;
            lgwinobj.AddLog(DateTime.Now, "Movie Save: " + PathTextBox.Text);
            string ffmpegoption = FFMPEG_OPTION_BOX.Text;
            string cmdline = "-y -i temp.avi ";

            string cmdline2 = "-v error -i temp.avi -show_streams -of json";
            ffprobec = new Process();
            string JSON_DATA = "";
            await Task.Run(() =>
            {
                using (var ctoken = new CancellationTokenSource())
                {
                    ffprobec.StartInfo.FileName = "ffprobe.exe";
                    ffprobec.StartInfo.Arguments = cmdline2;
                    ffprobec.StartInfo.UseShellExecute = false;
                    ffprobec.StartInfo.RedirectStandardOutput = true;
                    ffprobec.StartInfo.RedirectStandardError = true;
                    ffprobec.StartInfo.RedirectStandardInput = false;
                    ffprobec.ErrorDataReceived += FFprobe_ErrorDataReceived;
                    ffprobec.EnableRaisingEvents = true;
                    ffprobec.Exited += (sender114514, ev) =>
                    {
                        ffprobec.WaitForExit();
                        ctoken.Cancel();
                    };
                    //ウィンドウを表示しないようにする
                    ffprobec.StartInfo.CreateNoWindow = true;
                    ffprobec.Start();
                    ffprobec.BeginErrorReadLine();
                    JSON_DATA = ffprobec.StandardOutput.ReadToEnd();
                    ctoken.Token.WaitHandle.WaitOne();
                    ffprobec.Close();
                }
            });
            FFprobeJSON ffjson = new FFprobeJSON();
            await Task.Run(() =>
            {
                ffjson = JsonConvert.DeserializeObject<FFprobeJSON>(JSON_DATA);
                allframe = ffjson.streams[0].nb_frames;
            });
            lgwinobj.AddLog(DateTime.Now, "Frame count : " + allframe);
            cmdline += ffmpegoption;
            cmdline += " \"";
            cmdline += PathTextBox.Text;
            cmdline += "\"";
            ffctoken = new CancellationTokenSource();
            proc = new Process();
            proc.StartInfo.FileName = "ffmpeg.exe";
            proc.StartInfo.Arguments = cmdline;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = false;
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += FFmpeg_OutputDataReceived;
            proc.ErrorDataReceived += FFmpeg_ErrorDataReceived;
            proc.Exited += (sender1, ev) =>
            {
                proc.WaitForExit();
                proc.Close();
                Dispatcher.InvokeAsync(() =>
                {
                    lgwinobj.taskbarItemProgressState = System.Windows.Shell.TaskbarItemProgressState.None;
                    Save_button.IsEnabled = true;
                    System.Windows.Forms.MessageBox.Show("エンコード終了!", "Encoded");
                });

                EncodeFlag = false;

            };
            //ウィンドウを表示しないようにする
            await Task.Run(() => 
            {
                proc.StartInfo.CreateNoWindow = true;
                EncodeFlag = true;
                Dispatcher.InvokeAsync(() =>
                {
                    lgwinobj.taskbarItemProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;

                });
                proc.Start();
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
            });


        }
        //ErrorDataReceivedイベントハンドラ
        private void FFprobe_ErrorDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            if(e.Data=="" || e.Data == null)
            {
                return;
            }
            //エラー出力された文字列を表示する
            Dispatcher.InvokeAsync(() =>
            {
                lgwinobj.AddLog(DateTime.Now,"Error: " + e.Data);

            });
        }
        private void FFmpeg_OutputDataReceived(object sender,
System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data == "" || e.Data == null)
            {
                return;
            }
            //出力された文字列を表示する
            Dispatcher.InvokeAsync(() =>
            {
                lgwinobj.AddLog(DateTime.Now, e.Data);

            });
        }

        //ErrorDataReceivedイベントハンドラ
        private void FFmpeg_ErrorDataReceived(object sender,
            System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                System.Text.RegularExpressions.MatchCollection mc =
        System.Text.RegularExpressions.Regex.Matches(
        e.Data, @"frame.*?fps");
                foreach (System.Text.RegularExpressions.Match m in mc)
                {
                    string datasuuji = m.Value.Replace("frame", "");
                    datasuuji = datasuuji.Replace("fps", "");
                    datasuuji = datasuuji.Replace(" ", "");
                    datasuuji = datasuuji.Replace("=", "");
                    long datasujiII = long.Parse(datasuuji);
                    double ara = (double)datasujiII / (double)allframe;
                    ara = Math.Truncate(ara * 100.0) / 100.0;
                    Dispatcher.InvokeAsync(() =>
                    {
                        lgwinobj.taskbarItemValue = ara;
                        //lgwinobj.AddLog(DateTime.Now, "TEST:" + ara + "END");
                    });
                }
            }
            //エラー出力された文字列を表示する
            Dispatcher.InvokeAsync(() =>
            {
                lgwinobj.AddLog(DateTime.Now, e.Data);

            });
        }
        private void Close_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            msw32.SaveFileDialog sdr = new msw32.SaveFileDialog();
            sdr.Filter = "MP4ファイル (*.mp4;*.m4v)|*.mp4;*.m4v|WebMファイル(*.webm)|*.webm|すべてのファイル(*)|*";
            sdr.Title = "エクスポートするファイルを指定してください。";
            sdr.OverwritePrompt = true;
            if (sdr.ShowDialog() == true)
            {
                PathTextBox.Text = sdr.FileName;
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            //DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EncodeFlag == true)
            {
                System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("エンコード中です。\n中断してもよろしいですか?","情報",System.Windows.Forms.MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        proc.Kill();

                        break;
                    case System.Windows.Forms.DialogResult.No:
                        e.Cancel = true;
                        break;

                }
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            FormCloseEvent(this, null);
        }
    }
}
