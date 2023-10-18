using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class EducationInstitution
{
    public int EducationInstitutionId { get; set; }

    public string? InstitutionName { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
