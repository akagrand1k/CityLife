using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Banners
{
    [Table("BannerOrder")]
    public class BannerOrder : BaseEntity
    {
        public int UserProfileId { get; set; }
        public string Guid { get; set; }
        public string CustomerComment { get; set; }


        [ForeignKey("UserProfileId")]
        UserProfile UserProfile { get; set; }
    }
}