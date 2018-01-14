using Infrastructure;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace ModuleA.ViewModels
{
    class RibbonViewModel : ViewModel
    {
        private double m_zoom;

        public RibbonViewModel(IEventAggregator ea)
        {
            AboutCommand = new DelegateCommand(() => ea.GetEvent<AboutCommandEvent>().Publish());
            Items = new double[] { 10, 20, 30, 30, 40, 50 };
            Zoom = 50;
        }

        ICommand AddButton { get; set; }

        public DelegateCommand DeleteCommand {get;set;}

        public DelegateCommand AboutCommand { get; set; }

        public double[] Items { get; set; }

        public double Zoom
        {
            get { return m_zoom; }
            set
            {
                m_zoom = value;
                OnPropertyChanged("Zoom");
            }
        }

    }
}
