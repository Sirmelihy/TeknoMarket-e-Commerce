using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeknoMarket
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product",
                url: "Index/{category}",
                defaults: new { controller = "Product", action = "Index", category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category",
                url: "Index/{category}",
                defaults: new { controller = "Category", action = "Index", category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Anasayfa", id = UrlParameter.Optional }
            );
        }
    }
}
