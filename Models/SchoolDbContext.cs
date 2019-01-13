using System;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace SchoolProject.Models
{
    public partial class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
        {
        }

        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Attendances> Attendances { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Parents> Parents { get; set; }
        public virtual DbSet<PersonalDatas> PersonalDatas { get; set; }
        public virtual DbSet<Principals> Principals { get; set; }
        public virtual DbSet<Qualifications> Qualifications { get; set; }
        public virtual DbSet<QualificationTeachers> QualificationTeachers { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
            
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.StreetName).HasMaxLength(30);

                entity.Property(e => e.ZipCode).HasMaxLength(6);
            });

            modelBuilder.Entity<Attendances>(entity =>
            {
                entity.HasKey(e => e.AttendanceId);

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_dbo.Attendances_dbo.Lessons_LessonId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_dbo.Attendances_dbo.Students_StudentId");
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Classes_dbo.Teachers_TeacherId");
            });

            modelBuilder.Entity<Grades>(entity =>
            {
                entity.HasKey(e => e.GradeId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Note).IsRequired();

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_dbo.Grades_dbo.Students_StudentId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Grades_dbo.Subjects_SubjectId");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_dbo.Grades_dbo.Teachers_TeacherId");
            });

            modelBuilder.Entity<Lessons>(entity =>
            {
                entity.HasKey(e => e.LessonId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("time(0)");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Lessons_dbo.Classes_ClassId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_dbo.Lessons_dbo.Subjects_SubjectId");
            });

            modelBuilder.Entity<Parents>(entity =>
            {
                entity.HasKey(e => e.ParentId);

                entity.HasOne(d => d.PersonalData)
                    .WithMany(p => p.Parents)
                    .HasForeignKey(d => d.PersonalDataId)
                    .HasConstraintName("FK_dbo.Parents_dbo.PersonalDatas_PersonalDataId");
            });

            modelBuilder.Entity<PersonalDatas>(entity =>
            {
                entity.HasKey(e => e.PersonalDataId);

                entity.HasIndex(e => e.LastName)
                    .HasName("index_lastname");

                entity.HasIndex(e => new { e.FirstName, e.LastName })
                    .HasName("index_firstname_lastname");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(40);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PersonalDatas)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_dbo.PersonalDatas_dbo.Addresses_AddressId");
            });

            modelBuilder.Entity<Principals>(entity =>
            {
                entity.HasKey(e => e.PrincipalId);

                entity.Property(e => e.PrincipalId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Principal)
                    .WithOne(p => p.Principals)
                    .HasForeignKey<Principals>(d => d.PrincipalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Principals_dbo.Teachers_PrincipalId");
            });

            modelBuilder.Entity<Qualifications>(entity =>
            {
                entity.HasKey(e => e.QualificationId);

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<QualificationTeachers>(entity =>
            {
                entity.HasKey(e => new { e.QualificationQualificationId, e.TeacherTeacherId });

                entity.Property(e => e.QualificationQualificationId).HasColumnName("Qualification_QualificationId");

                entity.Property(e => e.TeacherTeacherId).HasColumnName("Teacher_TeacherId");

                entity.HasOne(d => d.QualificationQualification)
                    .WithMany(p => p.QualificationTeachers)
                    .HasForeignKey(d => d.QualificationQualificationId)
                    .HasConstraintName("FK_dbo.QualificationTeachers_dbo.Qualifications_Qualification_QualificationId");

                entity.HasOne(d => d.TeacherTeacher)
                    .WithMany(p => p.QualificationTeachers)
                    .HasForeignKey(d => d.TeacherTeacherId)
                    .HasConstraintName("FK_dbo.QualificationTeachers_dbo.Teachers_Teacher_TeacherId");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Students_dbo.Classes_ClassId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Students_dbo.Parents_ParentId");

                entity.HasOne(d => d.PersonalData)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.PersonalDataId)
                    .HasConstraintName("FK_dbo.Students_dbo.PersonalDatas_PersonalDataId");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_dbo.Subjects_dbo.Teachers_TeacherId");
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.HasOne(d => d.PersonalData)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.PersonalDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Teachers_dbo.PersonalDatas_PersonalDataId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                //entity.HasOne(d => d.Parent)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.ParentId)
                //    .HasConstraintName("FK_dbo.Users_dbo.Parents_ParentId");

                //entity.HasOne(d => d.Principal)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.PrincipalId)
                //    .HasConstraintName("FK_dbo.Users_dbo.Principals_PrincipalId");

                //entity.HasOne(d => d.Teacher)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.TeacherId)
                //    .HasConstraintName("FK_dbo.Users_dbo.Teachers_TeacherId");
            });
        }
        
    }
}
