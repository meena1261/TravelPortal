using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services.FilterRule
{
    public class StopsFilter : IFilterRule<OfferData>
    {
        public IQueryable<OfferData> Apply(IQueryable<OfferData> query, JToken values)
        {
            var stops = values.ToObject<List<string>>();

            if (stops == null || !stops.Any())
                return query;

            return query.Where(f =>
                f.Itineraries
                 .OrderBy(i => i.Stops)
                 .Select(i => i.Stops.ToString())
                 .FirstOrDefault() != null &&
                stops.Contains(
                    f.Itineraries
                     .OrderBy(i => i.Stops)
                     .Select(i => i.Stops.ToString())
                     .FirstOrDefault()
                )
            );
        }

    }


}