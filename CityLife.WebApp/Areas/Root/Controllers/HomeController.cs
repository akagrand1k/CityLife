using CityLife.BusinessLogic.UserProfileService;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Root.Controllers
{
    [Roles(RoleConstant.RoleRoot)]
    public class HomeController : DefaultController
    {
        public HomeController(IUserProfileService userSrv) : base(userSrv)
        {

        }
        // GET: Root/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}