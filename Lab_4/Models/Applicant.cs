using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Applicant
{
    public int ApplicantId { get; set; }

    [Required]
    public string? Surname { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Middlename { get; set; }

    [Required]
    public string? IdentificationDocument { get; set; }

    [Required]
    public string? EducationDocument { get; set; }

    [Required]
    [Range(0.0, 10.0, ErrorMessage = "Value must be from 0 to 10")]
    public decimal? AverageGrade { get; set; }

    [Required]
    public string? ResidentialAddress { get; set; }

    [Required]
    [StringLength(15, MinimumLength = 6)]
    public string? Phone { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    [Range(1910, 2050)]
    public int? GraduationYear { get; set; }
      
    public int? EducationInstitutionId { get; set; }

    [Required]
    public string? ForeignLanguage { get; set; }

    public int? ParentsId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<ApplicantCertificate> ApplicantCertificates { get; set; } = new List<ApplicantCertificate>();

    public virtual EducationInstitution? EducationInstitution { get; set; }

    public virtual Parent? Parents { get; set; }
}
