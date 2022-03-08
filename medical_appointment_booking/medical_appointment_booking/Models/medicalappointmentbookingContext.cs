using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace medical_appointment_booking.Models
{
    public partial class medicalappointmentbookingContext : DbContext
    {
        public medicalappointmentbookingContext()
        {
        }

        public medicalappointmentbookingContext(DbContextOptions<medicalappointmentbookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Specialist> Specialists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=mssql-71296-0.cloudclusters.net,19886;Uid=phuc;Pwd=Phuc123456;Database=medical-appointment-booking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.AppointmentDate).HasColumnType("date");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.IsApproved).HasColumnName("isApproved");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Appointment_Account");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Appointment_Doctor");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AcademicRank).HasMaxLength(50);

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.SpecialistId).HasColumnName("SpecialistID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Doctor_Account");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialistId)
                    .HasConstraintName("FK_Doctor_Specialist");
            });

            modelBuilder.Entity<Specialist>(entity =>
            {
                entity.ToTable("Specialist");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
