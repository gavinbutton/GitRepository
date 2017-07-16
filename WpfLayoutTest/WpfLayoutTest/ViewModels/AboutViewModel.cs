using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfLayoutTest.ViewModels
{
    public class AboutViewModel : BindableBase
    {
        public AboutViewModel()
        {
            ProductName = "Test WPF LAyout";

            ProductDetails = "Toolbox 2017";

            ProductVersion = "1.0.0.1";

            ProductCopyright = "C 2017 MyCompany";
            
            Modules = new ModuleDetails[]
            {
                new ViewModels.ModuleDetails
                {
                    Name = "ModuleA",
                    Description = "Desc for Module A",
                    Version = "1.0.1.2",
                    Icon = new BitmapImage(new Uri("pack://application:,,,/WpfLayoutTest;component/Images/info.png"))
                },

                new ViewModels.ModuleDetails
                {
                    Name = "ModuleB",
                    Description = "Desc for Module B",
                    Version = "1.0.1.3",
                    Icon = new BitmapImage(new Uri("pack://application:,,,/WpfLayoutTest;component/Images/info.png"))
                },
            };
        }
        public string ProductName { get; set; }

        public string ProductDetails { get; set; }

        public string ProductVersion { get; set; }

        public string ProductCopyright { get; set; }

        public IEnumerable<ModuleDetails> Modules { get; set; }

        private ModuleDetails m_selectedModule;
        public ModuleDetails SelectedModule
        {
            get { return m_selectedModule; }
            set
            {
                SetProperty(ref m_selectedModule, value);
                    }
        }

    }

    public class ModuleDetails
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public BitmapImage Icon { get; set; }
    }
}
