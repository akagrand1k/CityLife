using CityLife.BusinessLogic.SysReferencesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLife.BusinessLogic.Agregate
{
    public interface IInitialPrimaryData
    {
        ISysReferencesService SysReferencesService { get; }
        void InitSysReferences();
    }
}
