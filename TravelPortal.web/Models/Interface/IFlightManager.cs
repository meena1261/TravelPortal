using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Interface
{
    public interface IFlightManager
    {
        Task<List<CityAirportViewModel>> GetCityAirportList(string SearchbyCityAirport = "");
        FlightResults FlightSearch(SearchRequestDto model,int Usrno);
        FlightInfoVM FlightDetails(int usrno,string offerId, string cacheKey,string tripType);
        List<OfferData> ApplyFilters(FlightResults data, Dictionary<string, JToken> filters);

        JsonResponse<FlightOrderPNRModel> CreateOrderPNR(int Usrno,CreateOrderPNRModel model);
        JsonResponse ConfirmOrder(int Usrno,FlightOrderPNRModel flightOrder, CreateOrderPNRModel model, decimal walletBalance, decimal CreditLimit);
    }
}