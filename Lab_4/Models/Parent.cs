﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_4;

public partial class Parent
{
    public int ParentsId { get; set; }

    [Required]
    public string? Parent1Name { get; set; }

    [Required]
    public string? Parent2Name { get; set; }

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
}
