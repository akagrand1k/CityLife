using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Pictures
{
    public class ArticlePicture:BaseEntity
    {
        public string Path { get; set; }
        public string Alt { get; set; }
    }
}
