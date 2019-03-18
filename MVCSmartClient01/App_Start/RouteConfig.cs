using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCSmartClient01
{
    public class RouteConfig
    {
        public const string LoginRouteName = "LogIn";
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(LoginRouteName, "log-in", new { controller = "Account", Action = "LogIn" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                //defaults: new { controller = "HomeRekanan", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "ATest1", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "EmlNotification", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "MstTypeOfRekanan", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}