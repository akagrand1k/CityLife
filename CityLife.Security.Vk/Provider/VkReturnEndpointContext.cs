using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Security.Vk
{
    public class VkReturnEndpointContext:ReturnEndpointContext
    {
        public VkReturnEndpointContext(IOwinContext context,AuthenticationTicket ticket)
            :base(context,ticket)
        {

        }
    }
}
