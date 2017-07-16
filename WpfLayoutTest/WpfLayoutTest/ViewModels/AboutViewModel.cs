using Infrastructure;
using Infrastructure.Models;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfLayoutTest.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        public AboutViewModel(IBackstageService backstage)
        {
            Application = backstage.GetProductDetails();   
        }

        private ModuleDetails m_selectedModule;
        public ModuleDetails SelectedModule
        {
            get { return m_selectedModule; }
            set
            {
                SetProperty(ref m_selectedModule, value);
            }
        }

        public ProductDetails Application { get; set; }
    }
}
