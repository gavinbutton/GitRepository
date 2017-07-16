using Infrastructure;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    public class WorkspaceAViewModel : BindableBase
    {
        private IEventAggregator m_eventAggregator;
        private ILoggerFacade m_logger;

        public WorkspaceAViewModel(IEventAggregator ea, ILoggerFacade logger)
        {
            CommandBinding deleteItemCommandBinding = new CommandBinding(ApplicationCommands.Delete, OnDelete, CanDelete);
            CommandManager.RegisterClassCommandBinding(typeof(WorkspaceAViewModel), deleteItemCommandBinding);
            CommandBindings.Add(deleteItemCommandBinding);
            m_eventAggregator = ea;
            m_logger = logger;
        }

        private IEnumerable<string> m_items = new string[] { "A", "B", "C" };

        public IEnumerable<string> Items
        {
            get
            {
                return m_items;
            }

            set
            {
                SetProperty(ref m_items, value);
            }
        }

        private readonly CommandBindingCollection _CommandBindings = new CommandBindingCollection();
        public CommandBindingCollection CommandBindings
        {
            get
            {
                return _CommandBindings;
            }
        }

        private void OnDelete(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            m_logger.Log("Deleted Item", Prism.Logging.Category.Info, Priority.None);
        }

        private void CanDelete(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
