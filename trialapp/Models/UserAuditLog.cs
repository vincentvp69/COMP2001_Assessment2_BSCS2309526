using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class UserAuditLog
{
    public int AuditLogId { get; set; }

    public string? AuditType { get; set; }

    public int? UserId { get; set; }

    public string? Username { get; set; }

    public DateTime? AuditDateTime { get; set; }
}
