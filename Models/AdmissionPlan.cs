using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class AdmissionPlan
{
    public int AdmissionPlanId { get; set; }

    public int? Year { get; set; }

    public int? SpecialtyId { get; set; }

    public int? NumberOfSeats { get; set; }

    public virtual Specialty? Specialty { get; set; }
}
