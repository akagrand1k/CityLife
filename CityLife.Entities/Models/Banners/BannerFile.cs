using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Banners
{
    public class BannerFile : BaseEntity
    {
        public int ScheduleId { get; set; }
        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }
        public string File4 { get; set; }
        public string File5 { get; set; }

        [ForeignKey("ScheduleId")]
        public BannerSchedule BannerSchedule { get; set; }
    }
}
