using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterBusinessType
{
    public int Id { get; set; }

    public string? BusinessType { get; set; }

    public bool? IsActive { get; set; }
}
