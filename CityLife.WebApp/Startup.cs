using Autofac;
using Microsoft.Owin;
using Owin;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Security.Provider;
using Microsoft.AspNet.Identity.Owin;
using CityLife.Entities.Models.User;
using Microsoft.Owin.Security.Google;
using Owin.Security.Providers.VKontakte;
using System.Threading.Tasks;
using System.Security.Claims;
using CityLife.Extensions.Constant;
using Owin.Security.Providers.VKontakte.Provider;

[assembly: OwinStartupAttribute(typeof(CityLife.WebApp.Startup))]
namespace CityLife.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }      
    }
}