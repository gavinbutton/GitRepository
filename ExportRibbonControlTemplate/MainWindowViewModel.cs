using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportRibbonControlTemplate
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            RecentFileCommand = new DelegateCommand<string>((s) => { var i = 0; });
        }
        public IEnumerable<FileItem> MostRecentFiles { get; set; }
        public DelegateCommand<string> RecentFileCommand {get;set;}
    }


    public class FileItem
    {
        public string FileName { get; set; }
        public int Index { get; set; }
        public string FilePath { get; set; }
    }
}
