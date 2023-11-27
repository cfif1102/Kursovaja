
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    [MaxLength]
    public string? SpecialtyName { get; set; }

    public int? FacultyId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<AdmissionPlan> AdmissionPlans { get; set; } = new List<AdmissionPlan>();

    public virtual Faculty? Faculty { get; set; }
}
