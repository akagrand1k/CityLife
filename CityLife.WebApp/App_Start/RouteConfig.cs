using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CityLife.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



           

            routes.MapRoute(
                name: "articles_alias_route",
                url: "articles/{tag}",
                defaults: new { controller = "Articles", action = "Index", tag = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "article_alias_route",
            url: "articles/article/{alias}",
            defaults: new { controller = "Articles", action = "Article", alias = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "CityLife.WebApp.Controllers" }
            );
            
        }
    }
}
