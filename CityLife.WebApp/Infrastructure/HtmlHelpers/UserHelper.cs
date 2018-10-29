using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Infrastructure.HtmlHelpers
{
    public static class UserHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool UserIsInRole(this HtmlHelper self, string role)
        {            
            return self.ViewContext.HttpContext.User.IsInRole(role);
        }
        
    }
}