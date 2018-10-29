using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using CityLife.Security.Vk;

namespace Owin
{
    public static class VkAuthenticationExtensions
    {
        public static IAppBuilder UseVkAuthentication(this IAppBuilder app, VkAuthenticationOptions options)
        {

            if (app==null)
            {
                throw new ArgumentNullException("app");
            }

            if (options==null)
            {
                throw new ArgumentNullException("options");
            }

            app.Use(typeof(VkAuthenticationMiddleware), app, options);


            return app;
        }


        public static IAppBuilder UseVkAuthentication(this IAppBuilder app, string appId, string appSecret)
        {
            return UseVkAuthentication(app, new VkAuthenticationOptions { AppId = appId, AppSecret = appSecret });
        }
    }
}
