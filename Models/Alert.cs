using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class Alert
{
    public int AlertId { get; set; }

    public int StudentId { get; set; }

    public DateTime AlertTime { get; set; }

    public Student Student { get; set; }
}
