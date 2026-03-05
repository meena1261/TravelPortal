using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models;
using TravelPortal.web.Models.Common;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.Services;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Controllers
{
    public class FlightController : BaseController
    {
        // GET: Flight
        private readonly IFlightManager _flightManager;
        private readonly IWalletServiceManager _walletServiceManager;
        private readonly Repository _repo;
        private readonly db_silviEntities _db;
        public FlightController()
        {
            _flightManager = new FlightManager();
            _walletServiceManager = new WalletServiceManager();
            _repo = new Repository();
            _db = new db_silviEntities();
        }
        public ActionResult FlightSearch()
        {
            var model = new SearchRequestDto();
            var objTripTypes = DropdownLists.FlightTripType();
            var objClassTypes = DropdownLists.FlightClassTypes();

            var Cities = PreloadApplicationData.Cities;
            var firstCities = Cities.FirstOrDefault();
            var lastCities = Cities.OrderByDescending(e => e.CityAirportId).FirstOrDefault();
            model.Destinations = new List<OriginDestination>
            {
                new OriginDestination
                {
                    FromCity = firstCities.CityAirport,
                    FromCityIata = firstCities.IATACode,
                    FromCityAirport = firstCities.Address,
                    ToCity = lastCities.CityAirport,
                    ToCityIata = lastCities.IATACode,
                    ToCityAirport = lastCities.Address,
                    DepatureDate = DateTime.Now.ToString("dd-MM-yyyy")
                }
            };
            model.tripType = objTripTypes.FirstOrDefault().Value;
            model.CabinClass = objClassTypes.FirstOrDefault().Value;

            ViewBag.tripTypes = objTripTypes;
            ViewBag.ClassTypes = objClassTypes;
            return PartialView(model);
        }
        public JsonResult GetCityAirport(string term)
        {
            var cities = PreloadApplicationData.Cities;
            var result = cities.Where(x => x.IATACode.ToLower().Contains(term.ToLower()) || x.CityAirport.ToLower().Contains(term.ToLower())).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route(RouteConfig.FlightSearch)]
        public ActionResult SearchIndex(string tripType = "", string itinerary = "", string paxType = "", string cabinClass = "")
        {
            if (!SessionHelper.Islogin)
                return RedirectToAction("Logout", "Account");
            var Airports = PreloadApplicationData.Cities;

            var model = new SearchRequestDto();
            var returnmodel = new SearchRequestDto();
            int cnt = 0;
            if (!string.IsNullOrEmpty(itinerary))
            {
                string[] segments = itinerary.Split('~');

                foreach (var segment in segments)
                {
                    string[] item = segment.Split('_');
                    if (item.Length < 3)
                    {
                        // log and skip or return error
                        continue;
                    }

                    var origin = new OriginDestination
                    {
                        FromCity = item[0],
                        ToCity = item[1]
                    };

                    var dateStr = item[2];
                    DateTime dt;
                    var formats = new[] { "dd-MM-yyyy", "yyyy-MM-dd", "d-M-yyyy", "dd/MM/yyyy" };

                    if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    {
                        origin.DepatureDate = dt.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        // handle invalid date: default, skip, or return a validation error
                        // e.g. origin.DepatureDate = DateTime.Now.ToString("yyyy-MM-dd");
                        continue;
                    }
                    if (cnt == 0 || tripType.Equals(FlightTripTypes.MultiCity.ToString()))
                        model.Destinations.Add(origin);
                    else
                    {
                        var returndateStr = item[2];
                        DateTime dtReturn;
                        if (DateTime.TryParseExact(returndateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtReturn))
                        {
                            model.ReturnDate = dtReturn.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            continue;
                        }
                        returnmodel.ReturnDate = returndateStr;
                    }

                    string fromCity = string.Empty;
                    string fromCityIata = string.Empty;
                    string fromCityAirport = string.Empty;

                    var fromAirport = Airports.FirstOrDefault(e => e.IATACode == item[0]);
                    if (fromAirport != null)
                    {
                        fromCity = fromAirport.CityAirport;
                        fromCityIata = fromAirport.IATACode;
                        fromCityAirport = fromAirport.Address;
                    }
                    else
                    {
                        fromCity = item[0];
                        fromCityIata = item[0];
                        fromCityAirport = item[0];
                    }

                    string toCity = string.Empty;
                    string toCityIata = string.Empty;
                    string toCityAirport = string.Empty;

                    var toAirport = Airports.FirstOrDefault(e => e.IATACode == item[1]);
                    if (toAirport != null)
                    {
                        toCity = toAirport.CityAirport;
                        toCityIata = toAirport.IATACode;
                        toCityAirport = toAirport.Address;
                    }
                    else
                    {
                        toCity = item[1];
                        toCityIata = item[1];
                        toCityAirport = item[1];
                    }

                    var originNew = new OriginDestination
                    {
                        FromCity = fromCity,
                        FromCityIata = fromCityIata,
                        FromCityAirport = fromCityAirport,
                        ToCity = toCity,
                        ToCityIata = toCityIata,
                        ToCityAirport = toCityAirport,
                        DepatureDate = item[2]
                    };
                    returnmodel.Destinations.Add(originNew);

                    cnt++;
                }
            }

            if (!string.IsNullOrEmpty(paxType))
            {
                string[] traveller = paxType.Split('_');

                model.Adults = Convert.ToInt32(traveller[0].Replace("A-", ""));
                model.Children = Convert.ToInt32(traveller[1].Replace("C-", ""));
                model.Infant = Convert.ToInt32(traveller[2].Replace("I-", ""));

                returnmodel.Adults = Convert.ToInt32(traveller[0].Replace("A-", ""));
                returnmodel.Children = Convert.ToInt32(traveller[1].Replace("C-", ""));
                returnmodel.Infant = Convert.ToInt32(traveller[2].Replace("I-", ""));
            }

            model.tripType = tripType;
            model.CabinClass = cabinClass;

            returnmodel.tripType = tripType;
            returnmodel.CabinClass = cabinClass;


            var flights = _flightManager.FlightSearch(model,SessionHelper.Usrno);
            SessionHelper.Flights = flights;
            ViewBag.flights = flights;
            var objTripTypes = DropdownLists.FlightTripType();
            var objClassTypes = DropdownLists.FlightClassTypes();

            ViewBag.tripTypes = objTripTypes;
            ViewBag.ClassTypes = objClassTypes;
            return View(returnmodel);
        }
        [HttpPost]
        public ActionResult ApplyFilters()
        {
            Request.InputStream.Position = 0;
            var json = new StreamReader(Request.InputStream).ReadToEnd();

            var request = Newtonsoft.Json.JsonConvert
                .DeserializeObject<FilterRequest>(json);


            // always original
            var flights = SessionHelper.Flights;

            // copy for UI
            var result = JsonConvert.DeserializeObject<FlightResults>(JsonConvert.SerializeObject(flights));

            var filterResult = _flightManager.ApplyFilters(flights, request.filters);

            result.data = filterResult;
            ViewBag.flights = result;
            return PartialView("FlightSearchList");
        }

        [Route(RouteConfig.FlightDetails)]
        public ActionResult FlightDetails(string offerId = "", string crId = "", string tripType = "")
        {
            if (!SessionHelper.Islogin)
                return RedirectToAction("Logout", "Account");

            // Flight Info
            var flightinfo = _flightManager.FlightDetails(SessionHelper.Usrno,offerId, crId, tripType);
            ViewBag.flightinfo = flightinfo;
            // Country List
            ViewBag.Country = _repo.Country();
            // Wallet Balance
            ViewBag.walletBalance = _walletServiceManager.GetWalletBalance(SessionHelper.Usrno);
            // Credit Limit
            ViewBag.CreditLimit = _walletServiceManager.GetCreditLimit(SessionHelper.Usrno);

            CreateOrderPNRModel model = new CreateOrderPNRModel();
            model.Adults.Add(new PassangerDetail());
            model.Childs.Add(new PassangerDetail());
            model.Infants.Add(new PassangerDetail());
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateOrderPNR(CreateOrderPNRModel model, string crId)
        {
            model.cacheKey = crId;
            var result = _flightManager.CreateOrderPNR(SessionHelper.Usrno,model);
            return Json(result);
        }
        public ActionResult BookingConfirm(string orderId)
        {
            return View();
        }

        #region Payment Getway
        [HttpPost]
        public async Task<ActionResult> CreateOrder(decimal amount, string flightOrderId)
        {
            string orderId = await _walletServiceManager.PaymentGetwayCreateOrder(SessionHelper.Usrno, amount);
            return Json(new { orderId = orderId }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> VerifyPayment(string paymentId, string orderId, string signature, string flightOrderId)
        {
            JsonResponse result = new JsonResponse();

            dynamic orderDetails = await _walletServiceManager.PaymentGetwayVerifyPaymentAsync(SessionHelper.Usrno, paymentId, orderId, signature);

            if (orderDetails.status == 1)
            {
                var obj = _db.FlightBookingHistories.FirstOrDefault(e => e.FlightOrderID == flightOrderId);
                if (obj != null)
                {
                    obj.PaymentStatus = "Success";
                    obj.BookingStatus = "Success";
                    obj.PaymentOrderID = paymentId;
                    _db.SaveChanges();
                }
                result.status = 1;
                result.message = "Successfully paid";
            }
            else
            {
                result.status = 0;
                result.message = "Payment verification failed.";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}