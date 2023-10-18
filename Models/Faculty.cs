using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class Faculty
{
    public int FacultyId { get; set; }

    public string? FacultyName { get; set; }

    public virtual ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();
}
