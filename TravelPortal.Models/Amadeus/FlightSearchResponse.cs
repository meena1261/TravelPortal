
namespace TravelPortal.Models.Amadeus
{
    public class FlightOffersRoot
    {
        public Meta meta { get; set; }
        public List<Offer_Flight> data { get; set; }
        public Offer_Dictionaries dictionaries { get; set; }
    }
    public class Meta
    {
        public int count { get; set; }
    }

    public class Offer_Flight
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }

        public bool instantTicketingRequired { get; set; }
        public bool nonHomogeneous { get; set; }
        public bool oneWay { get; set; }
        public bool isUpsellOffer { get; set; }

        public string lastTicketingDate { get; set; }
        public string lastTicketingDateTime { get; set; }

        public int numberOfBookableSeats { get; set; }

        public List<Offer_Itinerary> itineraries { get; set; }
        public Offer_Price price { get; set; }
        public Offer_PricingOptions pricingOptions { get; set; }
        public List<string> validatingAirlineCodes { get; set; }

        public List<Offer_TravelerPricing> travelerPricings { get; set; }
    }
    public class Offer_PricingOptions
    {
        public List<string> fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }

    // ---------------------------
    // Itinerary
    // ---------------------------
    public class Offer_Itinerary
    {
        public string duration { get; set; }
        public List<Offer_Segment> segments { get; set; }
    }
    // ---------------------------
    // Price
    // ---------------------------
    public class Offer_Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public string grandTotal { get; set; }

        public List<Offer_PriceFee> fees { get; set; }
    }

    public class Offer_PriceFee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }

    // ---------------------------
    // Traveler pricing
    // ---------------------------
    public class Offer_TravelerPricing
    {
        public string travelerId { get; set; }
        public string fareOption { get; set; }
        public string travelerType { get; set; }
        public string associatedAdultId { get; set; }
        public Offer_Price price { get; set; }
        public List<Offer_FareDetailBySegment> fareDetailsBySegment { get; set; }
    }
    public class Offer_FareDetailBySegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string brandedFareLabel { get; set; }
        public string @class { get; set; }
        public Offer_Baggage includedCheckedBags { get; set; }
        public Offer_Baggage includedCabinBags { get; set; }
        public List<Offer_Amenity> amenities { get; set; }
    }
    public class Offer_Baggage
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
    }
    public class Offer_Amenity
    {
        public string description { get; set; }
        public bool isChargeable { get; set; }
        public string amenityType { get; set; }

        public Offer_AmenityProvider amenityProvider { get; set; }
    }

    public class Offer_AmenityProvider
    {
        public string name { get; set; }
    }
    // ---------------------------
    // Segment
    // ---------------------------
    public class Offer_Segment
    {

        public Offer_AirportInfo departure { get; set; }
        public Offer_AirportInfo arrival { get; set; }

        public string carrierCode { get; set; }
        public string number { get; set; }

        public Offer_AircraftInfo aircraft { get; set; }
        public Offer_Operating operating { get; set; }
        public string duration { get; set; }
        public string id { get; set; }
        public int numberOfStops { get; set; }
        public bool blacklistedInEU { get; set; }
    }
    // ---------------------------
    // Airport / Time
    // ---------------------------
    public class Offer_AirportInfo
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }
    // ---------------------------
    // Aircraft
    // ---------------------------
    public class Offer_AircraftInfo
    {
        public string code { get; set; }
    }
    public class Offer_Operating
    {
        public string carrierCode { get; set; }
    }
    public class Offer_Dictionaries
    {
        public Dictionary<string, Offer_Location> locations { get; set; }
        public Dictionary<string, string> aircraft { get; set; }
        public Dictionary<string, string> carriers { get; set; }
        public Dictionary<string, string> currencies { get; set; }
    }

    public class Offer_Location
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
        public string name { get; set; }
    }
}
