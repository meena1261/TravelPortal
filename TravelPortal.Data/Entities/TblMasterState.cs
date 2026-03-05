using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblMasterState
{
    public int Id { get; set; }

    public string? StateName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TblMasterCity> TblMasterCities { get; set; } = new List<TblMasterCity>();
}
