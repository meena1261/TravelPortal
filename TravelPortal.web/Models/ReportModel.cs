using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class SearchModel
    {
        public int Usrno { get; set; } = 0;
        public string FromDate { get; set; } = "";
        public string ToDate { get; set; } = "";
        public string SearchText { get; set; } = "";

    }
    public class SearchReportModel
    {
        public string type { get; set; } = "";
        public int LoginUsrno { get; set; } = 0;
        public string Searchtext { get; set; } = "";
        public string fromDate { get; set; } = "";
        public string toDate { get; set; } = "";
        public string DateRange { get; set; } = "";
        public string userId { get; set; } = "";
        public string status { get; set; } = "";
        public int IntroUsrno { get; set; } = 0;
    }
}