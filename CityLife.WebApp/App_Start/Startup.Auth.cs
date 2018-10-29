
using CityLife.Authenticate.Managers;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Owin.Security.Providers.VKontakte;
using Owin.Security.Providers.VKontakte.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CityLife.WebApp
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            // app.CreatePerOwinContext<IUserService>(UserService);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.GetInstance);
            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.CreateSignInManager);

            //first step , define  default app cookie, and set provider for two-factor and external authorize.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = SysConstants.Identity_CookieName,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = new TimeSpan(8, 0, 0),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AppUserManager, AppUser>
                     (
                         validateInterval: TimeSpan.FromMinutes(35),
                         regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)
                     )
                }
            });

            // second step, define type external cookie for external login
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // temp data about user, for two-factor validate
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // stories information abount second factor in browser cookie. For example, mail inbox, phone etc..
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // last step, using external srvices for authorize. 

            app.UseVKontakteAuthentication(VkAuthOptions);           
            app.UseGoogleAuthentication(GoogleAuthOptions);
        }  
    }
}
