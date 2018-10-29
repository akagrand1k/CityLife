using CityLife.Entities.Models.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.Entities.Models.Exceptions
{
    public class AppExpection : BaseEntity
    {
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionSource { get; set; }
        
        public string Section { get; set; }
        public string Log_Key { get; set; }
    }
}
