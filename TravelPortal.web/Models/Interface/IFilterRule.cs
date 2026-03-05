using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Interface
{
    public interface IFilterRule<T>
    {
        IQueryable<T> Apply(IQueryable<T> query, JToken value);
    }

}