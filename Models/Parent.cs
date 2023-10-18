using System;
using System.Collections.Generic;

namespace Kursovaja;


public partial class Parent
{
    public int ParentsId { get; set; }

    public string? Parent1Name { get; set; }

    public string? Parent2Name { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
