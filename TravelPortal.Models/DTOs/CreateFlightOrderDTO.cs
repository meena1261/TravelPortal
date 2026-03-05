using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class CreateFlightOrderDTO
    {
        public CreateFlightOrderDTO()
        {
            Adults = new List<PassangerDetail>();
            Childs = new List<PassangerDetail>();
            Infants = new List<PassangerDetail>();
        }
        public string tripType { get; set; }
        public string cacheKey { get; set; }
        public string offerId { get; set; }
        public string CountryCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string PaymentMode { get; set; }
        public List<PassangerDetail> Adults { get; set; } = new List<PassangerDetail>();
        public List<PassangerDetail> Childs { get; set; } = new List<PassangerDetail>();
        public List<PassangerDetail> Infants { get; set; } = new List<PassangerDetail>();
    }
    public class PassangerDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FrequentFlyerAirline { get; set; } = string.Empty;
        public string FrequentFlyerNo { get; set; }=string.Empty;
        public string DOB { get; set; } = string.Empty;
        public bool? IsRequiredWheelChair { get; set; }
        public bool? IsHasFrequentFlyerNumber { get; set; }
    }

   
}
