using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class CreditLimitRequestListViewModel
    {
        public int id { get; set; }
        public string AgentName { get; set; }
        public string AgentUserId { get; set; }
        public string AgentEmail { get; set; }
        public string Amount { get; set; }
        public string CreditType { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime RequestDate { get; set; }
    }
}