using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Lab_4;

public partial class AdmissionPlan
{
    [Display(Name = "Код плана")]
    public int AdmissionPlanId { get; set; }


    [Required]
    [Range(1910, 2050, ErrorMessage = "Year should be from range 1910 to 2050!")]
    [Display(Name = "Год")]
    public int? Year { get; set; }

    [Display(Name = "Код специальнтсти")]
    public int? SpecialtyId { get; set; }

    [Required]
    [RegularExpression(@"^[+]?\d+([.]\d+)?$", ErrorMessage = "Number of seats should be positive number!")]
    [Display(Name = "Кол-во мест")]

    public int NumberOfSeats { get; set; }

    [Display(Name = "Специальность")]
    public virtual Specialty? Specialty { get; set; }
}
