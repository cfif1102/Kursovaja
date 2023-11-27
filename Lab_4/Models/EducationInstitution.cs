using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class EducationInstitution
{
    public int EducationInstitutionId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    [MaxLength]
    public string? InstitutionName { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
