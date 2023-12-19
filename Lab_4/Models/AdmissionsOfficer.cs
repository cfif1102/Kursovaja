using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class AdmissionsOfficer
{
    [Display(Name = "Код члена комиссии")]
    public int AdmissionsOfficerId { get; set; }

    [Required]
    [Display(Name = "Имя")]
    public string? FullName { get; set; }

    [Display(Name = "Отдел")]
    [Required]
    public string? Department { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();
}
