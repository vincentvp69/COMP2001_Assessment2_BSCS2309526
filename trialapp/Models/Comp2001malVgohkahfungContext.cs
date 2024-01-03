using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace trialapp.Models;

public partial class Comp2001malVgohkahfungContext : DbContext
{
    public Comp2001malVgohkahfungContext()
    {
    }

    public Comp2001malVgohkahfungContext(DbContextOptions<Comp2001malVgohkahfungContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityRecord> ActivityRecords { get; set; }

    public virtual DbSet<CoachRecord> CoachRecords { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuditLog> UserAuditLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001MAL_VGohkahfung;User Id=VGohkahfung;Password=LodJ529+;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityRecord>(entity =>
        {
            entity.HasKey(e => e.TrialId).HasName("PK__Activity__57E5E9A39575668D");

            entity.ToTable("Activity_Record");

            entity.Property(e => e.TrialId)
                .ValueGeneratedNever()
                .HasColumnName("Trial_ID");
            entity.Property(e => e.TrialDifficulty).HasColumnName("Trial_Difficulty");
            entity.Property(e => e.TrialDistance)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Trial_Distance");
            entity.Property(e => e.TrialHeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Trial_Height");
            entity.Property(e => e.TrialTime).HasColumnName("Trial_Time");
            entity.Property(e => e.TrialType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Trial_Type");
        });

        modelBuilder.Entity<CoachRecord>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Coach_Re__A60ECAA2A57BCDC5");

            entity.ToTable("Coach_Record");

            entity.Property(e => e.ProfileId)
                .ValueGeneratedNever()
                .HasColumnName("Profile_ID");
            entity.Property(e => e.CoachExperience).HasColumnName("Coach_Experience");
            entity.Property(e => e.CoachRating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("Coach_Rating");

            entity.HasOne(d => d.Profile).WithOne(p => p.CoachRecord)
                .HasForeignKey<CoachRecord>(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Coach_Rec__Profi__6477ECF3");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__A60ECAA2169A8945");

            entity.ToTable("Profile");

            entity.Property(e => e.ProfileId)
                .ValueGeneratedNever()
                .HasColumnName("Profile_ID");
            entity.Property(e => e.ProfileStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Profile_Status");
            entity.Property(e => e.ProfileType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Profile_Type");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Subscrip__B7FCB94A9EF4889C");

            entity.ToTable("Subscription");

            entity.Property(e => e.PackageId)
                .ValueGeneratedNever()
                .HasColumnName("Package_ID");
            entity.Property(e => e.SubDuration).HasColumnName("Sub_Duration");
            entity.Property(e => e.SubFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Sub_Fee");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", tb => tb.HasTrigger("UserAuditTrigger"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserHeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("User_height");
            entity.Property(e => e.UserWeight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("User_weight");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("PK__UserAudi__EB5F6CDDBD7D725E");

            entity.ToTable("UserAuditLog");

            entity.Property(e => e.AuditLogId).HasColumnName("AuditLogID");
            entity.Property(e => e.AuditDateTime).HasColumnType("datetime");
            entity.Property(e => e.AuditType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
