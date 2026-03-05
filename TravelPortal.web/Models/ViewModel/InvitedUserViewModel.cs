using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.ViewModel
{
    public class InvitedUserViewModel
    {
        public int InvitedUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserType { get; set; }
        public string Status { get; set; } 
        public string InvitedDate { get; set; }

    }
}