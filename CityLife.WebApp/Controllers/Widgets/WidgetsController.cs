using CityLife.BusinessLogic.SectionServices;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Extensions.Constant;
using CityLife.Extensions.Tools;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.WebApp.Models.Section;
using CityLife.WebApp.Models.TopNavigate;
using CityLife.WebApp.Models.Widgets;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Controllers
{
    public class WidgetsController : Controller
    {
        private readonly IUserProfileService profileService;
        private readonly ISectionService section;

        public WidgetsController(IUserProfileService profileService, ISectionService section)
        {
            this.profileService = profileService;
            this.section = section;
        }

        public PartialViewResult UserInfo()
        {
            string avatarPath = "";
            UserInfoViewModel model = new UserInfoViewModel()
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Name = User.Identity.Name
            };

            if (model.IsAuthenticated)
            {
                var profile = profileService.Get(User.Identity.GetUserId());

                if (profile != null)
                {
                    if (profile.IsExternal)
                    {
                        if (string.IsNullOrEmpty(profile.PicturePath))
                            avatarPath = profile.ExternalPic;
                        else
                            avatarPath =
                                AppConstants.directoryProfileAvatar + profile.PicturePath;
                    }
                    else
                    {
                        avatarPath = 
                            AppConstants.directoryProfileAvatar + profile.PicturePath;
                    }

                    model.Name = string.IsNullOrEmpty(profile.GivenName) ?
                        User.Identity.GetUserName() :
                        $"{profile.GivenName} {profile.FamilyName}";

                    model.Avatar = avatarPath;
                }
            }

            return PartialView(model);
        }

        public PartialViewResult TopNavigate()
        {
            TopNavigateViewModel model = new TopNavigateViewModel
            {
                CurrentUrl = HttpContext.Request.Url.AbsolutePath,
                Sections = section.Sections
                                    .Where(x => x.IsActive)
                                    .Select(x => SectionMapper.ToViewModel(x))
            };

            return PartialView("_TopNavigate", model);
        }

        /// <summary>
        /// Tiny MCE action uploader
        /// </summary>
        /// <param name="file">upload file for page</param>
        /// <returns>location - parameter path image</returns>
        [HttpPost]
        public JsonResult EditorUpload(HttpPostedFileBase file)
        {
            Random rnd = new Random(1);
            int num = rnd.Next();

            string fileName = "", mapPath = "", filePath = "";

            if (file != null && file.ContentLength > 0)
            {
                FileInfo fileInfo = new FileInfo(file.FileName);

                fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                mapPath = HttpContext.Server.MapPath(AppConstants.dirNewsPics);
                filePath = Path.Combine(mapPath, Path.GetFileName(fileName));
                //Welocme! This Induss code! =)

                if (file.ContentLength > 1024 * 1024 * 2)
                {
                    throw new HttpException(500, "Размер файла превышает 2 MB!");
                }

                DirectoryTools.CheckDirectoryExist(mapPath);
                try
                {
                    file.SaveAs(filePath);
                }
                catch 
                {
                }

            }
            string target = Path.Combine(AppConstants.dirNewsPics, fileName);
            return Json(new { location = target });
        }
    }
}