using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using sysio = System.IO;
using sysenv = System.Environment;
namespace lupinmail_CS
{
    class FontWindowViewmodel
    {
        public IEnumerable<FontFamily> FontList { get; set; }
        public IEnumerable<string> FontPathList { get; set; }

        public FontWindowViewmodel()
        {


        }
    }
}
