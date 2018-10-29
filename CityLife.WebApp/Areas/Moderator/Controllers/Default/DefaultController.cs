﻿using CityLife.BusinessLogic.UserProfileService;
using CityLife.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{
    public class DefaultController : BaseController
    {
        public DefaultController(IUserProfileService _userSrv) : base(_userSrv)
        {
        }
    }
}