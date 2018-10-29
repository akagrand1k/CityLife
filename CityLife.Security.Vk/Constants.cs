using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Security.Vk
{
    internal static class Constants
    {
        public const string DefaultAuthenticationType = "Vk";

        internal const string AuthorizationEndpoint = "https://oauth.vk.com/authorize";
        internal const string TokenEndpoint = "https://oauth.vk.com/access_token";
        internal const string UserInformationEndpoint = "https://api.vk.com/method/users.get";
    }
}
