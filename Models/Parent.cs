using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class Parent
{
    public int ParentId { get; set; }

    public string ParentName { get; set; }

    public string ParentEmail { get; set; }

    public int ParentPhone { get; set; }

    public string ParentAddress { get; set; }

    public Absentreport Absentreports { get; set; }

    public virtual Student Students { get; set; }
}
