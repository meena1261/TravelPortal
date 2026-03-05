using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Helpers
{
    public class ConfigHelper
    {
        public static string DatabaseConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString ?? string.Empty;
        public static string ApplicationUrl = ConfigurationManager.AppSettings["ApplicationUrl"] ?? string.Empty;
        public static string ApiUrl = ConfigurationManager.AppSettings["ApiUrl"] ?? string.Empty;
        public static string DefaultPassword = ConfigurationManager.AppSettings["DefaultPassword"] ?? string.Empty;
        public static string RazorpayKey = ConfigurationManager.AppSettings["RazorpayKey"] ?? string.Empty;
    }
}