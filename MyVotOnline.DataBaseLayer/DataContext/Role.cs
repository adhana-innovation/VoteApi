using System;
using System.Collections.Generic;

namespace MyVotOnline.DataBaseLayer.DataContext;

public partial class Role
{
    public int Id { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
