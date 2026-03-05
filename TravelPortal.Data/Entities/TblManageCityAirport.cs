using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblManageCityAirport
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Icaocode { get; set; }
}
