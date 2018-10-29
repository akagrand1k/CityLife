using CityLife.Entities.Models.References;
using Evernote.EDAM.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.News
{
    public class ArticlesViewModel:Abstract.AbstractViewModel
    {
        public IEnumerable<ApplicationReferences> Tags { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }

    public class ArticleViewModel 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescript { get; set; }
        public string Content { get; set; }
        public string PathPreview { get; set; }
        public string Alias { get; set; }
        public string Tags { get; set; }
        public int? OwnerId { get; set; }
        public DateTime DateCreate { get; set; }
        public string PathTitlePic { get; set; }
        public string PathAutorAvatar { get; set; }
        public string AutorName { get; set; }

        public IEnumerable<ApplicationReferences> TagsList { get; set; }
    }

}