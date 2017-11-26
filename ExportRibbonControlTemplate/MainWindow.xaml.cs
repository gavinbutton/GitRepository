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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExportRibbonControlTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel() { MostRecentFiles = new FileItem[]
                {
                    new FileItem(){ FileName="A", FilePath="C:\\a.txt", Index=1},
                    new FileItem(){ FileName="b", FilePath="C:\\B.txt", Index=2},
                }
            };
        }
    }
}
