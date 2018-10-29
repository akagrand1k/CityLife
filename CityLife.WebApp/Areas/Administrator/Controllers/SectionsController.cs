using CityLife.BusinessLogic.SectionServices;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Models.Section;
using CityLife.WebApp.Infrastructure.Mapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityLife.BusinessLogic.AppExceptionService;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.BusinessLogic.UserProfileService;

namespace CityLife.WebApp.Areas.Administrator.Controllers
{
    [Roles(RoleConstant.RoleRoot,RoleConstant.RoleAdmin)]
    public class SectionsController : DefaultController
    {
        private readonly ISectionService sectionSrv;

        public SectionsController(ISectionService sectionSrv, IUserProfileService userSrv) : base(userSrv)
        {
            this.sectionSrv = sectionSrv;

            if (sectionSrv == null)
            {
                throw new NullReferenceException("sectionSrv");
            }
        }

        // GET: Administrator/Sections
        public ViewResult Index()
        {
            ViewBag.CurrentRole = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            SectionViewModel model = new SectionViewModel { IsActive = true };

            return View(model);
        }

        // Get disabled sections
        public ViewResult Disabled()
        {
            SectionViewModel model = new SectionViewModel { IsActive = false };

            ViewBag.CurrentRole = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            return View(model);
        }

        public ViewResult Edit(int id = 0)
        {
            SectionViewModel model = new SectionViewModel();

            if (id > 0)
            {
                model = SectionMapper.ToViewModel(sectionSrv.Get(id));
            }

            model.CurrentRole = UserManager.GetRoles(User.Identity.GetUserId()).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var section = sectionSrv.Edit(SectionMapper.ToEntity(model));
                model = SectionMapper.ToViewModel(section);
            }

            return RedirectToAction("Edit", new { id = model.Id });
        }

        // marked as disabled section
        [HttpPost]
        public ActionResult Disable(int id)
        {
            SectionViewModel section = SectionMapper.ToViewModel(sectionSrv.Get(id));

            section.IsActive = false;

            sectionSrv.Edit(SectionMapper.ToEntity(section));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Enable(int id)
        {
            SectionViewModel section = SectionMapper.ToViewModel(sectionSrv.Get(id));

            section.IsActive = true;

            sectionSrv.Edit(SectionMapper.ToEntity(section));

            return RedirectToAction("Disabled");
        }


        #region JOSN actions 

        public JsonResult Get(SectionViewModel model)
        {
            var result = sectionSrv.Sections
                .Where(x => x.IsActive==model.IsActive)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                    ObsoleteUrl = x.ObsoleteUrl,
                    DateCreate = x.DateCreate.ToString(),
                    DateUpdate = x.DateUpdate == null ? "" : x.DateUpdate.Value.ToString(),
                });

            model.CountTotal = result.Count();

            result = result.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();

            return Json(new { data = result, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}