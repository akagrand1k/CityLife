using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{
    [Roles(RoleConstant.RoleModerator)]
    public class HomeController : Controller
    {
        // GET: Moderator/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}