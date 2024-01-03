using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class Profile
{
    public int? UserId { get; set; }

    public int ProfileId { get; set; }

    public string? ProfileType { get; set; }

    public string? ProfileStatus { get; set; }

    public virtual CoachRecord? CoachRecord { get; set; }
}
