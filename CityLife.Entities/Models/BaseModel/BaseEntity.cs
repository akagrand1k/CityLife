using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.BaseModel
{
    public class BaseEntity
    {       
        public BaseEntity()
        {
            DateCreate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
