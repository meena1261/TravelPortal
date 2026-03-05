using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Interface;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Services.FilterRule
{
    public class ArivalFilter : IFilterRule<OfferData>
    {
        public IQueryable<OfferData> Apply(IQueryable<OfferData> query, JToken values)
        {
            var slots = values.ToObject<List<string>>();
            if (slots == null || !slots.Any())
                return query;

            return query.Where(d =>
                d.Itineraries.Any() &&
                slots.Any(slot => MatchSlot(d.Itineraries.First().ArrivalDate, slot))
            );
        }

        private bool MatchSlot(string dt, string slot)
        {
            if (!DateTime.TryParse(dt, out var date))
                return false;

            int h = date.Hour;

            switch (slot)
            {
                case "morning": return h >= 6 && h < 12;
                case "afternoon": return h >= 12 && h < 18;
                case "evening": return h >= 18 && h < 24;
                case "night": return h < 6;
                default: return false;
            }
        }
    }


}