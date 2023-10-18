using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class Applicant
{
    public int ApplicantId { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Middlename { get; set; }

    public string? IdentificationDocument { get; set; }

    public string? EducationDocument { get; set; }

    public decimal? AverageGrade { get; set; }

    public string? ResidentialAddress { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int? GraduationYear { get; set; }

    public int? EducationInstitutionId { get; set; }

    public string? ForeignLanguage { get; set; }

    public int? ParentsId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<ApplicantCertificate> ApplicantCertificates { get; set; } = new List<ApplicantCertificate>();

    public virtual EducationInstitution? EducationInstitution { get; set; }

    public virtual Parent? Parents { get; set; }
}
