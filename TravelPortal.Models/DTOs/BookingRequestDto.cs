using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class BookingRequestDto
    {
        public int UserId { get; set; }
        public string Supplier { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public string PNR { get; set; }
        //public List<PassengerDto> Passengers { get; set; }
    }

}
