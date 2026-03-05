using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TravelPortal.Models.Amadeus
{
    // Root response object
    public class SeatmapsResponse
    {
        [JsonPropertyName("meta")]
        public MetaSetas Meta { get; set; }

        [JsonPropertyName("data")]
        public List<SeatmapData> Data { get; set; }

        [JsonPropertyName("dictionaries")]
        public Dictionaries Dictionaries { get; set; }
    }

    public class MetaSetas
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    public class SeatmapData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("departure")]
        public LocationInfo Departure { get; set; }

        [JsonPropertyName("arrival")]
        public LocationInfo Arrival { get; set; }

        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonPropertyName("number")]
        public string FlightNumber { get; set; }

        [JsonPropertyName("operating")]
        public Seats_Operating Operating { get; set; }

        [JsonPropertyName("aircraft")]
        public Seats_Aircraft Aircraft { get; set; }

        [JsonPropertyName("class")]
        public string TravelClass { get; set; }

        [JsonPropertyName("flightOfferId")]
        public string FlightOfferId { get; set; }

        [JsonPropertyName("segmentId")]
        public string SegmentId { get; set; }

        [JsonPropertyName("decks")]
        public List<Deck> Decks { get; set; }
        [JsonPropertyName("availableSeatsCounters")]
        public List<AvailableSeatsCounters> availableSeatsCounters { get; set; }
    }

    public class LocationInfo
    {
        [JsonPropertyName("iataCode")]
        public string IataCode { get; set; }

        [JsonPropertyName("terminal")]
        public string Terminal { get; set; }

        [JsonPropertyName("at")]
        public DateTime At { get; set; }
    }

    public class Seats_Operating
    {
        [JsonPropertyName("carrierCode")]
        public string carrierCode { get; set; }
    }

    public class Seats_Aircraft
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    public class Deck
    {
        [JsonPropertyName("deckType")]
        public string DeckType { get; set; }

        [JsonPropertyName("deckConfiguration")]
        public DeckConfiguration DeckConfiguration { get; set; }

        [JsonPropertyName("facilities")]
        public List<Facility> Facilities { get; set; }
        [JsonPropertyName("seats")]
        public List<Seats> Seats { get; set; }
    }

    public class DeckConfiguration
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("startSeatRow")]
        public int StartSeatRow { get; set; }

        [JsonPropertyName("endSeatRow")]
        public int EndSeatRow { get; set; }

        [JsonPropertyName("startWingsX")]
        public int StartWingsX { get; set; }

        [JsonPropertyName("endWingsX")]
        public int EndWingsX { get; set; }

        [JsonPropertyName("startWingsRow")]
        public int StartWingsRow { get; set; }

        [JsonPropertyName("endWingsRow")]
        public int EndWingsRow { get; set; }

        [JsonPropertyName("exitRowsX")]
        public List<int> ExitRowsX { get; set; }
    }

    public class Facility
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("column")]
        public string Column { get; set; }

        [JsonPropertyName("row")]
        public string Row { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }

    public class Seats
    {
        [JsonPropertyName("cabin")]
        public string cabin { get; set; }
        [JsonPropertyName("number")]
        public string number { get; set; }
        [JsonPropertyName("characteristicsCodes")]
        public List<string> characteristicsCodes { get; set; }
        [JsonPropertyName("travelerPricing")]
        public List<TravelerPricing> travelerPricing { get; set; }
        [JsonPropertyName("coordinates")]
        public Coordinates coordinates { get; set; }
    }
    public class TravelerPricing
    {
        [JsonPropertyName("travelerId")]
        public string TravelerId { get; set; }
        [JsonPropertyName("seatAvailabilityStatus")]
        public string seatAvailabilityStatus { get; set; }
        [JsonPropertyName("price")]
        public Pricing price { get; set; }
    }
    public class Pricing
    {
        [JsonPropertyName("currency")]
        public string currency { get; set; }
        [JsonPropertyName("total")]
        public string total { get; set; }
        [JsonPropertyName("base")]
        public string @base { get; set; }
        [JsonPropertyName("taxes")]
        public List<PriceTaxes> taxes { get; set; }
    }
    public class PriceTaxes
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
    public class AvailableSeatsCounters
    {
        [JsonPropertyName("travelerId")]
        public string travelerid { get; set; }
        [JsonPropertyName("value")]
        public string value { get; set; }
    }

    // Dictionaries section
    public class Dictionaries
    {
        [JsonPropertyName("locations")]
        public Dictionary<string, LocationDictionary> Locations { get; set; }

        [JsonPropertyName("facilities")]
        public Dictionary<string, string> Facilities { get; set; }

        [JsonPropertyName("seatCharacteristics")]
        public Dictionary<string, string> SeatCharacteristics { get; set; }
    }

    public class LocationDictionary
    {
        [JsonPropertyName("cityCode")]
        public string CityCode { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
    }
}
