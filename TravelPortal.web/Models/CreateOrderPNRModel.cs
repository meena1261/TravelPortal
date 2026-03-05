using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class CreateOrderPNRModel
    {
        public CreateOrderPNRModel() 
        {
            Adults = new List<PassangerDetail>();
            Childs = new List<PassangerDetail>();
            Infants = new List<PassangerDetail>();
        }
        public List<PassangerDetail> Adults {  get; set; } = new List<PassangerDetail>();
        public List<PassangerDetail> Childs {  get; set; } = new List<PassangerDetail>();
        public List<PassangerDetail> Infants {  get; set; } = new List<PassangerDetail>();
        public string tripType { get; set; }
        public string cacheKey { get; set; }
        public string offerId { get; set; }
        public string CountryCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string PaymentMode { get; set; }
    }
    public class PassangerDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string CountryCode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FrequentFlyerAirline { get; set; }
        public string FrequentFlyerNo { get; set; }
        public string DOB { get; set; }
        public bool IsRequiredWheelChair { get; set; }
        public bool IsHasFrequentFlyerNumber { get; set; }
    }
}