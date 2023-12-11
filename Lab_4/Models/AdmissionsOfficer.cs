using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class AdmissionsOfficer
{
    public int AdmissionsOfficerId { get; set; }
    [Required]

    public string? FullName { get; set; }

    [Required]
    public string? Department { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();
}
