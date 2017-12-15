using Microsoft.Practices.Unity;
using ModuleA.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System;
using Infrastructure;

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

            
            // Register ribbon Method A - allows unmerging
            if (_regionManager.Regions.ContainsRegionWithName(ShellRegions.ShellRibbon))
            {
                _regionManager.Regions[ShellRegions.ShellRibbon].Add(_container.Resolve<RibbonView>(), "ModuleA");
            }
            else
            {
                //m_logger.Log("Ribbon region not found. Has it been registered yet?", Category.Debug, Priority.None);
            }

            // Register ribbon region method B
            //_regionManager.RegisterViewWithRegion(ShellRegions.ShellRibbon, typeof(RibbonView));


            _regionManager.RegisterViewWithRegion("StatusRegion", typeof(StatusBar));
            //_regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(WorkspaceView));
            //IRegion region = _regionManager.Regions["WorkspaceRegion"];
            //region.Activate(workspaceView);

            //_regionManager.AddToRegion("ModeRegion", _container.Resolve<ModeSelector>());

        }
    }
}
