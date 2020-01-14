using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class AreaChecks
    {
        public int AreaChecksID { get; set; }

        
        public string SubAreaCode { get; set; }

        public int CheckID { get; set; }
        [ForeignKey("CheckID")]
        public Check Check { get; set; }

        public int ChecklistID { get; set; }
        [ForeignKey("ChecklistID")]
        public Checklist Checklist { get; set; }

    }
}
