using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class ActivityRecord
{
    public int TrialId { get; set; }

    public string? TrialType { get; set; }

    public TimeOnly? TrialTime { get; set; }

    public decimal? TrialDistance { get; set; }

    public int? TrialDifficulty { get; set; }

    public decimal? TrialHeight { get; set; }
}
