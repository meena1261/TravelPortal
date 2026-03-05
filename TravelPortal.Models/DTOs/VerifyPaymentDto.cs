using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.Enums;

namespace TravelPortal.Models.DTOs
{
    public class VerifyPaymentDto
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Signature { get; set; }
        public PaymentGatewayType GatewayType { get; set; }
    }
    public class PaymentResultDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic data { get; set; }
    }

}
