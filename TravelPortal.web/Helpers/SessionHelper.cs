using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelPortal.web.Models.ViewModel;

namespace TravelPortal.web.Helpers
{
    public class SessionHelper
    {
        public static bool Islogin
        {
            get
            {
                return Usrno <= 0 ? false : true;
            }
        }
        public static bool IsAdminlogin
        {
            get
            {
                return AdminUsrno <= 0 ? false : true;
            }
        }
        public static UserdetailViewModel UserDetail
        {
            get
            {
                return HttpContext.Current.Session["_UserDetail"] == null ? null : HttpContext.Current.Session["_UserDetail"] as UserdetailViewModel;
            }
            set
            {
                HttpContext.Current.Session["_UserDetail"] = value;
            }
        }
        public static FlightResults Flights
        {
            get
            {
                return HttpContext.Current.Session["_Flights"] == null ? null : HttpContext.Current.Session["_Flights"] as FlightResults;
            }
            set
            {
                HttpContext.Current.Session["_Flights"] = value;
            }
        }
        public static int Usrno
        {
            get
            {
                return HttpContext.Current.Session["_Usrno"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["_Usrno"]);
            }
            set
            {
                HttpContext.Current.Session["_Usrno"] = value;
            }
        }
        public static int AdminUsrno
        {
            get
            {
                return HttpContext.Current.Session["_AdminUsrno"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["_AdminUsrno"]);
            }
            set
            {
                HttpContext.Current.Session["_AdminUsrno"] = value;
            }
        }
        
        public static int RoleId
        {
            get
            {
                return HttpContext.Current.Session["_RoleId"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["_RoleId"]);
            }
            set
            {
                HttpContext.Current.Session["_RoleId"] = value;
            }
        }
        public static string OTP
        {
            get
            {
                return HttpContext.Current.Session["_OTP"] == null ? string.Empty : HttpContext.Current.Session["_OTP"].ToString();
            }
            set
            {
                HttpContext.Current.Session["_OTP"] = value;
            }
        }
        public static void SessionDestroy()
        {
            HttpContext.Current.Session["_Usrno"] = null;
            HttpContext.Current.Session["_UserDetail"] = null;
            HttpContext.Current.Session["_RoleId"] = null;
            HttpContext.Current.Session["_OTP"] = null;
        }
    }
}