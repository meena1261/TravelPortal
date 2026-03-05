using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.Services.FilterRule;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services
{
    public class FlightManager : IFlightManager
    {
        private readonly ApiClient _api;
        private readonly db_silviEntities _db;
        private readonly IWalletServiceManager _walletservices;
        public FlightManager()
        {
            _db = new db_silviEntities();
            _walletservices = new WalletServiceManager();
            _api = new ApiClient(ConfigHelper.ApiUrl);
        }
        public async Task<List<CityAirportViewModel>> GetCityAirportList(string SearchbyCityAirport = "")
        {
            try
            {
                string url = $"Flight/GetCityAirportList?SearchbyCityAirport={HttpUtility.UrlEncode(SearchbyCityAirport)}";
                var apiresponse = await _api.GetAsync<JsonResponse>(url.ToPrefixApiURL());
                if (apiresponse != null && apiresponse.status == 1)
                {
                    var cityAirportList = apiresponse.data.ToObject<List<CityAirportViewModel>>();
                    return cityAirportList;
                }
                return new List<CityAirportViewModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FlightResults FlightSearch(SearchRequestDto model,int Usrno)
        {
            try
            {
                string url = "Flight/Search";
                var payload = new
                {
                    tripType = model.tripType,
                    currencyCode = "INR",
                    destinations = model.Destinations,
                    returnDate = model.ReturnDate,
                    cabinClass = model.CabinClass,
                    adults = model.Adults,
                    children = model.Children,
                    infant = model.Infant,
                    maxResult = 100
                };
                string token = GenerateToken(Usrno);
                _api.SetBearerToken(token);
                var apiresponse = _api.Post<JsonResponse<FlightResults>>(url, payload);
                return apiresponse.data;
            }
            catch (Exception ex)
            {
                return new FlightResults();
            }
        }
        public FlightInfoVM FlightDetails(int Usrno,string offerId, string cacheKey, string tripType)
        {
            string url = "Flight/FlightDetails";
            var payload = new
            {
                cacheKey = cacheKey,
                offerId = offerId,
                tripType = tripType
            };
            string token = GenerateToken(Usrno);
            _api.SetBearerToken(token);
            var apiresponse = _api.Post<JsonResponse<FlightInfoVM>>(url, payload);
            return apiresponse.data;
        }

        public List<OfferData> ApplyFilters(FlightResults flights, Dictionary<string, JToken> filters)
        {
            IQueryable<OfferData> query = flights.data.AsQueryable();

            foreach (var filter in filters)
            {
                if (FilterRegistry.Rules.TryGetValue(filter.Key, out var rule))
                    query = rule.Apply(query, filter.Value);
            }

            return query.ToList();
        }

        public JsonResponse<FlightOrderPNRModel> CreateOrderPNR(int Usrno,CreateOrderPNRModel model)
        {
            string url = "Flight/PNRGenerate";
            string token = GenerateToken(Usrno);
            _api.SetBearerToken(token);
            var apiresponse = _api.Post<JsonResponse<FlightOrderPNRModel>>(url, model);
            return apiresponse;
        }

        public JsonResponse ConfirmOrder(int usrno, FlightOrderPNRModel flightOrder, CreateOrderPNRModel model, decimal walletBalance, decimal CreditLimit)
        {
            JsonResponse response = new JsonResponse();

            decimal totalPrice = flightOrder.TotalPrice;
            decimal walletUsed = 0;
            decimal creditUsed = 0;
            decimal payableAmount = totalPrice;

            // WALLET PAYMENT
            if (model.PaymentMode == PaymentModeEnum.Wallet.ToString())
            {
                walletUsed = Math.Min(walletBalance, totalPrice);
                payableAmount = totalPrice - walletUsed;

                decimal DebitAmount = 0 - walletUsed;

                Random rnd = new Random();
                string transactionId = "FL" + rnd.Next(000000, 999999).ToString();
                var creditResult = _walletservices.CreditDebit(new CreditDebitModel
                {
                    Usrno = SessionHelper.Usrno,
                    Amount = DebitAmount,
                    Factor = "Dr", // Credit
                    Narration = "Flight Booked OrderId : " + flightOrder.flightOrderId,
                    Remark = "Fund deduct due to flight booking and PNR Generated",
                    TransactionId = transactionId
                });
            }

            // CREDIT PAYMENT
            else if (model.PaymentMode == PaymentModeEnum.Credit.ToString())
            {
                creditUsed = Math.Min(CreditLimit, totalPrice);
                payableAmount = totalPrice - creditUsed;
            }

            // ONLINE PAYMENT (no deduction)
            else
            {
                payableAmount = totalPrice;
            }

            // BOOKING HISTORY SAVE
            var dbbookingHistory = new FlightBookingHistory
            {
                Usrno = usrno,
                Sector = "Booking",
                FlightOrderID = flightOrder.flightOrderId,
                QueuingOfficeId = flightOrder.QueuingOfficeId,
                PaymentMode = model.PaymentMode,
                TotalAmount = totalPrice,
                DiscountAmount = walletUsed + creditUsed,
                PaybleAmount = payableAmount,
                PaymentStatus = payableAmount > 0 ? "Pending" : "Success",
                BookingStatus = payableAmount > 0 ? "Pending" : "Success",
                AddDate = DateTime.Now
            };

            _db.FlightBookingHistories.Add(dbbookingHistory);
            _db.SaveChanges();

            // RESPONSE
            response.status = payableAmount > 0 ? 2 : 1;
            response.message = payableAmount > 0 ? "Payment Online Required" : "Booking Successfully";
            response.data = new
            {
                payableAmount = payableAmount,
                walletUsed = walletUsed,
                creditUsed = creditUsed,
                FlightOrderID = flightOrder.flightOrderId
            };

            return response;
        }

        public string GenerateToken(int usrno)
        {
            string cacheKey = $"ApiToken_{usrno}";
            if (AppCache.Exists(cacheKey))
                return AppCache.Get<string>(cacheKey);

            string token = string.Empty;
            var objUser = _db.tblMaster_User.FirstOrDefault(e => e.Usrno == usrno);
            if (objUser != null)
            {
                var playload = new
                {
                    userId = objUser.Email,
                    password = objUser.Password,
                    userType = "B2B"
                };
                string url = "Auth/GetToken";
                var apiresponse = _api.Post<JsonResponse>(url, playload);
                if (apiresponse.status > 0)
                {
                    token = Convert.ToString(apiresponse.data.token);
                    AppCache.Set(cacheKey, token, 40); // 40 minutes
                }
            }
            return token;
        }
    }
}