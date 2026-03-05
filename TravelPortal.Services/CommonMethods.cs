using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPortal.Services
{
    public class CommonMethods
    {
        public static string FormatTime(string iso) =>
            DateTime.Parse(iso).ToString("HH:mm");
        public static string FormatDuration(string isoDuration) =>
            isoDuration.Replace("PT", "")
                       .Replace("H", " h ")
                       .Replace("M", " m")
                       .Trim();
        public static string DateFormat(string date)
        {
            DateTime dt = DateTime.Parse(date);
            string formatted = dt.ToString("ddd, dd MMM yy"); // Wed, 20 Aug 25
            return formatted;
        }
        public static string AirlineLogo(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return string.Format($"https://content.airhex.com/content/logos/airlines_{{0}}_50_50_s.png", input);
            }
            return input;
        }
        public static string GetTimeSlot(DateTime time)
        {
            int h = time.Hour;

            if (h < 6) return "0-6";
            if (h < 12) return "6-12";
            if (h < 18) return "12-18";

            return "18-24";
        }
        public static string GetLabel(string slot)
        {
            return slot switch
            {
                "0-6" => "Before 6AM",
                "6-12" => "6AM - 12PM",
                "12-18" => "12PM - 6PM",
                "18-24" => "After 6PM",
                _ => ""
            };
        }

    }
}
