using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.Enums;
using TravelPortal.Services.Implementations;
using TravelPortal.Services.Interfaces;

namespace TravelPortal.Services.Factory
{
    public class PaymentGatewayFactory
    {
        private readonly IServiceProvider _provider;

        public PaymentGatewayFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IPaymentGateway Get(PaymentGatewayType type)
        {
            return type switch
            {
                PaymentGatewayType.Razorpay =>
                    _provider.GetRequiredService<RazorpayGateway>(),

                PaymentGatewayType.Stripe =>
                    _provider.GetRequiredService<StripeGateway>(),

                _ => throw new NotImplementedException()
            };
        }
    }
}
