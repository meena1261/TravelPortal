using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models
{
    public class UsersModels
    {
        public int Usrno { get; set; }
        public int RoleId { get; set; }
        public int UserTypeID { get; set; }
        public string AspNetID { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ReferralCode { get; set; }
        public string Password { get; set; }
        public int CreatedbyUsrno { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public HttpPostedFileBase ProfilePhoto { get; set; }

    }
    public class UserInvitedModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string RoleId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Mobile { get; set; }

    }
}