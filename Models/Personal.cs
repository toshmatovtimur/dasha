using System;
using System.Collections.Generic;

namespace HumanResourcesDepartmentWPFApp.Models;

public partial class Personal
{
    public int Id { get; set; }

    public string? Family { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public int? SubDivision { get; set; }

    public int? JobTitle { get; set; }

    public string? Adress { get; set; }

    public int? Area { get; set; }

    public string? Inn { get; set; }

    public int? ChildrenCount { get; set; }

    public virtual Area? AreaNavigation { get; set; }

    public virtual JobTitle? JobTitleNavigation { get; set; }

    public virtual SubDivision? SubDivisionNavigation { get; set; }
}
