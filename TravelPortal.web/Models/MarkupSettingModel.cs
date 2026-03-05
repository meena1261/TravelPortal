using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class MarkupSettingModel
    {
        public int Id { get; set; }
        public string MarkupTypeID { get; set; }
        public decimal? MarkupValue { get; set; }
        public string CurrencyID { get; set; }
        public string MarkupMethodType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DateRange { get; set; }

        public string MarkupCategoryId { get; set; }
        public string Usrno { get; set; }
        public string AirlineId { get; set; }
        public string AirlineName { get; set; }
    }
}