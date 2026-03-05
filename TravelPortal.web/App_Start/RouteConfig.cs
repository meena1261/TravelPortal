using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TravelPortal.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            
            routes.MapRoute(
                name: "AgentLogin",
                url: AgentLogin,
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminLogin",
                url: AdminLogin,
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
        public const string AdminLogin = "admin/login";
        public const string AgentLogin = "agent/login";
        public const string AgentHome = "agent/home";
        public const string NewApplication = "agent/new-application";
        public const string AddAgent = "agent/Add-Agent";
        public const string EditAgent = "agent/Edit-Agent/{id}";
        public const string ListAgent = "agent/list-agent";
        public const string AgentDashboard = "agent/dashboard";
        public const string MyBooking = "agent/mybooking/{type}";
        public const string MyProfile = "agent/myprofile";
        public const string Wallet = "agent/wallet";
        public const string FundRequest = "agent/FundRequest";
        public const string CreditLimit = "agent/creditlimit";
        public const string AgentChat = "agent/chat";
        public const string AgentSetting = "agent/settings";
        public const string Security = "agent/settings/security";
        public const string UpdateProfile = "agent/settings/updateprofile";
        public const string ManageInventory = "agent/inventory";
        public const string ManageInventoryAdd = "agent/inventory/add";
        public const string ManageInventoryList = "agent/inventory/list";
        public const string ManageSubUsers = "agent/users";
        public const string FlightSearch = "flight/search";
        public const string FlightDetails = "flight/reviewDetails";

    }
}
