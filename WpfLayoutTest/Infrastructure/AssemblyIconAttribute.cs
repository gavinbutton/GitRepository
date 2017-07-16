using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyIconAttribute : Attribute
    {
        public AssemblyIconAttribute()
        {

        }

        public AssemblyIconAttribute(string iconSource)
        {
            IconSource = new Uri(iconSource);
        }

        public Uri IconSource { get; set; }
    }
}
