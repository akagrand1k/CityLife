using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Banners
{
    [Table("Checkout")]
    public class Checkout: BaseEntity
    {
        public int BannerId { get; set; }

        public decimal TotalSumm { get; set; }

        public DateTime DatePayment { get; set; }

        [ForeignKey("BannerId")]
        public BannerSchedule BannerSchedule { get; set; }
    }
}
