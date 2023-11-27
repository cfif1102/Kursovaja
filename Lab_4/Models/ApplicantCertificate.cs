using Lab_4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_4;

public partial class ApplicantCertificate
{
    public int CertificateId { get; set; }

    public int? ApplicantId { get; set; }

    [Required]
    [Range(0.0, 10.0, ErrorMessage = "Value must be from 0 to 10")]
    public decimal? Grade { get; set; } = 0;

    public virtual Applicant? Applicant { get; set; }
}
