using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class AdmissionsOfficer
{
    public int AdmissionsOfficerId { get; set; }

    public string? FullName { get; set; }

    public string? Department { get; set; }

    public virtual ICollection<AdmissionApplication> AdmissionApplications { get; set; } = new List<AdmissionApplication>();
}
