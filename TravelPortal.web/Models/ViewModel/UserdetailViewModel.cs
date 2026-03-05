using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class UserdetailViewModel
    {
        public int Usrno { get; set; }
        public string UserId { get; set; }
        public string AspNetID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ProfilePhoto { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserTypeID { get; set; }
        public string UserType { get; set; }
        public bool? IsKYCCompleted { get; set; }
        public bool? IsSupplier { get; set; }
        public string SupplierAgreement { get; set; }
        public string AgreementRemark { get; set; }
        public string CreatedDate { get; set; }

    }
}