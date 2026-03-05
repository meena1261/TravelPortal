using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class FlightRoot
    {
        public int Id { get; set; }
        public int NumberOfBookableSeats { get; set; }
        public List<FlightItinerariesModel> Itineraries { get; set; }
        public PriceModel Price { get; set; }
        public List<TravelersModel> Travelers { get; set; }
    }
    public class FlightItinerariesModel
    {
        public string Duration { get; set; }
        public string ItinerariesID { get; set; }
        public List<FlightSegmentsModel> FlightSegments { get; set; }
    }
    public class TravelersModel
    {
        public int TravelerID { get; set; }
        public string TravelerType { get; set; }
        public string FareOption { get; set; }
        public string BasePrice { get; set; }
        public string TotalPrice { get; set; }
    }
    public class PriceModel
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public List<Fee> fees { get; set; }
        public string grandTotal { get; set; }
    }
    public class Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }
    public class FlightSegmentsModel
    {
        public int SegmentId { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureIataCode { get; set; }
        public string DepartureDateTime { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalIataCode { get; set; }
        public string ArrivalDateTime { get; set; }
        public string ArrivalTerminal { get; set; }
        public string CarrierCode { get; set; }
        public string CarrierLogo { get; set; }
        public string CarrierName { get; set; }
        public string CarrierNumber { get; set; }
        public string AircraftCode { get; set; }
        public string Aircraft { get; set; }
        public int NumberofStops { get; set; }
        public string Cabin { get; set; }
        public string FareBasis { get; set; }
        public string BrandedFare { get; set; }
        public string BrandedFareLabel { get; set; }
        public string classType { get; set; }
        public string CheckedBagsWeight { get; set; }
        public string CheckedBagsWeightUnit { get; set; }
        public string CabinBagsWeight { get; set; }
        public string CabinBagsWeightUnit { get; set; }
        //public List<SegmentAmenitiesModel> Amenities { get; set; }
    }
}
