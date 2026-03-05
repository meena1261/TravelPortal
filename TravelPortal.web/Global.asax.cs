using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Services.Description;
using TravelPortal.EDMX;
using TravelPortal.web.Helpers;
using TravelPortal.web.Models;

namespace TravelPortal.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            PreloadDataHelper.Initialize();
        }
        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();

            // --- Log to database ---
            try
            {
                using (var db = new db_silviEntities())
                {
                    var log = new EDMX.ErrorLog
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Url = HttpContext.Current.Request.Url.ToString(),
                        HttpMethod = HttpContext.Current.Request.HttpMethod,
                        UserHostAddress = HttpContext.Current.Request.UserHostAddress,
                        CreatedAt = DateTime.Now
                    };
                    db.ErrorLogs.Add(log);
                    db.SaveChanges();
                }
            }
            catch
            {
                // Agar DB me log na ho paye, ignore
            }

            Server.ClearError();

            // AJAX request handling
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Response.StatusCode = 500;
                Response.ContentType = "application/json";
                Response.Write("{\"success\":false,\"message\":\"Something went wrong.\"}");
                Response.End();
            }
            else
            {
                Response.Redirect("~/Error/Index");
            }
        }

    }
}
