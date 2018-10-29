using CityLife.BusinessLogic.Agregate;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Administrator.Controllers
{
    [Roles(RoleConstant.RoleRoot,RoleConstant.RoleAdmin)]
    public class HomeController : DefaultController
    {
        private IInitialPrimaryData initService;

        public HomeController(IInitialPrimaryData _initService, IUserProfileService userSrv) : base(userSrv)
        {
            this.initService = _initService;
        }
        // GET: Administrator/Home
        public ActionResult Index()
        {
            initService.InitSysReferences();
            return View();
        }
    }
}