using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPortal.web.Models.Enum;

namespace TravelPortal.web.Models.Common
{
    public class DropdownLists
    {
        public static List<SelectListItem> PaymentModeTypes()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Wallet", Value = PaymentModeEnum.Wallet.ToString() },
                    new SelectListItem { Text = "Credit Limit", Value = PaymentModeEnum.Credit.ToString() },
                    new SelectListItem { Text = "Online Payment", Value = PaymentModeEnum.Online.ToString() }
                };
            return list;
        }
        public static List<SelectListItem> Gender()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Male", Value = "MALE" },
                    new SelectListItem { Text = "Female", Value = "FEMALE" }
                };
            return list;
        }
        public static List<SelectListItem> States()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text="Andhra Pradesh", Value="AP"},
                new SelectListItem { Text="Arunachal Pradesh", Value="AR"},
                new SelectListItem { Text="Assam", Value="AS"},
                new SelectListItem { Text="Bihar", Value="BR"},
                new SelectListItem { Text="Chhattisgarh", Value="CG"},
                new SelectListItem { Text="Goa", Value="GA"},
                new SelectListItem { Text="Gujarat", Value="GJ"},
                new SelectListItem { Text="Haryana", Value="HR"},
                new SelectListItem { Text="Himachal Pradesh", Value="HP"},
                new SelectListItem { Text="Jharkhand", Value="JH"},
                new SelectListItem { Text="Karnataka", Value="KA"},
                new SelectListItem { Text="Kerala", Value="KL"},
                new SelectListItem { Text="Madhya Pradesh", Value="MP"},
                new SelectListItem { Text="Maharashtra", Value="MH"},
                new SelectListItem { Text="Manipur", Value="MN"},
                new SelectListItem { Text="Meghalaya", Value="ML"},
                new SelectListItem { Text="Mizoram", Value="MZ"},
                new SelectListItem { Text="Nagaland", Value="NL"},
                new SelectListItem { Text="Odisha", Value="OD"},
                new SelectListItem { Text="Punjab", Value="PB"},
                new SelectListItem { Text="Rajasthan", Value="RJ"},
                new SelectListItem { Text="Sikkim", Value="SK"},
                new SelectListItem { Text="Tamil Nadu", Value="TN"},
                new SelectListItem { Text="Telangana", Value="TS"},
                new SelectListItem { Text="Tripura", Value="TR"},
                new SelectListItem { Text="Uttar Pradesh", Value="UP"},
                new SelectListItem { Text="Uttarakhand", Value="UK"},
                new SelectListItem { Text="West Bengal", Value="WB"},

                // Union Territories
                new SelectListItem { Text="Andaman and Nicobar Islands", Value="AN"},
                new SelectListItem { Text="Chandigarh", Value="CH"},
                new SelectListItem { Text="Dadra and Nagar Haveli and Daman & Diu", Value="DN"},
                new SelectListItem { Text="Delhi", Value="DL"},
                new SelectListItem { Text="Jammu and Kashmir", Value="JK"},
                new SelectListItem { Text="Ladakh", Value="LA"},
                new SelectListItem { Text="Lakshadweep", Value="LD"},
                new SelectListItem { Text="Puducherry", Value="PY"}
            };
        }

        public static List<SelectListItem> FlightTripType()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "OneWay", Value = FlightTripTypes.OneWay.ToString() },
                    new SelectListItem { Text = "Round Trip", Value = FlightTripTypes.RoundTrip.ToString() },
                    new SelectListItem { Text = "Multi City", Value = FlightTripTypes.MultiCity.ToString() }
                };
            return list;
        }
        public static List<SelectListItem> YesNo()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Yes", Value = "1" },
                    new SelectListItem { Text = "No", Value = "0" }
                };
            return list;
        }
        public static List<SelectListItem> UnitType()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "KG", Value = "KG" },
                    new SelectListItem { Text = "PC", Value = "PC" }
                };
            return list;
        }
        public static List<SelectListItem> FlightViaConnectionTypes()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Roundtrip", Value = "1" },
                    new SelectListItem { Text = "Via", Value = "2" },
                    new SelectListItem { Text = "Connecting", Value = "3" },
                    new SelectListItem { Text = "Roundtrip Via", Value = "4" },
                    new SelectListItem { Text = "Roundtrip Connecting", Value = "5" },
                };
            return list;
        }
        public static List<SelectListItem> FlightClassTypes()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Economy", Value = "ECONOMY" },
                    new SelectListItem { Text = "Premium Economy", Value = "PREMIUM_ECONOMY" },
                    new SelectListItem { Text = "Business", Value = "BUSINESS" },
                    new SelectListItem { Text = "First Class", Value = "FIRST" }
                };
            return list;
        }
        public static List<SelectListItem> WeekList()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Monday", Value = "Mon" },
                    new SelectListItem { Text = "Tuesday", Value = "Tue" },
                    new SelectListItem { Text = "Wednesday", Value = "Wed" },
                    new SelectListItem { Text = "Thuresday", Value = "Thu" },
                    new SelectListItem { Text = "Friday", Value = "Fri" },
                    new SelectListItem { Text = "Satuarday", Value = "Sat" },
                    new SelectListItem { Text = "Sunday", Value = "Sun" },
                };
            return list;
        }
        public static List<SelectListItem> PaymentMode()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "NEFT", Value = "NEFT" },
                    new SelectListItem { Text = "RTGS", Value = "RTGS" },
                    new SelectListItem { Text = "IMPS", Value = "IMPS" },
                    new SelectListItem { Text = "UPI", Value = "UPI" },
                    new SelectListItem { Text = "Credit Card", Value = "Credit_Card" },
                    new SelectListItem { Text = "Debit Card", Value = "Debit_Card" },
                    new SelectListItem { Text = "Net Banking", Value = "Net_Banking" },
                };
            return list;
        }
        public static List<SelectListItem> Factor()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Credit", Value = "CR" },
                    new SelectListItem { Text = "Debit", Value = "DR" }
                };
            return list;
        }
        public static List<SelectListItem> MarkupType()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Percentage", Value = "1" },
                    new SelectListItem { Text = "Fixed", Value = "2" }
                };
            return list;
        }
        public static List<SelectListItem> MarkupMethodType()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "As Base", Value = "1" },
                    new SelectListItem { Text = "As Tax", Value = "2" },
                    new SelectListItem { Text = "As Servive", Value = "3" }
                };
            return list;
        }
        public static List<SelectListItem> MarkupCategories()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "All Categories", Value = "1" },
                    new SelectListItem { Text = "Domestic LCC", Value = "2" },
                    new SelectListItem { Text = "Domestic FSC", Value = "3" },
                    new SelectListItem { Text = "International LCC", Value = "4" },
                    new SelectListItem { Text = "International FSC", Value = "5" }
                };
            return list;
        }
        public static List<SelectListItem> PeriodType()
        {
            var list = new List<SelectListItem>{
                    new SelectListItem { Text = "Day", Value = "Day" },
                    new SelectListItem { Text = "Weekly", Value = "Weekly" },
                    new SelectListItem { Text = "Monthly", Value = "Monthly" }
                };
            return list;
        }
    }
}