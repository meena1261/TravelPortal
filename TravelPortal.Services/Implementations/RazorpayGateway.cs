using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;
using TravelPortal.Models.Enums;
using TravelPortal.Services.Interfaces;

namespace TravelPortal.Services.Implementations
{
    public class RazorpayGateway : IPaymentGateway
    {
        private readonly IConfiguration _config;

        private readonly string _key;
        private readonly string _secret;
        private readonly string _webhookSecret;

        public RazorpayGateway(IConfiguration config)
        {
            _config = config;
            _key = _config["Razorpay:KeyId"];
            _secret = _config["Razorpay:KeySecret"];
            _webhookSecret = _config["Razorpay:WebhookSecret"];
        }

        public async Task<string> CreateOrderAsync(decimal amount)
        {
            var client = new RazorpayClient(_key, _secret);

            var options = new Dictionary<string, object>
            {
                { "amount", amount * 100 },
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString() }
            };

            var order = client.Order.Create(options);

            //_context.Payments.Add(new Payment
            //{
            //    OrderId = order["id"].ToString(),
            //    Amount = amount,
            //    GatewayType = PaymentGatewayType.Razorpay,
            //    Status = PaymentStatus.Created
            //});

            //await _context.SaveChangesAsync();

            return order["id"].ToString();
        }

        public async Task<PaymentResultDto> VerifyPaymentAsync(string orderId, string paymentId, string signature)
        {
            PaymentResultDto response = new PaymentResultDto();
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);

                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("razorpay_payment_id", paymentId);
                attributes.Add("razorpay_order_id", orderId);
                attributes.Add("razorpay_signature", signature);

                Utils.verifyPaymentSignature(attributes);
                Payment payment = client.Payment.Fetch(paymentId);
                var amount = payment["amount"];
               
                response.Status = 1;
                response.Message = "Payment verified successfully.";
                response.data = new
                {
                    amount = Convert.ToDecimal(amount) / 100
                };
            }
            catch(Exception ex)
            {
                response.Status = 0;
                response.Message = "Payment verification failed.";
            }

            //var payment = await _context.Payments
            //    .FirstAsync(x => x.OrderId == dto.OrderId);

            //payment.PaymentId = dto.PaymentId;
            //payment.Status = PaymentStatus.Paid;

            //await _context.SaveChangesAsync();

            return response;
        }

        public async Task HandleWebhookAsync(string body, string signature)
        {
            Utils.verifyWebhookSignature(body, signature, _webhookSecret);

            var data = JsonDocument.Parse(body);

            var orderId = data.RootElement
                .GetProperty("payload")
                .GetProperty("payment")
                .GetProperty("entity")
                .GetProperty("order_id")
                .GetString();

            //var payment = await _context.Payments
            //    .FirstOrDefaultAsync(x => x.OrderId == orderId);

            //if (payment != null)
            //{
            //    payment.Status = PaymentStatus.Paid;
            //    await _context.SaveChangesAsync();
            //}
        }
    }

}
