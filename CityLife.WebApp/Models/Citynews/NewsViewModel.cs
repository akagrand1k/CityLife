using CityLife.Entities.Models.News;
using CityLife.Entities.Models.References;
using CityLife.WebApp.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityLife.WebApp.Models.News
{
    public class NewsViewModel : AbstractViewModel
    {
        public HttpPostedFileBase[] PreviewPics { get; set; }
       public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ShortDescript { get; set; }        
        public int OwnerId { get; set; }
        public PreviewPictures PreviewPic { get; set; }
        public IEnumerable<PreviewPictures> PreviewPictures { get; set; }
        public IEnumerable<ApplicationReferences> Tags { get; set; }
        public int[] TagIds { get; set; }
        public int[] DeletingPics { get; set; }
        public int PictureId { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime DateCreate { get; set; }
    }
}