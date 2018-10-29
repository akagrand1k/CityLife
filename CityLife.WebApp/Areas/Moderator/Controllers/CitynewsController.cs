using CityLife.BusinessLogic.CityNewsService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Entities.Models.News;
using CityLife.Entities.Models.User;
using CityLife.Extensions.Constant;
using CityLife.Extensions.Tools;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.WebApp.Infrastructure.ImageResize;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.WebApp.Models.News;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{
    [Roles(RoleConstant.RoleRoot, RoleConstant.RoleAdmin, RoleConstant.RoleModerator)]
    public class CitynewsController : DefaultController
    {
        // GET: Administrator/Citynews
        private readonly INewsService newsSrv;
        private readonly IUserProfileService userSrv;
        public CitynewsController(INewsService newsSrv, IUserProfileService userSrv) : base(userSrv)
        {
            if (newsSrv == null)
            {
                throw new NullReferenceException("newsSrv");
            }

            this.newsSrv = newsSrv;
            this.userSrv = userSrv;
        }

        public ViewResult Index()
        {
            ViewBag.CurrentRole = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();
            return View();
        }

        public ViewResult Edit(int id = 0)
        {
            NewsViewModel model = new NewsViewModel();
            string userId = User.Identity.GetUserId();
            UserProfile userProf = userSrv.Get(userId);

            if (id > 0)
            {
                var article = newsSrv.Get(id);

                if (article == null)
                {
                    ModelState.AddModelError("", "Указанная статья не найдена!");
                }

                model = NewsMapper.ToViewModel(article);

                model.TagIds = model.Tags?.Select(x => x.Id).ToArray();
            }
            else
            {
                //newsSrv.Edit()
            }

            model.Tags = newsSrv.Tags;
            model.IsInRole = User.IsInRole;
            model.OwnerId = userProf.Id;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(NewsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //notify!
                return RedirectToAction("Edit");
            }
            HttpPostedFileBase[] previewPics = model.PreviewPics;

            var article = NewsMapper.ToEntity(model);

            var entry = newsSrv.Edit(article, model.TagIds);

            model = NewsMapper.ToViewModel(entry);

            if (model == null)
            {
                //notify!
                return RedirectToAction("Edit");
            }

            if (model.ArticleId != 0 && previewPics != null && previewPics.Count() > 0)
            {
                SavePreviewPictrures(previewPics, model.ArticleId, model.Name, true);
            }

            return RedirectToAction("Edit", new { id = model.ArticleId });
        }

        [HttpPost]
        public ActionResult DeletePicture(int picId, int articleId)
        {
            var pic = newsSrv.GetPicture(picId);

            if (pic != null)
            {
                if (pic.LargeSizePath != null)
                {
                    DirectoryTools.DeleteFile(HttpContext, pic.LargeSizePath);
                }
                if (pic.SmallSizePath != null)
                {
                    DirectoryTools.DeleteFile(HttpContext, pic.SmallSizePath);
                }
                newsSrv.RemovePicture(picId);

            }

            return RedirectToAction("Edit", new { id = articleId });
        }

        [HttpPost]
        public JsonResult UplTitlePic(int articleId)
        {
            return Json(null);
        }


        #region JSON actions

        public JsonResult GetNews(NewsViewModel model)
        {
            IEnumerable<Article> source = newsSrv.News
                .Where(x => !x.IsDelete && x.IsActive == model.IsActive).ToList();

            var result = source.Select(x => new
            {
                Id = x.Id,
                Title = x.Name,
                DateCreate = x.DateCreate.ToShortDateString(),
                Tags = x.Tags != null && x.Tags.Count() > 0 ? x.Tags.Select(y => y.Name).Aggregate((cur, nxt) => cur + ", " + nxt) : "empty",
                AutorName = x.Owner != null ? $"{x.Owner.GivenName} {x.Owner.FamilyName}" : "",
                AutorAvatar = x.Owner != null ? $"{x.Owner.PicturePath}" : ""
            });

            return Json(new { data = result, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Support metnods

        private void SavePreviewPictrures(HttpPostedFileBase[] files, int articleId, string alt, bool isHeader = false)
        {
            if (files != null && files.Length <= 4)
            {
                if (files[0] != null)
                {
                    int smWidth = 150, smHeight = 150, lgWidth = 280, lgHeight = 280;

                    DirectoryTools.CheckDirectoryExist(AppConstants.dirNewsPreview);

                    var pics = newsSrv.GetPictures.Where(x => x.ArticleId == articleId);

                    DeletePreviewPictures(pics, articleId);

                    foreach (HttpPostedFileBase pic in files)
                    {
                        lgHeight = 280;
                        lgWidth = 280;

                        if (isHeader)
                        {
                            lgWidth = 1920;
                            lgHeight = 720;
                            smWidth = 450;
                            smHeight = 290;
                        }

                        isHeader = false;

                        string smSize = ImageResize.Resize(pic, AppConstants.dirNewsPreview, smWidth, smHeight, true),
                            lgSize = ImageResize.Resize(pic, AppConstants.dirNewsPreview, lgWidth, lgHeight, true);

                        newsSrv.SavePicture(articleId, smSize, lgSize, $"{alt}");
                    }
                }

            }
        }

        private void DeletePreviewPictures(IEnumerable<PreviewPictures> pics, int articleId)
        {
            if (pics != null && pics.Count() > 0)
            {
                foreach (var pic in pics)
                {
                    if (pic.LargeSizePath != null)
                    {
                        DirectoryTools.DeleteFile(HttpContext, pic.LargeSizePath, AppConstants.dirNewsPreview);
                    }
                    if (pic.SmallSizePath != null)
                    {
                        DirectoryTools.DeleteFile(HttpContext, pic.SmallSizePath, AppConstants.dirNewsPreview);
                    }
                }

                newsSrv.RemovePicture(articleId, null);
            }
        }

        #endregion
    }
}