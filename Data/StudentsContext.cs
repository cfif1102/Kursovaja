using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kursovaja;

public partial class StudentsContext : DbContext
{
    public StudentsContext()
    {
    }

    public StudentsContext(DbContextOptions<StudentsContext> options)
        : base(options)
    {
    }
    public virtual DbSet<AdmissionApplication> AdmissionApplications { get; set; }

    public virtual DbSet<AdmissionPlan> AdmissionPlans { get; set; }

    public virtual DbSet<AdmissionsOfficer> AdmissionsOfficers { get; set; }

    public virtual DbSet<Applicant> Applicants { get; set; }

    public virtual DbSet<ApplicantCertificate> ApplicantCertificates { get; set; }

    public virtual DbSet<EducationInstitution> EducationInstitutions { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmissionApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Admissio__E063E1CBFDF99D16");

            entity.ToTable("Admission_Applications");

            entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");
            entity.Property(e => e.AdmissionsOfficerId).HasColumnName("Admissions_Officer_ID");
            entity.Property(e => e.ApplicationDate)
                .HasColumnType("date")
                .HasColumnName("Application_Date");
            entity.Property(e => e.OtherDetails)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Other_Details");
            entity.Property(e => e.SpecialtyId).HasColumnName("Specialty_ID");

            entity.HasOne(d => d.AdmissionsOfficer).WithMany(p => p.AdmissionApplications)
                .HasForeignKey(d => d.AdmissionsOfficerId)
                .HasConstraintName("FK__Admission__Admis__49C3F6B7");

            entity.HasOne(d => d.Applicant).WithMany(p => p.AdmissionApplications)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK__Admission__Appli__4AB81AF0");

            entity.HasOne(d => d.Specialty).WithMany(p => p.AdmissionApplications)
                .HasForeignKey(d => d.SpecialtyId)
                .HasConstraintName("FK__Admission__Speci__48CFD27E")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AdmissionPlan>(entity =>
        {
            entity.HasKey(e => e.AdmissionPlanId).HasName("PK__Admissio__F9E1BF7D2043216C");

            entity.ToTable("Admission_Plan");

            entity.Property(e => e.AdmissionPlanId).HasColumnName("Admission_Plan_ID");
            entity.Property(e => e.NumberOfSeats).HasColumnName("Number_of_Seats");
            entity.Property(e => e.SpecialtyId).HasColumnName("Specialty_ID");

            entity.HasOne(d => d.Specialty).WithMany(p => p.AdmissionPlans)
                .HasForeignKey(d => d.SpecialtyId)
                .HasConstraintName("FK__Admission__Speci__3E52440B")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AdmissionsOfficer>(entity =>
        {
            entity.HasKey(e => e.AdmissionsOfficerId).HasName("PK__Admissio__FA695646B5B76D8C");

            entity.ToTable("Admissions_Officers");

            entity.Property(e => e.AdmissionsOfficerId).HasColumnName("Admissions_Officer_ID");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Full_Name");
        });

        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(e => e.ApplicantId).HasName("PK__Applican__9F991F67C28503A3");

            entity.Property(e => e.ApplicantId).HasColumnName("Applicant_ID");
            entity.Property(e => e.AverageGrade)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Average_Grade");
            entity.Property(e => e.EducationDocument)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Education_Document");
            entity.Property(e => e.EducationInstitutionId).HasColumnName("Education_Institution_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ForeignLanguage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Foreign_Language");
            entity.Property(e => e.GraduationYear).HasColumnName("Graduation_Year");
            entity.Property(e => e.IdentificationDocument)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Identification_Document");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ParentsId).HasColumnName("Parents_ID");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ResidentialAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Residential_Address");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EducationInstitution).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.EducationInstitutionId)
                .HasConstraintName("FK__Applicant__Educa__44FF419A")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Parents).WithMany(p => p.Applicants)
                .HasForeignKey(d => d.ParentsId)
                .HasConstraintName("FK__Applicant__Paren__45F365D3")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApplicantCertificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Applican__3AE412EAA469E17E");

            entity.ToTable("Applicant_Certificates");

            entity.Property(e => e.CertificateId).HasColumnName("Certificate_ID");
            entity.Property(e => e.ApplicantId).HasColumnName("Applicant_ID");
            entity.Property(e => e.Grade).HasColumnType("decimal(4, 2)");

            entity.HasOne(d => d.Applicant).WithMany(p => p.ApplicantCertificates)
                .HasForeignKey(d => d.ApplicantId)
                .HasConstraintName("FK__Applicant__Appli__4D94879B")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EducationInstitution>(entity =>
        {
            entity.HasKey(e => e.EducationInstitutionId).HasName("PK__Educatio__96E5773A453D8E04");

            entity.ToTable("Education_Institutions");

            entity.Property(e => e.EducationInstitutionId).HasColumnName("Education_Institution_ID");
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Institution_Name")
                ;
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Facultie__4EFCEA4AF894DCFD");

            entity.Property(e => e.FacultyId).HasColumnName("Faculty_ID");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Faculty_Name")
                ;
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentsId).HasName("PK__Parents__7CBE7523030166E4");

            entity.Property(e => e.ParentsId).HasColumnName("Parents_ID");
            entity.Property(e => e.Parent1Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Parent1_Name");
            entity.Property(e => e.Parent2Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Parent2_Name");
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("PK__Specialt__0DD46021487829C1");

            entity.Property(e => e.SpecialtyId).HasColumnName("Specialty_ID");
            entity.Property(e => e.FacultyId).HasColumnName("Faculty_ID");
            entity.Property(e => e.SpecialtyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Specialty_Name");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Specialties)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__Specialti__Facul__398D8EEE")
                .OnDelete(DeleteBehavior.Cascade); ;
                
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
