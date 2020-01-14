using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class Media
    {
        public int MediaID { get; set; }
        public string Format { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string URL { get; set; }

    }
}
