using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelPortal.web.Helpers
{
    public static class StringExtensionHelper
    {
        public static string ToPrefixApiURL(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("/") || IsAbsoluteUrl(input))
                    return input;
                else
                    return string.Format($"{ConfigHelper.ApiUrl}{{0}}", input);
            }
            return input;
        }
        public static string ToPrefixApplicationURL(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("/") || IsAbsoluteUrl(input))
                    return input;
                else
                    return string.Format($"{ConfigHelper.ApplicationUrl}{{0}}", input);
            }
            return input;
        }
        public static string ToPrefixForwardSlash(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("/") || IsAbsoluteUrl(input))
                    return input;
                else
                    return string.Format("/{0}", input);
            }
            return input;
        }


        public static string ToRemoveFirstForwardSlash(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("/"))
                    return input.Remove(0, 1);
                else
                    return input;
            }
            return input;
        }
        static bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }
    }
}