using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityLife.BusinessLogic.ClassifierService;
using CityLife.BusinessLogic.OrganizationService;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.WebApp.Models.Organization;
using CityLife.WebApp.Models.Organization.JSON;

namespace CityLife.WebApp.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IUserProfileService userSrv;
        private readonly IClassifierService classifierService;
        private readonly IOrganizationService orgService;

        public CompanyController(IUserProfileService _userSrv, IClassifierService _classifierService,
            IOrganizationService _orgService) : base(_userSrv)
        {
            this.userSrv = _userSrv;
            this.classifierService = _classifierService;
            this.orgService = _orgService;
        }

        public ActionResult Index()
        {
            CompanyViewModel model = new CompanyViewModel();

            var classifier = classifierService.GetAll;

            if (classifier != null)
            {
                model.ListKlassifier = classifier.Select(x => x.Name).ToList();
            }

            return View(model);
        }


        #region JSON
        public ActionResult GetMatched(string query)
        {
            var orgs = orgService.GetMatchNames(query);
            var classifier = classifierService.GetMatchNames(query);

            var query1 = classifier.Select(c => new Suggestions
            {
                data = "cl",
                value = c.Name,
                searchCritery = c.Alias
            }).ToList();

            var query2 = orgs.Select(c => new Suggestions
            {
                data = "org",
                value = c.Name,
                searchCritery = c.Alias
            }).ToList();

            AutoCompleteJSON result = new AutoCompleteJSON();
            result.suggestions.AddRange(query1);
            result.suggestions.AddRange(query2);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGeoPoints(string type = "", string critery = "")
        {
            var result = orgService.GetGeoObject(type, critery);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}