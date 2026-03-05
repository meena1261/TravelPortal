using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterCreditType
{
    public int CreditTypeId { get; set; }

    public string? CreditType { get; set; }

    public bool? IsActive { get; set; }
}
