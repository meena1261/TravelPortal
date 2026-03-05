using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPortal.Models;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.Enums;

namespace TravelPortal.Services.Implementations
{
    public class MappingResults
    {
        private readonly Repository _repo;
        private readonly string _cacheKey;
        private readonly string _json;

        public MappingResults(Repository repository, string cacheKey, string Json)
        {
            _repo = repository;
            _cacheKey = cacheKey;
            _json = Json;
        }
        public FlightResults FlightResults()
        {
            var objAirports = _repo.Airports();
            var objresult = ParseJson.ParseFlightResults(_json);

            var model = new FlightResults();

            if (objresult == null || objresult.data == null || objresult.data.Count == 0)
                return model;

            List<dynamic> listLayoverAirports = new List<dynamic>();

            model.Supplier = "Amedues";
            model.cacheKey = _cacheKey;
            model.data = new List<OfferData>();

            foreach (var item in objresult.data)
            {
                var objdata = new OfferData();
                objdata.offerId = item.id ?? string.Empty;
                objdata.Itineraries = new List<FlightItinerary>();
                foreach (var itineraries in item.itineraries)
                {
                    var FirstSegment = itineraries?.segments.FirstOrDefault() ?? null;
                    var LastSegment = itineraries?.segments.LastOrDefault() ?? null;

                    // VIA airports

                    List<string> AirlineNameStr = new List<string>();
                    List<string> AirlineLogoStr = new List<string>();
                    List<string> AirlineCodeStr = new List<string>();
                    double Price = Convert.ToDouble(item.travelerPricings[0]?.price?.total);
                    Price += ApplyMarkup(Price, "", 1, 0);
                    for (int i = 0; i < itineraries.segments.Count; i++)
                    {
                        // Airline Name
                        string airlineName = objresult?.dictionaries?.carriers[itineraries?.segments[i]?.carrierCode ?? string.Empty] ?? string.Empty;
                        if (!AirlineNameStr.Contains(airlineName))
                            AirlineNameStr.Add(airlineName);

                        // Airline Logo
                        string airlinelogo = CommonMethods.AirlineLogo(itineraries.segments[i].carrierCode);
                        if (!AirlineLogoStr.Contains(airlinelogo))
                            AirlineLogoStr.Add(airlinelogo);

                        // Airline Code
                        string airlineCode = itineraries.segments[i].carrierCode;
                        string markupAirlineCode = airlineCode;
                        airlineCode = airlineCode + " " + itineraries.segments[i].number;
                        if (!AirlineCodeStr.Contains(airlineCode))
                        {
                            // Markup Apply
                            Price += ApplyMarkup(Price, markupAirlineCode, 0, 1);
                            AirlineCodeStr.Add(airlineCode);
                        }


                    }
                    List<string> viaAirports = new List<string>();
                    for (int i = 0; i < itineraries.segments.Count - 1; i++)
                    {
                        string iatacode = itineraries.segments[i].arrival.iataCode;
                        // Via Airports
                        string airports = objAirports.FirstOrDefault(e => e.IATACode == iatacode).CityName;
                        viaAirports.Add(airports);

                        var layover = new
                        {
                            IataCode = iatacode,
                            Airport = airports
                        };
                        var IsExistLayover = listLayoverAirports.FirstOrDefault(e => e.IataCode == iatacode);
                        if (IsExistLayover == null)
                            listLayoverAirports.Add(layover);
                    }

                    string fromIata = FirstSegment?.departure.iataCode ?? string.Empty;
                    string ToIata = LastSegment?.arrival.iataCode ?? string.Empty;

                    if (!string.IsNullOrEmpty(fromIata))
                    {
                        var obj = objAirports.FirstOrDefault(e => e.IATACode == fromIata);
                        if (obj != null)
                            fromIata = obj.CityName;

                    }

                    if (!string.IsNullOrEmpty(ToIata))
                    {
                        var obj = objAirports.FirstOrDefault(e => e.IATACode == ToIata);
                        if (obj != null)
                            ToIata = obj.CityName;

                    }

                    var ItemItinerary = new FlightItinerary
                    {
                        FromCity = fromIata,
                        ToCity = ToIata,
                        FromIata = FirstSegment?.departure.iataCode ?? string.Empty,
                        ToIata = LastSegment?.arrival.iataCode ?? string.Empty,
                        DepartureDate = Convert.ToDateTime(FirstSegment?.departure.at),
                        DepartureTime = CommonMethods.FormatTime(FirstSegment?.departure.at.ToString() ?? string.Empty),
                        ArrivalDate = Convert.ToDateTime(LastSegment?.arrival.at),
                        ArrivalTime = CommonMethods.FormatTime(LastSegment?.arrival.at.ToString() ?? string.Empty),
                        AirlineName = string.Join(", ", AirlineNameStr),
                        AirlineCode = string.Join(", ", AirlineCodeStr),
                        AirlineLogo = string.Join(", ", AirlineLogoStr),
                        Duration = CommonMethods.FormatDuration(itineraries.duration),
                        FlightNumber = FirstSegment?.number ?? string.Empty + "," + LastSegment?.number ?? string.Empty,
                        Price = Price.ToString(),
                        Stops = viaAirports.Count,
                        Via = viaAirports.Count > 0 ? string.Join(", ", viaAirports) : "Non Stop",
                    };
                    objdata.Itineraries.Add(ItemItinerary);
                }
                model.data.Add(objdata);
            }

            model.filters = GenerateFilters(model.data, objresult.dictionaries);


            return model;

        }

        public FlightDetailModel FlightDetails()
        {
            var objAirports = _repo.Airports();
            var model = new FlightDetailModel();
            var objresult = ParseJson.ParseFlightOfferPricing(_json);

            model.CacheKey = _cacheKey;
            model.flightItineraries = new List<Models.FlightItinerariesModel>();
            model.flightSummary = new FlightSummaryModel();

            int ItineryCount = 0;
            foreach (var item in objresult.data.flightOffers)
            {
                double airlineMarkup = 0;
                foreach (var itineryItem in item.itineraries)
                {
                    var firstSeg = itineryItem?.segments.FirstOrDefault();
                    var lastSeg = itineryItem?.segments.LastOrDefault();

                    string fromCity = firstSeg?.departure.iataCode ?? string.Empty;
                    string toCity = lastSeg?.arrival.iataCode ?? string.Empty;
                    if (!string.IsNullOrEmpty(fromCity))
                        fromCity = objAirports.FirstOrDefault(e => e.IATACode == fromCity).CityName;

                    if (!string.IsNullOrEmpty(toCity))
                        toCity = objAirports.FirstOrDefault(e => e.IATACode == toCity).CityName;

                    var Itinerary = new Models.FlightItinerariesModel
                    {
                        offerId = item.id,
                        FromCity = fromCity,
                        ToCity = toCity,
                        Stops = (itineryItem.segments.Count - 1),
                        Duration = CommonMethods.FormatDuration(firstSeg?.duration ?? string.Empty),
                        TravelDate = firstSeg.departure.at.ToLongDateString(),
                        segments = new List<SegmentDetail>()
                    };
                    foreach (var segItem in itineryItem.segments)
                    {
                        var fairDetail = item?.travelerPricings[ItineryCount]?.fareDetailsBySegment.FirstOrDefault(e => e.segmentId == segItem.id);

                        string fromIataCode = segItem?.departure.iataCode ?? string.Empty;
                        string toIataCode = segItem?.arrival.iataCode ?? string.Empty;
                        var objFromAirport = objAirports.FirstOrDefault(e => e.IATACode == fromIataCode);
                        var objToAirport = objAirports.FirstOrDefault(e => e.IATACode == toIataCode);

                        // safe lookup: get the rule object (not the KeyValuePair)
                        Pricing_FareRule fareRule = objresult?.included?.DetailedFareRules?.Values.FirstOrDefault(fr => fr != null && fr.segmentId == segItem.id);
                        var fareRuleObj = new FareRule();
                        if (fareRule != null)
                        {
                            fareRuleObj = new FareRule
                            {
                                fareBasis = fareRule?.fareBasis ?? string.Empty,
                                name = fareRule?.name ?? string.Empty,
                                fareNotes = (fareRule?.fareNotes?.descriptions != null)
                                    ? new FareNotes
                                    {
                                        descriptions = fareRule.fareNotes.descriptions
                                            .Select(d => new Description
                                            {
                                                descriptionType = d.descriptionType,
                                                text = d.text
                                            }).ToList()
                                    }
                                    : null
                            };
                        }

                        var objBage = objresult?.included?.bags ?? null;

                        var sement = new SegmentDetail
                        {
                            FromCity = objFromAirport != null ? objFromAirport.CityName : string.Empty,
                            ToCity = objToAirport != null ? objToAirport.CityName : string.Empty,
                            FromAirport = objFromAirport != null ? objFromAirport.Airport : string.Empty,
                            ToAirport = objToAirport != null ? objToAirport.Airport : string.Empty,
                            DepatureTime = CommonMethods.FormatTime(segItem?.departure.at.ToString() ?? string.Empty),
                            ArrivalTime = CommonMethods.FormatTime(segItem?.arrival.at.ToString() ?? string.Empty),
                            DepartureTerminal = segItem?.departure.terminal ?? string.Empty,
                            ArrivalTerminal = segItem?.arrival.terminal ?? string.Empty,
                            AirlineName = segItem?.carrierCode?.ToString() ?? string.Empty,
                            AirlineCode = segItem?.carrierCode ?? string.Empty,
                            AirlineLogo = $"https://content.airhex.com/content/logos/airlines_{segItem?.carrierCode}_50_50_s.png",
                            AirCraftCode = segItem?.aircraft?.code ?? string.Empty,
                            FlightNumber = segItem?.number ?? string.Empty,
                            Duration = CommonMethods.FormatDuration(segItem?.duration),
                            CabinClass = fairDetail?.cabin ?? string.Empty,
                            CabinClassType = fairDetail?.brandedFare ?? string.Empty,
                            CheckInBaggage = new IncludedBags
                            {
                                weight = fairDetail?.includedCheckedBags?.weight.ToString() ?? string.Empty,
                                weightUnit = fairDetail?.includedCheckedBags?.weightUnit ?? string.Empty
                            },
                            fareRule = fareRuleObj
                        };
                        Itinerary.segments.Add(sement);
                    }
                    model.flightItineraries.Add(Itinerary);
                }
                var travelerPricing = item.travelerPricings;
                if (travelerPricing != null)
                {
                    var objAdult = travelerPricing.Where(e => e.travelerType == "ADULT").ToList();
                    var objChild = travelerPricing.Where(e => e.travelerType == "CHILD").ToList();
                    var objINFANT = travelerPricing.Where(e => e.travelerType == "HELD_INFANT").ToList();
                    double TaxPrice = 0;
                    double TotalPrice = 0;
                    if (objAdult != null && objAdult.Count > 0)
                    {
                        model.flightSummary.Adult = objAdult.Count();
                        model.flightSummary.AdultBasePrice = Convert.ToDouble(objAdult.FirstOrDefault().price.@base);
                        model.flightSummary.AdultTotalPrice = model.flightSummary.AdultBasePrice * model.flightSummary.Adult;
                        TaxPrice += Convert.ToDouble(objAdult.FirstOrDefault().price.total) - model.flightSummary.AdultBasePrice;
                        TotalPrice += TaxPrice + model.flightSummary.AdultTotalPrice;
                    }
                    if (objChild != null && objChild.Count > 0)
                    {
                        model.flightSummary.Children = objChild.Count();
                        model.flightSummary.ChildrenBasePrice = Convert.ToDouble(objChild.FirstOrDefault().price.@base);
                        model.flightSummary.ChildrenTotalPrice = model.flightSummary.ChildrenBasePrice * model.flightSummary.Children;
                        TaxPrice += Convert.ToDouble(objChild.FirstOrDefault().price.total) - model.flightSummary.ChildrenBasePrice;
                        TotalPrice += TaxPrice + model.flightSummary.ChildrenTotalPrice;
                    }
                    if (objINFANT != null && objINFANT.Count > 0)
                    {
                        model.flightSummary.Infant = objINFANT.Count();
                        model.flightSummary.InfantBasePrice = Convert.ToDouble(objINFANT.FirstOrDefault().price.@base);
                        model.flightSummary.InfantTotalPrice = model.flightSummary.InfantBasePrice * model.flightSummary.Infant;
                        TaxPrice += Convert.ToDouble(objINFANT.FirstOrDefault().price.total) - model.flightSummary.InfantBasePrice;
                        TotalPrice += TaxPrice + model.flightSummary.InfantTotalPrice;
                    }

                    model.flightSummary.UniversalMarkup = ApplyMarkup(TotalPrice, "", 1, 0);

                    if (model?.flightItineraries != null && model.flightItineraries.Any())
                    {
                        foreach (var itinerary in model.flightItineraries)
                        {
                            if (itinerary?.segments == null || !itinerary.segments.Any())
                                continue;

                            foreach (var airlineGroup in itinerary.segments.GroupBy(s => s.AirlineCode))
                            {
                                var airlineName = airlineGroup.First().AirlineCode;

                                model.flightSummary.AirlineMarkup +=
                                    ApplyMarkup(TotalPrice, airlineName, 0, 1);
                            }

                        }
                    }


                    TotalPrice += model.flightSummary.UniversalMarkup + model.flightSummary.AirlineMarkup;
                    model.flightSummary.TaxesServiceCharges = TaxPrice;
                    model.flightSummary.TotalPrice = TotalPrice;
                }
                ItineryCount++;
            }
            return model;
        }


        // MARKUP APPLY
        private double ApplyMarkup(double value, string airlineCode, int IsUniversal, int IsAirline)
        {
            double AddMarkup = 0;
            if (IsUniversal > 0)
            {
                var objUniversal = _repo.MarkupAdd().Where(e => string.IsNullOrEmpty(e.Airline) == true).ToList();
                if (objUniversal.Count > 0)
                {
                    foreach (var item in objUniversal)
                    {
                        if (item.MarkupTypeId == 1) // Percentage
                        {
                            AddMarkup += value * (item.MarkupPrice / 100);
                        }
                        else
                        {
                            AddMarkup += value + item.MarkupPrice;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(airlineCode) && IsAirline > 0)
            {
                var objAirline = _repo.MarkupAdd().Where(e => string.IsNullOrEmpty(e.Airline) == false).ToList().FirstOrDefault(e => e.Airline == airlineCode);
                if (objAirline != null)
                {
                    if (objAirline.MarkupTypeId == 1) // Percentage
                    {
                        AddMarkup += value * (objAirline.MarkupPrice / 100);
                    }
                    else
                    {
                        AddMarkup += value + objAirline.MarkupPrice;
                    }
                }
            }
            return AddMarkup;
        }

        public List<FlightFilters> GenerateFilters(List<OfferData> flights, Offer_Dictionaries dictionaries)
        {
            var filters = new List<FlightFilters>();
            var objItineraries = flights.Where(f => f?.Itineraries != null).SelectMany(f => f.Itineraries).ToList();

            if (!objItineraries.Any())
                return filters;

            decimal ParsePrice(string price)
            {
                return decimal.TryParse(price, out var p) ? p : 0;
            }

            var offerPrices = flights.Select(f => f.Itineraries.Select(i => ParsePrice(i.Price)).Where(p => p > 0).DefaultIfEmpty().Min()).Where(p => p > 0).ToList();

            filters.Add(new FlightFilters
            {
                FilterKey = FilterKeys.price.ToString(),
                Title = "One Way Price",
                Type = InputTypeEnum.Range.ToString(),
                Items = new
                {
                    Min = offerPrices.Min(),
                    Max = offerPrices.Max()
                }
            });


            // Stops Filter
            filters.Add(new FlightFilters
            {
                FilterKey = FilterKeys.stops.ToString(),
                Title = $"Stops From {objItineraries.First().FromCity}",
                Type = InputTypeEnum.CheckBox.ToString(),
                Items = objItineraries.GroupBy(x => x.Stops).OrderBy(g => g.Key)
                .Select(g => new FilterItem
                {
                    Label = g.Key == 0 ? "Non Stop" : $"{g.Key} Stop",
                    Value = g.Key.ToString(),
                    Count = g.Count()
                }).ToList()
            });


            // Departure Time Filter
            filters.Add(new FlightFilters
            {
                FilterKey = FilterKeys.departur.ToString(),
                Title = $"Departure From {objItineraries.FirstOrDefault().FromCity}",
                Type = InputTypeEnum.CheckBox.ToString(),
                Items = GetDepartureSlots(objItineraries)
            });

            // Arrival Time Filter
            filters.Add(new FlightFilters
            {
                FilterKey = FilterKeys.arrivals.ToString(),
                Title = $"Arrival at {objItineraries.FirstOrDefault().ToCity}",
                Type = InputTypeEnum.CheckBox.ToString(),
                Items = GetDepartureSlots(objItineraries)
            });

            // Airline Filter
            filters.Add(new FlightFilters
            {
                FilterKey = FilterKeys.airline.ToString(),
                Title = "Airlines",
                Type = InputTypeEnum.CheckBox.ToString(),
                Items = dictionaries.carriers.Select(g => new FilterItem
                {
                    Label = g.Value,
                    Value = g.Key,
                    Count = objItineraries.Count()
                }).OrderByDescending(x => x.Count).ToList()
            });



            return filters;
        }
        private static List<FilterItem> GetDepartureSlots(List<FlightItinerary> flights)
        {
            return new List<FilterItem>
            {
                new FilterItem {
                    Label = "Morning (6AM-12PM)",
                    Value = "morning",
                    Count = flights.Count(f => f.DepartureDate.Hour >= 6 && f.DepartureDate.Hour < 12)
                },
                new FilterItem {
                    Label = "Afternoon (12PM-6PM)",
                    Value = "afternoon",
                    Count = flights.Count(f => f.DepartureDate.Hour >= 12 && f.DepartureDate.Hour < 18)

                },
                new FilterItem {
                    Label = "Evening (6PM-12AM)",
                    Value = "evening",
                    Count = flights.Count(f => f.DepartureDate.Hour >= 18 && f.DepartureDate.Hour < 24)
                }
            };
        }


    }
}
