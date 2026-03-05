using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Models.Common
{
    public class ActionTypes
    {
        public static string IsActive { get { return "IsActive"; } }
        public static string IsSupplier { get { return "IsSupplier"; } }
        public static string IsSales { get { return "IsSales"; } }
        public static string IsPaymentByCC { get { return "IsPaymentByCC"; } }
        public static string IsPaymentByAccount { get { return "IsPaymentByAccount"; } }
        public static string IsUseLogo { get { return "IsUseLogo"; } }
        public static string IsContract { get { return "IsContract"; } }
        public static string IsToken { get { return "IsToken"; } }
    }
}