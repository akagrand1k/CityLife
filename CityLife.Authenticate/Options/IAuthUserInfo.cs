
using CityLife.Authenticate.Extends.OperationResult;
using CityLife.Entities.Models.User;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Authenticate.Options
{
    public interface IAuthUserInfo<TUser>
        where TUser : class, new()

    {

        /// <summary>
        /// Deleting login information.
        /// </summary>
        /// <param name="userId">user identifier</param>
        /// <returns>Collection states</returns>
        OperationDetails DeleteInfo(string userId);
        /// <summary>
        /// Adds login information from external services for specified provider. 
        /// </summary>
        /// <param name="loginInfo">Login info with claims for interconnected with external services.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="provider">Provider name.</param>
        /// <returns></returns>
        bool AddInfo(ExternalLoginInfo loginInfo, string userId, string provider = null);


        /// <summary>
        /// Create specified user.
        /// </summary>
        /// <param name="user">Ready instance to create.</param>
        /// <returns>Instance TUser</returns>
        TUser Create(TUser user);

        /// <summary>
        /// Delete specified user.
        /// </summary>
        /// <param name="user">Instance user.</param>
        /// <returns></returns>
        bool Delete(string userId);
    }
}