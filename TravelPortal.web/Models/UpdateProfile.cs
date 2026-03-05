using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class UpdateProfileModel
    {
        public int Usrno { get; set; }
        public string AgentID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public HttpPostedFileBase UploadProfilePhoto { get; set; }
        public string ProfilePhoto { get; set; }
    }
}