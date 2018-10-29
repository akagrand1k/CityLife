using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Security.Vk
{
    public interface IVkAuthenticationProvider
    {
        Task Authenticated(VkAuthenticatedContext context);

        Task ReturnEndpoint(VkReturnEndpointContext context);

        void ApplyRedirect(VkApplyRedirectContext context);
    }
}
