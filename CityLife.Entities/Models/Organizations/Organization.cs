using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Organizations
{
   public class Organization:BaseEntity
    {

        public string ShortDescription { get; set; }
        public string DetailsDescription { get; set; }
        public string OwnerId { get; set; }
        public int ClassifierId { get; set; }
        public string LargePathImage { get; set; }
        public string SmallPathImage { get; set; }
        public uint WidthImg { get; set; }
        public uint HeightImg { get; set; }

        public string Street { get; set; }
        public string Home { get; set; }
        public string Building { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Notation { get; set; }

        [ForeignKey("OwnerId")]
        public AppUser Owner { get; set; }

        [ForeignKey("ClassifierId")]
        public ClassifierOrganization Classifier { get; set; }

    }
}
