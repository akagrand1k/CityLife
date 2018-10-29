using CityLife.Extensions.Constant;
using Microsoft.Owin.Security.Google;
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

        private GoogleOAuth2AuthenticationOptions GoogleAuthOptions
        {
            get
            {
                var googleOpts = new GoogleOAuth2AuthenticationOptions
                {
                    ClientId = SysConstants.External_GoogleClinetId,
                    ClientSecret = SysConstants.External_GoogleSecret,
                    Provider = new GoogleOAuth2AuthenticationProvider()
                    {
                        OnAuthenticated = ctx =>
                        {
                            ctx.Identity.AddClaim(new Claim(SysConstants.AccessToken_Google, ctx.AccessToken));

                            if (ctx.RefreshToken != null)
                            {
                                ctx.Identity.AddClaim(new Claim(SysConstants.AccessToken_GoogleRefresh, ctx.RefreshToken));
                            }
                            return Task.FromResult(0);
                        }
                    }
                };

                return googleOpts;
            }
        }

        private VKontakteAuthenticationOptions VkAuthOptions
        {
            get
            {
                var vkopts = new VKontakteAuthenticationOptions
                {
                    ClientId = SysConstants.External_VkClientId,
                    ClientSecret = SysConstants.External_VkSecret,
                    Scope = new string[] { "email", "user_id" },
                    Provider = new VKontakteAuthenticationProvider
                    {
                        OnAuthenticated = ctx =>
                        {
                            ctx.Identity.AddClaim(new Claim(SysConstants.AccessToken_Vk, ctx.AccessToken));

                            if (ctx.Response != null)
                            {
                                ctx.Identity.AddClaim(new Claim("user_id", ctx.Id));
                            }
                            
                            return Task.FromResult(0);
                        }
                    }
                };

                return vkopts;
            }
        }

    }
}