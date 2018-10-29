using CityLife.BusinessLogic.ClassifierService;
using CityLife.BusinessLogic.OrganizationService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.Entities.Models.Organizations;
using CityLife.Extensions.Constant;
using CityLife.Extensions.ExtensionMethods;
using CityLife.Extensions.Tools;
using CityLife.WebApp.Areas.Administrator.Models.Organization;
using CityLife.WebApp.Areas.Moderator.Models.Organization;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.WebApp.Infrastructure.ImageResize;
using CityLife.WebApp.Infrastructure.Mapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Areas.Moderator.Controllers
{

    [Roles(RoleConstant.RoleRoot, RoleConstant.RoleAdmin, RoleConstant.RoleModerator)]
    public class OrganizationsController : DefaultController
    {
        private IOrganizationService orgService;
        private IClassifierService classifierService;
        public OrganizationsController(IOrganizationService _orgService, IClassifierService _classifierService, IUserProfileService userSrv) : base(userSrv)
        {
            this.orgService = _orgService;
            this.classifierService = _classifierService;
        }

        #region Organization

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            OrganizationViewModel model = new OrganizationViewModel();
            if (id > 0)
            {
                var entity = orgService.GetById(id);
                model = OrganizationMapper.ToViewModel(entity);
            }

            
            model.IsInRole = User.IsInRole;
            ViewBag.ListСlassifikate = new SelectList(classifierService.GetAll.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrganizationViewModel model)
        {
            model.IsInRole = User.IsInRole;

            if (ModelState.IsValid)
            {
                if (model.ImgFile != null)
                {
                    DirectoryTools.CheckDirectoryExist(AppConstants.directoryProfileAvatar);

                    var entity = orgService.GetById(model.Id);
                    if (entity != null)
                    {
                        if (entity.SmallPathImage != null)
                            DirectoryTools.DeleteFile(HttpContext, entity.SmallPathImage);

                        if (entity.LargePathImage != null)
                            DirectoryTools.DeleteFile(HttpContext, entity.LargePathImage);
                    }

                    model.SmallPathImage = ImageResize.Resize(model.ImgFile, AppConstants.directoryProfileAvatar, 50,50);
                    model.LargePathImage = ImageResize.Resize(model.ImgFile, AppConstants.directoryProfileAvatar, 250,250);
                }

                var result = orgService.Edit(OrganizationMapper.ToEntity(model));
                if (result != null)
                    return RedirectToAction("Index", "Organization");
            }
            ViewBag.ListСlassifikate = new SelectList(classifierService.GetAll.ToList(), "Id", "Name");
            return View(model);
        }

        #endregion

        #region JSON

        public ActionResult GetOrganization(OrgFilterFiewModel model)
        {
            model.InitSortingData();
            var list = GetOrganizationList(model);
            var res = list.Select(x => OrganizationMapper.ToViewModel(x));

            var result = res.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                SmallPathImage = x.SmallPathImage,
                DateReg = x.DateCreate.GetFormatDate(),
                ShortDescription = x.ShortDescription,
                Classifier = x.Classifier.Name,
            });

            return Json(new { data = result, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SupportMethods

        [NonAction]
        private List<Organization> GetOrganizationList(OrgFilterFiewModel model)
        {
            List<Organization> entity = null;
            var currentUser = GetCurrentUser();

            var result = orgService.GetWithInclude(x => x.Classifier).Where(x => x.OwnerId == currentUser.Id);

            entity = result.Where(x => x.IsDelete == false).ToList();

            entity = entity.OrderBy(model.FieldOrderBy + (model.IsAscending ? " ASC" : " DESC")).ToList();
            model.CountTotal = entity.Count();
            entity = entity.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();
            return entity;
        }

        #endregion
    }
}