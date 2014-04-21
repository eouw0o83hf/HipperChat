using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HipperChat.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "WriteOnlyPage",
                url: "writeonly/{apiKey}",
                defaults: new { controller = "WriteOnly", action = "Index", apiKey = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Dogify",
                url: "writeonly/dogify/{toDogify}",
                defaults: new { controller = "WriteOnly", action = "Dogify", toDogify = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DogifyImg",
                url: "writeonly/dogifyimg/{toDogify}",
                defaults: new { controller = "WriteOnly", action = "DogifyImg", toDogify = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}