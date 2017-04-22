using Microsoft.Practices.Unity;
using ModuleB.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;
        IRegion _region;

        public ModuleBModule(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());
            _container.RegisterTypeForNavigation<WorkspaceViewB>();
        }
    }
}
