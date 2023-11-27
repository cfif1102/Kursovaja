using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class AdmissionsOfficer
{
    public int AdmissionsOfficerId { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [MaxLength]
    public string? FullName { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    [MaxLength]
    public string? Department { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();
}
