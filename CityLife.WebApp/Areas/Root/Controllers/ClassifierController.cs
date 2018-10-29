using CityLife.BusinessLogic.ClassifierService;
using CityLife.Entities.Models.Organizations;
using CityLife.WebApp.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using System.Web.Mvc;
using CityLife.Extensions.Constant;
using CityLife.WebApp.Areas.Root.Models.Classifier;
using CityLife.WebApp.Infrastructure.CustomAttribute;
using CityLife.BusinessLogic.UserProfileService;

namespace CityLife.WebApp.Areas.Root.Controllers
{
    [Roles(RoleConstant.RoleRoot)]
    public class ClassifierController : DefaultController
    {
        private IClassifierService classifierService;

        public ClassifierController(IClassifierService _classifierService,IUserProfileService userSrv) : base(userSrv)
        {
            this.classifierService = _classifierService;
        }

        public ActionResult Index()
        {
            return View();
        }
        #region ClassifikatorOrg

        [HttpGet]//Get для наглядности, можно опустить. 
        public ActionResult Edit(int id = 0)
        {
            ClassifierViewModel model = new ClassifierViewModel();//подготавливаю экземпляр объекта к использованию

            if (id > 0)
            {
                var entity = classifierService.GetById(id); //получаю нужные данные по идентификатору. 
                model = ClassifierMapper.ToViewModel(entity); //преобразую данные сущности в экземпляр модели представления.
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassifierViewModel model)//сохраняю данные.
        {
            if (ModelState.IsValid)
            {
                var entity = classifierService.Edit(ClassifierMapper.ToEntity(model));//метод редактирования(сохранение/обновление), возвращает объект сущности, для дальнейшего использования.
                model = ClassifierMapper.ToViewModel(entity);

                return RedirectToAction("Edit", "Classifier", new { id = model.Id });//делаю перенаправлени на метод Get Edit(тот что выше) 
            }
            return View(model);
        }

        public ActionResult Delete(int Id)
        {
            if (classifierService.Delete(Id))
                return RedirectToAction("Index", "Classifier");

            return View();
        }
        #endregion

        #region JSON
        public ActionResult GetClassifier(ClassifierFilterViewModel model)
        {
            model.InitSortingData();
            var list = GetClassifierList(model);

            var result = list.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Alias = x.Alias,
                IsActive = x.IsActive ? "Активна" : "Не активна",
            });

            return Json(new { data = result, draw = model.draw, recordsTotal = model.CountTotal, recordsFiltered = model.CountTotal }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SupportMethods

        [NonAction]
        private List<ClassifierOrganization> GetClassifierList(ClassifierFilterViewModel model)
        {
            List<ClassifierOrganization> entity = null;
            var result = classifierService.GetAll.Where(x => x.IsDelete == false);
            result = result.OrderBy(model.FieldOrderBy + (model.IsAscending ? " ASC" : " DESC"));
            model.CountTotal = result.Count();
            entity = result.Skip(model.CountOnPage * (model.NumPage - 1)).Take(model.CountOnPage).ToList();
            return entity;
        }
        #endregion
    }
}