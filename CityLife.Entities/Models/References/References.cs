using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.References
{
    public class ApplicationReferences: BaseEntity
    {
        public ApplicationReferences()
        {
           this.CityNews = new HashSet<Article>();
        }
        public int? ParentId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsParent { get; set; }

        public virtual ICollection<Article> CityNews { get; set; }
    }
}
