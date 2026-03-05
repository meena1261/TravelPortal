using Newtonsoft.Json;
using TravelPortal.Models.Amadeus;
namespace TravelPortal.Services
{
    public class ParseJson
    {
        public static FlightOffersRoot ParseFlightResults(string json)
        {
            var result = JsonConvert.DeserializeObject<FlightOffersRoot>(json);
            return result;
        }
        public static FlightPricingRoot ParseFlightOfferPricing(string json)
        {
            var result = JsonConvert.DeserializeObject<FlightPricingRoot>(json);
            return result;
        }
    }
}
