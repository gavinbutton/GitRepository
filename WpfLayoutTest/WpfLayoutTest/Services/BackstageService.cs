using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using System.Reflection;
using Prism.Modularity;
using System.Windows.Media.Imaging;

namespace WpfLayoutTest.Services
{
    class BackstageService : IBackstageService
    {
        private IModuleCatalog m_catalogue;

        public BackstageService(IModuleCatalog catalogue)
        {
            m_catalogue = catalogue;
        }

        public ProductDetails GetProductDetails()
        {
            var defaultImage = new Uri("pack://application:,,,/WpfLayoutTest;component/Images/info.png");
            var shell = Assembly.GetExecutingAssembly();
            var shellName = shell.GetName();

            var details = new ProductDetails()
            {
                Product = new ModuleDetails()
                {
                    Name = shellName.Name ?? string.Empty,
                    Copyright = shell.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty,
                    Description = shell.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty,
                    Icon = new BitmapImage(shell.GetCustomAttribute<AssemblyIconAttribute>()?.IconSource ?? defaultImage),
                    Location = shell.Location,
                    Version = shellName.Version.ToString(),
                    LicenseAgreementRtf = Properties.Resources.TestLicense
                },

                Modules = m_catalogue.Modules.Select(m =>
                {
                    var assembly = Type.GetType(m.ModuleType).Assembly;
                    return new ModuleDetails()
                    {
                        Name = m.ModuleName,
                        Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty,
                        Description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty,
                        Icon = new BitmapImage(assembly.GetCustomAttribute<AssemblyIconAttribute>()?.IconSource ?? defaultImage),
                        Location = assembly.Location,
                        Version = assembly.GetName().Version.ToString()
                    };
                })
            };

            return details;
        }
    }
}
