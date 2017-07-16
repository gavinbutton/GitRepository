using Infrastructure;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Logging;

namespace WpfLayoutTest.ViewModels
{
    /// <summary>
    /// VM for the log view
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    class LogViewModel : BindableBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LogViewModel(IEventAggregator eventAggregator)
        {
            // listen for any log update events
            eventAggregator.GetEvent<LogEvent>().Subscribe(item => LogHistory.Add(item));    
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
