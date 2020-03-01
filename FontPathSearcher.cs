using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lupinmail_CS
{
    class FontPathSearcher
    {
        public struct FontCollectionPath
        {
            public string fontnamekun;
            public string fontpath;
        }
        private FontCollectionPath[] FontPathRegGetter(LogWindow logw)
        {
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", false);
            logw.AddLog(DateTime.Now, "Font values:" + regkey.ValueCount);
            FontCollectionPath[] fcolle = null;
            string[] valueNames = regkey.GetValueNames();
            int index1 = -1;
            foreach (string v in valueNames)
            {
                index1++;
                fcolle[index1].fontnamekun = v;
                fcolle[index1].fontpath = (string)regkey.GetValue(v);
            }
            return fcolle;

        }
        public string FontCollectionSearcher(string fontname,LogWindow lahg)
        {
            FontCollectionPath[] fcollekun = FontPathRegGetter(lahg);
            FontCollectionPath[] fcollekungensen = null;
            int index2 = -1; 
            foreach (FontCollectionPath ea in fcollekun)
            {
                if (ea.fontnamekun.Contains(fontname))
                {
                    index2++;
                    fcollekungensen[index2] = ea;
                }
            }
            return null;
        }
    }
}
