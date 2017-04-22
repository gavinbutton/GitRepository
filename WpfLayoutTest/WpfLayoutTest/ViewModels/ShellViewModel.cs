using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Views;

namespace WpfLayoutTest.ViewModels
{
    class ShellViewModel : BindableBase
    {
        public ShellViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion("LogRegion", typeof(LogView));
            //regionManager.RegisterViewWithRegion("ModeRegion", typeof(ModeView));
        }
    }
}
