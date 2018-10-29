using CityLife.BusinessLogic.ReferencesService;
using CityLife.BusinessLogic.SysReferencesService;
using CityLife.Entities.Models.References;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Areas.Root.Models.References;
using CityLife.WebApp.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.BusinessLogic.UserProfileService;

namespace CityLife.WebApp.Areas.Root.Controllers
{
    [Roles(RoleConstant.RoleRoot)]
    public class ReferencesController : DefaultController
    {
        private ISysReferencesService sysRefService;
        private IReferencesService refService;
        public ReferencesController(ISysReferencesService _sysRefService,
            IReferencesService _refService, IUserProfileService userSrv) : base(userSrv)
        {
            this.sysRefService = _sysRefService;
            this.refService = _refService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult SysReferences()
        {
            return View();
        }


        #region SysReferences

        public ActionResult EditSysReferences(int Id)
        {
            ApplicationSysReferenceViewModel model = new ApplicationSysReferenceViewModel();
            if (Id != 0)
            {
                var entity = sysRefService.GetSysRefById(Id);
                model.Value = entity.Value;
                model.Key = entity.Key;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSysReferences(ApplicationSysReferenceViewModel model)
        {
            if (ModelState.IsValid)
            {
                sysRefService.Edit(SysReferencesMapper.ToEntity(model));
                return RedirectToAction("Index", "References");
            }
            return View(model);
        }


        #endregion

        #region References
        public ActionResult EditReferences(int Id = 0)
        {
            ApplicationReferenceViewModel model = new ApplicationReferenceViewModel();
            if (Id != 0)
            {
                var entity = refService.GetById(Id);
                model = ReferencesMapper.ToViewModel(entity);
            }
            ViewBag.ListReferences = refService.GetAll
                .Where(x => x.IsParent && !x.IsDelete);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReferences(ApplicationReferenceViewModel model)
        {
            if (ModelState.IsValid)
            {
                refService.Edit(ReferencesMapper.ToEntity(model));
                return RedirectToAction("Index", "References");
            }

            ViewBag.ListReferences = refService.GetAll.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteReference(int id = 0)
        {
            if (id > 0)
            {
                var reference = refService.GetById(id);
                reference.IsDelete = true;
                refService.Edit(reference);
            }

            return Redirect("~/Root/References");
        }

        #endregion

        #region JSON

        public ActionResult GetSysRefs(SysRefFilterViewModel model)
        {
            model.InitSortingData();
            var list = GetSysRefsList(model);

            return Json(new { data = list, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRefs(ReferenceFilterViewModel model)
        {
            model.InitSortingData();
            var list = GetRefsList(model);
            var source = list.Select(x => new { Name = x.Name, Alias = x.Alias, Key = x.Key, Value = x.Value, Id = x.Id });

            return Json(new { data = source, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SupportsMethods

        [NonAction]
        private List<ApplicationSysReferences> GetSysRefsList(SysRefFilterViewModel model)
        {
            List<ApplicationSysReferences> entity = null;
            var result = sysRefService.GetAll;
            result = result.OrderBy(model.FieldOrderBy + (model.IsAscending ? " ASC" : " DESC"));
            model.CountTotal = result.Count();
            entity = result.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();
            return entity;
        }

        [NonAction]
        private List<ApplicationReferences> GetRefsList(ReferenceFilterViewModel model)
        {
            List<ApplicationReferences> entity = null;
            var result = refService.GetAll.Where(x => !x.IsDelete);
            result = result.OrderBy(model.FieldOrderBy + (model.IsAscending ? " ASC" : " DESC"));
            model.CountTotal = result.Count();
            entity = result.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();
            return entity;
        }
        #endregion
    }
}