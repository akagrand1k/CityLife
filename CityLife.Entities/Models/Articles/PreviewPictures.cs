using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.News
{
    public class PreviewPictures:BaseEntity
    {
        public string SmallSizePath { get; set; }
        public string LargeSizePath { get; set; }
        public string Alt { get; set; }
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public Article CityNews { get; set; }
    }
}
