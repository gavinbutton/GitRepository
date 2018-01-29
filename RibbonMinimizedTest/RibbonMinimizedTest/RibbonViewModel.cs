using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RibbonMinimizedTest
{
    public class RibbonViewModel
    {
        public RibbonViewModel()
        {
            AppFourCommand = new DelegateCommand(() => MessageBox.Show("AppFour"));
            AppFiveCommand = new DelegateCommand(() => MessageBox.Show("AppFive"));
            AppOneCommand = new DelegateCommand(() => MessageBox.Show("AppOne"));
            AppTwoCommand = new DelegateCommand(() => MessageBox.Show("AppTwo"));
            AppThreeCommand = new DelegateCommand(() => MessageBox.Show("AppThree"));
            ExitCommand = new DelegateCommand(() => MessageBox.Show("Exit"));
            T1G1B1Command = new DelegateCommand(() => MessageBox.Show("T1G1B1"));
            T1G1B2Command = new DelegateCommand(() => MessageBox.Show("T1G1B2"));
            T1G2B1Command = new DelegateCommand(() => MessageBox.Show("T1G2B1"));
            T2G2B1Command = new DelegateCommand(() => MessageBox.Show("T2G2B1"));
        }

        public ICommand AppFourCommand { get; set; }

        public ICommand AppFiveCommand { get; set; }

        public ICommand AppOneCommand { get; set; }
    
        public ICommand AppTwoCommand { get; set; }

        public ICommand AppThreeCommand { get; set; }

        public ICommand ExitCommand { get; set; }

        public ICommand T1G1B1Command { get; set; }

        public ICommand T1G1B2Command { get; set; }

        public ICommand T1G2B1Command { get; set; }

        public ICommand T2G2B1Command { get; set; }
    }
}
