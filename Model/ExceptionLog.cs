using System;
using System.Collections.Generic;

namespace Ecommerce_Api.Model;

public partial class ExceptionLog
{
    public int LogId { get; set; }

    public DateTime Timestamp { get; set; }

    public string? UserId { get; set; }

    public string? ExceptionMessage { get; set; }

    public string? StackTrace { get; set; }

    public int? ErrorCode { get; set; }

    public string? Severity { get; set; }

    public string? Data { get; set; }

    public string? HelpLink { get; set; }

    public string? InnerException { get; set; }

    public string? Source { get; set; }

    public string? TargetSite { get; set; }

    public string? AdditionalInfo { get; set; }
}
