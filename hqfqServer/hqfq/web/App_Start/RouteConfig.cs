using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "lineOp",
            url: "line/op/{id}/{op}",
            defaults: new { controller = "Line", action = "Op", id = UrlParameter.Optional, op = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "line",
               url: "linelist/{state}/{category}/{p}",
               defaults: new { controller = "Line", action = "List",state=UrlParameter.Optional, category = UrlParameter.Optional, p = UrlParameter.Optional }
               );
            routes.MapRoute(
               name: "linesApi",
               url: "lineapi/{lineId}/{state}/{category}/{pageSize}/{pageIndex}/",
               defaults: new { controller = "Line", action = "LineJson", lineId = UrlParameter.Optional, state = UrlParameter.Optional, category = UrlParameter.Optional, pageSize = UrlParameter.Optional, pageIndex = UrlParameter.Optional }
               );
          
           


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}