using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterCity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StateId { get; set; }

    public virtual TblMasterState? State { get; set; }
}
