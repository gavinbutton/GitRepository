using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Infrastructure;

namespace ModuleB.ViewModels
{
    class WorkspaceBViewModel : BindableActiveAwareBase
    {
        private ILoggerFacade logger;
        public WorkspaceBViewModel(ILoggerFacade logger)
        {
            this.logger = logger;
            this.IsActiveChanged += WorkspaceBViewModel_IsActiveChanged;
        }

        private void WorkspaceBViewModel_IsActiveChanged(object sender, EventArgs e)
        {
            logger.Log($"Workspace B Active = {this.IsActive}", Category.Info, Priority.None);
        }
    }
}
