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

namespace lupinmail_CS
{
    /// <summary>
    /// LogWIndow.xaml の相互作用ロジック
    /// </summary>
    public partial class LogWindow : Window
    {
        private bool Mainwindow_closed = false;
        public void MainWin_Close()
        {
            Mainwindow_closed = true;
            this.Close();
        }
        public struct LogStruct
        {
            public DateTime logtime;
            public string logdata;

        }
        public void AddLog(DateTime logtime,string logdata)
        {
            LogListBox.Items.Add(logtime.ToShortTimeString() + "   " + logdata);
        }
        public System.Windows.Shell.TaskbarItemProgressState taskbarItemProgressState
        {
            get { return TaskbarItemInfo.ProgressState; }
            set { TaskbarItemInfo.ProgressState = value; }
        }
        public double taskbarItemValue
        {
            get { return TaskbarItemInfo.ProgressValue; }
            set { TaskbarItemInfo.ProgressValue = value; }
        }
        public LogWindow()
        {
            InitializeComponent();
            this.TaskbarItemInfo = new System.Windows.Shell.TaskbarItemInfo();
        }

        private void LogWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Mainwindow_closed)
            {
                return;
            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
