using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class AgentDashboardSummary
    {
        public decimal TotalSales { get; set; }
        public decimal TotalCommission { get; set; }
        public int TotalBookings { get; set; }
        public int TotalSubUsers { get; set; }
    }
}