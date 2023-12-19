using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Applicant
{
    [Display(Name = "Код абитуриента")]
    public int ApplicantId { get; set; }

    [Required]
    [Display(Name = "Имя")]
    public string? Surname { get; set; }

    [Required]
    [Display(Name = "Фамилия")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Отчество")]
    public string? Middlename { get; set; }

    [Required]
    [Display(Name = "Документ")]
    public string? IdentificationDocument { get; set; }

    [Required]
    [Display(Name = "Документ об образовании")]
    public string? EducationDocument { get; set; }

    [Required]
    [Range(0.0, 10.0, ErrorMessage = "Value must be from 0 to 10")]
    [Display(Name = "Средний балл")]
    public decimal? AverageGrade { get; set; }

    [Required]
    [Display(Name = "Адрес")]
    public string? ResidentialAddress { get; set; }

    [Required]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }

    [Required]
    [Display(Name = "Почта")]
    public string? Email { get; set; }

    [Required]
    [Range(1910, 2050)]
    [Display(Name = "Год выпуска")]
    public int? GraduationYear { get; set; }

    [Display(Name = "Код учреждения образования")]
    public int? EducationInstitutionId { get; set; }

    [Required]
    [Display(Name = "Ин. яз.")]
    public string? ForeignLanguage { get; set; }

    [Display(Name = "Код родителей")]
    public int? ParentsId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<ApplicantCertificate> ApplicantCertificates { get; set; } = new List<ApplicantCertificate>();

    public virtual EducationInstitution? EducationInstitution { get; set; }

    [Display(Name = "Родители")]
    public virtual Parent? Parents { get; set; }
}
