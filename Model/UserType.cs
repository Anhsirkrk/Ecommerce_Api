using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class UserType
{
    public int TypeId { get; set; }

    public string? UserType1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
