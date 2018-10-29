using System;
using Microsoft.Owin.Security.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace CityLife.Security.Vk
{
    public class VkApplyRedirectContext : BaseContext<VkAuthenticationOptions>
    {
        public VkApplyRedirectContext(IOwinContext context, VkAuthenticationOptions options,
            AuthenticationProperties properties,
            string redirectUri)
            : base(context, options)
        {
            RedirectUri = redirectUri;
            Properties = properties;
        }

        public string RedirectUri { get; set; }
        public AuthenticationProperties Properties { get; set; }
    }
}
