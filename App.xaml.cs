using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace lupinmail_CS
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        static MainWindow mainWindow;
        static LogWindow logWindow;


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            logWindow = new LogWindow();
            mainWindow = new MainWindow(logWindow);
            logWindow.Show();
            mainWindow.Show();
        }
    }
}
