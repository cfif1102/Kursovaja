﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class EducationInstitution
{
    [Display(Name = "Код учреждения образования")]
    public int EducationInstitutionId { get; set; }

    [Required]
    [Display(Name = "Название")]
    public string? InstitutionName { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
