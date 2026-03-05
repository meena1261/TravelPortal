using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblFlightConnectionVium
{
    public int FlightConnectionViaId { get; set; }

    public int FlightInventoryId { get; set; }

    public string? Operator { get; set; }

    public string? Flight { get; set; }

    public string? FlightNo { get; set; }

    public string? OriginCode { get; set; }

    public TimeOnly? DepatureTime { get; set; }

    public string? DepatureTerminal { get; set; }

    public string? DestinationCode { get; set; }

    public TimeOnly? ArrivalTime { get; set; }

    public string? ArrivalTerminal { get; set; }

    public bool? IsNextDay { get; set; }

    public bool? DepatureOnNextDay { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? AddDate { get; set; }

    public virtual TblFlightInventory FlightInventory { get; set; } = null!;
}
