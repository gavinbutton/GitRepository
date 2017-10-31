using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace TestApplication.Module1
{
	[Module(ModuleName = "Module1", OnDemand = true)]
	class Module1 : IModule
	{

    private readonly IRegionManager _regionManager;

        private readonly IUnityContainer m_container;

		public Module1(IRegionManager regionManager, IUnityContainer container)
    {
            m_container = container;
      _regionManager = regionManager;
    }

		public void Initialize()
		{
            m_container.RegisterType<Module1HelloCommand, Module1HelloCommand>();

            _regionManager.RegisterViewWithRegion(ShellRegions.EditorContextMenu, GetContextMenu);
			//_regionManager.RegisterViewWithRegion(ShellRegions.MainMenu, typeof(Ribbon));
		}

		private object GetContextMenu()
		{
			var cmv = ServiceLocator.Current.GetInstance<EditorContextMenuView>();
			return cmv.ContextMenu;
		}
	}
}
