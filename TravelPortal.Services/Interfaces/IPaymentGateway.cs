using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Services.Interfaces
{
    public interface IPaymentGateway
    {
        Task<string> CreateOrderAsync(decimal amount);

        Task<PaymentResultDto> VerifyPaymentAsync(string orderId, string paymentId, string signature);

        Task HandleWebhookAsync(string body, string signature);
    }

}
