using System;
using System.Collections.Generic;

namespace HumanResourcesDepartmentWPFApp.Models;

public partial class JobTitle
{
    public int Id { get; set; }

    public string? NameJobTitle { get; set; }

    public double? Salary { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
