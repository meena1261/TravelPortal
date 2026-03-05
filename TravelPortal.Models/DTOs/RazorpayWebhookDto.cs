using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class RazorpayWebhookDto
    {
        public string @event { get; set; }

        public RazorpayPayload payload { get; set; }
    }

    public class RazorpayPayload
    {
        public RazorpayPayment payment { get; set; }
    }

    public class RazorpayPayment
    {
        public RazorpayEntity entity { get; set; }
    }

    public class RazorpayEntity
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public int amount { get; set; }
        public string status { get; set; }
    }

}
