using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblFlightFare
{
    public int FlightFareId { get; set; }

    public int FlightInventoryId { get; set; }

    public string? ClassType { get; set; }

    public string? Rbd { get; set; }

    public int? Seats { get; set; }

    public int? AdultFare { get; set; }

    public decimal? AdultFareBreakup { get; set; }

    public int? ChildFare { get; set; }

    public decimal? ChildFareBreakup { get; set; }

    public int? InfantFare { get; set; }

    public decimal? InfantFareBreakup { get; set; }

    public bool? IsBaggage { get; set; }

    public int? AdultBaggage { get; set; }

    public int? ChildBaggage { get; set; }

    public int? InfantBaggage { get; set; }

    public string? Unit { get; set; }

    public bool? IsRefund { get; set; }

    public string? Pnr { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? AddDate { get; set; }

    public virtual TblFlightInventory FlightInventory { get; set; } = null!;
}
