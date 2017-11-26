using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRibbonControlTemplate
{
   public  class MainWindowViewModel
    {
        public IEnumerable<FileItem> MostRecentFiles { get; set; }
    }

    public class FileItem
    {
        public string FileName { get; set; }
        public int Index { get; set; }
        public string FilePath { get; set; }
    }
}
