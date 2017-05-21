using Prism;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLayoutTest.Infrastructure
{
    public class BindableActiveAwareBase : BindableBase, IActiveAware
    {
        bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnIsActiveChanged();
            }
        }
        private void OnIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, new EventArgs());
        }


        public event EventHandler IsActiveChanged;
    }
}
