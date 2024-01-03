using System;
using System.Collections.Generic;

namespace trialapp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public decimal? UserHeight { get; set; }

    public decimal? UserWeight { get; set; }

    public string? Email { get; set; }
}
