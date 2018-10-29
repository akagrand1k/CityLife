using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Security.Vk
{
    public class VkAuthenticationProvider : IVkAuthenticationProvider
    {
        public VkAuthenticationProvider()
        {
            OnAuthenticated = ctx => Task.FromResult<object>(null);
            OnReturnEndPoint = ctx => Task.FromResult<object>(null);
            OnApplyRedirect = ctx => ctx.Response.Redirect(ctx.RedirectUri);            
        }

        public Func<VkAuthenticatedContext, Task> OnAuthenticated { get; set; }
        public Func<VkReturnEndpointContext, Task> OnReturnEndPoint { get; set; }
        public Action<VkApplyRedirectContext> OnApplyRedirect { get; set; }

        public virtual void ApplyRedirect(VkApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }

        public virtual Task Authenticated(VkAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(VkReturnEndpointContext context)
        {
            return OnReturnEndPoint(context);
        }
    }
}
