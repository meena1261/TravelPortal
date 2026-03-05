using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.Ado
{
    public class AddEditFlightBookingHistory
    {
        public int Usrno { get; set; }
        public string UserType { get; set; }
        public string TripType { get; set; }
        public string Sector { get; set; }
        public string DepartureDates { get; set; }
        public string ArrivalDates { get; set; }
        public int Adults { get; set; }
        public int Childs { get; set; }
        public int Infants { get; set; }
        public string CabinClass { get; set; }
        public string FlightOrderID { get; set; }
        public string QueuingOfficeId { get; set; }
        public string PaymentMode { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal UseWalletAmount { get; set; }
        public decimal UserCreditLimit { get; set; }
        public decimal PaybleAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentOrderID { get; set; }
        public string BookingStatus { get; set; }
    }
}
