﻿using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfLayoutTest.Views;
using System.Windows.Controls.Ribbon;

namespace WpfLayoutTest.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : RibbonWindow
    {
        public Shell()
        {
            InitializeComponent();
        }

        //[Import]
        //public ShellViewModel ViewModel
        //{
        //    set { DataContext = value; }
        //}

    }
}
