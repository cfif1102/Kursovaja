using Lab_4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_4;

public partial class ApplicantCertificate
{
    [Display(Name = "Код сертификата")]
    public int CertificateId { get; set; }

    [Display(Name = "Код абитуриента")]
    public int? ApplicantId { get; set; }

    [Required]
    [Range(0.0, 10.0, ErrorMessage = "Value must be from 0 to 10")]
    [Display(Name = "Балл")]
    public decimal? Grade { get; set; } = 0;

    [Display(Name = "Абитуриент")]
    public virtual Applicant? Applicant { get; set; }
}
