using System;
using System.Collections.Generic;

namespace TravelPortal.Data.Entities;

public partial class TblFlightInventory
{
    public int FlightInventoryId { get; set; }

    public int Usrno { get; set; }

    public string AirlineName { get; set; } = null!;

    public string AirlineCode { get; set; } = null!;

    public DateOnly FromDate { get; set; }

    public DateOnly Todate { get; set; }

    public string OriginCode { get; set; } = null!;

    public TimeOnly DepartureTime { get; set; }

    public string? DepartureTerminal { get; set; }

    public string DestinationCode { get; set; } = null!;

    public TimeOnly ArrivalTime { get; set; }

    public string? ArrivalTerminal { get; set; }

    public string? FareRules { get; set; }

    public int? FareDisplayType { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public int? StopTicketingDays { get; set; }

    public TimeOnly? StopTicketingTime { get; set; }

    public string AvailableDays { get; set; } = null!;

    public string? ConnectionType { get; set; }

    public int? Stops { get; set; }

    public DateTime AddDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsRejected { get; set; }

    public string? Remark { get; set; }

    public virtual ICollection<TblFlightConnectionVium> TblFlightConnectionVia { get; set; } = new List<TblFlightConnectionVium>();

    public virtual ICollection<TblFlightFare> TblFlightFares { get; set; } = new List<TblFlightFare>();
}
