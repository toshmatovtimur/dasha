using System;
using System.Collections.Generic;

namespace HumanResourcesDepartmentWPFApp.Models;

public partial class Area
{
    public int Id { get; set; }

    public string? NameArea { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
