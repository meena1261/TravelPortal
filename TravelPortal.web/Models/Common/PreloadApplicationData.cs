using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Models.Common
{
    public class PreloadApplicationData
    {
        public static List<CityAirportViewModel> Cities
        {
            get
            {
                return HttpContext.Current.Application[EApplicationKeys.Cities.ToString()] as List<CityAirportViewModel>;
            }
        }

    }
}