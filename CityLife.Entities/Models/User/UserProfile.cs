using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.User
{
    public class UserProfile:BaseEntity
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BrithDate { get; set;}
        public string FirstPhone { get; set; }
        public string SecondPhone { get; set; }
        public string PicturePath { get; set; }
        public string ExternalPic { get; set; }
        public bool IsExternal { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

    }
}
