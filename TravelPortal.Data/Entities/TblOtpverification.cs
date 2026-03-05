using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblOtpverification
{
    public int Id { get; set; }

    public string? Mobile { get; set; }

    public string? Otp { get; set; }

    public DateTime? AddDate { get; set; }

    public bool? IsVerified { get; set; }
}
