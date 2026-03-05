using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelPortal.web.Helpers;

namespace TravelPortal.web.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected string CurrentUrl = string.Empty;
        public BaseController()
        {
            try
            {
                // Check if the request is an AJAX request
                if (!System.Web.HttpContext.Current.Request.Headers["X-Requested-With"]?.Equals("XMLHttpRequest", StringComparison.OrdinalIgnoreCase) ?? true)
                {
                    // This is a regular (non-AJAX) request, so process the URL
                    CurrentUrl = GetCurrentUrl(System.Web.HttpContext.Current.Request.RawUrl).ToRemoveFirstForwardSlash();
                    // Proceed with any other operations you want to perform on currentUrl
                }
                ViewBag.CurrentUrl = CurrentUrl;
            }
            catch
            {

            }

        }
        // GET: Base
        public string GetCurrentUrl(string url)
        {
            try
            {
                char[] urlSpearators = { '?' };
                return url.Split(urlSpearators)[0].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}