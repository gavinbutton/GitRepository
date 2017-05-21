using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Infrastructure;
using WpfLayoutTest.Logging;

namespace WpfLayoutTest.ViewModels
{
    /// <summary>
    /// VM for the log view
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    class LogViewModel : BindableActiveAwareBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LogViewModel(IEventAggregator eventAggregator)
        {
            // listen for any log update events
            var logCommand = new DelegateCommand<LogItem>(item => LogHistory.Add(item));
            GlobalCommands.LogCommand.RegisterCommand(logCommand);
        }

        /// <summary>
        /// Gets or sets the log history.
        /// </summary>
        /// <value>
        /// The log history.
        /// </value>
        public ObservableCollection<LogItem> LogHistory { get; set; } = new ObservableCollection<LogItem>();
    }
}
