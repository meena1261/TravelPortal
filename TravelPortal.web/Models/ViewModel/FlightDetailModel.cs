using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class FlightInfoVM
    {
        public string CacheKey { get; set; }
        public List<ItinerariesInfoVM> flightItineraries { get; set; }
        public FlightSummaryVM flightSummary { get; set; }
    }
    public class FlightSummaryVM
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
    public class ItinerariesInfoVM
    {
        public string offerId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public string TravelDate { get; set; }
        public int Stops { get; set; }
        public string Duration { get; set; }
        public List<SegmentInfoVM> segments { get; set; }
    }
    public class SegmentInfoVM
    {
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
        public IncludedBagsInfoVM CabinBaggage { get; set; } = new IncludedBagsInfoVM();
        public IncludedBagsInfoVM CheckInBaggage { get; set; } = new IncludedBagsInfoVM();
        public FareRuleVM fareRule { get; set; }
        public int IsExtraBaggage { get; set; }
    }
    public class IncludedBagsInfoVM
    {
        public string weight { get; set; }
        public string weightUnit { get; set; }
    }
    public class FareRuleVM
    {
        public string fareBasis { get; set; }
        public string name { get; set; }
        public FareNotesVM fareNotes { get; set; }
    }

    public class FareNotesVM
    {
        public List<DescriptionVM> descriptions { get; set; }
    }

    public class DescriptionVM
    {
        public string descriptionType { get; set; }
        public string text { get; set; }
    }
}