using CityLife.IoC.Autofac;
using Nemiro.OAuth;
using Nemiro.OAuth.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CityLife.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacContainer.GetContainer(typeof(MvcApplication).Assembly);

            //OAuthRegisterClient
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            bool isGet = HttpContext.Current.Request.RequestType.ToLowerInvariant().Contains("get");

            if (isGet && HttpContext.Current.Request.Url.AbsolutePath.Contains(".") == false)
            {
                string lowercaseURL = (Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath);
                if (Regex.IsMatch(lowercaseURL, @"[A-Z]"))
                {
                    
                    lowercaseURL = lowercaseURL.ToLower() + HttpContext.Current.Request.Url.Query;

                    Response.Clear();
                    Response.Status = "301 Moved Permanently";
                    Response.AddHeader("Location", lowercaseURL);
                    Response.End();
                }
            }
        }
    }
}
