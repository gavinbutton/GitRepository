using Prism.Commands;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    class ModeSelectorViewModel : BindableBase
    {
        IRegionManager _regionManager;
        ILoggerFacade _logger;

        public ModeSelectorViewModel(IRegionManager regionManager, ILoggerFacade logger)
        {
            _regionManager = regionManager;
            _logger = logger;

            SwitchToModeA = new DelegateCommand<string>((navigatePath) => { _regionManager.RequestNavigate("WorkspaceRegion", navigatePath); _logger.Log($"Switched to {navigatePath}", Category.Info, Priority.High); }, (navigatePath) => true);
        }
        public ICommand SwitchToModeA { get; set; }
    }
}
