using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class Subscription
{
    public int PackageId { get; set; }

    public decimal? SubFee { get; set; }

    public int? SubDuration { get; set; }
}
