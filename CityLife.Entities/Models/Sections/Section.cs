using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Sections
{
    public class Section : BaseEntity
    {

        public string  Url { get; set; }
        public string ObsoleteUrl { get; set; }
        public int Weight { get; set; }

    }
}
