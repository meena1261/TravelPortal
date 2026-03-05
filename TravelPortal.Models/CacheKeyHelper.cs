using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Models
{
    public class CacheKeyHelper
    {
        public static string FlightSearchKey(SearchRequestDto r)
        {
            if (r == null)
                return string.Empty;

            var destPart = string.Join("|",
                r.Destinations.Select(d =>
                    $"{d.FromCity}-{d.ToCity}-{d.DepatureDate}")
            );

            var rawKey =
                $"FLIGHT:{r.tripType}:{r.currencyCode}:{r.CabinClass}:" +
                $"{r.Adults}:{r.Children}:{r.Infant}:{destPart}";

            return rawKey;
        }
        public static string FlightPricingKey(FlightDetailsRequestDto r)
        {
            if (r == null)
                return string.Empty;

            var rawKey = $"{r.cacheKey}:{r.offerId}";

            return rawKey;
        }
    }
}
