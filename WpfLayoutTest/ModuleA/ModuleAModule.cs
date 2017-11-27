using Microsoft.Practices.Unity;
using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;
        
        public ModuleAModule(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());
            _container.RegisterTypeForNavigation<WorkspaceAView>();

            //var workspaceView = _container.Resolve<WorkspaceView>();
            _regionManager.RegisterViewWithRegion("RibbonRegion", typeof(RibbonView));
            _regionManager.RegisterViewWithRegion("CommandRegion", typeof(CommandView));
            //_regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(WorkspaceView));
            //IRegion region = _regionManager.Regions["WorkspaceRegion"];
            //region.Activate(workspaceView);

            //_regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());
            
        }
    }
}
