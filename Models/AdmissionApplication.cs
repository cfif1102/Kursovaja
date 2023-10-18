using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class AdmissionApplication
{
    public int ApplicationId { get; set; }

    public DateTime? ApplicationDate { get; set; }

    public int? ApplicantId { get; set; }

    public int? SpecialtyId { get; set; }

    public int? AdmissionsOfficerId { get; set; }

    public string? OtherDetails { get; set; }

    public virtual AdmissionsOfficer? AdmissionsOfficer { get; set; }

    public virtual Applicant? Applicant { get; set; }

    public virtual Specialty? Specialty { get; set; }
}
