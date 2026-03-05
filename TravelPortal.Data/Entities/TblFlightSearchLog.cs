using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblFlightSearchLog
{
    public int SearchLogId { get; set; }

    public int? Usrno { get; set; }

    public string? TripType { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public DateTime? DepartureDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? AdultCount { get; set; }

    public int? ChildCount { get; set; }

    public int? InfantCount { get; set; }

    public string? TravelClass { get; set; }

    public bool? IsBook { get; set; }

    public string? Ipaddress { get; set; }

    public DateTime? AddDate { get; set; }

    public virtual TblMasterUser? UsrnoNavigation { get; set; }
}
