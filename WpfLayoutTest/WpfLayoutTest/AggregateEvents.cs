using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLayoutTest.Logging;

namespace WpfLayoutTest
{
    public class LogEvent : PubSubEvent<LogItem>
    {
    }
}
