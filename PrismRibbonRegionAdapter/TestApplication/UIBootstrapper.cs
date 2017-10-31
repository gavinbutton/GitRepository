using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.RibbonRegionAdapter;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using Prism.Unity;
using TestApplication.Views;

namespace TestApplication
{
	public class UIBootstrapper : UnityBootstrapper
	{
        protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

            Container.RegisterType<RibbonRegionAdapter,RibbonRegionAdapter>();

            //builder.RegisterAssemblyTypes(typeof(RibbonRegionAdapter).Assembly)
            //	.InNamespaceOf<RibbonRegionAdapter>()
            //	.AsSelf()
            //	.SingleInstance();

            //builder.RegisterAssemblyTypes(typeof(UIBootstrapper).Assembly)
            //	.InNamespaceOf<MainWindowViewModel>()
            //	.AsSelf()
            //	.SingleInstance();



            Container.RegisterType<LoadModule1RibbonCommand, LoadModule1RibbonCommand>();
            Container.RegisterType<UnloadModule1RibbonCommand, UnloadModule1RibbonCommand>();
            Container.RegisterType<Module1.Module1HelloCommand, Module1.Module1HelloCommand>();

            //LoadModule1RibbonCommand
            //UnloadModule1RibbonCommand
            //builder.RegisterAssemblyTypes(typeof(UIBootstrapper).Assembly)
            //	.InNamespaceOf<Module1.Module1HelloCommand>()
            //	.AsSelf()
            //	.InstancePerDependency();
        }

		protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			var mappings = base.ConfigureRegionAdapterMappings();

			mappings.RegisterMapping(typeof(Ribbon), ServiceLocator.Current.GetInstance<RibbonRegionAdapter>());
			mappings.RegisterMapping(typeof(ContextMenu), ServiceLocator.Current.GetInstance<MergingItemsControlRegionAdapter>());

			return mappings;
		}

		protected override void ConfigureModuleCatalog()
		{
			base.ConfigureModuleCatalog();
			AddModule<Module1.Module1>();
		}

		private void AddModule<T>(string moduleName = null) where T : class, IModule
		{
			var moduleType = typeof(T);
			var module = new ModuleInfo
			{
				Ref = moduleType.Assembly.CodeBase,
				ModuleName = moduleName ?? moduleType.Name,
				ModuleType = moduleType.AssemblyQualifiedName,
				InitializationMode = InitializationMode.WhenAvailable,
			};
			ModuleCatalog.AddModule(module);
		}

		protected override DependencyObject CreateShell()
		{
			var shell = Container.Resolve<MainWindow>();
			var regionManager = Container.Resolve<IRegionManager>();
			RegionManager.SetRegionManager(shell, regionManager);
			RegionManager.UpdateRegions();
			return shell;
		}

		protected override void InitializeShell()
		{
			base.InitializeShell();
			var shell = (MainWindow)Shell;
			Application.Current.MainWindow = shell;
			Application.Current.MainWindow.Show();
		}

	}
}
