using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class FlightResults
    {
        public FlightResults()
        {
            data = new List<OfferData>();
            Search = new SearchRequestDto();
            filters = new List<FlightFilters>();
        }
        public string Supplier { get; set; } = string.Empty;
        public string cacheKey { get; set; } = string.Empty;
        public SearchRequestDto Search { get; set; }
        public List<OfferData> data { get; set; }
        public List<FlightFilters> filters { get; set; }
    }
    public class OfferData
    {
        public OfferData()
        {
            Itineraries = new List<FlightItinerary>();
        }
        public string offerId { get; set; } = string.Empty;
        public List<FlightItinerary> Itineraries { get; set; } = new List<FlightItinerary>();
    }
    public class FlightItinerary
    {
        public string FromCity { get; set; } = string.Empty;
        public string FromAirport { get; set; } = string.Empty;
        public string ToCity { get; set; } = string.Empty;
        public string TravelDate { get; set; } = string.Empty;
        public string DepartureDate { get; set; } = string.Empty;
        public string DepartureTime { get; set; } = string.Empty;
        public string ArrivalDate { get; set; } = string.Empty;
        public string ArrivalTime { get; set; } = string.Empty;
        public string AirlineLogo { get; set; } = string.Empty;
        public string AirlineName { get; set; } = string.Empty;
        public string AirlineCode { get; set; } = string.Empty;
        public string AircraftCode { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public int Stops { get; set; } = 0;
        public string Via { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
    public class FlightFilters
    {
        public string FilterKey { get; set; }     // airline / stops / price / departure
        public string Title { get; set; }         // Airlines / Stops / Departure Time
        public string Type { get; set; }          // checkbox / range / radio
        public dynamic Items { get; set; }
    }

}