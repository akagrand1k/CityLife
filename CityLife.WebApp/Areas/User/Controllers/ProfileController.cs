using CityLife.BusinessLogic.ActionLogService;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.BusinessLogic.OrganizationService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Entities.Models.Organizations;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.Extensions.Tools;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.WebApp.Infrastructure.Extension;
using CityLife.WebApp.Infrastructure.ImageResize;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.WebApp.Models.Profile;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.User.Controllers
{
    [Roles(RoleConstant.RoleRoot, RoleConstant.RoleAdmin, RoleConstant.RoleUser, RoleConstant.RoleModerator, RoleConstant.RoleCompany)]
    public class ProfileController : DefaultController
    {
        private IUserProfileService userSrv;
        private IExceptionService excepSrv;
        private IOrganizationService orgSrv;
        private IActionLogService actionLogSrv;
        //public int CountCompany { get; set; }
        //public int CountArticles { get; set; }
        //public int CountExceptionToday { get; set; }

        public ProfileController(IUserProfileService _userSrv, IExceptionService _excepSrv, IOrganizationService _orgSrv
            , IUserProfileService userSrv,
            IActionLogService _actionlogSrv) : base(userSrv)
        {
            this.userSrv = _userSrv;
            this.excepSrv = _excepSrv;
            this.orgSrv = _orgSrv;
            this.actionLogSrv = _actionlogSrv;
        }

        public ActionResult Index()
        {
            ProfileViewModel model = new ProfileViewModel();

            AppUser user = GetCurrentUser();
            if (user == null)
            {
                throw new NullReferenceException("Index: user id null !");
            }

            UserProfile profile = userSrv.Get(user.Id);
            if (profile == null)
            {
                if (User.IsInRole(RoleConstant.RoleRoot))
                {
                    profile = new UserProfile();
                    profile.GivenName = user.Email;
                    profile = userSrv.Edit(profile);
                }
                else
                {
                    throw new NullReferenceException("Index: profile is null!");
                }
            }

            model = UserProfileMapper.ToViewModel(profile);
            ProfileInit(model);

            return View(model);
        }


        public ActionResult EditProfile()
        {
            var profile = UserProfileMapper.ToViewModel(userSrv.Get(GetCurrentUser().Id));
            return Json(profile, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            userSrv.Edit(UserProfileMapper.ToEntity(model), GetCurrentUser().Id);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #region Ajax Method

        [HttpPost]
        public string UploadAvatar()
        {
            HttpPostedFileBase file = Request.Files["Avatar"];
            DirectoryTools.CheckDirectoryExist(AppConstants.directoryProfileAvatar);
            string ownerId = User.Identity.GetUserId();
            var path = ImageResize.Resize(file, AppConstants.directoryProfileAvatar, 135, 135);
            var smallPath = ImageResize.Resize(file, AppConstants.directoryProfileAvatar, 40, 40);

            if (UserManager.IsInRole(ownerId, RoleConstant.RoleCompany))
            {
                var company = orgSrv.GetById(GetCurrentUser().Id);

                if (company != null)
                {
                    if (!string.IsNullOrEmpty(company.LargePathImage))
                        DirectoryTools.DeleteFile(HttpContext, company.LargePathImage, AppConstants.directoryProfileAvatar);

                    if (!string.IsNullOrEmpty(company.SmallPathImage))
                        DirectoryTools.DeleteFile(HttpContext, company.SmallPathImage, AppConstants.directoryProfileAvatar);

                    company.LargePathImage = path;
                    company.SmallPathImage = smallPath;

                    orgSrv.Edit(company);
                }
            }
            else
            {
                var userPicture = userSrv.Get(GetCurrentUser().Id);

                if (!string.IsNullOrEmpty(userPicture.PicturePath))
                    DirectoryTools.DeleteFile(HttpContext, userPicture.PicturePath, AppConstants.directoryProfileAvatar);

                userSrv.UploadAvatar(path, GetCurrentUser());
            }



            return AppConstants.directoryProfileAvatar + path;
        }

        [HttpPost]
        public JsonResult EmailChange(string email)
        {
            var flag = false;

            if (!string.IsNullOrEmpty(email))
            {
                var user = UserManager.FindByIdAsync(GetCurrentUser().Id).Result;
                if (user != null)
                {
                    user.Email = email;
                    user.UserName = email;

                    var result = UserManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        flag = result.Succeeded;
                        //   return RedirectToAction("Logout", "Account",new { state = flag});
                        //User.AddUpdateClaim("Name", email);
                    }
                    //TODO Confirm Email with send code.
                }
            }
            //return View();
            var authManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Json(flag ? "success" : "unsuccessfull", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PasswordChange(string currentPassword, string newPassword)
        {
            var res = false;

            var user = GetCurrentUser();
            if (user != null)
            {
                var identityResult = UserManager.ChangePasswordAsync(user.Id, currentPassword, newPassword).Result;
                if (identityResult.Succeeded)
                    res = true;
            }

            return Json(res ? "success" : "unsuccessfull", JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region HelpersMethod
        private void ProfileInit(ProfileViewModel model)
        {
            if (model != null)
            {
                model.CountUser = UserManager.Users.Where(x => !x.IsDelete).Count();
                model.CountCompany = orgSrv.GetAll.Where(x => x.IsActive & !x.IsDelete).Count();
                model.CountExceptionToday = excepSrv.GetAll.Where(x => x.DateCreate.Date.ToString("dd.MM.yyyy") == DateTime.Today.ToString("dd.MM.yyyy")).Count();
            }
        }
        #endregion
    }
}