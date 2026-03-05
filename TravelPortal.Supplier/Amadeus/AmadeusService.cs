using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using TravelPortal.Models.Amadeus;
using TravelPortal.Models.DTOs;
using TravelPortal.Supplier.Interfaces;

namespace TravelPortal.Supplier.Amadeus
{

    public class AmadeusService : IFlightSupplier
    {
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;
        private readonly string EndpointUrl = "https://test.api.amadeus.com";
        private readonly string ApiKey = "Ld8PD5Ms80pvXna3oj9qwx1PBOG16NDc";
        private readonly string ApiSecret = "Nn0rJByfkpBv4I0A";
        public AmadeusService(IHttpClientFactory factory, IMemoryCache cache)
        {
            _client = factory.CreateClient();
            _cache = cache;
        }

        // =====================================
        // TOKEN
        // =====================================
        private async Task<string> GetAccessTokenAsync()
        {
            _cache.TryGetValue("AmeduesToken", out string value);
            if (!string.IsNullOrEmpty(value))
                return value;


            var response = await _client.PostAsync(
                $"{EndpointUrl}/v1/security/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                { "grant_type", "client_credentials" },
                { "client_id", ApiKey },
                { "client_secret", ApiSecret }
                }));

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(json);
            string token = doc.RootElement.GetProperty("access_token").GetString();
            _cache.Set("AmeduesToken", token, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            });
            return token;
        }


        // =====================================
        // SEARCH
        // =====================================
        public async Task<string> SearchAsync(SearchRequestDto model)
        {
            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var url = $"{EndpointUrl}/v2/shopping/flight-offers";

            // ----------------------------
            // 1️⃣ Origin Destinations
            // ----------------------------
            var objDestination = model.Destinations
                .Select((item, index) => new
                {
                    id = (index + 1).ToString(),
                    originLocationCode = item.FromCity,
                    destinationLocationCode = item.ToCity,
                    departureDateTimeRange = new
                    {
                        date = item.DepatureDate,
                        time = "12:00:00"
                    }
                }).ToList();

            // RoundTrip Handling
            if (model.tripType.Equals("RoundTrip", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrEmpty(model.ReturnDate))
            {
                objDestination.Add(new
                {
                    id = (objDestination.Count + 1).ToString(),
                    originLocationCode = model.Destinations[0].ToCity,
                    destinationLocationCode = model.Destinations[0].FromCity,
                    departureDateTimeRange = new
                    {
                        date = model.ReturnDate,
                        time = "12:00:00"
                    }
                });
            }

            // ----------------------------
            // 2️⃣ Travelers
            // ----------------------------
            var objTravelers = new List<object>();
            int travelerId = 1;

            // Adults
            var adultIds = new List<string>();
            for (int i = 0; i < model.Adults; i++)
            {
                var id = travelerId.ToString();
                adultIds.Add(id);

                objTravelers.Add(new
                {
                    id = id,
                    travelerType = "ADULT",
                    fareOptions = new[] { "STANDARD" }
                });

                travelerId++;
            }

            // Children
            for (int i = 0; i < model.Children; i++)
            {
                objTravelers.Add(new
                {
                    id = travelerId.ToString(),
                    travelerType = "CHILD",
                    fareOptions = new[] { "STANDARD" }
                });

                travelerId++;
            }

            // Infants (Mapped to Adults Properly)
            for (int i = 0; i < model.Infant; i++)
            {
                if (adultIds.Count == 0)
                    throw new Exception("Infant cannot travel without Adult.");

                objTravelers.Add(new
                {
                    id = travelerId.ToString(),
                    travelerType = "HELD_INFANT",
                    associatedAdultId = adultIds[i % adultIds.Count], // Proper Mapping
                    fareOptions = new[] { "STANDARD" }
                });

                travelerId++;
            }

            // ----------------------------
            // 3️⃣ Cabin Restrictions (Only if Provided)
            // ----------------------------
            object flightFilters = null;

            if (!string.IsNullOrEmpty(model.CabinClass))
            {
                flightFilters = new
                {
                    cabinRestrictions = new[]
                    {
                new
                {
                    cabin = model.CabinClass,
                    coverage = "MOST_SEGMENTS",
                    originDestinationIds = objDestination.Select(d => d.id).ToList()
                }
            }
                };
            }

            // ----------------------------
            // 4️⃣ Final Payload
            // ----------------------------
            var payload = new
            {
                currencyCode = string.IsNullOrEmpty(model.currencyCode) ? "INR" : model.currencyCode,
                originDestinations = objDestination,
                travelers = objTravelers,
                sources = new[] { "GDS" },
                searchCriteria = new
                {
                    maxFlightOffers = model.MaxResult > 0 ? model.MaxResult : 50,
                    pricingOptions = new
                    {
                        fareType = new[] { "PUBLISHED" }
                    },
                    flightFilters = flightFilters
                }
            };

            var jsonPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception(json);

            return json;
        }


        // =====================================
        // Offer Pricing
        // =====================================
        public async Task<string> OfferPricing(List<dynamic> offer, int IsQueryParameter)
        {

            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var url =
                $"{EndpointUrl}/v1/shopping/flight-offers/pricing";
            if (IsQueryParameter > 0)
                url += "?include=detailed-fare-rules,bags";

            var payload = new
            {
                data = new
                {
                    type = "flight-offers-pricing",
                    flightOffers = offer
                }
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json;

        }

        // =====================================
        // Seats
        // =====================================
        public async Task<string> Seats(List<Pricing_FlightOffer> offers)
        {

            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var url =
                $"{EndpointUrl}/v1/shopping/seatmaps";

            var payload = new
            {
                data = offers
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json;

        }


        // =====================================
        // Flight Order
        // =====================================
        public async Task<string> BookingCreate(List<Pricing_FlightOffer> offers, CreateFlightOrderDTO travelers)
        {

            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var url =
                $"{EndpointUrl}/v1/booking/flight-orders";

            var passangers = new List<PassangerDetail>();
            if (travelers.Adults.Count > 0)
            {
                foreach (var adult in travelers.Adults)
                {
                    passangers.Add(adult);
                }
            }
            if (travelers.Childs.Count > 0)
            {
                foreach (var child in travelers.Childs)
                {
                    passangers.Add(child);
                }
            }
            if (travelers.Infants.Count > 0)
            {
                foreach (var infant in travelers.Infants)
                {
                    passangers.Add(infant);
                }
            }

            var objTravelers = new List<dynamic>();
            int travelerId = 1;
            if (passangers.Count > 0)
            {
                foreach (var item in passangers)
                {
                    var traveler = new
                    {
                        id = travelerId,
                        dateOfBirth = Convert.ToDateTime(item.DOB).ToString("yyyy-MM-dd"),
                        name = new
                        {
                            firstName = item.FirstName,
                            lastName = item.LastName
                        },
                        gender = item.Gender,
                        contact = new
                        {
                            emailAddress = item.Email,
                            phones = new[]
                            {
                                new
                                {
                                    deviceType = "MOBILE",
                                    countryCallingCode = item.CountryCode,
                                    number = item.Mobile
                                }
                            }
                        },
                        //documents = new[]
                        //{
                        //    new
                        //    {
                        //        documentType="PASSPORT",
                        //        birthPlace = "Madrid",
                        //        issuanceLocation="Madrid",
                        //        issuanceDate ="2015-04-14",
                        //        number="00000000",
                        //        expiryDate="2025-04-14",
                        //        issuanceCountry="IN",
                        //        validityCountry="IN",
                        //        nationality="IN",
                        //        holder=true
                        //    }
                        //}

                    };
                    objTravelers.Add(traveler);
                    travelerId++;
                }
            }


            var payload = new
            {
                data = new
                {
                    type = "flight-order",
                    flightOffers = offers,
                    travelers = objTravelers,
                    //remarks = new
                    //{
                    //    general = new[]
                    //    {
                    //        new
                    //        {
                    //            subType="",
                    //            text=""
                    //        }
                    //    }
                    //},
                    //ticketingAgreement = new
                    //{
                    //    option = "DELAY_TO_CANCEL",
                    //    delay = "6D"
                    //},
                    //contacts = new[]
                    //{
                    //    new
                    //    {
                    //        addresseeName = new
                    //        {
                    //            firstName="",
                    //            lastName=""
                    //        },
                    //        companyName="",
                    //        purpose="",
                    //        phones = new[]
                    //        {
                    //            new
                    //            {
                    //                deviceType="LANDLINE",
                    //                countryCallingCode="91",
                    //                number="999999999"
                    //            },
                    //            new
                    //            {
                    //                deviceType="MOBILE",
                    //                countryCallingCode="91",
                    //                number="999999999"
                    //            }
                    //        },
                    //        emailAddress="",
                    //        address = new {
                    //            lines = new[]
                    //            {
                    //                new {}
                    //            },
                    //            postalCode="",
                    //            cityName="",
                    //            countryCode=""
                    //        }
                    //    }
                    //}
                }
            };
            var jsonPayload = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return json;

        }


    }
}
