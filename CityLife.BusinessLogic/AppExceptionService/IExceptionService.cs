using CityLife.Entities.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.AppExceptionService
{
    public interface IExceptionService
    {
        void WriteException(Exception e,string section,string key);
        IEnumerable<AppExpection> GetAll { get; }
    }
}
