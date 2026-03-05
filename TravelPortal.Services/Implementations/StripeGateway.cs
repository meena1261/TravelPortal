using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;
using TravelPortal.Services.Interfaces;

namespace TravelPortal.Services.Implementations
{
    public class StripeGateway : IPaymentGateway
    {
        public Task<string> CreateOrderAsync(decimal amount)
        {
            // Stripe payment intent logic here
            return Task.FromResult("stripe_order_id");
        }

        public Task<PaymentResultDto> VerifyPaymentAsync(string orderId, string paymentId, string signature)
        {
            return Task.FromResult(new PaymentResultDto());
        }

        public Task HandleWebhookAsync(string body, string signature)
        {
            return Task.CompletedTask;
        }
    }

}
