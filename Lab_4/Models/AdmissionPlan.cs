using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Lab_4;

public partial class AdmissionPlan
{
    public int AdmissionPlanId { get; set; }


    [Required]
    [Range(1910, 2050, ErrorMessage = "Year should be from range 1910 to 2050!")]
    public int? Year { get; set; }

    
    public int? SpecialtyId { get; set; }

    [Required]
    [RegularExpression(@"^[+]?\d+([.]\d+)?$", ErrorMessage = "Number of seats should be positive number!")]

    public int NumberOfSeats { get; set; }

    public virtual Specialty? Specialty { get; set; }
}
