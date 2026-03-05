using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models
{
    public class FlightDetailModel
    {
        public string CacheKey { get; set; }
        public string tripType { get; set; }
        public List<FlightItinerariesModel> flightItineraries { get; set; }
        public FlightSummaryModel flightSummary { get; set; }
    }

    public class FlightItinerariesModel
    {
        public string offerId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string TravelDate { get; set; }
        public int Stops { get; set; }
        public string Duration { get; set; }
        public List<SegmentDetail> segments { get; set; }
    }
    public class SegmentDetail
    {
        public int SegmentId { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public string AirlineCode { get; set; }
        public string AirCraftCode { get; set; }
        public string CabinClass { get; set; }
        public string CabinClassType { get; set; }
        public string FromCity { get; set; }
        public string FromAirport { get; set; }
        public string ToCity { get; set; }
        public string ToAirport { get; set; }
        public string DepatureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string Duration { get; set; }
        public IncludedBags CabinBaggage { get; set; } = new IncludedBags();
        public IncludedBags CheckInBaggage { get; set; } = new IncludedBags();
        public FareRule fareRule { get; set; }
        public int IsExtraBaggage { get; set; }
        public int BaggageQuantity { get; set; }
        public string BaggagePrice { get; set; }
      
    }
    public class IncludedBags
    {
        public string weight { get; set; }
        public string weightUnit { get; set; }
    }
    public class Included
    {
        [JsonProperty("detailed-fare-rules")]
        public Dictionary<string, FareRule> DetailedFareRules { get; set; }
    }
    public class FareRuleDisplayModel
    {
        public string Title { get; set; }
        public List<string> Lines { get; set; } = new();
    }

    public class FareRule
    {
        public string fareBasis { get; set; }
        public string name { get; set; }
        public FareNotes fareNotes { get; set; }
    }

    public class FareNotes
    {
        public List<Description> descriptions { get; set; }
    }

    public class Description
    {
        public string descriptionType { get; set; }
        public string text { get; set; }
    }
    public class FlightSummaryModel
    {
        public double BaseFare { get; set; }
        public int Adult { get; set; }
        public double AdultBasePrice { get; set; }
        public double AdultTotalPrice { get; set; }
        public int Children { get; set; }
        public double ChildrenBasePrice { get; set; }
        public double ChildrenTotalPrice { get; set; }
        public int Infant { get; set; }
        public double InfantBasePrice { get; set; }
        public double InfantTotalPrice { get; set; }
        public double TaxesServiceCharges { get; set; }
        public double TotalPrice { get; set; }
        public double UniversalMarkup { get; set; }
        public double AirlineMarkup { get; set; }
    }
}
