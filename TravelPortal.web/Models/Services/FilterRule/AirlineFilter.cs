using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services.FilterRule
{
    public class AirlineFilter : IFilterRule<OfferData>
    {
        public IQueryable<OfferData> Apply(IQueryable<OfferData> query, JToken value)
        {
            var values = value.ToObject<List<string>>();
            if (values == null || !values.Any()) return query;

            return query.Where(f =>
                f.Itineraries.Any(i =>
                    i.AirlineCode != null &&
                    i.AirlineCode.Split(',')
                        .Select(x => x.Trim().Split(' ')[0])
                        .Any(code => values.Contains(code))
                ));
        }
    }


}