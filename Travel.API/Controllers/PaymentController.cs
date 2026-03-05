using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPortal.Models.DTOs;
using TravelPortal.Models.Enums;
using TravelPortal.Services.Factory;

namespace Travel.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly PaymentGatewayFactory _factory;

        public PaymentController(PaymentGatewayFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> Create(AddPaymentDto dto)
        {
            var gateway = _factory.Get(dto.GatewayType);

            var orderId = await gateway.CreateOrderAsync(dto.Amount);

            return Ok(new { orderId });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(VerifyPaymentDto dto)
        {
            var gateway = _factory.Get(dto.GatewayType);

            var response = await gateway.VerifyPaymentAsync(dto.OrderId,dto.PaymentId,dto.Signature);
           
            return Ok(response);
        }

        [HttpPost("webhook/{gatewayType}")]
        public async Task<IActionResult> Webhook(
            PaymentGatewayType gatewayType)
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            var signature = Request.Headers["X-Razorpay-Signature"];

            var gateway = _factory.Get(gatewayType);

            await gateway.HandleWebhookAsync(body, signature);

            return Ok();
        }
    }
}
