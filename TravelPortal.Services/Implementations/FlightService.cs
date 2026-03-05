using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Razorpay.Api;
using System.Reflection;
using TravelPortal.Models;
using TravelPortal.Models.Ado;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;
using TravelPortal.Models.Json;
using TravelPortal.Services.Interfaces;
using TravelPortal.Supplier.Amadeus;
using TravelPortal.Supplier.Interfaces;
using TravelPortal.Supplier.Sabre;

namespace TravelPortal.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly IConfiguration _config;
        private readonly IServiceProvider _provider;
        private readonly int CacheExpiry = 0;
        private readonly ICacheService _cache;
        private readonly Repository _repo;
        public FlightService(
            IServiceProvider provider,
            IConfiguration config,
            Repository repository)
        {
            _provider = provider;
            _config = config;
            _cache = provider.GetRequiredService<ICacheService>();
            CacheExpiry = Convert.ToInt32(_config["CacheExpiryLimit"] ?? "5");
            _repo = repository;
        }
        public enum SupplierTypes
        {
            Amadeus
        }

        #region Flight Search
        public async Task<JsonResponse> SearchFlightsAsync(SearchRequestDto request)
        {
            JsonResponse response = new JsonResponse();
            try
            {

                string KeyName = CacheKeyHelper.FlightSearchKey(request);
                var cacheKey = _cache.GenerateKey(KeyName);

                string cachedData = _cache.Get<string>(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                    goto wind;

                var service = ResolveSupplierService(SupplierTypes.Amadeus.ToString());
                cachedData = await service.SearchAsync(request);
                _cache.Set(cacheKey, cachedData, CacheExpiry);

            wind:
                var result = new MappingResults(_repo, cacheKey, cachedData).FlightResults();
                result.Search = request;

                response.Status = 1;
                response.Message = "Success";
                response.data = result;
                return response;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "Data not found";
            }
            return response;
        }
        #endregion

        #region Flight Offer Pricing
        public async Task<JsonResponse> FlightDetails(FlightDetailsRequestDto request)
        {
            JsonResponse response = new JsonResponse();
            try
            {

                var model = new FlightDetailModel();
                string KeyName = CacheKeyHelper.FlightPricingKey(request);
                var cacheKey = _cache.GenerateKey(KeyName);
                string cachedData = _cache.Get<string>(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                    model = new MappingResults(_repo, cacheKey, cachedData).FlightDetails();

                // Search offers Cache data
                var SearchCacheData = _cache.Get<string>(request.cacheKey);
                if (!string.IsNullOrEmpty(SearchCacheData))
                {
                    // Search Result Cache data
                    var objCacheData = ParseJson.ParseFlightResults(SearchCacheData);
                    var SelectedOffer = objCacheData.data.FirstOrDefault(e => e.id == request.offerId);

                    var objflightOffers = new List<dynamic>();
                    objflightOffers.Add(SelectedOffer);

                    // API Call
                    var service = ResolveSupplierService(SupplierTypes.Amadeus.ToString());
                    var apiresponsestr = await service.OfferPricing(objflightOffers, 1);

                    _cache.Set(cacheKey, apiresponsestr, CacheExpiry);
                    model = new MappingResults(_repo, cacheKey, apiresponsestr).FlightDetails();
                    model.tripType = request.tripType;
                }
                response.Status = 1;
                response.Message = "Success";
                response.data = model;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "Data not found !";
            }
            return response;
        }
        #endregion

        #region Flight Seats
        public async Task<JsonResponse> FlightSeats(FlightDetailsRequestDto request)
        {
            JsonResponse response = new JsonResponse();
            try
            {

                var model = new SeatmapsResponse();
                var PricingCacheData = _cache.Get<string>(request.cacheKey);
                if (!string.IsNullOrEmpty(PricingCacheData))
                {
                    var objresult = ParseJson.ParseFlightOfferPricing(PricingCacheData);
                    // API Call
                    var service = ResolveSupplierService(SupplierTypes.Amadeus.ToString());

                    var offerPricing = objresult.data.flightOffers;
                    var apiresponse = await service.Seats(offerPricing);

                    model = JsonConvert.DeserializeObject<SeatmapsResponse>(apiresponse);
                }
                response.Status = 1;
                response.Message = "Success";
                response.data = model;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "Data not found !";
            }
            return response;
        }
        #endregion

        #region Flight Order
        public async Task<JsonResponse> BookingCreate(CreateFlightOrderDTO request, int UserId, string Role)
        {
            JsonResponse response = new JsonResponse();
            try
            {
                var model = new FlightOrderResponse();
                var PricingCacheData = _cache.Get<string>(request.cacheKey);
                if (!string.IsNullOrEmpty(PricingCacheData))
                {
                    var objresult = ParseJson.ParseFlightOfferPricing(PricingCacheData);

                    // API Call
                    var service = ResolveSupplierService(SupplierTypes.Amadeus.ToString());

                    var offerPricing = objresult.data.flightOffers;
                    var apiresponse = await service.BookingCreate(offerPricing, request);

                    model = JsonConvert.DeserializeObject<FlightOrderResponse>(apiresponse);
                    AddEditFlightBookingHistory bookingHistory = new AddEditFlightBookingHistory();
                    if (model != null && model.data != null)
                    {
                        bookingHistory.Usrno = UserId;
                        bookingHistory.UserType = Role;
                        bookingHistory.TripType = request.tripType;
                        bookingHistory.FlightOrderID = model.data.id;
                        bookingHistory.QueuingOfficeId = model.data.queuingOfficeId;
                        bookingHistory.PaymentMode = request.PaymentMode;

                        decimal totalPrice = 0;
                        int iti = 0;
                        foreach (var item in model.data.flightOffers)
                        {
                            totalPrice += Convert.ToDecimal(item.price.total);

                            // With this corrected line:
                            var segfirst = item.itineraries[iti].segments.FirstOrDefault();
                            var seglast = item.itineraries[iti].segments.LastOrDefault();

                            bookingHistory.Sector += $"{segfirst?.departure.iataCode}-{seglast?.arrival.iataCode}";
                            bookingHistory.DepartureDates += $"{segfirst?.departure.at},{seglast?.departure.at}";
                            bookingHistory.ArrivalDates += $"{segfirst?.arrival.at},{seglast?.arrival.at}";

                            bookingHistory.Adults = item.travelerPricings.Where(e => e.travelerType == "ADULT").Count();
                            bookingHistory.Childs = item.travelerPricings.Where(e => e.travelerType == "CHILD").Count();
                            bookingHistory.Infants = item.travelerPricings.Where(e => e.travelerType == "HELD_INFANT").Count();

                            iti++;
                        }

                        bookingHistory.TotalAmount = totalPrice;
                        response = _repo.AddEditFlightBookingHistory(bookingHistory);
                    }
                    else
                    {
                        response.Status = 0;
                        response.Message = "Booking Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        // DYNAMIC SERVICE RESOLVE
        private IFlightSupplier ResolveSupplierService(string name)
        {
            return name switch
            {
                "Amadeus" => _provider.GetRequiredService<AmadeusService>(),
                "Sabre" => _provider.GetRequiredService<SabreService>(),
                _ => throw new Exception("Supplier not supported")
            };
        }

        public JsonResponse GetAirportsAsync(string keyword)
        {
            var response = new JsonResponse();
            var objAirport = _repo.Airports().Where(e=>e.IATACode.Contains(keyword) || e.Airport.Contains(keyword)).ToList();
            if (objAirport != null && objAirport.Count > 0)
            {
                response.Status = 1;
                response.Message = "Success";
                response.data = objAirport;
            }
            else
            {
                response.Status = 0;
                response.Message = "Data not found !";
            }
            return response;
            //throw new NotImplementedException();
        }
    }
}
