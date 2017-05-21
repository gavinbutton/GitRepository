using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    class RibbonViewModel : BindableBase
    {
        public RibbonViewModel()
        {
            AddCommand = new DelegateCommand(OnAddCommand, CanAddCommand);
        }
        
        public DelegateCommand AddCommand { get; set; }

        private void OnAddCommand()
        {
            if(CanAddCommand())
            {
                ModuleA.Commands.ModuleACommands.ApplicationAddCommand.Execute(this.AddCommand);
            }
        }

        private bool CanAddCommand()
        {

            bool canAdd = ModuleA.Commands.ModuleACommands.ApplicationAddCommand.CanExecute(this.AddCommand);
            return canAdd;
            //return true;
        }

        
    }
}
