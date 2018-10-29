using CityLife.Entities.Models.BaseModel;
using CityLife.Entities.Models.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CityLife.Entities.Models.Posters
{
    public class Poster : BaseEntity
    {        
        public int OrganizationId { get; set; }
        public string Shortdescription { get; set; }
        public string DetailsDescription { get; set; }
        public decimal ChildrenTicketPrice { get; set; }
        public decimal StudentTicketPrice { get; set; }
        public decimal TicketPrice { get; set; }
        public string RelPathImage { get; set; }
        public string AbsPathImage { get; set; }
        public uint WidthImg { get; set; }
        public uint HeightImg { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
    }
}
