﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LogEvent : PubSubEvent<LogItem>
    {
    }

    public class AboutCommandEvent :PubSubEvent
    {

    }
}
