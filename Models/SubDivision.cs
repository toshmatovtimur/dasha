using System;
using System.Collections.Generic;

namespace HumanResourcesDepartmentWPFApp.Models;

public partial class SubDivision
{
    public int Id { get; set; }

    public string? NameDivisions { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
