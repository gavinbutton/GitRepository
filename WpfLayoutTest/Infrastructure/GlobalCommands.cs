using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLayoutTest.Infrastructure
{
    public class GlobalCommands
    {
        public static readonly CompositeCommand LogCommand = new CompositeCommand();
    }
}
