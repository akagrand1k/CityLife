using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLife.BusinessLogic.SysReferencesService;
using CityLife.Extensions.Constant;
using CityLife.DataAccess.Interfaces;
using CityLife.Entities.Models.References;
using CityLife.DataAccess.Interfaces.UnitOfWork;

namespace CityLife.BusinessLogic.Agregate
{
    public class InitialPrimaryData : IInitialPrimaryData
    {
        private readonly IRepository<ApplicationSysReferences> repository;
        private readonly IUnitOfWork unitOfWork;
        public InitialPrimaryData(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new NullReferenceException("parameter: unitOfWork");
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.Repository<ApplicationSysReferences>();
        }

        public ISysReferencesService SysReferencesService
        {
            get
            {
                return new SysReferencesService.SysReferencesService(unitOfWork);
            }
        }

        public void InitSysReferences()
        {
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_Ssl, "true");
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_SmtpServer, "smtp.yandex.ru");
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_SmtpPort, "25");
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_Login, "kamcitylife@yandex.ru");
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_Password, "a!sads0185%!Sasd");
            SysReferencesService.CreateSysByKey(SysConstants.EmailGate_From, "Подтвердите ваш почтовый адрес");
        }
    }
}
