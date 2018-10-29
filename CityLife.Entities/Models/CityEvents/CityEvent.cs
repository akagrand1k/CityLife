using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.CityEvents
{
    public class CityEvent:BaseEntity
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Location { get; set; }
        public string ShortDescription { get; set; }
        public string DetailsDescription { get; set; }
        public string RelPathImage { get; set; }
        public string AbsPathImage { get; set; }
        public uint WidthImg { get; set; }
        public uint HeightImg { get; set; }
    }
}
