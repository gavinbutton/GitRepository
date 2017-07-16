using Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Views;

namespace WpfLayoutTest.ViewModels
{
    class ShellViewModel : BindableBase
    {
        private IUnityContainer m_container;
        public ShellViewModel(IRegionManager regionManager, IEventAggregator ea, IUnityContainer container)
        {
            m_container = container;
            regionManager.RegisterViewWithRegion("LogRegion", typeof(LogView));
            ea.GetEvent<AboutCommandEvent>().Subscribe(OnAbout);
        }

        private void OnAbout()
        {
            var dialog = m_container.Resolve<AboutView>();
            dialog.Owner = App.Current.MainWindow;
            dialog.Show();
        }
    }
}
