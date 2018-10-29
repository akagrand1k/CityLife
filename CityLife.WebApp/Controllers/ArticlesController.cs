using CityLife.BusinessLogic.CityNewsService;
using CityLife.BusinessLogic.SectionServices;
using CityLife.BusinessLogic.UserProfileService;
using CityLife.WebApp.Infrastructure.Mapper;
using CityLife.WebApp.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityLife.WebApp.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly INewsService newsService;
        private readonly ISectionService sectionService;
        private readonly IUserProfileService userService;

        public ArticlesController(INewsService newsService, ISectionService sectionService,IUserProfileService _userSrv) : base(_userSrv)
        {
            if (newsService == null || sectionService == null)
            {
                TempData["FlagError"] = true;
                TempData["ServerErrorMessage"] = "Ошибка сервера 500";
                //TODO log write
            }

            this.newsService = newsService;
            this.sectionService = sectionService;
        }

        public ActionResult Index(string tag)
        {
            ArticlesViewModel model = new ArticlesViewModel();

            if (TempData["FlagError"] != null && ((bool)TempData["FlagError"]) == true)
            {
                return View(model);
            }

            var newsSection = sectionService.Sections.FirstOrDefault(x => x.Url == "/articles");

            if (newsSection == null)
            {
                TempData["FlagError"] = true;
                ViewBag.ServerErrorMessage = "Ошибка сервера 500";

                return View(model);
            }
            if (!newsSection.IsActive)
            {
                return Redirect("/");
            }

            if (Request.Url.Segments.Any(x => x == "index"))
            {
                return RedirectPermanent($"/news");
            }

            model.Tags = newsService.Tags.Where(x => x.IsActive && !x.IsDelete);

            var news = newsService.Get(tag);

            if (news != null)
            {
                model.Articles = news.Where(x => x.IsActive && !x.IsDelete)
                .Select(x => ArticleMapper.ToViewModel(x));
                
            }

            return View(model);
        }


        public ActionResult Article(string alias)
        {
            
            ArticleViewModel article = ArticleMapper.ToViewModel(newsService.News.FirstOrDefault(x => x.Alias == alias));

            if (article == null)
            {
                throw new HttpException(404, "Указанная страница не найдена!");
            }
                        
            return View(article);
        }
    }
}