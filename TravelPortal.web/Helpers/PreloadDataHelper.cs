using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.EDMX;
using TravelPortal.web.Models.Enum;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Helpers
{
    public static class PreloadDataHelper
    {
        public static void Initialize()
        {
            // This method can be used to initialize any static data if needed in the future
           HttpContext.Current.Application[EApplicationKeys.Cities.ToString()] = GetCityAirport();
        }
        private static List<CityAirportViewModel> GetCityAirport()
        {
            List<CityAirportViewModel> models = new List<CityAirportViewModel>();

            using (var db = new db_silviEntities()) // ✅ fresh context
            {
                models =  db.tblManage_CityAirport
                         .Select(x => new CityAirportViewModel
                         {
                             CityAirportId = x.ID,
                             CityAirport = x.Name,
                             IATACode = x.Code,
                             Address = x.Description
                         })
                         .ToList();
            }
            return models;
        }
    }
}