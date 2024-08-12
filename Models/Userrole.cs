using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class Userrole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public User Users { get; set; }
}