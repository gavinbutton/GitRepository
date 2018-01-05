using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RibbonMinimizedTest
{
    public class RibbonViewModel
    {
        public RibbonViewModel()
        {
            OpenSettingsCommand = new DelegateCommand(() => MessageBox.Show("OpenSettings"));
            AboutCommand = new DelegateCommand(() => MessageBox.Show("About"));
            FileNewCommand = new DelegateCommand(() => MessageBox.Show("FileNew"));
            FileNewWidgetSetCommand = new DelegateCommand(() => MessageBox.Show("NewWidgetSet"));
            FileNewLayerRefCommand = new DelegateCommand(() => MessageBox.Show("LayerRef"));
            ExitCommand = new DelegateCommand(() => MessageBox.Show("LayerRef"));
        }

        public ICommand OpenSettingsCommand { get; set; }

        public ICommand AboutCommand { get; set; }

        public ICommand FileNewCommand { get; set; }
    
        public ICommand FileNewWidgetSetCommand { get; set; }

        public ICommand FileNewLayerRefCommand { get; set; }

        public ICommand ExitCommand { get; set; }
    }
}
