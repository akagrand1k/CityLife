using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Banners
{
    [Table("BannerSchedule")]
    public class BannerSchedule : BaseEntity
    {
        public int OrderId { get; set; }
        public DateTime DateSince { get; set; }
        public DateTime DateTill { get; set; }

        [ForeignKey("OrderId")]
        public BannerOrder BannerOrder { get; set; }
    }
}
