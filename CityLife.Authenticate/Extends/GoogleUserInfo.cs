using CityLife.Authenticate.Options;
using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.Authenticate.Extends.OperationResult;
using CityLife.Authenticate.Extends.Provider;
using Microsoft.AspNet.Identity.Owin;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.DALImplementation.Context;
using CityLife.DALImplementation;
using System.Security.Claims;
using CityLife.Extensions.Constant;

namespace CityLife.Authenticate.Extends
{
    internal class GoogleUserInfo : CoreUserInfo
    {

        private UserProfile profile;

        public GoogleUserInfo()
        {
            EndPointUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
        }

        public ResponseInfo InitializeInfo(ExternalLoginInfo loginInfo)
        {
            Claim claim = null;
            dynamic response = null;

            try
            {
              
                claim = GetClaim(loginInfo, SysConstants.AccessToken_Google);
                EndPointUrl = EndPointUrl + $"?access_token={claim.Value}";
                response = SendRequest(EndPointUrl);

                if (response.error != null)
                {
                    return null;
                }

                return new ResponseInfo { FirstName =response.given_name, LastName = response.family_name, Picture = response.picture };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
