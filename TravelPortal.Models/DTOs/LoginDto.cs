using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Models.DTOs
{
    public class LoginDto
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
    public class LoginViaOtpModel
    {
        public string Mobile { get; set; }
    }
    public class GetByUsrno
    {
        public int Usrno { get; set; }
    }
}
