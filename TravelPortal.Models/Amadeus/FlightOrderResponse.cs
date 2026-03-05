using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.Amadeus
{
    public class FlightOrderResponse
    {
        public FlightOrderData data { get; set; }
        public Dictionaries dictionaries { get; set; }
    }
    public class FlightOrderData
    {
        public string type { get; set; }
        public string id { get; set; }
        public string queuingOfficeId { get; set; }
        public List<AssociatedRecord> associatedRecords { get; set; }
        public List<Order_FlightOffer> flightOffers { get; set; }
        public List<Order_Traveler> travelers { get; set; }
        public Order_Remarks remarks { get; set; }
        public Order_TicketingAgreement ticketingAgreement { get; set; }
        public List<Order_AutomatedProcess> automatedProcess { get; set; }
        public List<Order_Contact> contacts { get; set; }
    }
    public class AssociatedRecord
    {
        public string reference { get; set; }
        public DateTime creationDate { get; set; }
        public string originSystemCode { get; set; }
        public string flightOfferId { get; set; }
    }
    public class Order_FlightOffer
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public bool nonHomogeneous { get; set; }
        public string lastTicketingDate { get; set; }
        public List<Order_Itinerary> itineraries { get; set; }
        public Order_Price price { get; set; }
        public Order_PricingOptions pricingOptions { get; set; }
        public List<string> validatingAirlineCodes { get; set; }
        public List<Order_TravelerPricing> travelerPricings { get; set; }
    }
    public class Order_Itinerary
    {
        public List<Order_Segment> segments { get; set; }
    }

    public class Order_Segment
    {
        public Order_Airport departure { get; set; }
        public Order_Airport arrival { get; set; }
        public string carrierCode { get; set; }
        public string number { get; set; }
        public Order_Aircraft aircraft { get; set; }
        public string duration { get; set; }
        public string id { get; set; }
        public int numberOfStops { get; set; }
        public List<Order_Co2Emission> co2Emissions { get; set; }
    }
    public class Order_Airport
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }

    public class Order_Aircraft
    {
        public string code { get; set; }
    }

    public class Order_Co2Emission
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
        public string cabin { get; set; }
    }
    public class Order_Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public List<Order_Fee> fees { get; set; }
        public string grandTotal { get; set; }
        public string billingCurrency { get; set; }
    }

    public class Order_Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }
    public class Order_PricingOptions
    {
        public List<string> fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }
    public class Order_TravelerPricing
    {
        public string travelerId { get; set; }
        public string fareOption { get; set; }
        public string travelerType { get; set; }
        public Order_PriceDetail price { get; set; }
        public List<Order_FareDetailsBySegment> fareDetailsBySegment { get; set; }
    }

    public class Order_PriceDetail
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public List<Order_Tax> taxes { get; set; }
        public string refundableTaxes { get; set; }
    }

    public class Order_Tax
    {
        public string amount { get; set; }
        public string code { get; set; }
    }
    public class Order_FareDetailsBySegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string @class { get; set; }
        public Order_IncludedCheckedBags includedCheckedBags { get; set; }
    }

    public class Order_IncludedCheckedBags
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
    }
    public class Order_Traveler
    {
        public string id { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string gender { get; set; }
        public Order_TravelerName name { get; set; }
        public Order_TravelerContact contact { get; set; }
    }

    public class Order_TravelerName
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Order_TravelerContact
    {
        public string purpose { get; set; }
        public List<Order_Phone> phones { get; set; }
        public string emailAddress { get; set; }
    }
    public class Order_Remarks
    {
        public List<Order_GeneralRemark> general { get; set; }
    }

    public class Order_GeneralRemark
    {
        public string subType { get; set; }
        public string text { get; set; }
    }

    public class Order_TicketingAgreement
    {
        public string option { get; set; }
        public string delay { get; set; }
    }

    public class Order_AutomatedProcess
    {
        public string code { get; set; }
        public Order_QueueInfo queue { get; set; }
        public string officeId { get; set; }
    }

    public class Order_QueueInfo
    {
        public string number { get; set; }
        public string category { get; set; }
    }
    public class Order_Contact
    {
        public Order_AddresseeName addresseeName { get; set; }
        public Order_Address address { get; set; }
        public string purpose { get; set; }
        public List<Order_Phone> phones { get; set; }
        public string companyName { get; set; }
        public string emailAddress { get; set; }
    }

    public class Order_AddresseeName
    {
        public string firstName { get; set; }
    }

    public class Order_Address
    {
        public List<string> lines { get; set; }
        public string postalCode { get; set; }
        public string countryCode { get; set; }
        public string cityName { get; set; }
    }

    public class Order_Phone
    {
        public string deviceType { get; set; }
        public string countryCallingCode { get; set; }
        public string number { get; set; }
    }
    public class Order_Dictionaries
    {
        public Dictionary<string, Order_Location> locations { get; set; }
    }

    public class Order_Location
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }


}
