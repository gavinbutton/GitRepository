using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using Prism.Modularity;
using Prism.Logging;
using WpfLayoutTest.Logging;
using WpfLayoutTest.Views;
using Prism.Events;
using System.Linq;
using System.Reflection;
using WpfLayoutTest.Services;
using Infrastructure;
using Prism.Regions;
using System.Windows.Controls.Ribbon;
using Prism.RibbonRegionAdapter;

namespace WpfLayoutTest
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            var rm = Container.Resolve<IRegionManager>();

            rm.RegisterViewWithRegion(ShellRegions.RibbonRegion, typeof(ShellRibbonView));
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            var backstage = new BackstageService(this.ModuleCatalog);
            Container.RegisterInstance<IBackstageService>(backstage);
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Creates the <see cref="T:Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <returns>A <see cref="IModuleCatalog"/>from modules contained in the applications bin folder </returns>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @"." };
        }

        /// <summary>
        /// Create the <see cref="T:Prism.Logging.ILoggerFacade" /> used by the bootstrapper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            return new Log4NetAdapter();
        }

        /// <summary>
        /// Run the bootstrapper process.
        /// Informs the logger that the bootstrapping is complete
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true" />, registers default Prism Library services in the container. This is the default behavior.</param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            var eventAggregator = Container.Resolve<IEventAggregator>();
            var logger = Container.Resolve<ILoggerFacade>() as Log4NetAdapter;
            logger.StartupComplete(eventAggregator);

            var backstage = Container.Resolve<IBackstageService>();
            var productDetails = backstage.GetProductDetails();

            var items = new string[] { $"Product:{productDetails.Product.Name}\nVersion:{ productDetails.Product.Version }\nPath:{productDetails.Product.Location}"}
                .Concat(productDetails.Modules.Select(m => $"Module:{m.Name}\nVersion:{ m.Version }\nPath:{m.Location}"))
                .ToList();
            items.ForEach(m => logger.Log(m, Category.Info, Priority.None));

        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Prism.Regions.RegionAdapterMappings" /> instance containing all the mappings.
        /// </returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(Ribbon), Container.Resolve<RibbonRegionAdapter>());
            return mappings;
        }
    }
}
