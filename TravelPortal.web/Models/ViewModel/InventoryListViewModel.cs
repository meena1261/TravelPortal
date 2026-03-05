using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class InventoryListViewModel
    {
        public int InvestorId { get; set; }
        public string FlightNo { get; set; }
        public string Sector {  get; set; }
        public string DeptTime { get; set; }
        public string ArrivalTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string BlackoutDays { get; set; }
        public string DaysofOperations { get; set; }
        public string Supplier {  get; set; }
        public int TotalEconomy { get; set; }
        public int TotalFirstClass { get; set; }
        public int TotalBussiness { get; set; }
        public string Status { get; set; }
    }
}