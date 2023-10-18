using System;
using System.Collections.Generic;

namespace Kursovaja;

public partial class Specialty
{
    public int SpecialtyId { get; set; }

    public string? SpecialtyName { get; set; }

    public int? FacultyId { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();

    public virtual ICollection<AdmissionPlan> AdmissionPlans { get; set; } = new List<AdmissionPlan>();

    public virtual Faculty? Faculty { get; set; }
}
