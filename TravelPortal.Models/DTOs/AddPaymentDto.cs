using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.Enums;

namespace TravelPortal.Models.DTOs
{
    public class AddPaymentDto
    {
        public int Usrno { get; set; }
        public decimal Amount { get; set; }
        public PaymentGatewayType GatewayType { get; set; }
    }
}
