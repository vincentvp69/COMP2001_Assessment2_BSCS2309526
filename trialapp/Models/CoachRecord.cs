using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class CoachRecord
{
    public int ProfileId { get; set; }

    public decimal? CoachRating { get; set; }

    public int? CoachExperience { get; set; }

    public virtual Profile Profile { get; set; } = null!;
}
