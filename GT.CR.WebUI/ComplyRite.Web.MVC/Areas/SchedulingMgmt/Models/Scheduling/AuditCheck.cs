namespace ComplyRite.Web.MVC.Areas.SchedulingMgmt.Models.Scheduling
{
    public class AuditCheck
    {
        public int AuditCheckId { get; set; }
        public string AuditChecklistId { get; set; }
        public string CheckCode { get; set; }
        public string CheckName { get; set; }
        public string CheckImage { get; set; }
        public string CheckScore { get; set; }
        public string CheckStatus { get; set; }
        public string CheckReponse { get; set; }
        public string CheckAnswer { get; set; }
        public string PerformedBy { get; set; }
        public string PerformedDatetime { get; set; }
    }
}