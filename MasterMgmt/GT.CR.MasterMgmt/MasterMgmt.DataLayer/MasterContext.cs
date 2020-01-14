using MasterMgmt.CommonLayer.Models.DTO;
using MasterMgmt.CommonLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMgmt.DataLayer
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaChecks>()
                   .Property(c => c.SubAreaCode).IsRequired();

            modelBuilder.Entity<AreaChecks>()
                .HasIndex(c => new { c.CheckID, c.ChecklistID, c.SubAreaCode }).IsUnique();

        }
        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Checklist> Checklist { get; set; }
        public virtual DbSet<AreaChecks> AreaChecks { get; set; }
        public virtual DbSet<AuditCheckInfo> AuditCheckInfo { get; set; }
        public virtual DbSet<AuditChecklistInfo> AuditChecklistInfo { get; set; }
        public virtual DbSet<AuditInfo> AuditInfo { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<AuditCheck> AuditCheck { get; set;}
        public DbQuery<MasterChecklistDTO> MasterChecklistDTO { get; set; }

        public DbQuery<ChecklistIDDTO> ChecklistIDDTO { get; set; }
    }
}
