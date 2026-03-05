using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class CityAirportViewModel
    {
        public int CityAirportId { get; set; }
        public string CityAirport { get; set; }
        public string IATACode { get; set; }
        public string Address { get; set; }
    }
}