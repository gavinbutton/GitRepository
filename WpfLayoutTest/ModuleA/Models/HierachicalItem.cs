using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ModuleA.Models
{
    public class HierarchicalItem : BindableBase
    {
        public HierarchicalItem()
        {
            EscapeKeyCommand = new DelegateCommand(() => Name = m_oldName);
        }

        public BitmapImage Image { get; set; }

        private string m_name;
        private string m_oldName;
        public string Name
        {
            get { return m_name; }
            set
            {
                SetProperty(ref m_name, value);
                IsEditing = false;
            }
        }

        public IEnumerable<HierarchicalItem> Children { get; set; }

        private bool m_isEditing;
        public bool IsEditing
        {
            get { return m_isEditing; }
            set
            {
                if (value == true)
                {
                    m_oldName = Name;
                }
                SetProperty(ref m_isEditing, value);
            }
        }
        
        public DelegateCommand EscapeKeyCommand { get; set; }
    }

    public static class Extensions
    {
        public static IEnumerable<T> Flatten<T>(
    this IEnumerable<T> e,
    Func<T, IEnumerable<T>> f)
        {

            return (e!=null) ? e.SelectMany(c => f(c).Flatten(f)).Concat(e) : new T[0];
        }
    }
}
