using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;

namespace TravelPortal.Supplier.Interfaces
{
    public interface IFlightSupplier
    {
        Task<string> SearchAsync(SearchRequestDto request);

        Task<string> OfferPricing(List<dynamic> offers,int IsQueryParameter);
        Task<string> Seats(List<Pricing_FlightOffer> offers);
        Task<string> BookingCreate(List<Pricing_FlightOffer> offers, CreateFlightOrderDTO travelers);

    }
}
