using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Infrastructure.ExternalLogin
{
    internal class ChallengeResult:HttpUnauthorizedResult
    {
        private const string XsrfKey = "XsrfId";
        public ChallengeResult(string provider,string returnUri)
            :this(provider,returnUri,null)
        {

        }

        public ChallengeResult(string provider,string returnUri,string userId=null)
        {
            ProviderName = provider;
            UserId = UserId;
            RedirectUri = returnUri;
        }

        public string ProviderName { get; set; }
        public string UserId { get; set; }
        public string RedirectUri { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var props = new AuthenticationProperties { RedirectUri = RedirectUri };

            if (!String.IsNullOrEmpty(UserId))
            {
                props.Dictionary[XsrfKey] = UserId;
            }

            context.HttpContext.GetOwinContext().Authentication.Challenge(props, ProviderName);
        }
    }
}