using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp4Sec001.Models
{
    public partial class SMSContext : DbContext
    {
        public SMSContext()
        {
        }

        public SMSContext(DbContextOptions<SMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Name=Connection2RDS");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseCode)
                    .HasName("PK__Course__FC00E00180211C1D");

                entity.ToTable("Course");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CourseTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.LoginName)
                    .HasName("PK__Login__DB8464FE0F7E6D4D");

                entity.ToTable("Login");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.LoginNameNavigation)
                    .WithOne(p => p.Login)
                    .HasForeignKey<Login>(d => d.LoginName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Login_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Program)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasMany(d => d.CourseCodes)
                    .WithMany(p => p.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "Enrollment",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseCode").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Enrollment_Course"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Enrollment_Student"),
                        j =>
                        {
                            j.HasKey("StudentId", "CourseCode");

                            j.ToTable("Enrollment");

                            j.IndexerProperty<string>("StudentId").HasMaxLength(10).IsUnicode(false).HasColumnName("StudentID");

                            j.IndexerProperty<string>("CourseCode").HasMaxLength(10).IsUnicode(false);
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
