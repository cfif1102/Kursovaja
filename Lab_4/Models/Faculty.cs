using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Faculty
{
    public int FacultyId { get; set; }

    [Required]
    public string? FacultyName { get; set; }

    public virtual ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
}
