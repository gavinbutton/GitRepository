using Infrastructure;
using ModuleA.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

            EditCommand = new DelegateCommand<HierarchicalItem>(item =>
            {
                TreeItems.Flatten(i => i.Children).ToList().ForEach(i => i.IsEditing = false);
                item.IsEditing = true;
            });

            RevertEditCommand = new DelegateCommand<HierarchicalItem>((item) => item.RevertEdit());

            TreeItems = new HierarchicalItem[]
            {
                new HierarchicalItem()
                {
                    Image = new BitmapImage(new Uri("pack://application:,,,/ModuleA;component/Images/add.png")),
                    Name = "Add",
                    Children = new HierarchicalItem[]
                    {
                        new HierarchicalItem()
                        {
                            Image = new BitmapImage(new Uri("pack://application:,,,/ModuleA;component/Images/add.png")),
                            Name = "Minus",
                        },
                        new HierarchicalItem()
                        {
                            Image = new BitmapImage(new Uri("pack://application:,,,/ModuleA;component/Images/add.png")),
                            Name = "Multiply",
                        },
                        new HierarchicalItem()
                        {
                            Image = new BitmapImage(new Uri("pack://application:,,,/ModuleA;component/Images/add.png")),
                            Name = "Divide",
                        },
                    }
                }
                
            };
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

        private IEnumerable<HierarchicalItem> m_treeItems;

        public IEnumerable<HierarchicalItem> TreeItems
        {
            get
            {
                return m_treeItems;
            }
            set
            {
                SetProperty(ref m_treeItems, value);
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

        public DelegateCommand<HierarchicalItem> EditCommand { get; set; }

        public DelegateCommand<HierarchicalItem> RevertEditCommand { get; set; }

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
