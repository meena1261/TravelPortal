using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class FlightOrderPNRModel
    {
        public string flightOrderId { get; set; }
        public string QueuingOfficeId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PaybleAmount { get; set; }
    }
}