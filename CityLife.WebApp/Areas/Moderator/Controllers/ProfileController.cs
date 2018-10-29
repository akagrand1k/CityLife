using CityLife.Extensions.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{
    public class ProfileController : DefaultController
    {
        public ActionResult Index()
        {
            if (User.IsInRole(RoleConstant.RoleRoot))
                return View("_RootProfile");

            if (User.IsInRole(RoleConstant.RoleAdmin))
                return View("_AdminProfile");

            if (User.IsInRole(RoleConstant.RoleModerator))
                return View("_ModeratorProfile");

            return View();
        }
    }
}