using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{

    public class SearchRequestDto
    {
        public SearchRequestDto() 
        {
            Destinations = new List<OriginDestination>();
        }
        public string tripType { get; set; }
        public string currencyCode { get; set; }
        public List<OriginDestination> Destinations { get; set; }
        public string ReturnDate { get; set; } = string.Empty;
        public string CabinClass { get; set; }
        public int Adults { get; set; } = 1;
        public int Children { get; set; } = 0;
        public int Infant { get; set; } = 0;
    }
    public class OriginDestination
    {
        public string FromCity { get; set; } = string.Empty;
        public string FromCityIata { get; set; } = string.Empty;
        public string FromCityAirport { get; set; } = string.Empty;
        public string ToCity { get; set; } = string.Empty;
        public string ToCityIata { get; set; } = string.Empty;
        public string ToCityAirport { get; set; } = string.Empty;
        public string DepatureDate { get; set; }
    }
    public class FilterRequest
    {
        public Dictionary<string, JToken> filters { get; set; }
    }

}