using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Models
{
    public class FlightResults
    {
        public FlightResults() 
        {
            data = new List<OfferData>();
            filters = new List<FlightFilters>();
        }
        public string Supplier { get; set; } = string.Empty;
        public string cacheKey { get; set; } = string.Empty;
        public SearchRequestDto Search { get; set; }
        public List<OfferData> data { get; set; }
        public List<FlightFilters> filters { get; set; }
    }
    public class ItinerarySearch
    {
        public string from { get; set; }
        public string To { get; set; }
        public string Depature { get; set; }
        public string Return { get; set; }
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
        public string ToCity { get; set; } = string.Empty;
        public string FromIata { get; set; } = string.Empty;
        public string ToIata { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public string DepartureTime { get; set; } = string.Empty;
        public DateTime ArrivalDate { get; set; }
        public string ArrivalTime { get; set; } = string.Empty;
        public string AirlineLogo { get; set; } = string.Empty;
        public string AirlineName { get; set; } = string.Empty;
        public string AirlineCode { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string AircraftCode { get; set; } = string.Empty;
        public string AircraftName { get; set; } = string.Empty;
        public int Stops { get; set; } = 0;
        public string Via {  get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
    }
    public class FlightFilters
    {
        public string FilterKey { get; set; }     // airline / stops / price / departure
        public string Title { get; set; }         // Airlines / Stops / Departure Time
        public string Type { get; set; }          // checkbox / range / radio
        public dynamic Items { get; set; } 
    }

    public class FilterItem
    {
        public string Label { get; set; }         // Indigo
        public string Value { get; set; }         // 6E
        public int Count { get; set; }            // 12 flights
        public bool Selected { get; set; }
    }


}
