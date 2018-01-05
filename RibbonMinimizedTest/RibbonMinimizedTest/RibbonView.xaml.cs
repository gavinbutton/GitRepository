using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;

namespace RibbonMinimizedTest
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    public partial class RibbonView : UserControl
    {
        private const string NameAttribute = "Name";
        private const string LabelAttribute = "Label";
        private const string HeaderAttribute = "Header";
        private const string CommandAttribute = "Command";

        private string filename = "QAT.json";
        public RibbonView()
        {
            InitializeComponent();
            this.DataContext = new RibbonViewModel();
        }

        private void SaveQAT()
        {
            var qatKeys = new List<string>();
            var items = ribbonControl.QuickAccessToolBar.Items;
            foreach (var item in items)
            {
                qatKeys.Add( GetRibbonControlKey(item as Control));
            }

            var state = new State() { QatKeys = qatKeys.ToArray() };

            var serializer = new DataContractJsonSerializer(typeof(State));

            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                serializer.WriteObject(stream, state);
            }
        }

        

        private void LoadQAT()
        {
            var serializer = new DataContractJsonSerializer(typeof(State));
            State state;

            if (File.Exists(filename))
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    state = (State)serializer.ReadObject(stream);
                }

                foreach (var key in state.QatKeys)
                {
                    var control = FindRibbonControlByKey(key, ribbonControl.Items) ??
                        FindRibbonControlByKey(key, ribbonControl.ApplicationMenu) ??
                        FindRibbonControlByKey(key, ribbonControl.HelpPaneContent);

                    if (control != null)
                    {
                        try
                        {
                            AddControlToQAT(control);
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }
            }
        }

        private void AddControlToQAT(UIElement originalSource)
        {
            RibbonQuickAccessToolBarCloneEventArgs e = new RibbonQuickAccessToolBarCloneEventArgs(originalSource);
            originalSource.RaiseEvent(e);

            Ribbon ribbon = RibbonControlService.GetRibbon(originalSource);
            if (ribbon != null &&
                ribbon.QuickAccessToolBar != null &&
                e.CloneInstance != null)
            {
                ribbon.QuickAccessToolBar.Items.Add(e.CloneInstance);
            }
        }

        private Control FindRibbonControlByKey(string key, object i)
        {
            Control control = null;
            
            if (i is RibbonButton)
            {
                var c = i as RibbonButton;
                control = GetRibbonControlKey(c) == key ? c : null;
            }
            else if (i is HeaderedItemsControl)
            {
                var c = i as HeaderedItemsControl;
                control = GetRibbonControlKey(c) == key ? c : null;
            }

            if (control == null && i is ItemsControl)
            {
                var c = i as ItemsControl;
                control = FindRibbonControlByKey(key, c?.Items);
            }
            

            return control;
        }
        

        private Control FindRibbonControlByKey(string key, ItemCollection collection)
        {
            Control control = null;

            foreach(var i in collection)
            {
                control = FindRibbonControlByKey(key, i);

                if (control != null)
                {
                    break;
                }
            }
            return control;
        }

        private string GetRibbonControlKey(Control control)
        {
            string key = control.Name;
            
            if(control is RibbonButton && string.IsNullOrEmpty(key))
            {
                var c = control as RibbonButton;
                key = c.Label;

                if(string.IsNullOrEmpty(key))
                {
                    key = GetCommandExpression(c);
                }
            }
            else if (control is RibbonMenuButton && string.IsNullOrEmpty(key))
            {
                var c = control as RibbonMenuButton;
                key = c.Label;

                if (string.IsNullOrEmpty(key))
                {
                    key = GetCommandExpression(c);
                }
            }

            else if(control is HeaderedItemsControl && string.IsNullOrEmpty(key))
            {
                var c = control as HeaderedItemsControl;
                key = c.Header.ToString();
            }
            
            return key;
        }

        private string GetCommandExpression(Control control)
        {
            var command = string.Empty;
            using (var stream = new MemoryStream())
            {
                var writer = XmlWriter.Create(stream, new XmlWriterSettings()
                {
                    Indent = true,
                    ConformanceLevel = ConformanceLevel.Fragment,
                    OmitXmlDeclaration = true,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                });

                var xamlManager = new XamlDesignerSerializationManager(writer)
                {
                    XamlWriterMode = XamlWriterMode.Expression
                };

                XamlWriter.Save(control, xamlManager);
                stream.Seek(0, SeekOrigin.Begin);
                XDocument doc = XDocument.Load(stream);
                command = doc.Root.Attribute("Command")?.Value;
            }
            
            return command;
        }

        #region Handlers
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Closing += Parent_Closing;
            LoadQAT();
        }

        private void Parent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveQAT();
        }

        private void ExecuteNew(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("DoneNew");
        }

        private void CanExecuteNew(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExecuteSave(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("DoneSave");
        }

        private void CanExecuteSave(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
    }
}
