using CityLife.BusinessLogic.UserProfileService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserProfileService profileService;
        public HomeController(IUserProfileService profileService) : base(profileService)
        {
            this.profileService = profileService;
        }

        // GET: Home
        public ActionResult Index()
        {
                                  
            return View();
        }
    }
}