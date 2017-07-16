using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Infrastructure.Models
{
    public class ModuleDetails
    {
        public string Name { get; set; }
        public string Copyright { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public BitmapImage Icon { get; set; }
        public string LicenseAgreementRtf { get; set; }
    }
}
