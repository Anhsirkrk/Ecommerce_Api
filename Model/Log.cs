using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class Log
{
    public int LogId { get; set; }

    public DateTime? LogDate { get; set; }

    public string? EventDescription { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
