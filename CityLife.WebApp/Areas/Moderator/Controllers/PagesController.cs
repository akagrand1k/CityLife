using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{
    [Roles(RoleConstant.RoleModerator)]
    public class PagesController : Controller
    {
        // GET: Moderator/Pages
        public ActionResult Index()
        {
            return View();
        }
    }
}