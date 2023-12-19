using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class AdmissionApplication
{
    [Display(Name="Код заявления")]
    public int ApplicationId { get; set; }

    [Required]
    [Display(Name = "Дата заявления")]
    public DateTime ApplicationDate { get; set; }

    [Display(Name = "Код абитуриента")]
    public int? ApplicantId { get; set; }

    [Display(Name = "Код специальности")]
    public int? SpecialtyId { get; set; }

    [Display(Name = "Код члена комиссии")]
    public int? AdmissionsOfficerId { get; set; }

    [Required]
    [Display(Name = "Детали заявления")]
    public string? OtherDetails { get; set; }

    [Display(Name = "Член комиссии")]
    public virtual AdmissionsOfficer? AdmissionsOfficer { get; set; }

    [Display(Name = "Абитуриент")]
    public virtual Applicant? Applicant { get; set; }

    [Display(Name = "Специальность")]
    public virtual Specialty? Specialty { get; set; }
}
