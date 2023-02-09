using System;
using System.Collections.Generic;

namespace Parfumeria.database;

public partial class Role
{
    public int Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
