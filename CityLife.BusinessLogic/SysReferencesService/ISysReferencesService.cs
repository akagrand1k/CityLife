using CityLife.Entities.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.SysReferencesService
{
    public interface ISysReferencesService
    {
        IEnumerable<ApplicationSysReferences> GetAll { get; }
        void CreateSysByKey(string key, string value);
        string GetValueByKey(string key);
        void Edit(ApplicationSysReferences entity);
        ApplicationSysReferences GetSysRefById(int id);
    }
}
