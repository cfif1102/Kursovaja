using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class ApplicantCertificate
{
    public int CertificateId { get; set; }

    public int? ApplicantId { get; set; }

    public decimal? Grade { get; set; }

    public virtual Applicant? Applicant { get; set; }
}
