using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfLayoutTest.Infrastructure;

namespace ModuleA.ViewModels
{
    class WorkspaceAViewModel : BindableActiveAwareBase
    {
        private DelegateCommand<object> _addCommand; 
        public WorkspaceAViewModel()
        {
            // Create a command binding to this view model
            CommandBinding testCommandBinding = new CommandBinding(ApplicationCommands.Save, OnTestRouteExecute, OnTestRouteCanExecute);

            //Register the binding to the class
            CommandManager.RegisterClassCommandBinding(typeof(WorkspaceAViewModel), testCommandBinding);

            //Adds the binding to the CommandBindingCollection
            _CommandBindings.Add(testCommandBinding);

            //register commands we are interested in
            _addCommand = new DelegateCommand<object>(OnApplicationAdd, CanApplicationAdd).ObservesProperty(() => CanAddFlag);
            ModuleA.Commands.ModuleACommands.ApplicationAddCommand.RegisterCommand(_addCommand);

        }

        private readonly CommandBindingCollection _CommandBindings = new CommandBindingCollection();
        public CommandBindingCollection CommandBindings
        {
            get
            {
                return _CommandBindings;
            }
        }

        private bool _canAddFlag = false;
        public bool CanAddFlag
        {
            get { return _canAddFlag; }
            set
                {
                    SetProperty(ref _canAddFlag, value);
                }
        }

        public string LabelText { get => _labelText; set => SetProperty(ref _labelText, value); }
        

        private string _labelText = string.Empty;

        #region On Test Routed Command

        public void OnTestRouteCanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void OnTestRouteExecute(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.LabelText = "Test RouteUICommand have been clicked and Executed!";
        }

        public void OnApplicationAdd(object c)
        {

        }

        public bool CanApplicationAdd(object c)
        {
            this.LabelText = DateTime.Now.ToString();
            return CanAddFlag;
        }

        #endregion
    }
}
