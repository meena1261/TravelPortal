using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services.FilterRule
{
    public class PriceFilter : IFilterRule<OfferData>
    {
        public IQueryable<OfferData> Apply(IQueryable<OfferData> query, JToken value)
        {
            if (value == null) return query;

            decimal min = value["min"].ToObject<decimal>();
            decimal max = value["max"].ToObject<decimal>();

            return query.Where(f => f.Itineraries.Sum(e=>e.Price) >= min && f.Itineraries.Sum(e => e.Price) <= max);
        }

        public IQueryable<OfferData> Apply(IQueryable<OfferData> query, List<string> values)
        {
            throw new NotImplementedException();
        }
    }


}