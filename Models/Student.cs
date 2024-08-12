using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int ParentId { get; set; }

    public string Name { get; set; }

    public string Gender { get; set; }

    public DateTime DayOfBirth { get; set; }

    public string Class { get; set; }

    public string Address { get; set; } 

    public int Phone { get; set; }

    public DateTime JoinDay { get; set; }

    public virtual Absentreport Absentreports { get; set; } 
    public virtual Alert Alerts { get; set; }

    public virtual Entrylog Entrylogs { get; set; }

    public virtual Parent Parent { get; set; }
}
