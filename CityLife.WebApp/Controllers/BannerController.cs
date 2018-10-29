using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Controllers
{
    public class BannerController : Controller
    {
        /// <summary>
        /// left or right position 
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Side()
        {
            return PartialView();
        }


        /// <summary>
        /// mixed in content between bloc's etc.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Mixin()
        {
            return PartialView();
        }
    }
}