﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolProject.Models;

namespace SchoolProject.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20190113144552_New")]
    partial class New
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SchoolProject.Models.Addresses", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AptNumber");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("StreetName")
                        .HasMaxLength(30);

                    b.Property<int>("StreetNumber");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SchoolProject.Models.Attendances", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttendanceType");

                    b.Property<int>("LessonId");

                    b.Property<int>("StudentId");

                    b.HasKey("AttendanceId");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("SchoolProject.Models.Classes", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TeacherId");

                    b.HasKey("ClassId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("SchoolProject.Models.Grades", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Note")
                        .IsRequired();

                    b.Property<int>("StudentId");

                    b.Property<int>("SubjectId");

                    b.Property<int>("TeacherId");

                    b.Property<int>("Value");

                    b.HasKey("GradeId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SchoolProject.Models.Lessons", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<int>("DayOfWeek");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time(0)");

                    b.Property<int>("SubjectId");

                    b.HasKey("LessonId");

                    b.HasIndex("ClassId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SchoolProject.Models.Parents", b =>
                {
                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PersonalDataId");

                    b.HasKey("ParentId");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("SchoolProject.Models.PersonalDatas", b =>
                {
                    b.Property<int>("PersonalDataId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(40);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.HasKey("PersonalDataId");

                    b.HasIndex("AddressId");

                    b.HasIndex("LastName")
                        .HasName("index_lastname");

                    b.HasIndex("FirstName", "LastName")
                        .HasName("index_firstname_lastname");

                    b.ToTable("PersonalDatas");
                });

            modelBuilder.Entity("SchoolProject.Models.Principals", b =>
                {
                    b.Property<int>("PrincipalId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TeacherId");

                    b.HasKey("PrincipalId");

                    b.ToTable("Principals");
                });

            modelBuilder.Entity("SchoolProject.Models.Qualifications", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("QualificationId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("SchoolProject.Models.QualificationTeachers", b =>
                {
                    b.Property<int>("QualificationQualificationId")
                        .HasColumnName("Qualification_QualificationId");

                    b.Property<int>("TeacherTeacherId")
                        .HasColumnName("Teacher_TeacherId");

                    b.HasKey("QualificationQualificationId", "TeacherTeacherId");

                    b.HasIndex("TeacherTeacherId");

                    b.ToTable("QualificationTeachers");
                });

            modelBuilder.Entity("SchoolProject.Models.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Id");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SchoolProject.Models.Students", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassId");

                    b.Property<int>("ParentId");

                    b.Property<int>("PersonalDataId");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolProject.Models.Subjects", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TeacherId");

                    b.HasKey("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolProject.Models.Teachers", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEducator");

                    b.Property<int>("PersonalDataId");

                    b.HasKey("TeacherId");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SchoolProject.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("SchoolProject.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int?>("PrincipalId");

                    b.Property<int?>("TeacherId");

                    b.HasKey("UserId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PrincipalId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SchoolProject.Models.Attendances", b =>
                {
                    b.HasOne("SchoolProject.Models.Lessons", "Lesson")
                        .WithMany("Attendances")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("FK_dbo.Attendances_dbo.Lessons_LessonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolProject.Models.Students", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_dbo.Attendances_dbo.Students_StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Classes", b =>
                {
                    b.HasOne("SchoolProject.Models.Teachers", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_dbo.Classes_dbo.Teachers_TeacherId");
                });

            modelBuilder.Entity("SchoolProject.Models.Grades", b =>
                {
                    b.HasOne("SchoolProject.Models.Students", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_dbo.Grades_dbo.Students_StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolProject.Models.Subjects", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK_dbo.Grades_dbo.Subjects_SubjectId");

                    b.HasOne("SchoolProject.Models.Teachers", "Teacher")
                        .WithMany("Grades")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_dbo.Grades_dbo.Teachers_TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Lessons", b =>
                {
                    b.HasOne("SchoolProject.Models.Classes", "Class")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassId")
                        .HasConstraintName("FK_dbo.Lessons_dbo.Classes_ClassId");

                    b.HasOne("SchoolProject.Models.Subjects", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("FK_dbo.Lessons_dbo.Subjects_SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Parents", b =>
                {
                    b.HasOne("SchoolProject.Models.PersonalDatas", "PersonalData")
                        .WithMany("Parents")
                        .HasForeignKey("PersonalDataId")
                        .HasConstraintName("FK_dbo.Parents_dbo.PersonalDatas_PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.PersonalDatas", b =>
                {
                    b.HasOne("SchoolProject.Models.Addresses", "Address")
                        .WithMany("PersonalDatas")
                        .HasForeignKey("AddressId")
                        .HasConstraintName("FK_dbo.PersonalDatas_dbo.Addresses_AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Principals", b =>
                {
                    b.HasOne("SchoolProject.Models.Teachers", "Principal")
                        .WithOne("Principals")
                        .HasForeignKey("SchoolProject.Models.Principals", "PrincipalId")
                        .HasConstraintName("FK_dbo.Principals_dbo.Teachers_PrincipalId");
                });

            modelBuilder.Entity("SchoolProject.Models.QualificationTeachers", b =>
                {
                    b.HasOne("SchoolProject.Models.Qualifications", "QualificationQualification")
                        .WithMany("QualificationTeachers")
                        .HasForeignKey("QualificationQualificationId")
                        .HasConstraintName("FK_dbo.QualificationTeachers_dbo.Qualifications_Qualification_QualificationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolProject.Models.Teachers", "TeacherTeacher")
                        .WithMany("QualificationTeachers")
                        .HasForeignKey("TeacherTeacherId")
                        .HasConstraintName("FK_dbo.QualificationTeachers_dbo.Teachers_Teacher_TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Students", b =>
                {
                    b.HasOne("SchoolProject.Models.Classes", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .HasConstraintName("FK_dbo.Students_dbo.Classes_ClassId");

                    b.HasOne("SchoolProject.Models.Parents", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_dbo.Students_dbo.Parents_ParentId");

                    b.HasOne("SchoolProject.Models.PersonalDatas", "PersonalData")
                        .WithMany("Students")
                        .HasForeignKey("PersonalDataId")
                        .HasConstraintName("FK_dbo.Students_dbo.PersonalDatas_PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Subjects", b =>
                {
                    b.HasOne("SchoolProject.Models.Teachers", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_dbo.Subjects_dbo.Teachers_TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Teachers", b =>
                {
                    b.HasOne("SchoolProject.Models.PersonalDatas", "PersonalData")
                        .WithMany("Teachers")
                        .HasForeignKey("PersonalDataId")
                        .HasConstraintName("FK_dbo.Teachers_dbo.PersonalDatas_PersonalDataId");
                });

            modelBuilder.Entity("SchoolProject.Models.UserRole", b =>
                {
                    b.HasOne("SchoolProject.Models.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SchoolProject.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SchoolProject.Models.Users", b =>
                {
                    b.HasOne("SchoolProject.Models.Parents", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("SchoolProject.Models.Principals", "Principal")
                        .WithMany()
                        .HasForeignKey("PrincipalId");

                    b.HasOne("SchoolProject.Models.Teachers", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });
#pragma warning restore 612, 618
        }
    }
}