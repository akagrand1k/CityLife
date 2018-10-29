using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.References
{
    public class ApplicationSysReferences : BaseEntity
    {
        public int? ParentId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}