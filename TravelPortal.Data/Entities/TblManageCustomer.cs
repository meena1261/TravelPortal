using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageCustomer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? UserId { get; set; }

    public string? Password { get; set; }

    public DateTime? AddDate { get; set; }

    public string? Otp { get; set; }
}
