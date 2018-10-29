using System;
using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace CityLife.Security.Vk
{
   public class VkAuthenticatedContext:BaseContext
    {
        public VkAuthenticatedContext(IOwinContext context,JObject user, string accessToken):base(context)
        {
            User = user;
            AccessToken = accessToken;

            Id = TryGetValue(user, "uid");

            UserName = $"{TryGetValue(user, "first_name")} {TryGetValue(user, "last_name")}";

            Phone = $"{TryGetValue(user, "phone")}";
        }

        public JObject User { get; set; }
        public string AccessToken { get; set; }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        private static string TryGetValue(JObject user, string propertyName)
        {
            JToken value;

            return user.TryGetValue(propertyName, out value) ? value.ToString() : string.Empty;
        }

    }
}
