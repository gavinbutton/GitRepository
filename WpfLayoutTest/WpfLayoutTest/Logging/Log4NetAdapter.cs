using log4net;
using Prism.Events;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfLayoutTest.Infrastructure;

/// <summary>
/// 
/// </summary>
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4NetConfig.config", Watch = true)]

namespace WpfLayoutTest.Logging
{
    /// <summary>
    /// An implementation of the PRISM logger facade
    /// </summary>
    /// <seealso cref="Prism.Logging.ILoggerFacade" />
    class Log4NetAdapter : ILoggerFacade
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _Logger = LogManager.GetLogger(typeof(Log4NetAdapter));

        /// <summary>
        /// A storage queue for log items while we are waiting for PRISM/Views to initialise
        /// </summary>
        private Queue<LogItem> _logQueue = new Queue<LogItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetAdapter"/> class.
        /// </summary>
        public Log4NetAdapter()
        {
            // Listen for changes in the subscription state
            GlobalCommands.LogCommand.CanExecuteChanged += LogCommand_CanExecuteChanged;
        }

        /// <summary>
        /// Handles the CanExecuteChanged event of the LogCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LogCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            if (GlobalCommands.LogCommand.CanExecute(null))
            {
                _logQueue.ToList().ForEach(item => NotifyItemAdded(item));
                _logQueue.Clear();
            }
        }

        /// <summary>
        /// Write a new log entry with the specified category and priority.
        /// </summary>
        /// <param name="message">Message body to log.</param>
        /// <param name="category">Category of the entry.</param>
        /// <param name="priority">The priority of the entry.</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    _Logger.Debug(message);
                    break;
                case Category.Warn:
                    _Logger.Warn(message);
                    break;
                case Category.Exception:
                    _Logger.Error(message);
                    break;
                case Category.Info:
                    _Logger.Info(message);
                    break;
            }

            var item = new LogItem() { Category = category, Message = message };
            NotifyItemAdded(item);

        }

        /// <summary>
        /// Notifies the item added.
        /// </summary>
        /// <param name="item">The item.</param>
        private void NotifyItemAdded(LogItem item)
        {
            // if we havent got any subscriptions for the LogCommand then store the log event until we have
            if (GlobalCommands.LogCommand.CanExecute(null))
            {

                GlobalCommands.LogCommand.Execute(item);
            }
            else
            {
                _logQueue.Enqueue(item);
            }
        }
    }
}
