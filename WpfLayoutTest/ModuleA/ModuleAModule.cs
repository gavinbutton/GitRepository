using Microsoft.Practices.Unity;
using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;

namespace ModuleA
{
    /// <summary>
    /// Module A stuff
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;
        IRegion _region;

        public ModuleAModule(IUnityContainer container, RegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());
            _container.RegisterTypeForNavigation<WorkspaceViewA>();

            //var workspaceView = _container.Resolve<WorkspaceView>();
            _regionManager.RegisterViewWithRegion("RibbonRegion", typeof(RibbonView));
            //_regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(WorkspaceView));
            //IRegion region = _regionManager.Regions["WorkspaceRegion"];
            //region.Activate(workspaceView);

            //_regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());
            
        }
    }
}
