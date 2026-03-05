using Newtonsoft.Json;
using System.Diagnostics;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Models.Amadeus
{
    public class FlightPricingRoot
    {
        public Pricing_Data data { get; set; }
        public Pricing_Included included { get; set; }
    }

    public class Pricing_Data
    {
        public string type { get; set; }
        public List<Pricing_FlightOffer> flightOffers { get; set; }
        public Pricing_BookingRequirements bookingRequirements { get; set; }
    }

    public class Pricing_FlightOffer
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }

        public bool instantTicketingRequired { get; set; }
        public bool nonHomogeneous { get; set; }
        public bool paymentCardRequired { get; set; }
        public string lastTicketingDate { get; set; }
        public List<Pricing_Itinerary> itineraries { get; set; }
        public Pricing_Price price { get; set; }
        public Pricing_PricingOptions pricingOptions { get; set; }
        public List<string> validatingAirlineCodes { get; set; }
        public List<Pricing_TravelerPricing> travelerPricings { get; set; }
    }
    // ---------------------------
    // Itinerary
    // ---------------------------
    public class Pricing_Itinerary
    {
        public List<Pricing_Segment> segments { get; set; }
    }
    // ---------------------------
    // Segment
    // ---------------------------
    public class Pricing_Segment
    {
        public string id { get; set; }

        public Pricing_AirportTime departure { get; set; }
        public Pricing_AirportTime arrival { get; set; }

        public string carrierCode { get; set; }
        public string number { get; set; }

        public Pricing_AircraftInfo aircraft { get; set; }
        public Pricing_Operating operating { get; set; }

        public string duration { get; set; }
        public int numberOfStops { get; set; }
        public List<Pricing_co2Emissions> co2Emissions { get; set; }
    }
    // ---------------------------
    // Airport / Time
    // ---------------------------
    public class Pricing_AirportTime
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }
    // ---------------------------
    // Aircraft
    // ---------------------------
    public class Pricing_AircraftInfo
    {
        public string code { get; set; }
    }

    public class Pricing_Operating
    {
        public string carrierCode { get; set; }
    }
    public class Pricing_co2Emissions
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
        public string cabin { get; set; }
    }
    // ---------------------------
    // Price
    // ---------------------------
    public class Pricing_PricingOptions
    {
        public List<string> fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }
    public class Pricing_Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public string grandTotal { get; set; }
        public string billingCurrency { get; set; }
        public List<Pricing_Fee> fees { get; set; }
    }
    public class Pricing_Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }
    public class Pricing_TravelerPricing
    {
        public string travelerId { get; set; }
        public string travelerType { get; set; }
        public string fareOption { get; set; }

        public TravelerPricing_Price price { get; set; }

        public List<Pricing_FareDetailsBySegment> fareDetailsBySegment { get; set; }
    }
    public class TravelerPricing_Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public List<TravelPricing_PriceTax> taxes { get; set; }
        public string refundableTaxes { get; set; }
    }
    public class TravelPricing_PriceTax
    {
        public string amount { get; set; }
        public string code { get; set; }
    }
    public class Pricing_FareDetailsBySegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string @class { get; set; }
        public Pricing_IncludedCheckedBags includedCheckedBags { get; set; }
    }

    public class Pricing_IncludedCheckedBags
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
    }
    public class Pricing_BookingRequirements
    {
        public bool emailAddressRequired { get; set; }
        public bool mobilePhoneNumberRequired { get; set; }
    }

    public class Pricing_Included
    {
        [JsonProperty("detailed-fare-rules")]
        public Dictionary<string, Pricing_FareRule> DetailedFareRules { get; set; }
        public Dictionary<string, Pricing_BagInfo> bags { get; set; }
    }

    public class Pricing_FareRule
    {
        public string segmentId { get; set; }
        public string fareBasis { get; set; }
        public string name { get; set; }
        public Pricing_FareNotes fareNotes { get; set; }
    }

    public class Pricing_FareNotes
    {
        public List<Pricing_Description> descriptions { get; set; }
    }

    public class Pricing_Description
    {
        public string descriptionType { get; set; }
        public string text { get; set; }
    }
    public class Pricing_BagInfo
    {
        public string quantity { get; set; }

        public string name { get; set; }
        public Pricing_ExtraBagsPrice price { get; set; }
        public List<string> SegmentIds { get; set; } = new();
        public List<string> TravelerIds { get; set; } = new();
    }
    public class Pricing_ExtraBagsPrice
    {
        public string amount { get; set; }
    }
}
