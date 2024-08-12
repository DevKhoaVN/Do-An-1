using System;
using System.Collections.Generic;

namespace TestMySql.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public int ParentId { get; set; }

    public int RoleId { get; set; }

    public  Userrole Role { get; set; }
    public Parent Parent { set; get; }
}
