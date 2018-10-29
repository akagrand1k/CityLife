using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.References;
using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.News
{
    public class Article:BaseEntity
    {

        public Article()
        {
            this.Tags = new HashSet<ApplicationReferences>();
        }

        public string MetaDescription { get; set; }        
        public string MetaKeywords { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string PathPreviewPic { get; set; }
        public string PathTitlePic { get; set; }
        public int CountView { get; set; }        
        public int? OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public UserProfile Owner { get; set; }
        public virtual ICollection<ApplicationReferences> Tags { get; set; }

       public ICollection<PreviewPictures> PreviewPictures { get; set; }

    }
}

