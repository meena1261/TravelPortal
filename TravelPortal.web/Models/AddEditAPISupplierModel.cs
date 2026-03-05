using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class AddEditAPISupplierModel
    {
        public int APIID { get; set; }
        public string Supplier { get; set; }
        public string APIType { get; set; }
        public string LiveEndPointUrl { get; set; }
        public string LiveClientId { get; set; }
        public string LiveClientSecret { get; set; }
        public string TestEndPointUrl { get; set; }
        public string TestClientId { get; set; }
        public string TestClientSecret { get; set; }
        public bool IsLive { get; set; }
    }
}