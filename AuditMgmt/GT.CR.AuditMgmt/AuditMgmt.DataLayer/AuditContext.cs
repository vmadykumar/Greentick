using AuditMgmt.CommonLayer.Models.DTO;
using AuditMgmt.CommonLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditMgmt.DataLayer
{
    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChecklistAuditInfo>()
                .HasAlternateKey(c => new { c.UserID, c.FBOCode,c.LocationCode});
        }

        public virtual DbSet<Approval> Approval { get; set; }
        public virtual DbSet<Audit> Audit { get; set; }
     
        public virtual DbSet<AuditCheck> AuditCheck { get; set; }
        public virtual DbSet<AuditChecklist> AuditChecklist { get; set; }
        public virtual DbSet<AuditChecklistHistory> AuditChecklistHistory { get; set; }
        public virtual DbSet<AuditHistory> AuditHistory { get; set; }
    
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<CertificateType> CertificateType { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<ExternalAuditLog> ExternalAuditLog { get; set; }
        public virtual DbSet<Attachements> Attachement { get; set; }
        public virtual DbSet<AttachementType> AttachementType { get; set; }
        public virtual DbSet<NotificationLog> NotificationLog { get; set; }
        public virtual DbSet<NotificationCategory> NotificationCategory { get; set; }
        public virtual DbSet<NotificationReminderType> NotificationReminderType { get; set; }
        public virtual DbSet<Reminder> Reminder { get; set; }
        public virtual DbSet<ReminderCategory> ReminderCategory { get; set; }
        public virtual DbSet<UserChecklistAuditInfo> UserChecklistAuditInfos { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleHistory> ScheduleHistory { get; set; }
        public virtual DbSet<Template> Template { get; set; }

        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<UserReminder> UserReminder { get; set; }
        [NotMapped]
        public virtual DbSet<ChecklistLastPerformedDetailsDto> ChecklistLastPerformedDetailsDTO { get; set; }
        public DbQuery<ReportDto> ReportDto { get; set; }
        public DbQuery<AuditDto> AuditDto { get; set; }
        public DbQuery<ChecklistDto> ChecklistDto { get; set; }

    }
}
