using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class SearchAirport
    {
        public string Keyword { get; set; }
    }
    public class SearchRequestDto
    {
        public SearchRequestDto() 
        {
            Destinations = new List<OriginDestination>();
        }
        [Required(ErrorMessage = "Required")]
        public string tripType { get; set; }
        [Required(ErrorMessage = "Required")]
        public string currencyCode { get; set; }
        [Required(ErrorMessage = "Required")]
        public List<OriginDestination> Destinations { get; set; }
        public string ReturnDate { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string CabinClass { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infant { get; set; }
        public int MaxResult { get; set; }
    }
    public class OriginDestination
    {
        [Required(ErrorMessage = "Required")]
        public string FromCity { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string ToCity { get; set; } = string.Empty;
        [Required(ErrorMessage = "Required")]
        public string DepatureDate { get; set; }
    }
    public class FlightDetailsRequestDto
    {
        public string cacheKey { get; set; }
        public string offerId { get; set; }
        public string tripType { get; set; }
    }

}
