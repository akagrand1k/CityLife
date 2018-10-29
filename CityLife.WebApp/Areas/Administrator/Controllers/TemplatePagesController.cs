using CityLife.Extensions.Constant;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Administrator.Controllers
{
    [Roles(RoleConstant.RoleRoot, RoleConstant.RoleAdmin)]
    public class TemplatePagesController : Controller
    {
        // GET: Administrator/TemplatePages
        public ActionResult Index()
        {
            return View();
        }
    }
}