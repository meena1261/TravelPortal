using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterKycdocumentType
{
    public int Id { get; set; }

    public string? KycdocumentType { get; set; }

    public bool? IsActive { get; set; }
}
