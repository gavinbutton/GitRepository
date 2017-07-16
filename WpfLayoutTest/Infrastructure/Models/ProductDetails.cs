using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ProductDetails
    {
        public ModuleDetails Product { get; set; }

        public IEnumerable<ModuleDetails> Modules { get; set; }
    }
}
