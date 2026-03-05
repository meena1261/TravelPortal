using TravelPortal.Models;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;
using TravelPortal.Supplier.Interfaces;

namespace TravelPortal.Supplier.Sabre
{
    public class SabreService : IFlightSupplier
    {
        public async Task<string> SearchAsync(SearchRequestDto request)
        {
            //await Task.Delay(1200); // simulate API call

            return string.Empty;
        }
        public async Task<string> OfferPricing(string offer)
        {
            //await Task.Delay(1200); // simulate API call

            return string.Empty;
        }
        public async Task<FlightResults> BookFlightAsync(BookingRequestDto request)
        {
            // Call Amadeus Flight Booking API here
            return new FlightResults(); // implement later
        }

        private List<FlightResults> ParseFlightResults(string json, string supplier)
        {
            // TODO: parse real JSON → map to FlightResult
            return new List<FlightResults>();
        }

        public Task<string> OfferPricing(List<dynamic> offers, int IsQueryParameter)
        {
            throw new NotImplementedException();
        }

        public Task<string> Seats(List<Pricing_FlightOffer> offers)
        {
            throw new NotImplementedException();
        }

        public Task<string> BookingCreate(List<Pricing_FlightOffer> offers, CreateFlightOrderDTO travelers)
        {
            throw new NotImplementedException();
        }
    }
}
