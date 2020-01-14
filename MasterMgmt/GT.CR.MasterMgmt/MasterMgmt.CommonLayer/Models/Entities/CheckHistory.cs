using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterMgmt.CommonLayer.Models.Entities
{
    public class CheckHistory
    {
        #region CheckHistory_Attributes
        [Key]
        public int CheckHistoryID { get; set; }
        public string CheckCode { get; set; }
        public string CheckName { get; set; }
        public string CheckImage { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        #endregion

        #region FKRelationship

        public int CheckTypeID { get; set; }

        public int? CheckID { get; set; }
        [ForeignKey("CheckID")]
        public Check Check { get; set; }
        #endregion
    }
}
