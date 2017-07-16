using Infrastructure;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    class RibbonViewModel : ViewModel
    {
        public RibbonViewModel(IEventAggregator ea)
        {
            AboutCommand = new DelegateCommand(() => ea.GetEvent<AboutCommandEvent>().Publish());
        }

        ICommand AddButton { get; set; }

        public DelegateCommand DeleteCommand {get;set;}

        public DelegateCommand AboutCommand { get; set; }

    }
}
