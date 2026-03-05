using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterAgencyType
{
    public int Id { get; set; }

    public string? AgencyType { get; set; }

    public bool? IsActive { get; set; }
}
