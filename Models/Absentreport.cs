using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class Absentreport
{
    public int AbsentReportId { get; set; }

    public int StudentId { set; get; }
    public int ParentId { get; set; }
    public DateTime CreateDay { get; set; }

    public string Reason { get; set; }

    public Parent Parent { get; set; }


}
