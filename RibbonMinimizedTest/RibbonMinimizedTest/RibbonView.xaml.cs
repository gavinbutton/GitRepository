using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Markup;
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
                            var clonedControl = control.XamlClone();
                            ribbonControl.QuickAccessToolBar.Items.Add(clonedControl);
                        }
                        catch(Exception e)
                        {

                        }
                    }
                }
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
            string key = string.Empty;
            string attribute = NameAttribute;
            string id = control.Name;
            
            if(control is RibbonButton && string.IsNullOrEmpty(id))
            {
                attribute = LabelAttribute;
                var c = control as RibbonButton;
                id = c.Label;

                if(string.IsNullOrEmpty(id))
                {
                    attribute = CommandAttribute;
                    id = GetCommandExpression(c);
                }
            }

            else if(control is HeaderedItemsControl && string.IsNullOrEmpty(id))
            {
                attribute = HeaderAttribute;
                var c = control as HeaderedItemsControl;
                id = c.Header.ToString();
            }

            if(!string.IsNullOrEmpty(id))
            {
                //key = $"{attribute}={id}";
                key = id;
            }
            return key;
        }

        private string GetCommandExpression(Control control)
        {
            var command = string.Empty;
            var xamlDoc = XamlWriter.Save(control);
            XDocument doc = XDocument.Parse(xamlDoc);
            command = doc.Root.Attribute("Command")?.Value;

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
