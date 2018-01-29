using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RibbonMinimizedTest
{
    /// <summary>
    /// Contains the state of the Quick Access Toolbar
    /// </summary>
    [DataContract]
    internal class State
    {
        /// <summary>
        /// Gets or sets the quick access toolbar keys.
        /// </summary>
        /// <value>
        /// The quick access toolbar keys.
        /// </value>
        [DataMember]
        public string[] QuickAccessToolbarKeys { get; set; }
    }
}
