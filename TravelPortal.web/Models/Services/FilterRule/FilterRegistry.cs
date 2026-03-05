using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services.FilterRule
{
    public static class FilterRegistry
    {
        public static readonly Dictionary<string, IFilterRule<OfferData>> Rules
            = new Dictionary<string, IFilterRule<OfferData>>
        {
                { FilterKeys.airline.ToString(), new AirlineFilter() },
                { FilterKeys.stops.ToString(), new StopsFilter() },
                { FilterKeys.departur.ToString(), new DepartureFilter() },
                { FilterKeys.arrivals.ToString(), new ArivalFilter() },
                { FilterKeys.price.ToString(), new PriceFilter() },
        };
    }

}