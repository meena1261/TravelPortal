using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class BecomeSupplierModel
    {
        public int Usrno { get; set; }
        public HttpPostedFileBase SupplierAgreementFile { get; set; }
    }
    public class FlightInventoryServiceModel
    {
        public FlightInventoryServiceModel()
        {
            FlightDetails = new List<FlightDetailViewModel> {
                new FlightDetailViewModel()
            };
            flightConnections = new List<FlightConnectionViaModel> {
                new FlightConnectionViaModel(),
                new FlightConnectionViaModel()
            };
        }

        // Master
        public string AirlineName { get; set; }
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Origin { get; set; }
        public string OriginCode { get; set; }
        public string Destination { get; set; }
        public string DestinationCode { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public string DepartureTerminal { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public string ArrivalTerminal { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        [DataType(DataType.MultilineText)]
        public string FareRules { get; set; }
        public string FareType { get; set; }
        public string SelectedDays { get; set; }
        public string StopTicketingBefore { get; set; }
        public TimeSpan? StopTicketingTime { get; set; }
        public string ConnectionTypes { get; set; }
        public string FlightStops { get; set; }
        // Detail
        public List<FlightDetailViewModel> FlightDetails { get; set; }
        public List<FlightConnectionViaModel> flightConnections { get; set; }

    }
    public class FlightDetailViewModel
    {
        public string Class { get; set; }
        public string RBD { get; set; } = "Y";
        public int? Seat { get; set; }
        public int? AdultFare { get; set; }
        public decimal? AdultFareBrackup { get; set; }
        public int? ChildFare { get; set; }
        public decimal? ChildFareBrackup { get; set; }
        public int? InfantFare { get; set; }
        public decimal? InfantFareBrackup { get; set; }
        public string IsBaggageIncluded { get; set; }
        public int? AdultBaggage { get; set; }
        public int? ChildBaggage { get; set; }
        public int? InfantBaggage { get; set; }
        public string UnitType { get; set; }
        public string IsRefundable { get; set; }
        public string PNR { get; set; }
    }
    public class FlightConnectionViaModel
    {
        public string SectorType { get; set; }
        public string Operator { get; set; }
        public string Flight { get; set; }
        public string FlightNo { get; set; }
        public string Origin { get; set; }
        public string OriginCode { get; set; }
        public string OriginTerminal { get; set; }
        public TimeSpan? DepatureTime { get; set; }
        public string Destination { get; set; }
        public string DestinationCode { get; set; }
        public string DestinationTerminal { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public bool IsNextDay { get; set; }
        public bool IsDepatureNextDay { get; set; }
    }
}