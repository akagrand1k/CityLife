using CityLife.Authenticate.Managers;
using CityLife.BusinessLogic.SectionServices;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Models.Default;
using CityLife.WebApp.Models.Section;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.BusinessLogic.UserProfileService;
using System.Security.Principal;

namespace CityLife.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private IUserProfileService userSrv;
        public BaseController(IUserProfileService _userSrv)
        {
            this.userSrv = _userSrv;
        }

        protected AppUserManager UserManager
        {
            get
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                
                return userManager;
            }
        }

        protected AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppRoleManager>();
            }
        }

        public PartialViewResult BackLeftSidebar()
        {
            Navigate vmodel = new Navigate();
            vmodel.CurrentUrl = Request.Url.AbsolutePath;
            vmodel.CurrentRole = UserManager.GetRolesAsync(User.Identity.GetUserId()).Result.FirstOrDefault();

            return PartialView("_BackSidebar", vmodel);
        }

        public PartialViewResult BackTopPanel()
        {
            Navigate vmodel = new Navigate();
            var currProfile = userSrv.Get(GetCurrentUser().Id);
            if (currProfile != null)
            {
                if (!string.IsNullOrEmpty(currProfile.PicturePath))
                    vmodel.PathAvatar = AppConstants.directoryProfileAvatar + currProfile.PicturePath;

                else
                    vmodel.PathAvatar = currProfile.ExternalPic;
            }
            return PartialView("_BackTop", vmodel);
        }
        
        #region SupportMethods
        internal AppUser GetCurrentUser()
        {
            return UserManager.FindByNameAsync(User.Identity.Name).Result;
        }

        internal string GetCurrentUrl()
        {
            return Request.Url.AbsoluteUri;
        }
        #endregion
    }
}