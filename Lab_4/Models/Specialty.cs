
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Specialty
{
    [Display(Name = "Код специальностей")]
    public int SpecialtyId { get; set; }

    [Required]
    [Display(Name = "Специальность")]
    public string? SpecialtyName { get; set; }

    [Display(Name = "Код факультета")]
    public int? FacultyId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<AdmissionPlan> AdmissionPlans { get; set; } = new List<AdmissionPlan>();

    [Display(Name = "Факультет")]
    public virtual Faculty? Faculty { get; set; }
}
