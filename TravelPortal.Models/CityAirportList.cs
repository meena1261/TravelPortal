using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models
{
    public class CityAirportList
    {
        public int AirportId { get; set; }
        public string CityName { get; set; }
        public string Airport { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
    }

}
