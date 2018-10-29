using CityLife.BusinessLogic.UserProfileService;
using CityLife.DALImplementation;
using CityLife.DALImplementation.Context;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Authenticate.Extends
{
    internal class VkUserInfo : CoreUserInfo
    {
        private string version;
        private double apiVersion = 5.73;
        public VkUserInfo()
        {
            EndPointUrl = "https://api.vk.com/method/users.get";

            version = $"v={apiVersion}";
        }

        public ResponseInfo InitializeUserProfileInfo(ExternalLoginInfo loginInfo)
        {
            dynamic response_vk = null;

            try
            {
                var accessToken = GetClaim(loginInfo, SysConstants.AccessToken_Vk);
                var vk_userId = GetClaim(loginInfo, "user_id");
                //https://api.vk.com/method/METHOD_NAME?PARAMETERS&access_token=ACCESS_TOKEN&v=V
                EndPointUrl = EndPointUrl + $"?id={vk_userId}&access_token={accessToken.Value}&fields=photo_200&v={version}";

                response_vk = SendRequest(EndPointUrl);

                if (response_vk.error_code != null)
                {
                    return null;
                }

                var arr = response_vk["response"].First;

                var firstName = arr.Value<string>("first_name");
                var lastName = arr.Value<string>("last_name");
                var picture = arr.Value<string>("photo_200");

                var res =
                    new ResponseInfo
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Picture = picture
                    };

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
