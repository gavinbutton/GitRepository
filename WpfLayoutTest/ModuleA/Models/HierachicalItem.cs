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
            
        }

        public BitmapImage Image { get; set; }

        private string m_name;
        private string m_oldName;
        public string Name
        {
            get { return m_name; }
            set
            {
                var errorMessage = GetErrorMessage(value);
                SetProperty(ref m_name, string.IsNullOrEmpty(errorMessage) ? value : m_oldName);

                ErrorMessage = errorMessage;
                IsEditing = false;
            }
        }

        public void RevertEdit()
        {
            Name = m_oldName;
        }

        string m_errorMessage;
        public string ErrorMessage
        {
            get
            {
                return m_errorMessage;
            }

            set
            {
                SetProperty(ref m_errorMessage, value);
                RaisePropertyChanged("HasError");
            }
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
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
        
        private string GetErrorMessage(string name)
        {
            string errorMessage = string.Empty;

            if(name.Contains("Gav"))
            {
                errorMessage = "Name contains[Gav]";
            }

            return errorMessage;
        }
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
