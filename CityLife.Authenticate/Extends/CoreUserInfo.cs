using CityLife.BusinessLogic.UserProfileService;
using CityLife.DALImplementation;
using CityLife.DALImplementation.Context;
using CityLife.Entities.Models.User;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.ApplicationServices;

namespace CityLife.Authenticate.Extends
{
    internal class CoreUserInfo
    {       
        protected string EndPointUrl { get; set; }

        public CoreUserInfo()
        {
           
        }

       
        /// <summary>
        /// Send request on external API services.
        /// </summary>
        /// <param name="uri">Target uri.</param>
        /// <returns>Dynamic instance from external service.</returns>
        protected virtual dynamic SendRequest(string uri)
        {
            dynamic result = null;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;
                var t = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject(t);
            }

            return result;
        }

        protected Claim GetClaim(ExternalLoginInfo info, string accessTokenKey)
        {
            Claim claim = info.ExternalIdentity.Claims.FirstOrDefault(x => x.Type == accessTokenKey);

            return claim;
        }


        protected void AddState(string key, string value)
        {

        }

        protected void AddError(string key, string value)
        {

        }
    }
}
